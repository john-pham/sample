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
import { GrpMaterial } from '../models/grpMaterial.model';


@Injectable()
export class IOStudentListEndpoint extends EndpointFactory {

    private readonly _serviceUrl: string = "/api/iostudent";
    private readonly _serviceUrlPayment: string = "/api/iostock";
    private get serviceUrl() { return this.configurations.baseUrl + this._serviceUrl; }
    private get serviceUrlPayment() { return this.configurations.baseUrl + this._serviceUrlPayment; }

    constructor(http: Http, configurations: ConfigurationService, injector: Injector) {
        super(http, configurations, injector);
    }

    search(filter: string, value: string, fromDate: Date, toDate: Date, page: number, size: number): Observable<Response> {

        let url = this.getUrl('search?filter=' + filter + '&value=' + value + '&fromDate=' + fromDate + '&toDate=' + toDate
            + '&page=' + page + '&size=' + size + '&hash_id=' + Math.random());
        return this.http.get(url, this.getAuthHeader())
            .map((response: Response) => {
                return response;
            })
            .catch(error => {
                return this.handleError(error, () => this.search(filter, value, fromDate, toDate, page, size));
            });
    }

    reportsearch(filter: string, value: string, fromDate: Date, toDate: Date, page: number, size: number): Observable<Response> {

        let url = this.getUrl('reportsearch?filter=' + filter + '&value=' + value + '&fromDate=' + fromDate + '&toDate=' + toDate
            + '&page=' + page + '&size=' + size + '&hash_id=' + Math.random());
        return this.http.get(url, this.getAuthHeader())
            .map((response: Response) => {
                return response;
            })
            .catch(error => {
                return this.handleError(error, () => this.search(filter, value, fromDate, toDate, page, size));
            });
    }

    getiobyiotypeid(filter: string, value: string, fromDate: Date, toDate: Date, page: number, size: number): Observable<Response> {

        let url = this.getUrlIO('getiobyiotypeid?filter=' + filter + '&value=' + value + '&fromDate=' + fromDate + '&toDate=' + toDate
            + '&page=' + page + '&size=' + size + '&hash_id=' + Math.random());
        return this.http.get(url, this.getAuthHeader())
            .map((response: Response) => {
                return response;
            })
            .catch(error => {
                return this.handleError(error, () => this.getiobyiotypeid(filter, value, fromDate, toDate, page, size));
            });
    }

    reportiobyiotypeid(filter: string, value: string, fromDate: Date, toDate: Date, page: number, size: number): Observable<Response> {

        let url = this.getUrlIO('reportiobyiotypeid?filter=' + filter + '&value=' + value + '&fromDate=' + fromDate + '&toDate=' + toDate
            + '&page=' + page + '&size=' + size + '&hash_id=' + Math.random());
        return this.http.get(url, this.getAuthHeader())
            .map((response: Response) => {
                return response;
            })
            .catch(error => {
                return this.handleError(error, () => this.reportiobyiotypeid(filter, value, fromDate, toDate, page, size));
            });
    }

    getiodetailbyiotypeid(filter: string, value: string, fromDate: Date, toDate: Date, page: number, size: number): Observable<Response> {

        let url = this.getUrlIO('getiodetailbyiotypeid?filter=' + filter + '&value=' + value + '&fromDate=' + fromDate + '&toDate=' + toDate
            + '&page=' + page + '&size=' + size + '&hash_id=' + Math.random());
        return this.http.get(url, this.getAuthHeader())
            .map((response: Response) => {
                return response;
            })
            .catch(error => {
                return this.handleError(error, () => this.getiodetailbyiotypeid(filter, value, fromDate, toDate, page, size));
            });
    }

