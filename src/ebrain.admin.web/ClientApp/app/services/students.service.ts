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
import { Student } from '../models/student.model';
import 'rxjs/add/operator/map';

import { StudentsEndpoint } from './students-endpoint.service';
import { UnitsEndpoint } from './units-endpoint.service';

import { ConfigurationService } from './configuration.service';
import { JwtHelper } from './jwt-helper';
import { Unit } from "../models/unit.model";
import { TypeMaterial } from "../models/typeMaterial.model";
import { TypeMaterialsEndpoint } from "./typeMaterials-endpoint.service";
import { GrpMaterialsEndpoint } from "./grpMaterials-endpoint.service";
import { GrpMaterial } from "../models/grpMaterial.model";
import { SuppliersEndpoint } from "./suppliers-endpoint.service";
import { Supplier } from "../models/supplier.model";
import { StudentstatusEndpoint } from "./studentstatus-endpoint.service";
import { Studentstatus } from "../models/studentstatus.model";
import { GenderStudent } from "../models/genderstudent.model";
import { GenderStudentEndpoint } from "./genderstudent-endpoint.service";
import { Results } from "../models/results.model";


@Injectable()
export class StudentsService {

    constructor(private router: Router, private configurations: ConfigurationService, private endpointFactory: StudentsEndpoint,
        private typesEndpoint: TypeMaterialsEndpoint, private unitsEndpoint: UnitsEndpoint,
        private grpEndpoint: GrpMaterialsEndpoint, private supEndpoint: SuppliersEndpoint, private statusEndpoint: StudentstatusEndpoint,
        private genderEndpoint: GenderStudentEndpoint) {
        this.initializeStatus();
    }

    getGender() {
        return this.genderEndpoint.getall()
            .map((response: Response) => <GenderStudent[]>response.json());
    }

    getStudentStatus() {
        return this.statusEndpoint.getall()
            .map((response: Response) => <Studentstatus[]>response.json());
    }

    search(filter: string, value: string) {
        return this.endpointFactory.search(filter, value)
            .map((response: Response) => <Student[]>response.json());
    }

    findGrpByTypeId(typeId: string) {
        return this.grpEndpoint.findFromTypeId(typeId)
            .map((response: Response) => <GrpMaterial[]>response.json());
    }

    getAll(page?: number, pageSize?: number) {
        return Observable.forkJoin(
            this.endpointFactory.getAll(page, pageSize).map((response: Response) => <Student[]>response.json()));
    }

    save(value: Student) {
        return this.endpointFactory.save(value)
            .map((response: Response) => <Student>response.json());
    }

    get(index: string) {
        return this.endpointFactory.get(index)
            .map((response: Response) => <Student>response.json());
    }

    getBirthdayStudent(fromDate: Date, toDate: Date, page: number, size: number) {
        return this.endpointFactory.getBirthdayStudent(fromDate, toDate, page, size).map((response: Response) => <Results<Student>>response.json());
    }

    getStudentEndClass(classId: string, toDate: Date, page: number, size: number) {
        return this.endpointFactory.getStudentEndClass(classId, toDate, page, size).map((response: Response) => <Results<Student>>response.json());
    }

    getStudentPotential(filterValue: string, page: number, size: number) {
        return this.endpointFactory.getStudentPotential(filterValue, page, size).map((response: Response) => <Results<Student>>response.json());
    }

    getStudentLearning(filterValue: string, studentId: string, classId: string, learning: number, page: number, size: number) {
        return this.endpointFactory.getStudentLearning(filterValue, studentId, classId, learning, page, size).map((response: Response) => <Results<Student>>response.json());
    }

    delete(id: string) {
        return this.endpointFactory.delete(id);
    }

    getNewStudent() {
        return this.endpointFactory.getNewStudent().map((response: Response) => <number>response.json());
    }

    getAllStudent() {
        return this.endpointFactory.getAllStudent().map((response: Response) => <number>response.json());
    }

    getstudentbycreatedate(filter: string, value: string, fromDate: Date, toDate: Date, page: number, size: number) {
        return this.endpointFactory.getstudentbycreatedate(filter, value, fromDate, toDate, page, size).map((response: Response) => <Results<Student>>response.json());
    }

    getStudentCourse(filterValue: string, page: number, size: number) {
        return this.endpointFactory.getStudentCourse(filterValue, page, size).map((response: Response) => <Results<Student>>response.json());
    }

    getTeacherCourse(filterValue: string, page: number, size: number) {
        return this.endpointFactory.getTeacherCourse(filterValue, page, size).map((response: Response) => <Results<Student>>response.json());
    }

    private initializeStatus() {

    }
}