// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

import { ClassTime } from "./classtime.model";
import { ClassStudent } from "./classstudent.model";
import { FeatureGroups } from "./featuregroups.model";
import { UserGroups } from "./usergroups.model";

export class AccessRight {
    // Note: Using only optional constructor properties without backing store disables typescript's type checking for the type
    constructor(id?: string) {
        this.id = id;
    }

    public id: string;
    public name: string;
    public code: string;
    public note: string;

    public features: FeatureGroups[];
    public usergroups: UserGroups[];
    

}