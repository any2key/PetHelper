import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { ForbiddenComponent } from './forbidden/forbidden.component';
import { HomeComponent } from './home/home.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { SpecDoctorsComponent } from './shared/spec-doctors/spec-doctors.component';
import { SpecListComponent } from './shared/spec-list/spec-list.component';


const appRoutes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'home', component: HomeComponent },
  { path: 'forbidden', component: ForbiddenComponent },
  { path: 'list', component: SpecListComponent },
  { path: 'docs/:specId', component: SpecDoctorsComponent },
  { path: 'docs', component: SpecDoctorsComponent },
  { path: '**', component: NotFoundComponent },
];

export const routing: ModuleWithProviders<any> = RouterModule.forRoot(appRoutes);
