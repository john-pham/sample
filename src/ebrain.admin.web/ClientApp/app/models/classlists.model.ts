// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

export class ClassList {
    // Note: Using only optional constructor properties without backing store disables typescript's type checking for the type
    constructor(classId?: string) {
        this.id = classId;
    }

    public id: string;
    public name: string;
    public code: string;
    public note: string;
    public iOStockId: string;
    public iOStockDetailId: string;
    public ioNumber: string;
    public studentName: string;
    public materialName: string;
    public totalPrice: number;
    public totalPriceLearning: number;
    public numberHourse: number;
    public oneHourMoney: number;
    public totalRemain: number;
    public numberLearning: number;
    public numberRemain: number;
    public materialId: string;
    public longLearn: number;
    public statusId: string;
    public maxStudent: number;
    public startDate: Date;
    public endDate: Date;
    public supplierId: string;
    public todayId: string;
    public roomId: string;
    public teacherTodayId: string;
    public studentId: string;
    public startTime: Date;
    public endTime: Date;
    public createdDate: Date;
    public supplierName: string;
    public address: string;
    public fullName: string;
    public countStudent: number;
}