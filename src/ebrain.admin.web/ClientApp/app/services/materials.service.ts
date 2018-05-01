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
import { Material } from '../models/material.model';
import 'rxjs/add/operator/map';

import { MaterialsEndpoint } from './materials-endpoint.service';
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

@Injectable()
export class MaterialsService {

    constructor(private router: Router, private configurations: ConfigurationService, private endpointFactory: MaterialsEndpoint,
        private typesEndpoint: TypeMaterialsEndpoint, private unitsEndpoint: UnitsEndpoint,
        private grpEndpoint: GrpMaterialsEndpoint, private supEndpoint: SuppliersEndpoint) {
        this.initializeStatus();
    }

    search(filter: string, value: string, page: number, size: number) {
        return this.endpointFactory.search(filter, value, page, size)
            .map((response: Response) => <Material[]>response.json());
    }

    findGrpByTypeId(typeId: string) {
        return this.grpEndpoint.findFromTypeId(typeId)
            .map((response: Response) => <GrpMaterial[]>response.json());
    }

    get(index: string) {
        return this.endpointFactory.get(index)
            .map((response: Response) => <Material>response.json());
    }

    getTypeMaterial() {
        return this.typesEndpoint.search("", "", 0, 0).map((response: Response) => <TypeMaterial[]>response.json());
    }

    getUnit() {
        return this.unitsEndpoint.getall().map((response: Response) => <Unit[]>response.json());
    }

    getSupplier() {
        return this.supEndpoint.search("", "", 2, 0, 0).map((response: Response) => <Supplier[]>response.json());
    }

    save(value: Material) {
        return this.endpointFactory.save(value)
            .map((response: Response) => <Material>response.json());
    }

    delete(id: string) {
        return this.endpointFactory.delete(id);
    }

    private initializeStatus() {

    }
}