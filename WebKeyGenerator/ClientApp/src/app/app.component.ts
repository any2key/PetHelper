import { Component, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSidenav } from '@angular/material/sidenav';
import { Router } from '@angular/router';
import { DataResponse } from './model';
import { ApiService } from './services/api.service';
import { TokenService } from './services/token.service';
import { UiService } from './services/ui.service';
import { UserService } from './services/user.service';
import { EntryComponent } from './shared/entry/entry.component';
import { LoginComponent } from './shared/login/login.component';
import { RegisterComponent } from './shared/register/register.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']

})
export class AppComponent implements OnInit {
  title = 'app';
  @ViewChild(MatSidenav)
  sidenav!: MatSidenav;

  reqsCount = 0;



  confirm = false;
  constructor(private api: ApiService, private userSerive: UserService, private tokenService: TokenService, private router: Router, public ui: UiService, public dialog: MatDialog) {

  }
  ngOnInit(): void {
    this.api.getData<DataResponse<number>>('admin/reqscount').subscribe(res =>
    {
      if (res.isOk)
        this.ui.reqcount = res.data;
    });

    if (this.Role == "doctor")
    {
      this.api.getData<DataResponse<boolean>>(`doctor/getconfirm`).subscribe(res =>
      {
        if (res.isOk)
          this.confirm = res.data;

        console.log(res);
      });
    }
    }

  get Confirm() {
    return this.confirm;
  }

  get Role() {
    return this.tokenService.getSession()?.userRole;
  }

  get User() {
    return this.tokenService.getSession()?.login;
  }


  openLogin() {
    const dialogRef = this.dialog.open(LoginComponent, {
      width: '400px',
    });
    dialogRef.afterClosed().subscribe(result => {
      if (!result.isOk || result == undefined || result == null)
        return;
      else {
        this.ui.show("Успешно");
      }
    });
  }

  openRegister() {
    const dialogRef = this.dialog.open(RegisterComponent, {
      width: '400px',
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result == null || result == undefined || !result.isOk)
        return;
      else {
        this.ui.show("Успешно");
      }
    });
  }

  logout() {
    this.ui.confirmation().subscribe(res => {
      if (res) {

        this.tokenService.logout();
        this.router.navigate(['home']);
      }
    });
  }
  route(path: string) {
    this.router.navigate([path]);
  }
}