    reportiodetailbyiotypeid(filter: string, value: string, fromDate: Date, toDate: Date, page: number, size: number): Observable<Response> {

        let url = this.getUrlIO('reportiodetailbyiotypeid?filter=' + filter + '&value=' + value + '&fromDate=' + fromDate + '&toDate=' + toDate
            + '&page=' + page + '&size=' + size + '&hash_id=' + Math.random());
        return this.http.get(url, this.getAuthHeader())
            .map((response: Response) => {
                return response;
            })
            .catch(error => {
                return this.handleError(error, () => this.reportiodetailbyiotypeid(filter, value, fromDate, toDate, page, size));
            });
    }

    getWarehouseCard(filter: string, value: string, fromDate: Date, toDate: Date, page: number, size: number): Observable<Response> {

        let url = this.getUrlIO('getwarehousecard?filter=' + filter + '&value=' + value + '&fromDate=' + fromDate + '&toDate=' + toDate
            + '&page=' + page + '&size=' + size + '&hash_id=' + Math.random());
        return this.http.get(url, this.getAuthHeader())
            .map((response: Response) => {
                return response;
            })
            .catch(error => {
                return this.handleError(error, () => this.getWarehouseCard(filter, value, fromDate, toDate, page, size));
            });
    }

    getIONew(): Observable<Response> {

        let url = this.getUrlIO('getionew?hash_id=' + Math.random());
        return this.http.get(url, this.getAuthHeader())
            .map((response: Response) => {
                return response;
            })
            .catch(error => {
                return this.handleError(error, () => this.getIONew());
            });
    }

    getIOAll(): Observable<Response> {

        let url = this.getUrlIO('getioall?hash_id=' + Math.random());
        return this.http.get(url, this.getAuthHeader())
            .map((response: Response) => {
                return response;
            })
            .catch(error => {
                return this.handleError(error, () => this.getIOAll());
            });
    }

    getiopayment(filter: string, value: string, isGetAll: number, isWaitingClass: number, ioid: string, isInput: boolean,
        fromDate: Date, toDate: Date, page: number, size: number): Observable<Response> {

        let url = "";
        if (isInput == false) {
            url = this.getUrlIO('getiopayment?filter=' + filter + '&value=' + value + '&getAll=' + isGetAll
                + '&isWaitingClass=' + isWaitingClass
                + '&ioid=' + ioid + '&fromDate=' + fromDate + '&toDate=' + toDate
                + '&page=' + page + '&size=' + size + '&hash_id=' + Math.random());
        }
        else {
            url = this.getUrlIO('getiopaymentvoucher?filter=' + filter + '&value=' + value + '&getAll=' + isGetAll + '&ioid=' + ioid + '&fromDate=' + fromDate + '&toDate=' + toDate
                + '&page=' + page + '&size=' + size + '&hash_id=' + Math.random());
        }

        return this.http.get(url, this.getAuthHeader())
            .map((response: Response) => {
                return response;
            })
            .catch(error => {
                return this.handleError(error, () => this.getiopayment(filter, value, isGetAll, isWaitingClass, ioid, isInput, fromDate, toDate, page, size));
            });
    }

    getiopaymentDetail(filter: string, value: string, isGetAll: number, isWaitingClass: number, ioid: string, isInput: boolean,
        isLearning: number, fromDate: Date, toDate: Date, page: number, size: number): Observable<Response> {

        let url = "";
        url = this.getUrlIO('getiopaymentdetail?filter=' + filter + '&value=' + value + '&getAll=' + isGetAll
            + '&isWaitingClass=' + isWaitingClass
            + '&isLearning=' + isLearning
            + '&ioid=' + ioid + '&fromDate=' + fromDate + '&toDate=' + toDate
            + '&page=' + page + '&size=' + size + '&hash_id=' + Math.random());

        return this.http.get(url, this.getAuthHeader())
            .map((response: Response) => {
                return response;
            })
            .catch(error => {
                return this.handleError(error, () => this.getiopaymentDetail(filter, value, isGetAll, isWaitingClass, ioid, isInput, isLearning, fromDate, toDate, page, size));
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
    private getUrlIO(prefix: string): string {
        return this.serviceUrlPayment + '/' + prefix;
    }
}