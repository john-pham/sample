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
        this.ID = shiftclassId;
    }

    public ID: string;
    public Name: string;
    public Code: string;
    public Note: string;

}