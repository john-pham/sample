import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { AuthGuard } from '../services/auth-guard.service';

import { AccessRightsComponent } from './access-rights/access-rights';
import { FeatureGroupsComponent } from './featuregroups/featuregroups.component';
import { SupportComponent } from './supports/supports.component';
import { UserGroupsComponent } from './usergroups/usergroups.component';
import { UserRolesComponent } from './userroles/userroles.component';
import { UsersComponent } from './users.component';

const routes: Routes = [
  {
    path: 'users',
    component: UsersComponent,
    children: [
      { path: "accessrights", component: AccessRightsComponent, canActivate: [AuthGuard], data: { title: "Access Rights" } },
      { path: "userroles", component: UserRolesComponent, canActivate: [AuthGuard], data: { title: "User Roles" } },
      { path: "supports", component: SupportComponent, canActivate: [AuthGuard], data: { title: "Supports" } },
      { path: "featuregroups", component: FeatureGroupsComponent, canActivate: [AuthGuard], data: { title: "Feature Groups" } },
      { path: "usergroups", component: UserGroupsComponent, canActivate: [AuthGuard], data: { title: "User Groups" } },
    ]
  }
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  declarations: [
    UsersComponent,
    AccessRightsComponent,
    FeatureGroupsComponent,
    SupportComponent,
    UserGroupsComponent,
    UserRolesComponent
  ]
})
export class UsesRouteModule { }