import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router'

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { AuthInterceptorProvider } from './Interceptors/auth.Interceptor';
import { NotFoundComponent } from './not-found/not-found.component';
import { ForbiddenComponent } from './forbidden/forbidden.component';
import { ErrorInterceptorProvider } from './Interceptors/error.Interceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';



import { ConfirmComponent } from './confirm/confirm.component';
import { routing } from './app.routing';
import { AccountModule } from './account/account.module';
import { SharedMaterialModule } from './shared-material/shared-material.module';
import { AdminModule } from './admin/admin.module';
import { UserModule } from './user/user.module';
import { SuccessModalComponent } from './success-modal/success-modal.component';
import { LoginComponent } from './shared/login/login.component';
import { RegisterComponent } from './shared/register/register.component';
import { EntryComponent } from './shared/entry/entry.component';
import { DoctorModule } from './doctor/doctor.module';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { DateAdapter, MatNativeDateModule } from '@angular/material/core';
import { CustomDateAdapter } from './custom-date-adapter.ts';
import { SpecListComponent } from './shared/spec-list/spec-list.component';
import { SpecDoctorsComponent } from './shared/spec-doctors/spec-doctors.component';



@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NotFoundComponent,
    ForbiddenComponent,
    ConfirmComponent,
    SuccessModalComponent,
    LoginComponent,
    RegisterComponent,
    EntryComponent,
    SpecListComponent,
    SpecDoctorsComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    routing,
    ReactiveFormsModule,
    AccountModule,
    SharedMaterialModule,
    BrowserAnimationsModule,
    AdminModule,
    UserModule,
    DoctorModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  providers: [
    AuthInterceptorProvider,
    ErrorInterceptorProvider,
    { provide: DateAdapter, useClass: CustomDateAdapter }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
