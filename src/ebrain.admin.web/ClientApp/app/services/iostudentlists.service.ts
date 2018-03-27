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

@Injectable()
export class IOStudentListService {

    constructor(private router: Router, private configurations: ConfigurationService, private endpointFactory: IOStudentListEndpoint) {
        this.initializeStatus();
    }

    search(filter: string, value: string, fromDate: Date, toDate: Date) {
        return this.endpointFactory.search(filter, value, fromDate, toDate)
            .map((response: Response) => <IOStockReport[]>response.json());
    }

    getiopayment(filter: string, value: string, isInput: boolean, ioid: string, fromDate: Date, toDate: Date) {
        return this.endpointFactory.getiopayment(filter, value, ioid, isInput, fromDate, toDate)
            .map((response: Response) => <IOStockReport[]>response.json());
    }

    getiobyiotypeid(filter: string, value: string, fromDate: Date, toDate: Date) {
        return this.endpointFactory.getiobyiotypeid(filter, value, fromDate, toDate)
            .map((response: Response) => <IOStockReport[]>response.json());
    }

    getiodetailbyiotypeid(filter: string, value: string, fromDate: Date, toDate: Date) {
        return this.endpointFactory.getiodetailbyiotypeid(filter, value, fromDate, toDate)
            .map((response: Response) => <IOStockReport[]>response.json());
    }

    getWarehouseCard(filter: string, value: string, fromDate: Date, toDate: Date) {
        return this.endpointFactory.getWarehouseCard(filter, value, fromDate, toDate)
            .map((response: Response) => <IOStockReport[]>response.json());
    }

    private initializeStatus() {

    }
}