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
import { Stock } from '../models/stock.model';
import 'rxjs/add/operator/map';

import { StocksEndpoint } from './stocks-endpoint.service';
import { ConfigurationService } from './configuration.service';
import { JwtHelper } from './jwt-helper';

@Injectable()
export class StocksService {

    constructor(private router: Router, private configurations: ConfigurationService, private endpointFactory: StocksEndpoint) {
        this.initializeStatus();
    }

    search(filter: string, value: string) {
        return this.endpointFactory.search(filter, value)
            .map((response: Response) => <Stock[]>response.json());
    }

    save(value: Stock) {
        return this.endpointFactory.save(value)
            .map((response: Response) => <Stock>response.json());
    }

    delete(id: string) {
        return this.endpointFactory.delete(id);
    }

    private initializeStatus() {

    }
}