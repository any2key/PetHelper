import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RootComponent } from './root/root.component';
import { SchedulleComponent } from './schedulle/schedulle.component';



@NgModule({
  declarations: [
    RootComponent,
    SchedulleComponent
  ],
  imports: [
    CommonModule
  ]
})
export class DoctorModule { }
