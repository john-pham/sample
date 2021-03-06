﻿// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

import { Component, OnInit, ViewChild, Input } from '@angular/core';

import { AlertService, MessageSeverity } from '../../services/alert.service';
import { AccountService } from "../../services/account.service";
import { Utilities } from '../../services/utilities';
import { User } from '../../models/user.model';
import { UserEdit } from '../../models/user-edit.model';
import { Role } from '../../models/role.model';
import { Permission } from '../../models/permission.model';
import { Branch } from "../../models/branch.model";
import { BranchesService } from "../../services/branches.service";
import { File } from '../../models/file.model';

@Component({
    selector: 'user-info',
    templateUrl: './user-info.component.html',
    styleUrls: ['./user-info.component.css']
})
export class UserInfoComponent implements OnInit {

    private isEditMode = false;
    private isNewUser = false;
    private isSaving = false;
    private isChangePassword = false;
    private isEditingSelf = false;
    private showValidationErrors = false;
    private editingUserName: string;
    private uniqueId: string = Utilities.uniqueId();
    private user: User = new User();
    private userEdit: UserEdit;
    private allRoles: Role[] = [];
    private branches = [];

    private src: string = "";
    private file_name: string = "";

    public formResetToggle = true;

    public changesSavedCallback: () => void;
    public changesFailedCallback: () => void;
    public changesCancelledCallback: () => void;

    @Input()
    isViewOnly: boolean;

    @Input()
    isGeneralEditor = false;


    @ViewChild('f')
    private form;

    //ViewChilds Required because ngIf hides template variables from global scope
    @ViewChild('userName')
    private userName;

    @ViewChild('userPassword')
    private userPassword;

    @ViewChild('email')
    private email;

    @ViewChild('currentPassword')
    private currentPassword;

    @ViewChild('newPassword')
    private newPassword;

    @ViewChild('confirmPassword')
    private confirmPassword;

    @ViewChild('roles')
    private roles;


    constructor(private alertService: AlertService, private accountService: AccountService, private branchService: BranchesService) {

    }

    ngOnInit() {
        if (!this.isGeneralEditor) {
            this.loadCurrentUserData();
        }
    }



    private loadCurrentUserData() {
        this.alertService.startLoadingMessage();

        if (this.canViewAllRoles) {
            this.accountService.getUserAndRoles().subscribe(results => this.onCurrentUserDataLoadSuccessful(results[0], results[1]), error => this.onCurrentUserDataLoadFailed(error));
        }
        else {
            this.accountService.getUser().subscribe(user => this.onCurrentUserDataLoadSuccessful(user, []), error => this.onCurrentUserDataLoadFailed(error));
        }
    }


    private onCurrentUserDataLoadSuccessful(user: User, roles: Role[]) {
        this.alertService.stopLoadingMessage();
        this.user = user;
        this.allRoles = roles;
    }

    private onCurrentUserDataLoadFailed(error: any) {
        this.alertService.stopLoadingMessage();
        this.alertService.showStickyMessage("Load Error", `Unable to retrieve user data from the server.\r\nErrors: "${Utilities.getHttpResponseMessage(error)}"`,
            MessageSeverity.error, error);

        this.user = new User();
    }



    private getRoleByName(name: string) {
        return this.allRoles.find((r) => r.name == name)
    }



    private showErrorAlert(caption: string, message: string) {
        this.alertService.showMessage(caption, message, MessageSeverity.error);
    }


    public deletePasswordFromUser(user: UserEdit | User) {
        let userEdit = <UserEdit>user;

        delete userEdit.currentPassword;
        delete userEdit.newPassword;
        delete userEdit.confirmPassword;
    }


    private edit() {
        if (!this.isGeneralEditor) {
            this.isEditingSelf = true;
            this.userEdit = new UserEdit();
            Object.assign(this.userEdit, this.user);
        }
        else {
            if (!this.userEdit)
                this.userEdit = new UserEdit();
            this.isEditingSelf = this.accountService.currentUser ? this.userEdit.id == this.accountService.currentUser.id : false;
        }

        this.isEditMode = true;
        this.showValidationErrors = true;
        this.isChangePassword = false;
    }


