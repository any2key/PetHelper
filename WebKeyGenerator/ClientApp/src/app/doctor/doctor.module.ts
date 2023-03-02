import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RootComponent } from './root/root.component';
import { SchedulleComponent } from './schedulle/schedulle.component';
import { SharedMaterialModule } from '../shared-material/shared-material.module';
import { routing } from './doctor.routing';



@NgModule({
  declarations: [
    RootComponent,
    SchedulleComponent
  ],
  imports: [
    CommonModule,
    SharedMaterialModule,
    routing
  ], exports: [SharedMaterialModule]
})
export class DoctorModule { }
