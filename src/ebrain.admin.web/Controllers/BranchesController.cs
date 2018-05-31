// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ebrain.admin.bc;
using Ebrain.ViewModels;
using ebrain.admin.bc.Models;
using Microsoft.Extensions.Logging;
using Ebrain.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using DinkToPdf;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using ebrain.admin.bc.Utilities;
namespace Ebrain.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [Security("D7239078-E67A-42FA-86D7-4A8C3F73D521")]
    public class BranchesController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        private ITemplateService _templateService;
        readonly ILogger _logger;
        readonly IHostingEnvironment _env;

        public BranchesController(IUnitOfWork unitOfWork, ILogger<BranchesController> logger, IHostingEnvironment env) : base(unitOfWork, logger)
        {
            this._unitOfWork = unitOfWork;
            this._logger = logger;
            this._env = env;
        }

        [HttpGet("getall")]
        [Produces(typeof(UserViewModel))]
        public async Task<IActionResult> GetAll()
        {
            var bus = await this._unitOfWork.Branches.GetAll();
            return Ok(bus.Select(c => new BranchViewModel
            {
                ID = c.BranchId,
                Code = c.BranchCode,
                Name = c.BranchName,
                Email = c.Email,
                Address = c.Address,
                PhoneNumber = c.PhoneNumber,
                Fax = c.FAX
            }));
        }

        [HttpGet("search")]
        [Produces(typeof(UserViewModel))]
        public async Task<JsonResult> Search(string filter, string value, int page, int size)
        {
            var bus = this._unitOfWork.Branches;
            var ret = from c in await bus.Search(filter, value, page, size)
                      select new BranchViewModel
                      {
                          ID = c.BranchId,
                          Code = c.BranchCode,
                          Name = c.BranchName,
                          Email = c.Email,
                          Address = c.Address,
                          PhoneNumber = c.PhoneNumber,
                          Fax = c.FAX,
                          Logo = new FileViewModel
                          {
                              Name = c.LogoName.WebRootPathLogo()
                          }
                      };

            return Json(new
            {
                Total = bus.Total,
                List = ret
            });
        }

        [HttpGet("get")]
        [Produces(typeof(UserViewModel))]
        public async Task<BranchViewModel> Get(Guid index)
        {
            var item = await this._unitOfWork.Branches.Get(index);

            var branch = new BranchViewModel
            {
                ID = item.BranchId,
                Code = item.BranchCode,
                Name = item.BranchName,
                Email = item.Email,
                Address = item.Address,
                PhoneNumber = item.PhoneNumber,
                Fax = item.FAX,
                Logo = new FileViewModel
                {
                    Name =  item.LogoName.WebRootPathLogo()
                }
            };

            var branchHead = await this._unitOfWork.Branches.GetBranchHead(index);
            if (branchHead != null)
            {
                branch.UserName = item.UserName;
                branch.Password = item.Password;
                branch.CPCode = item.CPCode;
                branch.RequestID = item.RequestID;
                branch.ServiceId = item.ServiceId;
                branch.CommandCode = item.CommandCode;
                branch.ContentType = item.ContentType;
            }

            return branch;
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] BranchViewModel value)
        {
            if (ModelState.IsValid)
            {
                //
                var userId = Utilities.GetUserId(this.User);
                //
                var branch = new Branch
                {
                    BranchId = Guid.NewGuid(),
                    BranchCode = value.Code,
                    BranchName = value.Name,
                    Address = value.Address,
                    Email = value.Email,
                    PhoneNumber = value.PhoneNumber,
                    FAX = value.Fax,
                    CreatedBy = userId,
                    UpdatedBy = userId,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    UserName = value.UserName,
                    Password = value.Password,
                    CPCode = value.CPCode,
                    RequestID = value.RequestID,
                    ServiceId = value.ServiceId,
                    CommandCode = value.CommandCode,
                    ContentType = value.ContentType
                };

                //save logo to physical file
                if (value.Logo != null &&
                    !string.IsNullOrEmpty(value.Logo.Name) &&
                    !string.IsNullOrEmpty(value.Logo.Value))
                {
                    //Convert Base64 Encoded string to Byte Array.
                    var base64String = value.Logo.Value;
                    var fileName = value.Logo.Name;
                    byte[] imageBytes = Convert.FromBase64String(base64String);

                    //Save the Byte Array as Image File.
                    string filePath = string.Format("{0}/uploads/logos/{1}{2}",
                        this._env.WebRootPath,
                        branch.BranchId.ToString().Replace("-", string.Empty),
                        System.IO.Path.GetFileName(fileName));

                    System.IO.File.WriteAllBytes(filePath, imageBytes);
                    //store filename to DB
                    branch.LogoName = filePath.GetFileName();
                }

                //commit
                var ret = await this._unitOfWork.Branches.Save(branch, value.ID);

                //return client side
                return Ok(ret);
            }

            return BadRequest(ModelState);
        }

        [HttpPost("remove")]
        public async Task<IActionResult> Remove([FromBody] Guid id)
        {
            if (ModelState.IsValid)
            {
                var ret = await this._unitOfWork.Branches.Delete(id);

                return Ok(ret);
            }

            return BadRequest(ModelState);
        }

        private Guid userId
        {
            get
            {
                return Utilities.GetUserId(this.User);
            }
        }

        [HttpGet("getbranchheads")]
        [Produces(typeof(UserViewModel))]
        public IActionResult GetBranchHead(string branchId)
        {
            // var results = await this._unitOfWork.IOStocks.Search(filter, value);
            var results = this._unitOfWork.Branches.GetBranchHead
                        (
                            branchId
                        );
            return Ok(results.Select(item => new BranchViewModel
            {
                ID = item.BranchId,
                Code = item.BranchCode,
                Name = item.BranchName,
                Email = item.Email,
                Address = item.Address,
                PhoneNumber = item.PhoneNumber,
                IsExist = item.IsExist,
                ParentBranchId = item.ParentBranchId
            }));

        }

        [HttpPost("savehead")]
        public async Task<IActionResult> SaveHead([FromBody] BranchViewModel[] values)
        {
            if (ModelState.IsValid)
            {
                Guid? id = Guid.NewGuid();
                var itemFirst = values[0];
                if (itemFirst != null)
                {
                    id = itemFirst.ParentBranchId;
                }

                await this._unitOfWork.Branches.SaveHead(values.Select(p => new Branch
                {
                    BranchId = p.ID.HasValue ? p.ID.Value : Guid.NewGuid(),
                    IsExist = p.IsExist,

                }).ToArray(), id, userId);


                return GetBranchHead(id.ToString());
            }
            return BadRequest(ModelState);
        }

        [HttpGet("pdf")]
        //[Produces("application/pdf")]
        public async Task<IActionResult> OutputPDF(string filter, string value, int page, int size)
        {
            var contents = await this.generateOutputContent(filter, value, page, size);
            var output = generatePdf(contents);

            return File(output, "application/pdf");
        }

        [HttpGet("csv")]
        [Produces(typeof(UserViewModel))]
        public async Task<JsonResult> OutputCSV(string filter, string value, int page, int size)
        {
            var contents = await this.generateOutputContent(filter, value, page, size);

            return Json(contents);
        }

        private byte[] generatePdf(string contents)
        {
            var converter = new SynchronizedConverter(new PdfTools());

            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
        ColorMode = ColorMode.Color,
        Orientation = Orientation.Landscape,
        PaperSize = PaperKind.A4Plus,
    },
                Objects = {
        new ObjectSettings() {
            PagesCount = true,
            HtmlContent = contents,
            WebSettings = { DefaultEncoding = "utf-8" },
            HeaderSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 }
        }
    }
            };

            byte[] pdf = converter.Convert(doc);

            return pdf;
        }

        private async Task<string> generateOutputContent(string filter, string value, int page, int size)
        {
            var ret = from c in await this._unitOfWork.Branches.Search(filter, value, page, size)
                      select new BranchViewModel
                      {
                          ID = c.BranchId,
                          Code = c.BranchCode,
                          Name = c.BranchName,
                          Email = c.Email,
                          Address = c.Address,
                          PhoneNumber = c.PhoneNumber,
                          Fax = c.FAX,
                          Logo = new FileViewModel
                          {
                              Name = c.LogoName.WebRootPathLogo()
                          }
                      };

            var contents = base.CSV<BranchViewModel>(ret);

            return contents;
        }
    }
}
