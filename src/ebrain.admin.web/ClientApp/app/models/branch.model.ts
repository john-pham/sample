// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================
import { File } from './file.model';

export class Branch {
    public id: string;
    public parentBranchId: string;
    public name: string;
    public code: string;
    public email: string;
    public address: string;
    public phoneNumber: string;
    public fax: string;
    public logo: File;
    public isExist: boolean;

    public userName: string = "smsbrand_cnkhoinguon";
    public password: string = "12345678a@";
    public cPCode: string = "SUPERBRAINCNKN";
    public requestID: string = "1";
    public serviceId: string = "SUPERBRAIN";
    public commandCode: string = "bulksms";
    public contentType: string = "0";
}