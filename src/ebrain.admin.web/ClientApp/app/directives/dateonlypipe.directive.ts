// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

import { Directive, Renderer, ElementRef, OnInit } from '@angular/core';
import { DatePipe } from '@angular/common';

export class DateOnlyPipe extends DatePipe {
    public transform(value): any {
        return super.transform(value, 'MM/dd/y');
    }
}