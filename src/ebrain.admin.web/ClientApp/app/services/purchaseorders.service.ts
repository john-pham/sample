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

import { IOStudentsEndpoint } from './iostudents-endpoint.service';
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
import { IOStock } from "../models/iostock.model";
import { AccountService } from "./account.service";
import { AccountEndpoint } from "./account-endpoint.service";
import { User } from "../models/user.model";
import { MaterialsEndpoint } from "./materials-endpoint.service";
import { Material } from "../models/material.model";
import { IOStockDetail } from "../models/iostockdetail.model";
import { StudentsEndpoint } from "./students-endpoint.service";
import { Results } from "../models/results.model";
import { PurchaseOrder } from "../models/purchaseorder.model";
import { PurchaseOrderReport } from "../models/purchaseorderreport.model";
import { PurchaseOrdersEndpoint } from "./purchaseorders-endpoint.service";
import { Chart } from "../models/chart.model";

@Injectable()
export class PurchaseOrderService {

    constructor(private router: Router, private configurations: ConfigurationService, private endpointFactory: PurchaseOrdersEndpoint,
        private typesEndpoint: TypeMaterialsEndpoint, private unitsEndpoint: UnitsEndpoint,
        private grpEndpoint: GrpMaterialsEndpoint, private supEndpoint: SuppliersEndpoint, private accountEndpoint: AccountEndpoint,
        private studentEndpoint: StudentsEndpoint,
        private materialendpoint: MaterialsEndpoint) {
        this.initializeStatus();
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
            this.endpointFactory.getAll(page, pageSize).map((response: Response) => <IOStock[]>response.json())
        );
    }

    getUsers(page?: number, pageSize?: number) {
        return this.accountEndpoint.getUsersEndpoint(page, pageSize)
            .map((response: Response) => <User[]>response.json());
    }

    getMaterial(filter: string, value: string) {
        return this.materialendpoint.search(filter, value, 0, 0)
            .map((response: Response) => <Results<Material>>response.json());
    }

    getStudent() {
        return this.studentEndpoint.search("", "")
            .map((response: Response) => <Student[]>response.json());
    }

    save(io: PurchaseOrder) {
        return this.endpointFactory.save(io)
            .map((response: Response) => <PurchaseOrder>response.json());
    }

    deletemaster(index: string) {
        return this.endpointFactory.deletemaster(index)
            .map((response: Response) => <PurchaseOrder>response.json());
    }

    cancelmaster(index: string) {
        return this.endpointFactory.cancel(index)
            .map((response: Response) => <PurchaseOrder>response.json());
    }

    get(index: string) {
        return this.endpointFactory.get(index)
            .map((response: Response) => <PurchaseOrder>response.json());
    }

    getdefault(index, isinput) {
        return this.endpointFactory.getdefault(index)
            .map((response: Response) => <PurchaseOrder>response.json());
    }

    delete(id: string) {
        return this.endpointFactory.delete(id);
    }

    getpurchaseorders(filter: string, value: string, fromDate: Date, toDate: Date, allData: number, page: number, size: number) {
        return this.endpointFactory.getpurchaseorders(filter, value, fromDate, toDate, allData, page, size)
            .map((response: Response) => <Results<PurchaseOrderReport>>response.json());
    }

    reportpurchaseorders(filter: string, value: string, fromDate: Date, toDate: Date, allData: number, page: number, size: number) {
        return this.endpointFactory.reportpurchaseorders(filter, value, fromDate, toDate, allData, page, size)
            .map((response: Response) => <Chart>response.json());
    }

    getpurchaseorderdetails(filter: string, value: string, fromDate: Date, toDate: Date, page: number, size: number) {
        return this.endpointFactory.getpurchaseorderdetails(filter, value, fromDate, toDate, page, size)
            .map((response: Response) => <Results<PurchaseOrderReport>>response.json());
    }

    retportpurchaseorderdetails(filter: string, value: string, fromDate: Date, toDate: Date, page: number, size: number) {
        return this.endpointFactory.reportpurchaseorderdetails(filter, value, fromDate, toDate, page, size)
            .map((response: Response) => <Chart>response.json());
    }

    getpurchaseorderdetailhistorys(filter: string, value: string, fromDate: Date, toDate: Date, page: number, size: number) {
        return this.endpointFactory.getpurchaseorderdetailhistorys(filter, value, fromDate, toDate, page, size)
            .map((response: Response) => <Results<PurchaseOrderReport>>response.json());
    }

    getpurchasedetailsbyid(index: string) {
        return this.endpointFactory.getpurchasedetailsbyid(index)
            .map((response: Response) => <IOStockDetail[]>response.json());
    }

    private initializeStatus() {

    }
}