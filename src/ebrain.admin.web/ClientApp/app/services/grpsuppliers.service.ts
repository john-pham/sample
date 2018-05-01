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
import { Grpsupplier } from '../models/grpsupplier.model';
import 'rxjs/add/operator/map';

import { GrpsuppliersEndpoint } from './grpsuppliers-endpoint.service';
import { ConfigurationService } from './configuration.service';
import { JwtHelper } from './jwt-helper';
import { Results } from "../models/results.model";

@Injectable()
export class GrpsuppliersService {

    constructor(private router: Router, private configurations: ConfigurationService, private endpointFactory: GrpsuppliersEndpoint) {
        this.initializeStatus();
    }

    search(filter: string, value: string, page: number, size: number) {
        return this.endpointFactory.search(filter, value, page, size)
            .map((response: Response) => <Results<Grpsupplier>>response.json());
    }

    getAll(option: number) {
        return this.endpointFactory.getAll(option)
            .map((response: Response) => <Grpsupplier[]>response.json());
    }

    save(value: Grpsupplier) {
        return this.endpointFactory.save(value)
            .map((response: Response) => <Grpsupplier>response.json());
    }

    get(index: string) {
        return this.endpointFactory.get(index)
            .map((response: Response) => <Grpsupplier>response.json());
    }

    delete(id: string) {
        return this.endpointFactory.delete(id);
    }

    private initializeStatus() {

    }
}