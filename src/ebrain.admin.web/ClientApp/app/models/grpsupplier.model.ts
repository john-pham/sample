// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

export class Grpsupplier {
    // Note: Using only optional constructor properties without backing store disables typescript's type checking for the type
    constructor(grpsupplierId?: string) {

        this.id = grpsupplierId;
    }
    
    public id: string;
    public name: string;
    public code: string;
    public note: string;
    public isEmployee: boolean;
    public isCustomer: boolean;
    public isSupplier : boolean;
}