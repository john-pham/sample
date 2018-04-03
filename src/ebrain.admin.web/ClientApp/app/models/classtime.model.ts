// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

export class ClassTime {
    // Note: Using only optional constructor properties without backing store disables typescript's type checking for the type
    constructor(classId?: string) {
        this.id = classId;
    }

    public id: string;
    public classId: string;
    public code: string;
    public note: string;
    public branchId: string;
    public onTodayId: string;
    public todayName: string;
    public supplierId: string;
    public shiftId: string;
    public maxStudent: number;
    public startTime: Date;
    public endTime: Date;
    public roomId: string;
    public roomName: string;
    public supplierName: string;
    public shiftName: string;
}