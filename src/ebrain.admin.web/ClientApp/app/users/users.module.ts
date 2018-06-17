import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../share/share.module';
import { UsesRouteModule } from './users-routing.module';

import { FeatureGroupsService } from './services/featuregroup.service';
import { FeatureGroupsEndpoint } from './services/featuregroup-endpoint.service';
import { SupportService } from './services/support.service';
import { SupportEndpoint } from './services/support-endpoint.service';
import { UserGroupsService } from './services/usergroup.service';
import { UserGroupsEndpoint } from './services/usergroup-endpoint.service';
import { UserRolesService } from './services/userroles.service';
import { UserRolesEndpoint } from './services/userroles-endpoint.service';

@NgModule({
  declarations: [],
  imports: [
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    UsesRouteModule,  
  ],
  providers: [
    FeatureGroupsService,
    FeatureGroupsEndpoint,
    SupportService,
    SupportEndpoint,
    UserGroupsService,
    UserGroupsEndpoint,
    UserRolesService,
    UserRolesEndpoint
  ]
})
export class UsersModule {
}