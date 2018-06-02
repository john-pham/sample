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
import { GrpMaterial } from '../models/grpMaterial.model';
import 'rxjs/add/operator/map';

import { GrpMaterialsEndpoint } from './grpMaterials-endpoint.service';
import { ConfigurationService } from './configuration.service';
import { JwtHelper } from './jwt-helper';
import { TypeMaterial } from "../models/typeMaterial.model";
import { TypeMaterialsEndpoint } from "./typeMaterials-endpoint.service";
import { IOStudentListEndpoint } from "./iostudentlists-endpoint.service";
import { IOStockReport } from "../models/iostockreport.model";
import { Results } from "../models/results.model";
import { Chart } from "../models/chart.model";

@Injectable()
export class IOStudentListService {

    constructor(private router: Router, private configurations: ConfigurationService, private endpointFactory: IOStudentListEndpoint) {
        this.initializeStatus();
    }

    search(filter: string, value: string, fromDate: Date, toDate: Date, page: number, size: number) {
        return this.endpointFactory.search(filter, value, fromDate, toDate, page, size)
            .map((response: Response) => <Results<IOStockReport>>response.json());
    }

    reportsearch(filter: string, value: string, fromDate: Date, toDate: Date, page: number, size: number) {
        return this.endpointFactory.reportsearch(filter, value, fromDate, toDate, page, size)
            .map((response: Response) => <Chart>response.json());
    }

    getiopayment(filter: string, value: string, isGetAll: number, isWaitingClass: number, isInput: boolean, ioid: string, fromDate: Date, toDate: Date, page: number, size: number) {
        return this.endpointFactory.getiopayment(filter, value, isGetAll, isWaitingClass, ioid, isInput, fromDate, toDate, page, size)
            .map((response: Response) => <Results<IOStockReport>>response.json());
    }

    getiobyiotypeid(filter: string, value: string, fromDate: Date, toDate: Date, page: number, size: number) {
        return this.endpointFactory.getiobyiotypeid(filter, value, fromDate, toDate, page, size)
            .map((response: Response) => <Results<IOStockReport>>response.json());
    }

    reportiobyiotypeid(filter: string, value: string, fromDate: Date, toDate: Date, page: number, size: number) {
        return this.endpointFactory.reportiobyiotypeid(filter, value, fromDate, toDate, page, size)
            .map((response: Response) => <Chart>response.json());
    }

    getiodetailbyiotypeid(filter: string, value: string, fromDate: Date, toDate: Date, page: number, size: number) {
        return this.endpointFactory.getiodetailbyiotypeid(filter, value, fromDate, toDate, page, size)
            .map((response: Response) => <Results<IOStockReport>>response.json());
    }

    reportiodetailbyiotypeid(filter: string, value: string, fromDate: Date, toDate: Date, page: number, size: number) {
        return this.endpointFactory.reportiodetailbyiotypeid(filter, value, fromDate, toDate, page, size)
            .map((response: Response) => <Chart>response.json());
    }

    getWarehouseCard(filter: string, value: string, fromDate: Date, toDate: Date, page: number, size: number) {
        return this.endpointFactory.getWarehouseCard(filter, value, fromDate, toDate, page, size)
            .map((response: Response) => <Results<IOStockReport>>response.json());
    }

    getIONew() {
        return this.endpointFactory.getIONew()
            .map((response: Response) => <number>response.json());
    }

    getIOAll() {
        return this.endpointFactory.getIOAll()
            .map((response: Response) => <number>response.json());
    }

    private initializeStatus() {

    }
}