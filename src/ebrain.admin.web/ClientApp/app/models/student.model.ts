// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

import { Unit } from "./unit.model";

export class Student {
    // Note: Using only optional constructor properties without backing store disables typescript's type checking for the type
    constructor(materialId?: string) {
        this.id = materialId;
    }

    public id: string;
    public name: string;
    public code: string;
    public note: string;
    public units: Unit[];
    public unitId: string;
    public address: string;
    public taxcode: string;
    public phone: string;
    public accountBank: string;
    public email: string;
    public genderId: number;
    public studentStatusId: string;

    public username: string;
    public password: string;
    public schoolName: string;
    public classId: string;
    public className: string;
    public birthday: Date;
    public avatar: string;
    public supplierId: string;
    public typeMaterialId: string;
    public grpMaterialId: string;

    public faUserName: string;
    public faBirthday: Date;
    public faPhone: string;
    public faFacebook: string;
    public faAddress: string;
    public faEmail: string;
    public faJob: string;
    public faRelationship: string;
    public faWanted: string;
    public studentId: string;
    public genderName: string;
    public totalDay: number;

    public classCode: string;
    public startDate: Date;
    public endDate: Date;
    public materialCode: string;
    public materialName: string;

    public supplierCode: string;
    public supplierName: string;
    public shiftClassName: string;
    public todayName: string;
    public roomName: string;

    public countAbsent: number;
    public countNotAbsent: number;

}