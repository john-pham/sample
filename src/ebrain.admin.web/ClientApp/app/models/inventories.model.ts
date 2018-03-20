// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

export class Inventories {
    // Note: Using only optional constructor properties without backing store disables typescript's type checking for the type
    constructor(id?: string) {
        this.id = id;
    }
    public id: string;
    public code: string;
    public name: string;
    public typeCode: string;
    public typeName: string;
    public grpCode  : string;
    public grpName: string;
    public quantityInv: string;
    public quantityInput: number;
    public quantityOutput: number;
    public quantityEnd: number; 
    
}