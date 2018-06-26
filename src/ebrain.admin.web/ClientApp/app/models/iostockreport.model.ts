// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

import { IOStockDetail } from "./iostockdetail.model";

export class IOStockReport {
    // Note: Using only optional constructor properties without backing store disables typescript's type checking for the type
    constructor(ioid?: string) {
        this.id = ioid;
    }

    public id: string;
    public ioTypeId: number;
    public name: string;
    public code: string;
    public note: string;
    public studentId: string;
    public studentName: string;
    public createDate: Date;
    public createBy: string;
    public createByName: string;
    public totalPrice: number;
    public quantity: number;
    public quantityInput: number;
    public quantityOutput: number;
    public SellPrice: number;
    public fullName: string;
    public totalPricePayment: number;
    public totalPriceExist: number;

    public materialId: string;
    public materialCode: string;
    public materialName: string;
    public ioStockDetailId: string;
    public inputExport: number;
    public ioDetails: IOStockDetail[];
}