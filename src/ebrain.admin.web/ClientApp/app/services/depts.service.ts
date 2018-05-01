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

import { DeptsEndpoint } from './depts-endpoint.service';
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
import { Depts } from "../models/depts.model";
import { AccountService } from "./account.service";
import { AccountEndpoint } from "./account-endpoint.service";
import { Results } from "../models/results.model";


@Injectable()
export class DeptService {

    constructor(private router: Router, private configurations: ConfigurationService, private endpointFactory: DeptsEndpoint) {
        this.initializeStatus();
    }

    getDepts(filter: string, value: string, fromDate: Date, toDate: Date, page: number, size: number) {
        return this.endpointFactory.getDepts(filter, value, fromDate, toDate, page, size)
            .map((response: Response) => <Results<Depts>>response.json());
    }
    updateDepts(filter: string, value: string, fromDate: Date, toDate: Date) {
        return this.endpointFactory.updateDepts(filter, value, fromDate, toDate)
            .map((response: Response) => <boolean>response.json());
    }

    private initializeStatus() {

    }
}