import { ModuleWithProviders } from '@angular/core';
import { RouterModule } from '@angular/router';
import { DoctorAuthGuard } from '../guards/doctorAuth.guard';

import { RootComponent } from './root/root.component';
import { SchedulleComponent } from './schedulle/schedulle.component';



export const routing: ModuleWithProviders<any> = RouterModule.forChild([
  {
    path: 'doctor',
    component: RootComponent, canActivate: [DoctorAuthGuard],

    children: [
      { path: '', component: SchedulleComponent },
      { path: 'home', component: SchedulleComponent },
      { path: 'schedulle', component: SchedulleComponent },
    ]
  }
]);
