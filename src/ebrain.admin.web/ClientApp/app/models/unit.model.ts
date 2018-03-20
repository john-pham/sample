// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

export class Unit {
    // Note: Using only optional constructor properties without backing store disables typescript's type checking for the type
    constructor(unitId?: string) {
        this.id = unitId;
    }

    public id: string;
    public name: string;
    public code: string;
    public note: string;

}