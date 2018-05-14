// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

import { ClassTime } from "./classtime.model";
import { ClassStudent } from "./classstudent.model";

export class Chart {
    // Note: Using only optional constructor properties without backing store disables typescript's type checking for the type
    constructor() {
    }

    chartModels: ChartModel[];
    chartLabels: string[];
}

export class ChartModel {
    // Note: Using only optional constructor properties without backing store disables typescript's type checking for the type
    constructor() {
    }

    data: Array<number>;
    label: string;
}