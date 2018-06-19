// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

import { Injectable } from '@angular/core';
import { Router, NavigationExtras } from "@angular/router";
import { Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { Class } from '../models/class.model';
import 'rxjs/add/operator/map';

import { ClassesEndpoint } from './classes-endpoint.service';
import { ConfigurationService } from './configuration.service';
import { JwtHelper } from './jwt-helper';
import { ClassList } from "../models/classlists.model";
import { ClassExamine } from "../models/classexamine.model";
import { Results } from "../models/results.model";
import { ClassOffset } from "../models/classoffset.model";
import { ClassEx } from "../models/classex.model";

@Injectable()
export class ClassesService {

    constructor(private router: Router, private configurations: ConfigurationService, private endpointFactory: ClassesEndpoint) {
        this.initializeStatus();
    }

    search(filter: string, value: string, isUsageTeacher: number) {
        return this.endpointFactory.search(filter, value, isUsageTeacher)
            .map((response: Response) => <Class[]>response.json());
    }

    getClassCurrent(studentId: string) {
        return this.endpointFactory.getClassCurrent(studentId)
            .map((response: Response) => <Class[]>response.json());
    }

    save(value: Class) {
        return this.endpointFactory.save(value)
            .map((response: Response) => <Class>response.json());
    }

    saveStudent(value: Class[]) {
        return this.endpointFactory.saveStudent(value)
            .map((response: Response) => <Class>response.json());
    }

    saveExamine(value: ClassExamine[]) {
        return this.endpointFactory.saveExamine(value)
            .map((response: Response) => <ClassExamine>response.json());
    }

    saveOffset(value: ClassOffset[]) {
        return this.endpointFactory.saveOffset(value)
            .map((response: Response) => <ClassOffset[]>response.json());
    }

    saveEx(value: ClassEx[]) {
        return this.endpointFactory.saveEx(value)
            .map((response: Response) => <ClassEx[]>response.json());
    }

    delete(id: string) {
        return this.endpointFactory.delete(id);
    }

    getDefault(index: string) {
        return this.endpointFactory.getDefault(index)
            .map((response: Response) => <Class>response.json());
    }

    get(index: string) {
        return this.endpointFactory.get(index)
            .map((response: Response) => <Class>response.json());
    }

    getFirstClass(index: string) {
        return this.endpointFactory.getFirstClass(index)
            .map((response: Response) => <Class>response.json());
    }

    getClassEx(studentId: string, classId: string) {
        return this.endpointFactory.getClassEx(studentId, classId)
            .map((response: Response) => <ClassEx[]>response.json());
    }

    getClassOffset(studentId: string, classId: string) {
        return this.endpointFactory.getClassOffset(studentId, classId)
            .map((response: Response) => <ClassOffset[]>response.json());
    }

    getClasses(filter: string, value: string, statusId: string, supplierId: string) {
        return this.endpointFactory.getClasses(filter, value, statusId, supplierId)
            .map((response: Response) => <ClassList[]>response.json());
    }

    getsummaries(filter: string, value: string, statusId: string, supplierId: string, classId: string,
        isUsageTeacher: number,
        page: number, size: number) {
        return this.endpointFactory.getsummaries(filter, value, statusId, supplierId, classId, isUsageTeacher, page, size)
            .map((response: Response) => <Results<ClassList>>response.json());
    }
    getScheduleStudent(classId: string, studentId: string, page: number, size: number) {
        return this.endpointFactory.getScheduleStudent(classId, studentId, page, size)
            .map((response: Response) => <Results<ClassList>>response.json());
    }

    getClassByStudentId(filter: string, value: string, statusId: string, supplierId: string, classId: string, studentId: string) {
        return this.endpointFactory.getClassByStudentId(filter, value, statusId, supplierId, classId, studentId)
            .map((response: Response) => <ClassList[]>response.json());
    }

    getClassExamines(classId: string, studentId: string) {
        return this.endpointFactory.getClassExamines(classId, studentId)
            .map((response: Response) => <ClassExamine[]>response.json());
    }

    private initializeStatus() {

    }
}