// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

export class Attendance {
    // Note: Using only optional constructor properties without backing store disables typescript's type checking for the type
    constructor(id?: string) {
        this.attendanceId = id;
    }

    public attendanceId: string;
    public classId: string;
    public studentId: string;
    public branchId: string;
    public absent: boolean;
    public notAbsent: boolean;
    public studentCode: string;
    public studentName: string;
    public phone: string;
    public attendanceDate: Date;
    public isAttendance: boolean;

}