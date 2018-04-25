// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

export class Support {
    // Note: Using only optional constructor properties without backing store disables typescript's type checking for the type
    constructor(id?: string) {
        this.supportId = id;
    }

    public supportId: string;
    public branchId: string;
    public supportCode: string;
    public supportName: string;
    public title: string;
    public email: string;
    public phone: string;
    public note: string;
}