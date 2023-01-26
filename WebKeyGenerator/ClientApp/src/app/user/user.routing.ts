import { ModuleWithProviders } from '@angular/core';
import { RouterModule } from '@angular/router';
import { UserAuthGuard } from '../guards/userAuth.guard';
import { KeyComponent } from './key/key.component';
import { RootComponent } from './root/root.component';



export const routing: ModuleWithProviders<any> = RouterModule.forChild([
  {
    path: 'user',
    component: RootComponent, canActivate: [UserAuthGuard],

    children: [
      { path: '', component: KeyComponent },

    ]
  }
]);
