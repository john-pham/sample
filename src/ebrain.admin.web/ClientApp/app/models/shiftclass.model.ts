// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

export class Shiftclass {
    // Note: Using only optional constructor properties without backing store disables typescript's type checking for the type
    constructor(shiftclassId?: string) {
        this.id = shiftclassId;
    }

    public id: string;
    public name: string;
    public code: string;
    public note: string;

}