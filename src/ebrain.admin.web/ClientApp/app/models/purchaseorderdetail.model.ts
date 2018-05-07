// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

export class PurchaseOrderDetail {
    // Note: Using only optional constructor properties without backing store disables typescript's type checking for the type
    constructor(ioid?: string) {
        this.id = ioid;
    }
    public id: string;
    public materialName: string;
    public materialCode: string;
    public typeMaterial: string;
    public grpMaterial: string;
    public materialid: string;
    public materialTypeId: string;
    public materialGrpId: string;
    public sellPrice: number;
    public quantity: number;
    public totalPrice: number; 
    public Note: string;
    
}