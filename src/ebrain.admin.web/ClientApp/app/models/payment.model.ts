// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

import { PaymentDetail } from "./paymentdetail.model";

export class Payment {
    // Note: Using only optional constructor properties without backing store disables typescript's type checking for the type
    constructor(ioid?: string) {
        this.id = ioid;
    }

    public id: string;
    public name: string;
    public code: string;
    public note: string;
    public studentId: string;
    public createDate: Date;
    public createBy: string;
    public totalPrice: number;
    public Quantity: number;
    public SellPrice: number;
    public paymentTypeId: number;
    public paymentTypeName: string;
    public fullName: string;
    public ioNumber: string;
    public ioDetails: PaymentDetail[];
}