// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

export class GrpMaterial {
    // Note: Using only optional constructor properties without backing store disables typescript's type checking for the type
    constructor(grpMaterialId?: string) {

        this.id = grpMaterialId;
    }
    
    public id: string;
    public name: string;
    public Code: string;
    public Note: string;
    public typeMaterialName: string;
}