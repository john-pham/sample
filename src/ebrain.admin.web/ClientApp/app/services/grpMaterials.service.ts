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
import { Results } from "../models/results.model";

@Injectable()
export class GrpMaterialsService {

    constructor(private router: Router, private configurations: ConfigurationService, private endpointFactory: GrpMaterialsEndpoint,
        private typeMaterialEndpoint: TypeMaterialsEndpoint) {
        this.initializeStatus();
    }

    search(filter: string, value: string, page: number, size: number) {
        return this.endpointFactory.search(filter, value, page, size)
            .map((response: Response) => <Results<GrpMaterial>>response.json());
    }

    getAll(filter: string, value: string, page: number, size: number) {
        return Observable.forkJoin(
            this.search(filter, value, page, size),
            this.typeMaterialEndpoint.search("", "", 0, 0).map((response: Response) => <TypeMaterial[]>response.json()));
    }

    save(value: GrpMaterial) {
        return this.endpointFactory.save(value)
            .map((response: Response) => <GrpMaterial>response.json());
    }

    delete(id: string) {
        return this.endpointFactory.delete(id);
    }

    get(index: string) {
        return this.endpointFactory.get(index)
            .map((response: Response) => <GrpMaterial>response.json());
    }

    private initializeStatus() {

    }
}