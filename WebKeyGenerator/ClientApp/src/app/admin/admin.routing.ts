import { ModuleWithProviders } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AdminAuthGuard } from '../guards/adminAuth.guard';
import { ReqsComponent } from './reqs/reqs.component';

import { RootComponent } from './root/root.component';
import { SpecialitiesComponent } from './specialities/specialities.component';
import { UsersComponent } from './users/users.component';



export const routing: ModuleWithProviders<any> = RouterModule.forChild([
  {
    path: 'admin',
    component: RootComponent, canActivate: [AdminAuthGuard],

    children: [
      { path: '', component: UsersComponent },
      { path: 'home', component: UsersComponent },
      { path: 'users', component: UsersComponent },
      { path: 'spec', component: SpecialitiesComponent },
      { path: 'reqs', component: ReqsComponent },


    ]
  }
]);
