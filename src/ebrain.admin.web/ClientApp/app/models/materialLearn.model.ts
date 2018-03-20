// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

import { Unit } from "./unit.model";
import { DecimalPipe } from '@angular/common';
import { TypeMaterialLearn } from "./typeMaterialLearn.model";
import { GrpMaterialLearn } from "./grpMaterialLearn.model";

export class MaterialLearn {
    // Note: Using only optional constructor properties without backing store disables typescript's type checking for the type
    constructor(materialId?: string) {
        this.id = materialId;
    }

    public id: string;
    public name: string;
    public code: string;
    public note: string;
    public units: Unit[];
    public types: TypeMaterialLearn[];
    public grps: GrpMaterialLearn[];

    public typeName: string;
    public grpName: string;

    public unitId: string;
    public typeMaterialId: string;
    public grpMaterialId: string;

    public sellPrice: number;
    public maskPassCourse: number;
    public numberHourse: number;

    public calBeCourse: string;
    public spBeCourse: string;
    public calEnCourse: string;
    public spEnCourse: string;
}