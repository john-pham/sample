// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

export class ClassStudent {
    // Note: Using only optional constructor properties without backing store disables typescript's type checking for the type
    constructor(classId?: string) {
        this.id = classId;
    }

    public id: string;
    public classId: string;
    public studentId: string;
    public address: string;
    public fullName: string;
    public startDate: Date;
    public endDate: Date;
    public materialId: string;
    public materialName: string;
    public ioStockId: string;
    public ioStockDetailId: string;
}