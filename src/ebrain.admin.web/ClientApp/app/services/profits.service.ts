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

import { ProfitsEndpoint } from './profits-endpoint.service';
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
import { Profit } from "../models/profits.model";
import { AccountService } from "./account.service";
import { AccountEndpoint } from "./account-endpoint.service";
import { Results } from "../models/results.model";

@Injectable()
export class ProfitsService {

    constructor(private router: Router, private configurations: ConfigurationService, private endpointFactory: ProfitsEndpoint) {
        this.initializeStatus();
    }

    getProfits(filter: string, value: string, fromDate: Date, toDate: Date, page: number, size: number) {
        return this.endpointFactory.getProfits(filter, value, fromDate, toDate, page, size)
            .map((response: Response) => <Results<Profit>>response.json());
    }
    updateProfits(filter: string, value: string, fromDate: Date, toDate: Date) {
        return this.endpointFactory.updateProfits(filter, value, fromDate, toDate)
            .map((response: Response) => <boolean>response.json());
    }

    private initializeStatus() {

    }
}