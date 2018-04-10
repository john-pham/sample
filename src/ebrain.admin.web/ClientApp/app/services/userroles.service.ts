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

import { ConfigurationService } from './configuration.service';
import { JwtHelper } from './jwt-helper';

import { Results } from "../models/results.model";
import { UserRolesEndpoint } from "./userroles-endpoint.service";
import { UserRoles } from "../models/userroles.model";

@Injectable()
export class UserRolesService {

    constructor(private router: Router, private configurations: ConfigurationService, private endpointFactory: UserRolesEndpoint) {
        this.initializeStatus();
    }

    getAll() {
        return this.endpointFactory.getall()
            .map((response: Response) => <UserRoles[]>response.json());
    }

    search(filter: string, value: string, page: number, size: number) {
        return this.endpointFactory.search(filter, value, page, size)
            .map((response: Response) => <Results<UserRoles>>response.json());
    }

    get(index: string) {
        return this.endpointFactory.get(index)
            .map((response: Response) => <UserRoles[]>response.json());
    }

    save(values: UserRoles[]) {
        return this.endpointFactory.save(values)
            .map((response: Response) => <Boolean>response.json());
    }


    delete(id: string) {
        return this.endpointFactory.delete(id)
            .map((response: Response) => <Boolean>response.json());
    }

    private initializeStatus() {

    }
}