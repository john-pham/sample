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
import { GrpMaterialLearn } from '../models/grpMaterialLearn.model';
import 'rxjs/add/operator/map';

import { GrpMaterialLearnsEndpoint } from './grpMaterialLearns-endpoint.service';
import { ConfigurationService } from './configuration.service';
import { JwtHelper } from './jwt-helper';
import { TypeMaterialLearnsEndpoint } from "./typeMaterialLearns-endpoint.service";
import { TypeMaterialLearn } from "../models/TypeMaterialLearn.model";
import { Results } from "../models/results.model";

@Injectable()
export class GrpMaterialLearnsService {

    constructor(private router: Router, private configurations: ConfigurationService, private endpointFactory: GrpMaterialLearnsEndpoint, private typeMaterialEndpoint: TypeMaterialLearnsEndpoint) {
        this.initializeStatus();
    }

    search(filter: string, value: string, page: number, size: number) {
        return this.endpointFactory.search(filter, value, page, size)
            .map((response: Response) => <Results<GrpMaterialLearn>>response.json());
    }

    findFromTypeId(typeid: string) {
        return this.endpointFactory.findFromTypeId(typeid)
            .map((response: Response) => <GrpMaterialLearn[]>response.json());
    }

    getAll(page?: number, pageSize?: number) {
        return Observable.forkJoin(
            this.search("", "", 0, 0),
            this.typeMaterialEndpoint.search("", "", 0, 0).map((response: Response) => <Results<TypeMaterialLearn>>response.json()));
    }

    save(value: GrpMaterialLearn) {
        return this.endpointFactory.save(value)
            .map((response: Response) => <GrpMaterialLearn>response.json());
    }

    delete(id: string) {
        return this.endpointFactory.delete(id);
    }

    get(index: string) {
        return this.endpointFactory.get(index)
            .map((response: Response) => <GrpMaterialLearn>response.json());
    }

    private initializeStatus() {

    }
}