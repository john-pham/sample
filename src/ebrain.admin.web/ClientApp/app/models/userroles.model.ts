﻿// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

export class UserRoles {
    // Note: Using only optional constructor properties without backing store disables typescript's type checking for the type
    constructor(id?: string) {
        this.id = id;
    }

    public id: string;
    public name: string;
    public code: string;
    public note: string;
    public description: string;

    public isActive: boolean;
    public branchId: string;
    public userId: string;
    public groupId: string;
    public userName: string;
    public fullName: string;
    public groupName: string;
    public branchName: string;

}