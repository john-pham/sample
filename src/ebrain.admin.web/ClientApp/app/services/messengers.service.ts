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
import { Branch } from '../models/branch.model';
import { Results } from '../models/results.model';
import 'rxjs/add/operator/map';

import { MessengerEndpoint } from './messengers-endpoint.service';
import { ConfigurationService } from './configuration.service';
import { JwtHelper } from './jwt-helper';
import { Messenger } from "../models/messenger.model";

@Injectable()
export class MessengerService {

    constructor(private router: Router, private configurations: ConfigurationService, private endpointFactory: MessengerEndpoint) {
        this.initializeStatus();
    }
    
    search(filter: string, value: string, page: number, size: number) {
        return this.endpointFactory.search(filter, value, page, size)
            .map((response: Response) => <Results<Messenger>>response.json());
    }

    getnewmessenger() {
        return this.endpointFactory.getnewmessenger()
            .map((response: Response) => <Results<Messenger>>response.json());
    }

    get(index: string) {
        return this.endpointFactory.get(index)
            .map((response: Response) => <Messenger>response.json());
    }

    save(value: Messenger) {
        return this.endpointFactory.save(value)
            .map((response: Response) => <Messenger>response.json());
    }
    

    delete(id: string) {
        return this.endpointFactory.delete(id)
            .map((response: Response) => <Boolean>response.json());
    }

    private initializeStatus() {
        
    }
}