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
import { Student } from '../models/student.model';


@Injectable()
export class StudentsEndpoint extends EndpointFactory {

    private readonly _serviceUrl: string = "/api/students";
    private get serviceUrl() { return this.configurations.baseUrl + this._serviceUrl; }


    constructor(http: Http, configurations: ConfigurationService, injector: Injector) {
        super(http, configurations, injector);
    }

    search(filter: string, value: string): Observable<Response> {

        let url = this.getUrl('search?filter=' + filter + '&value=' + value);

        return this.http.get(url, this.getAuthHeader())
            .map((response: Response) => {
                return response;
            })
            .catch(error => {
                return this.handleError(error, () => this.search(filter, value));
            });
    }

    getAll(page?: number, pageSize?: number): Observable<Response> {

        let url = this.getUrl('getall?page=' + page + '&pageSize=' + pageSize);

        return this.http.get(url, this.getAuthHeader())
            .map((response: Response) => {
                return response;
            })
            .catch(error => {
                return this.handleError(error, () => this.getAll(page, pageSize));
            });
    }

    save(value: any): Observable<Response> {
        let url = this.getUrl('update');
        let header = this.getAuthHeader(true);
        let params = JSON.stringify(value);

        return this.http.post(url, params, header)
            .map((response: Response) => {
                return response;
            })
            .catch(error => {
                return this.handleError(error, () => this.save(value));
            });
    }

    get(index: string): Observable<Response> {

        let url = this.getUrl('get?index=' + index + '&hash_id=' + Math.random());

        return this.http.get(url, this.getAuthHeader())
            .map((response: Response) => {
                return response;
            })
            .catch(error => {
                return this.handleError(error, () => this.get(index));
            });
    }

    delete(id: string): Observable<Response> {
        let url = this.getUrl('remove');
        let header = this.getAuthHeader(true);
        let params = JSON.stringify(id);

        return this.http.post(url, params, header)
            .map((response: Response) => {
                return response;
            })
            .catch(error => {
                return this.handleError(error, () => this.delete(id));
            });
    }

    getBirthdayStudent(fromDate: Date, toDate: Date, page: number, size: number): Observable<Response> {
        let url = this.getUrl('getbirthdaytudents?fromDate=' + fromDate + '&toDate=' + toDate
            + '&page=' + page + '&size=' + size + '&hash_id=' + Math.random());
        return this.http.get(url, this.getAuthHeader())
            .map((response: Response) => {
                return response;
            })
            .catch(error => {
                return this.handleError(error, () => this.getBirthdayStudent(fromDate, toDate, page, size));
            });
    }

    getStudentEndClass(classId: string, toDate: Date, page: number, size: number): Observable<Response> {
        let url = this.getUrl('getstudentendclass?classId=' + classId + '&toDate=' + toDate
            + '&page=' + page + '&size=' + size + '&hash_id=' + Math.random());
        return this.http.get(url, this.getAuthHeader())
            .map((response: Response) => {
                return response;
            })
            .catch(error => {
                return this.handleError(error, () => this.getStudentEndClass(classId, toDate, page, size));
            });
    }

    getNewStudent(): Observable<Response> {
        let url = this.getUrl('getnewstudent?hash_id=' + Math.random());
        return this.http.get(url, this.getAuthHeader())
            .map((response: Response) => {
                return response;
            })
            .catch(error => {
                return this.handleError(error, () => this.getNewStudent());
            });
    }

    getAllStudent(): Observable<Response> {
        let url = this.getUrl('getalltudent?hash_id=' + Math.random());
        return this.http.get(url, this.getAuthHeader())
            .map((response: Response) => {
                return response;
            })
            .catch(error => {
                return this.handleError(error, () => this.getNewStudent());
            });
    }

    getstudentbycreatedate(filter: string, value: string, fromDate: Date, toDate: Date, page: number, size: number): Observable<Response> {
        let url = this.getUrl('getstudentbycreatedate?filter=' + filter + '&value=' + value + '&fromDate=' + fromDate + '&toDate=' + toDate
            + '&page=' + page + '&size=' + size + '&hash_id=' + Math.random());
        return this.http.get(url, this.getAuthHeader())
            .map((response: Response) => {
                return response;
            })
            .catch(error => {
                return this.handleError(error, () => this.getBirthdayStudent(fromDate, toDate, page, size));
            });
    }

    getStudentCourse(filterValue: string, page: number, size: number): Observable<Response> {
        let url = this.getUrl('getstudentcourse?filterValue=' + filterValue + '&studentId=' + '' + '&page=' + page + '&size=' + size + '&hash_id=' + Math.random());
        return this.http.get(url, this.getAuthHeader())
            .map((response: Response) => {
                return response;
            })
            .catch(error => {
                return this.handleError(error, () => this.getStudentCourse(filterValue, page, size));
            });
    }

    getTeacherCourse(filterValue: string, page: number, size: number): Observable<Response> {
        let url = this.getUrl('getteachercourse?filterValue=' + filterValue + '&page=' + page + '&size=' + size + '&hash_id=' + Math.random());
        return this.http.get(url, this.getAuthHeader())
            .map((response: Response) => {
                return response;
            })
            .catch(error => {
                return this.handleError(error, () => this.getStudentCourse(filterValue, page, size));
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