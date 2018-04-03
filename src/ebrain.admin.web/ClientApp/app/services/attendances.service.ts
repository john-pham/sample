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
import { Attendance } from '../models/attendance.model';
import 'rxjs/add/operator/map';

import { AttendancesEndpoint } from './attendances-endpoint.service';
import { ConfigurationService } from './configuration.service';
import { JwtHelper } from './jwt-helper';

@Injectable()
export class AttendancesService {

    constructor(private router: Router, private configurations: ConfigurationService, private endpointFactory: AttendancesEndpoint) {
        this.initializeStatus();
    }

    search(classId: string, studentId: string, createDate: Date) {
        return this.endpointFactory.search(classId, studentId, createDate)
            .map((response: Response) => <Attendance[]>response.json());
    }

    save(values: Attendance[]) {
        return this.endpointFactory.save(values)
            .map((response: Response) => <Attendance[]>response.json());
    }
    
    private initializeStatus() {

    }
}