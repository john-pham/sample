﻿// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Ebrain.ViewModels;
using AutoMapper;
using ebrain.admin.bc.Models;
using ebrain.admin.bc.Core.Interfaces;
using Ebrain.Policies;
using Ebrain.Helpers;
using Microsoft.AspNetCore.JsonPatch;
using ebrain.admin.bc.Core;
using ebrain.admin.bc.Utilities;
using Microsoft.AspNetCore.Hosting;
using ebrain.admin.bc;

namespace Ebrain.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private readonly IAccountManager _accountManager;
        private readonly IAuthorizationService _authorizationService;
        private const string GetUserByIdActionName = "GetUserById";
        private const string GetRoleByIdActionName = "GetRoleById";
        readonly IHostingEnvironment _env;
        public AccountController(IUnitOfWork unitOfWork, IAccountManager accountManager, IAuthorizationService authorizationService, IHostingEnvironment env)
        {
            _accountManager = accountManager;
            _authorizationService = authorizationService;
            this._env = env;
            this._unitOfWork = unitOfWork;
        }

        [HttpGet("users/getallusers")]
        [Produces(typeof(UserViewModel))]
        public async Task<IActionResult> GetAllUsers()
        {
            var userId = Utilities.GetUserId(this.User);
            var list = _accountManager.GetAllUsers(this._unitOfWork.Branches.GetAllBranchOfUserString(userId));
            return this.Ok(list.Result);
        }

        [HttpGet("users/accessrights")]
        [Produces(typeof(UserViewModel))]
        public async Task<IActionResult> GetAccessRight()
        {
            var userId = Utilities.GetUserId(this.User);
            var list = _accountManager.GetAccessRight(userId);
            return this.Ok(list.Result);
        }

        [HttpGet("users/me")]
        [Produces(typeof(UserViewModel))]
        public async Task<IActionResult> GetCurrentUser()
        {
            return await GetUserByUserName(this.User.Identity.Name);
        }

        [HttpGet("users/{id}", Name = GetUserByIdActionName)]
        [Produces(typeof(UserViewModel))]
        public async Task<IActionResult> GetUserById(string id)
        {
            if (!(await _authorizationService.AuthorizeAsync(this.User, id, AuthPolicies.ViewUserByUserIdPolicy)).Succeeded)
                return new ChallengeResult();


            UserViewModel userVM = await GetUserViewModelHelper(id);

            if (userVM != null)
                return Ok(userVM);
            else
                return NotFound(id);
        }

        [HttpGet("users/username/{userName}")]
        [Produces(typeof(UserViewModel))]
        public async Task<IActionResult> GetUserByUserName(string userName)
        {
            ApplicationUser appUser = await _accountManager.GetUserByUserNameAsync(userName);

            if (!(await _authorizationService.AuthorizeAsync(this.User, appUser?.Id ?? "", AuthPolicies.ViewUserByUserIdPolicy)).Succeeded)
                return new ChallengeResult();

            if (appUser == null)
                return NotFound(userName);

            return await GetUserById(appUser.Id);
        }

        [HttpGet("users")]
        [Produces(typeof(List<UserViewModel>))]
        [Authorize(AuthPolicies.ViewUsersPolicy)]
        public async Task<IActionResult> GetUsers()
        {
            return await GetUsers(-1, -1);
        }

        [HttpGet("users/{page:int}/{pageSize:int}")]
        [Produces(typeof(List<UserViewModel>))]
        [Authorize(AuthPolicies.ViewUsersPolicy)]
        public async Task<IActionResult> GetUsers(int page, int pageSize)
        {
            var usersAndRoles = await _accountManager.GetUsersAndRolesAsync(page, pageSize);

            List<UserViewModel> usersVM = new List<UserViewModel>();

            foreach (var item in usersAndRoles)
            {
                var userVM = Mapper.Map<UserViewModel>(item.Item1);
                userVM.Roles = item.Item2;

                usersVM.Add(userVM);
            }

            return Ok(usersVM);
        }

        [HttpPut("users/me")]
        public async Task<IActionResult> UpdateCurrentUser([FromBody] UserEditViewModel user)
        {
            return await UpdateUser(Utilities.GetUserId(this.User).ToString(), user);
        }

        [HttpPut("users/{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UserEditViewModel user)
        {
            ApplicationUser appUser = await _accountManager.GetUserByIdAsync(id);
            string[] currentRoles = appUser != null ? (await _accountManager.GetUserRolesAsync(appUser)).ToArray() : null;

            var manageUsersPolicy = _authorizationService.AuthorizeAsync(this.User, id, AuthPolicies.ManageUserByUserIdPolicy);
            var assignRolePolicy = _authorizationService.AuthorizeAsync(this.User, Tuple.Create(user.Roles, currentRoles), AuthPolicies.AssignRolesPolicy);


            if ((await Task.WhenAll(manageUsersPolicy, assignRolePolicy)).Any(r => !r.Succeeded))
                return new ChallengeResult();


            if (ModelState.IsValid)
            {
                if (user == null)
                    return BadRequest($"{nameof(user)} cannot be null");

                if (!string.IsNullOrWhiteSpace(user.Id) && id != user.Id)
                    return BadRequest("Conflicting user id in parameter and model data");

                if (appUser == null)
                    return NotFound(id);


                if (Utilities.GetUserId(this.User).ToString() == id && string.IsNullOrWhiteSpace(user.CurrentPassword))
                {
                    if (!string.IsNullOrWhiteSpace(user.NewPassword))
                        return BadRequest("Current password is required when changing your own password");

                    if (appUser.UserName != user.UserName)
                        return BadRequest("Current password is required when changing your own username");
                }


                bool isValid = true;

                if (Utilities.GetUserId(this.User).ToString() == id && (appUser.UserName != user.UserName || !string.IsNullOrWhiteSpace(user.NewPassword)))
                {
                    if (!await _accountManager.CheckPasswordAsync(appUser, user.CurrentPassword))
                    {
                        isValid = false;
                        AddErrors(new string[] { "The username/password couple is invalid." });
                    }
                }

                if (isValid)
                {
                    Mapper.Map<UserViewModel, ApplicationUser>(user, appUser);

                    var result = await _accountManager.UpdateUserAsync(appUser, user.Roles);
                    if (result.Item1)
                    {
                        if (!string.IsNullOrWhiteSpace(user.NewPassword))
                        {
                            if (!string.IsNullOrWhiteSpace(user.CurrentPassword))
                                result = await _accountManager.UpdatePasswordAsync(appUser, user.CurrentPassword, user.NewPassword);
                            else
                                result = await _accountManager.ResetPasswordAsync(appUser, user.NewPassword);
                        }

                      
                        var value = user;
                        //update profierimage
                        //save logo to physical file
                        if (value.profier != null && !string.IsNullOrEmpty(value.profier.Name) && !string.IsNullOrEmpty(value.profier.Value))
                        {
                            //Convert Base64 Encoded string to Byte Array.
                            var base64String = value.profier.Value;
                            var fileName = value.profier.Name;
                            byte[] imageBytes = Convert.FromBase64String(base64String);

                            //Save the Byte Array as Image File.
                            string filePath = fileName.WebRootPathProfier(Guid.NewGuid().ToString(), _env);
                            imageBytes.WriteAllBytes(filePath);

                            //store filename to DB
                            appUser.ProfilerImage = filePath.GetFileName();

                            result = await _accountManager.UpdateUserAsync(appUser, user.Roles);
                        }

                        if (result.Item1)
                            return NoContent();
                    }

                    AddErrors(result.Item2);
                }
            }

            return BadRequest(ModelState);
        }


        [HttpPatch("users/me")]
        public async Task<IActionResult> UpdateCurrentUser([FromBody] JsonPatchDocument<UserPatchViewModel> patch)
        {
            return await UpdateUser(Utilities.GetUserId(this.User).ToString(), patch);
        }

        [HttpPatch("users/{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] JsonPatchDocument<UserPatchViewModel> patch)
        {
            if (!(await _authorizationService.AuthorizeAsync(this.User, id, AuthPolicies.ManageUserByUserIdPolicy)).Succeeded)
                return new ChallengeResult();


            if (ModelState.IsValid)
            {
                if (patch == null)
                    return BadRequest($"{nameof(patch)} cannot be null");


                ApplicationUser appUser = await _accountManager.GetUserByIdAsync(id);

                if (appUser == null)
                    return NotFound(id);


                UserPatchViewModel userPVM = Mapper.Map<UserPatchViewModel>(appUser);
                patch.ApplyTo(userPVM, ModelState);


                if (ModelState.IsValid)
                {
                    Mapper.Map<UserPatchViewModel, ApplicationUser>(userPVM, appUser);

                    var result = await _accountManager.UpdateUserAsync(appUser);
                    if (result.Item1)
                        return NoContent();


                    AddErrors(result.Item2);
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPost("users")]
        [Authorize(AuthPolicies.ManageUsersPolicy)]
        public async Task<IActionResult> Register([FromBody] UserEditViewModel user)
        {
            if (ModelState.IsValid)
            {
                if (user == null)
                    return BadRequest($"{nameof(user)} cannot be null");


                ApplicationUser appUser = Mapper.Map<ApplicationUser>(user);

                var result = await _accountManager.CreateUserAsync(appUser, user.Roles, user.NewPassword);
                if (result.Item1)
                {
                    UserViewModel userVM = await GetUserViewModelHelper(appUser.Id);
                    return CreatedAtAction(GetUserByIdActionName, new { id = userVM.Id }, userVM);
                }

                AddErrors(result.Item2);
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("users/{id}")]
        [Produces(typeof(UserViewModel))]
        [Authorize(AuthPolicies.ManageUsersPolicy)]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (!await _accountManager.TestCanDeleteUserAsync(id))
                return BadRequest("User cannot be deleted. Delete all orders associated with this user and try again");


            UserViewModel userVM = null;
            ApplicationUser appUser = await this._accountManager.GetUserByIdAsync(id);

            if (appUser != null)
                userVM = await GetUserViewModelHelper(appUser.Id);


            if (userVM == null)
                return NotFound(id);

            var result = await this._accountManager.DeleteUserAsync(appUser);
            if (!result.Item1)
                throw new Exception("The following errors occurred whilst deleting user: " + string.Join(", ", result.Item2));


            return Ok(userVM);
        }

        [HttpPut("users/unblock/{id}")]
        [Authorize(AuthPolicies.ManageUsersPolicy)]
        public async Task<IActionResult> UnblockUser(string id)
        {
            ApplicationUser appUser = await this._accountManager.GetUserByIdAsync(id);

            if (appUser == null)
                return NotFound(id);

            appUser.LockoutEnd = null;
            var result = await _accountManager.UpdateUserAsync(appUser);
            if (!result.Item1)
                throw new Exception("The following errors occurred whilst unblocking user: " + string.Join(", ", result.Item2));


            return NoContent();
        }

        [HttpGet("users/me/preferences")]
        [Produces(typeof(string))]
        public async Task<IActionResult> UserPreferences()
        {
            var userId = Utilities.GetUserId(this.User).ToString();
            ApplicationUser appUser = await this._accountManager.GetUserByIdAsync(userId);

            if (appUser != null)
                return Ok(appUser.Configuration);
            else
                return NotFound(userId);
        }

        [HttpPut("users/me/preferences")]
        public async Task<IActionResult> UserPreferences([FromBody] string data)
        {
            var userId = Utilities.GetUserId(this.User).ToString();
            ApplicationUser appUser = await this._accountManager.GetUserByIdAsync(userId);

            if (appUser == null)
                return NotFound(userId);

            appUser.Configuration = data;
            var result = await _accountManager.UpdateUserAsync(appUser);
            if (!result.Item1)
                throw new Exception("The following errors occurred whilst updating User Configurations: " + string.Join(", ", result.Item2));


            return NoContent();
        }

        [HttpGet("roles/{id}", Name = GetRoleByIdActionName)]
        [Produces(typeof(RoleViewModel))]
        public async Task<IActionResult> GetRoleById(string id)
        {
            var appRole = await _accountManager.GetRoleByIdAsync(id);

            if (!(await _authorizationService.AuthorizeAsync(this.User, appRole?.Name ?? "", AuthPolicies.ViewRoleByRoleNamePolicy)).Succeeded)
                return new ChallengeResult();

            if (appRole == null)
                return NotFound(id);

            return await GetRoleByName(appRole.Name);
        }

        [HttpGet("roles/name/{name}")]
        [Produces(typeof(RoleViewModel))]
        public async Task<IActionResult> GetRoleByName(string name)
        {
            if (!(await _authorizationService.AuthorizeAsync(this.User, name, AuthPolicies.ViewRoleByRoleNamePolicy)).Succeeded)
                return new ChallengeResult();


            RoleViewModel roleVM = await GetRoleViewModelHelper(name);

            if (roleVM == null)
                return NotFound(name);

            return Ok(roleVM);
        }

        [HttpGet("roles")]
        [Produces(typeof(List<RoleViewModel>))]
        [Authorize(AuthPolicies.ViewRolesPolicy)]
        public async Task<IActionResult> GetRoles()
        {
            return await GetRoles(-1, -1);
        }

        [HttpGet("roles/{page:int}/{pageSize:int}")]
        [Produces(typeof(List<RoleViewModel>))]
        [Authorize(AuthPolicies.ViewRolesPolicy)]
        public async Task<IActionResult> GetRoles(int page, int pageSize)
        {
            var roles = await _accountManager.GetRolesLoadRelatedAsync(page, pageSize);
            return Ok(Mapper.Map<List<RoleViewModel>>(roles));
        }

        [HttpPut("roles/{id}")]
        [Authorize(AuthPolicies.ManageRolesPolicy)]
        public async Task<IActionResult> UpdateRole(string id, [FromBody] RoleViewModel role)
        {
            if (ModelState.IsValid)
            {
                if (role == null)
                    return BadRequest($"{nameof(role)} cannot be null");

                if (!string.IsNullOrWhiteSpace(role.Id) && id != role.Id)
                    return BadRequest("Conflicting role id in parameter and model data");



                ApplicationRole appRole = await _accountManager.GetRoleByIdAsync(id);

                if (appRole == null)
                    return NotFound(id);


                Mapper.Map<RoleViewModel, ApplicationRole>(role, appRole);

                var result = await _accountManager.UpdateRoleAsync(appRole, role.Permissions?.Select(p => p.Value).ToArray());
                if (result.Item1)
                    return NoContent();

                AddErrors(result.Item2);

            }

            return BadRequest(ModelState);
        }

        [HttpPost("roles")]
        [Authorize(AuthPolicies.ManageRolesPolicy)]
        public async Task<IActionResult> CreateRole([FromBody] RoleViewModel role)
        {
            if (ModelState.IsValid)
            {
                if (role == null)
                    return BadRequest($"{nameof(role)} cannot be null");


                ApplicationRole appRole = Mapper.Map<ApplicationRole>(role);

                var result = await _accountManager.CreateRoleAsync(appRole, role.Permissions?.Select(p => p.Value).ToArray());
                if (result.Item1)
                {
                    RoleViewModel roleVM = await GetRoleViewModelHelper(appRole.Name);
                    return CreatedAtAction(GetRoleByIdActionName, new { id = roleVM.Id }, roleVM);
                }

                AddErrors(result.Item2);
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("roles/{id}")]
        [Produces(typeof(RoleViewModel))]
        [Authorize(AuthPolicies.ManageRolesPolicy)]
        public async Task<IActionResult> DeleteRole(string id)
        {
            if (!await _accountManager.TestCanDeleteRoleAsync(id))
                return BadRequest("Role cannot be deleted. Remove all users from this role and try again");


            RoleViewModel roleVM = null;
            ApplicationRole appRole = await this._accountManager.GetRoleByIdAsync(id);

            if (appRole != null)
                roleVM = await GetRoleViewModelHelper(appRole.Name);


            if (roleVM == null)
                return NotFound(id);

            var result = await this._accountManager.DeleteRoleAsync(appRole);
            if (!result.Item1)
                throw new Exception("The following errors occurred whilst deleting role: " + string.Join(", ", result.Item2));


            return Ok(roleVM);
        }

        [HttpGet("permissions")]
        [Produces(typeof(List<PermissionViewModel>))]
        [Authorize(AuthPolicies.ViewRolesPolicy)]
        public IActionResult GetAllPermissions()
        {
            return Ok(Mapper.Map<List<PermissionViewModel>>(ApplicationPermissions.AllPermissions));
        }

        private async Task<UserViewModel> GetUserViewModelHelper(string userId)
        {
            var userAndRoles = await _accountManager.GetUserAndRolesAsync(userId);
            if (userAndRoles == null)
                return null;

            var userVM = Mapper.Map<UserViewModel>(userAndRoles.Item1);
            userVM.Roles = userAndRoles.Item2;

            return userVM;
        }

        private async Task<RoleViewModel> GetRoleViewModelHelper(string roleName)
        {
            var role = await _accountManager.GetRoleLoadRelatedAsync(roleName);
            if (role != null)
                return Mapper.Map<RoleViewModel>(role);


            return null;
        }

        private void AddErrors(IdentityResult result)
        {
            AddErrors(result.Errors.Select(e => e.Description));
        }

        private void AddErrors(IEnumerable<string> errors)
        {
            foreach (var error in errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }
        }

    }
}
