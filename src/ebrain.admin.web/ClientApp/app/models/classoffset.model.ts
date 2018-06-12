// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

export class ClassOffset {
    // Note: Using only optional constructor properties without backing store disables typescript's type checking for the type
    constructor(classId?: string) {
        this.classId = classId;
    }

    public classOffsetId: string;
    public classId: string;
    public studentId: string;
    public code: string;
    public note: string;
    public shiftId: string;
    public learnDate: Date;
    public shiftName: string;
}