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

    public supplierName: string;
    public address: string;
    public fullName: string;
    public materialName: string;
}