    private save() {
        this.isSaving = true;
        this.alertService.startLoadingMessage("Saving changes...");

        var branch = this.branches.find(p => p.id == this.userEdit.branchId);
        this.userEdit.branchName = branch != null ? branch.name : "";
        this.user.branchId = this.userEdit.branchId;
        if (this.isNewUser) {
            this.accountService.newUser(this.userEdit).subscribe(user => this.saveSuccessHelper(user), error => this.saveFailedHelper(error));
        }
        else {
            this.accountService.updateUser(this.userEdit).subscribe(response => this.saveSuccessHelper(), error => this.saveFailedHelper(error));
        }
    }

    getBranch() {
        this.branchService.getAll().subscribe(user => this.loadBranchSuccessHelper(user), error => this.saveFailedHelper(error));
    }

    private loadBranchSuccessHelper(branches: Branch[]) {
        this.userEdit.branchId = this.userEdit.branchId;
        this.branches = branches;
    }
    private saveSuccessHelper(user?: User) {
        this.testIsRoleUserCountChanged(this.user, this.userEdit);

        if (user)
            Object.assign(this.userEdit, user);

        this.isSaving = false;
        this.alertService.stopLoadingMessage();
        this.isChangePassword = false;
        this.showValidationErrors = false;

        this.deletePasswordFromUser(this.userEdit);
        Object.assign(this.user, this.userEdit);
        this.userEdit = new UserEdit();
        this.resetForm();


        if (this.isGeneralEditor) {
            if (this.isNewUser)
                this.alertService.showMessage("Success", `User \"${this.user.userName}\" was created successfully`, MessageSeverity.success);
            else if (!this.isEditingSelf)
                this.alertService.showMessage("Success", `Changes to user \"${this.user.userName}\" was saved successfully`, MessageSeverity.success);
        }

        if (this.isEditingSelf) {
            this.alertService.showMessage("Success", "Changes to your User Profile was saved successfully", MessageSeverity.success);
            this.refreshLoggedInUser();
        }

        this.isEditMode = false;


        if (this.changesSavedCallback)
            this.changesSavedCallback();
    }


    private saveFailedHelper(error: any) {
        this.isSaving = false;
        this.alertService.stopLoadingMessage();
        this.alertService.showStickyMessage("Superbrain thông báo", "The below errors occured whilst saving your changes:", MessageSeverity.error, error);
        this.alertService.showStickyMessage(error, null, MessageSeverity.error);

        if (this.changesFailedCallback)
            this.changesFailedCallback();
    }



    private testIsRoleUserCountChanged(currentUser: User, editedUser: User) {

        let rolesAdded = this.isNewUser ? editedUser.roles : editedUser.roles.filter(role => currentUser.roles.indexOf(role) == -1);
        let rolesRemoved = this.isNewUser ? [] : currentUser.roles.filter(role => editedUser.roles.indexOf(role) == -1);

        let modifiedRoles = rolesAdded.concat(rolesRemoved);

        if (modifiedRoles.length)
            setTimeout(() => this.accountService.onRolesUserCountChanged(modifiedRoles));
    }



    private cancel() {
        if (this.isGeneralEditor)
            this.userEdit = this.user = new UserEdit();
        else
            this.userEdit = new UserEdit();

        this.showValidationErrors = false;
        this.resetForm();

        this.alertService.showMessage("Cancelled", "Operation cancelled by user", MessageSeverity.default);
        this.alertService.resetStickyMessage();

        if (!this.isGeneralEditor)
            this.isEditMode = false;

        if (this.changesCancelledCallback)
            this.changesCancelledCallback();
    }


    private close() {
        this.userEdit = this.user = new UserEdit();
        this.showValidationErrors = false;
        this.resetForm();
        this.isEditMode = false;

        if (this.changesSavedCallback)
            this.changesSavedCallback();
    }



