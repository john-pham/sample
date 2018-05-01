// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

import { Injectable, Injector } from '@angular/core';
import { Http, Headers, RequestOptions, Response, URLSearchParams } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { Subject } from 'rxjs/Subject';
import 'rxjs/add/observable/throw';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/mergeMap';
import 'rxjs/add/operator/catch';

import { EndpointFactory } from './endpoint-factory.service';
import { ConfigurationService } from './configuration.service';

@Injectable()
export class ProfitsEndpoint extends EndpointFactory {

    private readonly _serviceUrl: string = "/api/profit";

    private get serviceUrl() { return this.configurations.baseUrl + this._serviceUrl; }

    constructor(http: Http, configurations: ConfigurationService, injector: Injector) {
        super(http, configurations, injector);
    }

    getProfits(filter: string, value: string, fromDate: Date, toDate: Date, page: number, size: number): Observable<Response> {

        let url = this.getUrl('getprofits?filter=' + filter + '&value=' + value + '&fromDate=' + fromDate + '&toDate=' + toDate
            + '&page=' + page + '&size=' + size + '&hash_id=' + Math.random());

        return this.http.get(url, this.getAuthHeader())
            .map((response: Response) => {
                return response;
            })
            .catch(error => {
                return this.handleError(error, () => this.getProfits(filter, value, fromDate, toDate, page, size));
            });
    }

    updateProfits(filter: string, value: string, fromDate: Date, toDate: Date): Observable<Response> {

        let url = this.getUrl('updatedprofits?filter=' + filter + '&value=' + value + '&fromDate=' + fromDate + '&toDate=' + toDate + '&hash_id=' + Math.random());

        return this.http.get(url, this.getAuthHeader())
            .map((response: Response) => {
                return response;
            })
            .catch(error => {
                return this.handleError(error, () => this.updateProfits(filter, value, fromDate, toDate));
            });
    }

    protected handleError(error, continuation: () => Observable<any>) {

        if (error.status == 401) {

        }

        if (error.url && error.url.toLowerCase().includes(this.serviceUrl.toLowerCase())) {
            return Observable.throw('session expired');
        }
        else {
            return Observable.throw(error || 'server error');
        }
    }

    private getUrl(prefix: string): string {
        return this.serviceUrl + '/' + prefix;
    }
}