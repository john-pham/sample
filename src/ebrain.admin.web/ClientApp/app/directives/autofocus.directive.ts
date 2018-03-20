// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

import { Directive, Renderer, ElementRef, OnInit } from '@angular/core';


@Directive({
    selector: '[autofocus]'
})
export class AutofocusDirective implements OnInit {
    constructor(public renderer: Renderer, public elementRef: ElementRef) { }

    ngOnInit() {
        setTimeout(() => this.renderer.invokeElementMethod(this.elementRef.nativeElement, 'focus', []), 500);
    }
}