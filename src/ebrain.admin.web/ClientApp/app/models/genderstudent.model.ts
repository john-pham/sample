// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

export class GenderStudent {
    // Note: Using only optional constructor properties without backing store disables typescript's type checking for the type
    constructor(id?: number) {
        this.id = id;
    }

    public id: number;
    public name: string;
    public code: string;
    public note: string;

}