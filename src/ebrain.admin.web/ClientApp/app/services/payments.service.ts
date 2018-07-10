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

import { PaymentsEndpoint } from './payments-endpoint.service';
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
import { Payment } from "../models/payment.model";
import { AccountService } from "./account.service";
import { AccountEndpoint } from "./account-endpoint.service";
import { User } from "../models/user.model";
import { MaterialsEndpoint } from "./materials-endpoint.service";
import { Material } from "../models/material.model";
import { PaymentDetail } from "../models/paymentdetail.model";
import { StudentsEndpoint } from "./students-endpoint.service";
import { PaymentType } from "../models/paymenttype.model";
import { Results } from "../models/results.model";
import { Chart } from "../models/chart.model";

@Injectable()
export class PaymentsService {

    constructor(private router: Router, private configurations: ConfigurationService, private endpointFactory: PaymentsEndpoint,
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

    searchSummarize(filter: string, value: string,isInput: number, fromDate: Date, toDate: Date, page: number, size: number) {
        return this.endpointFactory.searchSummarize(filter, value,isInput, fromDate, toDate, page, size)
            .map((response: Response) => <Results<Payment>>response.json());
    }

    repotSummarize(filter: string, value: string, isInput: number, fromDate: Date, toDate: Date, page: number, size: number) {
        return this.endpointFactory.repotSummarize(filter, value, isInput, fromDate, toDate, page, size)
            .map((response: Response) => <Chart>response.json());
    }

    searchDetail(filter: string, value: string, fromDate: Date, toDate: Date, page: number, size: number) {
        return this.endpointFactory.searchDetail(filter, value, fromDate, toDate, page, size)
            .map((response: Response) => <Results<Payment>>response.json());
    }

    findGrpByTypeId(typeId: string) {
        return this.grpEndpoint.findFromTypeId(typeId)
            .map((response: Response) => <GrpMaterial[]>response.json());
    }

    getAll(page?: number, pageSize?: number) {
        return Observable.forkJoin(
            this.endpointFactory.getAll(page, pageSize).map((response: Response) => <Payment[]>response.json())
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

    getPaymentTypes(isInput: boolean) {
        return this.endpointFactory.getPaymentType("", "", isInput)
            .map((response: Response) => <PaymentType[]>response.json());
    }

    save(io: Payment) {
        return this.endpointFactory.save(io)
            .map((response: Response) => <Payment>response.json());
    }

    deletemaster(index: string) {
        return this.endpointFactory.deletemaster(index)
            .map((response: Response) => <Payment>response.json());
    }

    cancelmaster(index: string) {
        return this.endpointFactory.cancel(index)
            .map((response: Response) => <Payment>response.json());
    }

    get(index: string) {
        return this.endpointFactory.get(index)
            .map((response: Response) => <Payment>response.json());
    }

    getdefault(index, isInput) {
        return this.endpointFactory.getdefault(index, isInput)
            .map((response: Response) => <Payment>response.json());
    }

    delete(id: string) {
        return this.endpointFactory.delete(id);
    }

    private initializeStatus() {

    }
}