// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

export class User {
    // Note: Using only optional constructor properties without backing store disables typescript's type checking for the type
    constructor(id?: string, userName?: string, fullName?: string, email?: string, jobTitle?: string, phoneNumber?: string, branchId?: string, roles?: string[]) {

        this.id = id;
        this.userName = userName;
        this.fullName = fullName;
        this.email = email;
        this.jobTitle = jobTitle;
        this.phoneNumber = phoneNumber;
        this.roles = roles;
        this.branchId = branchId;
    }


    get friendlyName(): string {
        let name = this.fullName || this.userName;

        if (this.jobTitle)
            name = this.jobTitle + " " + name;

        return name;
    }


    public id: string;
    public userName: string;
    public fullName: string;
    public email: string;
    public jobTitle: string;
    public phoneNumber: string;
    public isEnabled: boolean;
    public isLockedOut: boolean;
    public roles: string[];
    public branchId: string;
}