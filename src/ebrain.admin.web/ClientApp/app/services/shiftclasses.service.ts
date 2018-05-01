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
import { Shiftclass } from '../models/Shiftclass.model';
import 'rxjs/add/operator/map';

import { ShiftclassesEndpoint } from './shiftclasses-endpoint.service';
import { ConfigurationService } from './configuration.service';
import { JwtHelper } from './jwt-helper';
import { Results } from "../models/results.model";

@Injectable()
export class ShiftclassesService {

    constructor(private router: Router, private configurations: ConfigurationService, private endpointFactory: ShiftclassesEndpoint) {
        this.initializeStatus();
    }

    search(filter: string, value: string, page: number, size: number) {
        return this.endpointFactory.search(filter, value, page, size)
            .map((response: Response) => <Results<Shiftclass>>response.json());
    }

    save(value: Shiftclass) {
        return this.endpointFactory.save(value)
            .map((response: Response) => <Shiftclass>response.json());
    }

    delete(id: string) {
        return this.endpointFactory.delete(id);
    }

    get(index: string) {
        return this.endpointFactory.get(index)
            .map((response: Response) => <Shiftclass>response.json());
    }

    private initializeStatus() {

    }
}