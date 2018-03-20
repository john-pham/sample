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
import { Levelclass } from '../models/levelclass.model';
import 'rxjs/add/operator/map';

import { LevelclassesEndpoint } from './levelclasses-endpoint.service';
import { ConfigurationService } from './configuration.service';
import { JwtHelper } from './jwt-helper';

@Injectable()
export class LevelclassesService {

    constructor(private router: Router, private configurations: ConfigurationService, private endpointFactory: LevelclassesEndpoint) {
        this.initializeStatus();
    }

    search(filter: string, value: string) {
        return this.endpointFactory.search(filter, value)
            .map((response: Response) => <Levelclass[]>response.json());
    }

    save(value: Levelclass) {
        return this.endpointFactory.save(value)
            .map((response: Response) => <Levelclass>response.json());
    }

    delete(id: string) {
        return this.endpointFactory.delete(id);
    }

    private initializeStatus() {

    }
}