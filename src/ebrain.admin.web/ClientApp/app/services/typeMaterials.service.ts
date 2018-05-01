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
import { TypeMaterial } from '../models/typeMaterial.model';
import 'rxjs/add/operator/map';

import { TypeMaterialsEndpoint } from './typeMaterials-endpoint.service';
import { ConfigurationService } from './configuration.service';
import { JwtHelper } from './jwt-helper';
import { Results } from "../models/results.model";

@Injectable()
export class TypeMaterialsService {

    constructor(private router: Router, private configurations: ConfigurationService, private endpointFactory: TypeMaterialsEndpoint) {
        this.initializeStatus();
    }

    search(filter: string, value: string, page: number, size: number) {
        return this.endpointFactory.search(filter, value, page, size)
            .map((response: Response) => <Results<TypeMaterial>>response.json());
    }

    get(index: string) {
        return this.endpointFactory.get(index)
            .map((response: Response) => <TypeMaterial>response.json());
    }

    save(value: TypeMaterial) {
        return this.endpointFactory.save(value)
            .map((response: Response) => <TypeMaterial>response.json());
    }

    delete(id: string) {
        return this.endpointFactory.delete(id)
            .map((response: Response) => <Boolean>response.json());
    }

    private initializeStatus() {

    }
}