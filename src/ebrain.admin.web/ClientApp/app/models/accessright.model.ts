// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

export class AccessRight {
    // Note: Using only optional constructor properties without backing store disables typescript's type checking for the type
    constructor(id?: string) {
        this.id = id;
    }

    public id: string;
    public name: string;
    public code: string;
    public note: string;

    public featureId: string;
    public featureName: string;

    public featureGroupId: string;

    public groupId: string;
    public groupName: string;

    public view: Boolean;
    public edit: Boolean;
    public create: Boolean;
    public delete: Boolean;

    public userId: string;
    public isActive: Boolean;

    public fullName: string;
    public userName: string;
    public branchName: string;
}