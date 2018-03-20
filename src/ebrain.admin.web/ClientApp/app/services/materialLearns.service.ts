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
import { MaterialLearn } from '../models/materialLearn.model';
import 'rxjs/add/operator/map';

import { MaterialLearnsEndpoint } from './materialLearns-endpoint.service';
import { UnitsEndpoint } from './units-endpoint.service';

import { ConfigurationService } from './configuration.service';
import { JwtHelper } from './jwt-helper';
import { Unit } from "../models/unit.model";
import { TypeMaterialLearn } from "../models/typeMaterialLearn.model";
import { TypeMaterialLearnsEndpoint } from "./typeMaterialLearns-endpoint.service";
import { GrpMaterialLearnsService } from "./grpMaterialLearns.service";
import { GrpMaterialLearn } from "../models/grpMaterialLearn.model";
import { GrpMaterialLearnsEndpoint } from "./grpMaterialLearns-endpoint.service";

@Injectable()
export class MaterialLearnsService {

    constructor(private router: Router, private configurations: ConfigurationService, private endpointFactory: MaterialLearnsEndpoint,
        private typesEndpoint: TypeMaterialLearnsEndpoint, private grpEndpoint: GrpMaterialLearnsEndpoint) {
        this.initializeStatus();
    }

    search(filter: string, value: string) {
        return this.endpointFactory.search(filter, value)
            .map((response: Response) => <MaterialLearn[]>response.json());
    }

    get(index: string) {
        return this.endpointFactory.get(index)
            .map((response: Response) => <MaterialLearn>response.json());
    }

    getMaterialLearn(filter: string, value: string, page: number, size: number) {
        return this.endpointFactory.getMaterialLearn(filter, value, page, size)
            .map((response: Response) => <MaterialLearn[]>response.json());
    }

    getTypeMaterial(index: string) {
        return this.typesEndpoint.get(index)
            .map((response: Response) => <TypeMaterialLearn[]>response.json());
    }

    getalltype() {
        return this.typesEndpoint.getalltype()
            .map((response: Response) => <TypeMaterialLearn[]>response.json());
    }

    findGrpByTypeId(typeId: string) {
        return this.grpEndpoint.findFromTypeId(typeId)
            .map((response: Response) => <GrpMaterialLearn[]>response.json());
    }
    
    save(value: MaterialLearn) {
        return this.endpointFactory.save(value)
            .map((response: Response) => <MaterialLearn>response.json());
    }

    delete(id: string) {
        return this.endpointFactory.delete(id).map((response: Response) => <Boolean>response.json());
    }

    private initializeStatus() {

    }
}