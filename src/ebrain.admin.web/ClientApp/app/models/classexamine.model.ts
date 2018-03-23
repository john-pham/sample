// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

export class ClassExamine {
    // Note: Using only optional constructor properties without backing store disables typescript's type checking for the type
    constructor(classId?: string) {
        this.classExamineId = classId;
    }

    public classExamineId: string;
    public classId: string;
    public examineId: string;
    public studentId: string;
    public examineCode: string;
    public examineName: string;
    public mark: string;
}