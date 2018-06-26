// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

import { ClassTime } from "./classtime.model";
import { ClassStudent } from "./classstudent.model";

export class Class {
    // Note: Using only optional constructor properties without backing store disables typescript's type checking for the type
    constructor(classId?: string) {
        this.id = classId;
    }

    public id: string;
    public name: string;
    public code: string;
    public classCode: string;
    public className: string;
    public note: string;
    public materialId: string;
    public materialName: string;
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
    public shiftId: string;
    public startTime: Date;
    public endTime: Date;
    public countStudent: number;

    public times: ClassTime[];
    public students: ClassStudent[];

    public studentName: string;
    public studentBirthday: Date;
    public studentAddress: string;
    public ioStockId: string;
    public ioStockDetailId: string;
    public learnDate: Date;
    public absent: number;
    public fromDate: Date;
    public toDate: Date;
}