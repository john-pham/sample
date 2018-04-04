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
import { GrpDocument } from '../models/grpdocument.model';
import { Results } from '../models/results.model';
import 'rxjs/add/operator/map';

import { GrpDocumentsEndpoint } from './grpdocuments-endpoint.service';
import { ConfigurationService } from './configuration.service';
import { JwtHelper } from './jwt-helper';

@Injectable()
export class GrpDocumentsService {

    constructor(private router: Router, private configurations: ConfigurationService, private endpointFactory: GrpDocumentsEndpoint) {
        this.initializeStatus();
    }
    
    getAll() {
        return this.endpointFactory.getall()
            .map((response: Response) => <GrpDocument[]>response.json());
    }

    search(filter: string, value: string, page: number, size: number) {
        return this.endpointFactory.search(filter, value, page, size)
            .map((response: Response) => <Results<GrpDocument>>response.json());
    }

    get(index: string) {
        return this.endpointFactory.get(index)
            .map((response: Response) => <GrpDocument>response.json());
    }

    save(value: GrpDocument) {
        return this.endpointFactory.save(value)
            .map((response: Response) => <GrpDocument>response.json());
    }
    
    delete(id: string) {
        return this.endpointFactory.delete(id)
            .map((response: Response) => <Boolean>response.json());
    }
    

    private initializeStatus() {
        
    }
}