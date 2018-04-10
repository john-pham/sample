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
import 'rxjs/add/operator/map';

import { UserGroupsEndpoint } from './usergroup-endpoint.service';
import { ConfigurationService } from './configuration.service';
import { JwtHelper } from './jwt-helper';
import { UserGroups } from "../models/usergroups.model";
import { Results } from "../models/results.model";

@Injectable()
export class UserGroupsService {

    constructor(private router: Router, private configurations: ConfigurationService, private endpointFactory: UserGroupsEndpoint) {
        this.initializeStatus();
    }

    getAll() {
        return this.endpointFactory.getall()
            .map((response: Response) => <UserGroups[]>response.json());
    }

    search(filter: string, value: string, page: number, size: number) {
        return this.endpointFactory.search(filter, value, page, size)
            .map((response: Response) => <Results<UserGroups>>response.json());
    }

    get(index: string) {
        return this.endpointFactory.get(index)
            .map((response: Response) => <UserGroups>response.json());
    }

    save(value: UserGroups) {
        return this.endpointFactory.save(value)
            .map((response: Response) => <UserGroups>response.json());
    }


    delete(id: string) {
        return this.endpointFactory.delete(id)
            .map((response: Response) => <Boolean>response.json());
    }

    private initializeStatus() {

    }
}