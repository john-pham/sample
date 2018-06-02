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
import { Unit } from '../models/unit.model';
import 'rxjs/add/operator/map';

import { UnitsEndpoint } from './units-endpoint.service';
import { ConfigurationService } from './configuration.service';
import { JwtHelper } from './jwt-helper';
import { Results } from "../models/results.model";
@Injectable()
export class UnitsService {

    constructor(private router: Router, private configurations: ConfigurationService, private endpointFactory: UnitsEndpoint) {
        this.initializeStatus();
    }

    search(filter: string, value: string, page: number, size: number) {
        return this.endpointFactory.search(filter, value, page, size)
            .map((response: Response) => <Results<Unit>>response.json());
    }

    csv(filter: string, value: string, page: number, size: number) {
        return this.endpointFactory.search(filter, value, page, size)
            .map((response: Response) => <Results<Unit>>response.json());
    }

    get(index: string) {
        return this.endpointFactory.get(index)
            .map((response: Response) => <Unit>response.json());
    }

    save(value: Unit) {
        return this.endpointFactory.save(value)
            .map((response: Response) => <Unit>response.json());
    }

    delete(id: string) {
        return this.endpointFactory.delete(id);
    }

    outputCSV(filter: string, value: string, page: number, size: number) {
        return this.endpointFactory.output('csv', filter, value, page, size)
            .map((response: Response) => <string>response.json());
    }

    outputPdf(filter: string, value: string, page: number, size: number) {
        return this.endpointFactory.output('pdf', filter, value, page, size)
            .map((response: Response) => <string>response.json());
    }

    private initializeStatus() {

    }
}