import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { KeyComponent } from './key/key.component';
import { RootComponent } from './root/root.component';
import { SharedMaterialModule } from '../shared-material/shared-material.module';
import { routing } from './user.routing';



@NgModule({
  declarations: [
    KeyComponent,
    RootComponent,
  ],
  imports: [
    CommonModule,
    SharedMaterialModule,
    routing
  ]
})
export class UserModule { }