    private refreshLoggedInUser() {
        this.accountService.refreshLoggedInUser()
            .subscribe(user => {
                this.loadCurrentUserData();
            },
            error => {
                this.alertService.resetStickyMessage();
                this.alertService.showStickyMessage("Refresh failed", "An error occured whilst refreshing logged in user information from the server", MessageSeverity.error, error);
            });
    }


    private changePassword() {
        this.isChangePassword = true;
    }


    private unlockUser() {
        this.isSaving = true;
        this.alertService.startLoadingMessage("Unblocking user...");


        this.accountService.unblockUser(this.userEdit.id)
            .subscribe(response => {
                this.isSaving = false;
                this.userEdit.isLockedOut = false;
                this.alertService.stopLoadingMessage();
                this.alertService.showMessage("Success", "User has been successfully unblocked", MessageSeverity.success);
            },
            error => {
                this.isSaving = false;
                this.alertService.stopLoadingMessage();
                this.alertService.showStickyMessage("Unblock Error", "The below errors occured whilst unblocking the user:", MessageSeverity.error, error);
                this.alertService.showStickyMessage(error, null, MessageSeverity.error);
            });
    }


    resetForm(replace = false) {
        this.isChangePassword = false;

        if (!replace) {
            this.form.reset();
        }
        else {
            this.formResetToggle = false;

            setTimeout(() => {
                this.formResetToggle = true;
            });
        }
    }


    newUser(allRoles: Role[]) {
        this.isGeneralEditor = true;
        this.isNewUser = true;

        this.allRoles = allRoles;
        this.editingUserName = null;
        this.user = this.userEdit = new UserEdit();
        this.userEdit.isEnabled = true;
        this.edit();

        return this.userEdit;
    }

    editUser(user: User, allRoles: Role[]) {
        if (user) {
            this.isGeneralEditor = true;
            this.isNewUser = false;

            this.setRoles(user, allRoles);
            this.editingUserName = user.userName;
            this.user = new User();
            this.userEdit = new UserEdit();
            Object.assign(this.user, user);
            Object.assign(this.userEdit, user);
            this.edit();
            this.src = user.profier != null ? user.profier.src : user.profilerImage;
            this.file_name = ""
            return this.userEdit;
        }
        else {
            return this.newUser(allRoles);
        }
    }


    displayUser(user: User, allRoles?: Role[]) {

        this.user = new User();
        Object.assign(this.user, user);
        this.deletePasswordFromUser(this.user);
        this.setRoles(user, allRoles);

        this.isEditMode = false;
    }



    private setRoles(user: User, allRoles?: Role[]) {

        this.allRoles = allRoles ? [...allRoles] : [];

        if (user.roles) {
            for (let ur of user.roles) {
                if (!this.allRoles.some(r => r.name == ur))
                    this.allRoles.unshift(new Role(ur));
            }
        }
    }



    get canViewAllRoles() {
        return this.accountService.userHasPermission(Permission.viewRolesPermission);
    }

    get canAssignRoles() {
        return this.accountService.userHasPermission(Permission.assignRolesPermission);
    }

    fileInputClick() {
        document.getElementById('avatar').click();
    }

    clearFile() {
        if (this.userEdit.profier == null) {
            this.userEdit.profier = new File();
        }
        this.userEdit.profier.name = null;
        this.userEdit.profier.type = null;
        this.userEdit.profier.value = null;
        this.src = "";
        this.file_name = "";
    }

    onFileChange(event) {
        let reader = new FileReader();
        if (event.target.files && event.target.files.length > 0) {
            let file = event.target.files[0];
            if (this.userEdit.profier == null) {
                this.userEdit.profier = new File();
            }
            //
            reader.onload = () => {
                this.file_name = file.name;
                this.src = reader.result;
                this.userEdit.profier.name = file.name;
                this.userEdit.profier.type = file.type;
                this.userEdit.profier.value = reader.result.split(',')[1];
            };
            //
            reader.onloadend = (loadEvent: any) => {
                this.src = loadEvent.target.result;
                this.userEdit.profier.src = this.src;
            };
            //
            reader.readAsDataURL(file);
        }
    }
}
