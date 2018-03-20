// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

export class Profit {
    // Note: Using only optional constructor properties without backing store disables typescript's type checking for the type
    constructor(id?: string) {
        this.id = id;
    }
    public id: string;
    public code: string;
    public name: string;
    public totalPriceFirst: number;
    public totalPriceReceipt: number;
    public totalPricePayment: number;
    public totalPriceEnd: number; 
    
}