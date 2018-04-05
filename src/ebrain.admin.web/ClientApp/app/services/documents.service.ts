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
import { Document } from '../models/document.model';
import { Results } from '../models/results.model';
import 'rxjs/add/operator/map';

import { DocumentsEndpoint } from './documents-endpoint.service';
import { ConfigurationService } from './configuration.service';
import { JwtHelper } from './jwt-helper';
import { GrpDocument } from "../models/grpdocument.model";

@Injectable()
export class DocumentsService {

    constructor(private router: Router, private configurations: ConfigurationService, private endpointFactory: DocumentsEndpoint) {
        this.initializeStatus();
    }

    search(filter: string, value: string, grpId: string, page: number, size: number) {
        return this.endpointFactory.search(filter, value, grpId, page, size)
            .map((response: Response) => <Results<Document>>response.json());
    }

    get(index: string) {
        return this.endpointFactory.get(index)
            .map((response: Response) => <Document>response.json());
    }

    save(value: GrpDocument) {
        return this.endpointFactory.save(value)
            .map((response: Response) => <Document>response.json());
    }

    delete(id: string) {
        return this.endpointFactory.delete(id)
            .map((response: Response) => <Boolean>response.json());
    }


    private initializeStatus() {

    }
}