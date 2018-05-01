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
import { Consultant } from '../models/consultant.model';
import 'rxjs/add/operator/map';

import { ConsultantsEndpoint } from './consultants-endpoint.service';
import { ConfigurationService } from './configuration.service';
import { JwtHelper } from './jwt-helper';
import { Results } from "../models/results.model";

@Injectable()
export class ConsultantsService {

    constructor(private router: Router, private configurations: ConfigurationService, private endpointFactory: ConsultantsEndpoint) {
        this.initializeStatus();
    }

    search(filter: string, value: string, page: number, size: number) {
        return this.endpointFactory.search(filter, value, page, size)
            .map((response: Response) => <Results<Consultant>>response.json());
    }

    save(value: Consultant) {
        return this.endpointFactory.save(value)
            .map((response: Response) => <Consultant>response.json());
    }

    delete(id: string) {
        return this.endpointFactory.delete(id);
    }

    get(index: string) {
        return this.endpointFactory.get(index)
            .map((response: Response) => <Consultant>response.json());
    }

    private initializeStatus() {

    }
}