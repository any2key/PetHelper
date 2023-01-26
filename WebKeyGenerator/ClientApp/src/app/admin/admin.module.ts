import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UsersComponent } from './users/users.component';
import { RootComponent } from './root/root.component';
import { SharedMaterialModule } from '../shared-material/shared-material.module';
import { RouterModule } from '@angular/router';
import { routing } from './admin.routing';
import { AddUserComponent } from './users/add-user/add-user.component';




@NgModule({
  declarations: [
    UsersComponent,
    RootComponent,
    AddUserComponent,
  ],
  imports: [
    CommonModule,
    SharedMaterialModule,
    routing
  ], exports: [SharedMaterialModule]
})
export class AdminModule { }
