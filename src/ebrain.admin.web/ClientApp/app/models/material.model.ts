// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

import { Unit } from "./unit.model";

export class Material {
    // Note: Using only optional constructor properties without backing store disables typescript's type checking for the type
    constructor(materialId?: string) {
        this.id = materialId;
    }

    public id: string;
    public name: string;
    public code: string;
    public note: string;
    public Units: Unit[];
    public unitId: string;
    public typeName: string;
    public grpName: string;

    public supplierId: string;
    public typeMaterialId: string;
    public grpMaterialId: string;

    public price: number;
    public sellPrice: number;
    public WholePrice: number;
}