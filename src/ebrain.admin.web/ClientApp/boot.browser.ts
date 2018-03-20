// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

import 'core-js/client/shim';
import 'web-animations-js';
import 'zone.js';
import 'bootstrap';
import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { AppModule } from './app/app.module';

if (module.hot) {
    module.hot.accept();
    module.hot.dispose(() => {
        /*
        // Before restarting the app, we create a new root element and dispose the old one
        const oldRootElem = document.querySelector('superbrain-app');
        const newRootElem = document.createElement('superbrain-app');
        oldRootElem!.parentNode!.insertBefore(newRootElem, oldRootElem);
        */
        modulePromise.then(appModule => appModule.destroy());
    });
} else {
    enableProdMode();
}

// Note: @ng-tools/webpack looks for the following expression when performing production
// builds. Don't change how this line looks, otherwise you may break tree-shaking.
const modulePromise = platformBrowserDynamic().bootstrapModule(AppModule);