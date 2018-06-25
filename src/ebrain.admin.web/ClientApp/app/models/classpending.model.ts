// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

export class ClassPending {
    // Note: Using only optional constructor properties without backing store disables typescript's type checking for the type
    constructor(classId?: string) {
        this.classId = classId;
    }

    public classPendingId: string;
    public classId: string;
    public studentId: string;
    public code: string;
    public note: string;
    public fromDate: Date;
    public toDate: Date;
}