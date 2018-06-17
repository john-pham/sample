import { CommonModule } from '@angular/common';
import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccessRightsEndpoint } from './services/access-rights.endpoint.service';
import { AccessRightsService } from './services/access-rights.service';

let ShareComponents = [
   //Todo
];

let ShareDirectives = [
    //Todo
];

let SharePrimeNgControls = [
    //Todo
];

@NgModule({
  declarations: [
    ShareDirectives,
    ShareComponents
  ],
  imports: [
    CommonModule,
    FormsModule,
    SharePrimeNgControls
  ],
  exports: [
    SharePrimeNgControls,
    ShareDirectives,
    ShareComponents,
    AccessRightsEndpoint,
    AccessRightsService
  ],
  schemas: [ NO_ERRORS_SCHEMA ],
})
export class SharedModule { }