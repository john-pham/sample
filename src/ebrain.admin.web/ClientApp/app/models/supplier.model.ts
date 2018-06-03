// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

export class Supplier {
    // Note: Using only optional constructor properties without backing store disables typescript's type checking for the type
    constructor(supplierId?: string) {
        this.id = supplierId;
    }

    public id: string;
    public name: string;
    public code: string;
    public note: string;
    public grpSupplierId: string;
    public address: string;
    public taxCode: string;
    public phone: string;
    public fax: string;
    public email: string;
    public accountBank: string;
    public birthday: Date;
    public userLoginId: string;
}