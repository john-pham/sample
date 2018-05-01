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
import { Supplier } from '../models/supplier.model';
import 'rxjs/add/operator/map';

import { GrpsuppliersEndpoint } from './grpsuppliers-endpoint.service';
import { SuppliersEndpoint } from './suppliers-endpoint.service';
import { ConfigurationService } from './configuration.service';
import { JwtHelper } from './jwt-helper';
import { Grpsupplier } from "../models/grpsupplier.model";

@Injectable()
export class SuppliersService {

    constructor(private router: Router, private configurations: ConfigurationService, private endpointFactory: SuppliersEndpoint, private grpSupplierEndpoint: GrpsuppliersEndpoint) {
        this.initializeStatus();
    }

    search(filter: string, value: string, option: number, page: number, size: number) {
        return this.endpointFactory.search(filter, value, option, page, size)
            .map((response: Response) => <Supplier[]>response.json());
    }

    getAll(page?: number, pageSize?: number, option?: number) {
        return this.search("", "", option, 0, 0);
    }

    getGrpSupplier(option: number) {
        return this.grpSupplierEndpoint.getAll(option).map((response: Response) => <Grpsupplier[]>response.json());
    }

    save(value: Supplier) {
        return this.endpointFactory.save(value)
            .map((response: Response) => <Supplier>response.json());
    }

    get(index: string) {
        return this.endpointFactory.get(index)
            .map((response: Response) => <Supplier>response.json());
    }

    delete(id: string) {
        return this.endpointFactory.delete(id);
    }

    private initializeStatus() {

    }
}