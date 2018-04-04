// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================
export class Document {
    // Note: Using only optional constructor properties without backing store disables typescript's type checking for the type
    constructor(id?: string) {

        this.id = id;
    }

    public id: string;
    public grpId: string;
    public path: string;
    public name: string;
    public code: string;
    public note: string;
}
