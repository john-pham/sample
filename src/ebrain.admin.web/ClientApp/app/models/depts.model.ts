// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

export class Depts {
    // Note: Using only optional constructor properties without backing store disables typescript's type checking for the type
    constructor(id?: string) {
        this.studentId = id;
    }
    public studentId: string;
    public studentCode: string;
    public studentName: string;
    public phone: string;
    public receiptFirst: number;
    public paymentFirst: number;
    public totalPricePayment: number;
    public totalPriceReceipt: number;
    public payment: number;
    public receipt: number;
    public endPayment: number;
    public endReceipt: number;

}