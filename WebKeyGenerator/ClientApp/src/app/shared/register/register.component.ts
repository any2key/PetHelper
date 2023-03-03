import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatButton } from '@angular/material/button';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { APIResponse, User } from '../../model';
import { ApiService } from '../../services/api.service';
import { TokenService } from '../../services/token.service';
import { UiService } from '../../services/ui.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  @ViewChild('btnRef')
  buttonRef!: MatButton;
  constructor(
    public dialogRef: MatDialogRef<RegisterComponent>, private ui: UiService, private token: TokenService, private api: ApiService,
    @Inject(MAT_DIALOG_DATA) public data: User) { }
  ngAfterViewInit(): void {
    this.buttonRef?.focus();
  }

  header!: string;
  admin: string = "admin";
  suser: string = "user";
  doctor: string = "doctor";
  show = false;

  ngOnInit(): void {
    this.header = 'Регистрация';
    this.data.password = "";
    console.log(this.data);
    this.user.patchValue(this.data);

    this.user.valueChanges.subscribe(res => {
      // this.buttonRef?.focus();

    });
  }


  generatePass() {
    this.user.get('password')?.reset();
    this.user.get('password')?.patchValue(Array(8).fill("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz~!@-#$").map(function (x) { return x[Math.floor(Math.random() * x.length)] }).join(''));
    ;
  }

  keyUpFunction(res: any) {
    this.apply();
  }

  changeShow() {
    this.show = !this.show;
  }
  user = new FormGroup(
    {
      email: new FormControl('', [Validators.email, Validators.required]),
      password: new FormControl(''),
    });

  onNoClick(): void {
    const result: any = { isOk: false };
    this.dialogRef.close(result);
  }

  apply() {

    if (!this.user.valid) {
      this.ui.show("Форма заполнена не корректно");
      return;
    }
    const p: User = this.user.value;
    p.id = -1;
    p.role = this.suser;
    p.login = p.email;
    p.passwordsalt = '';
    p.refreshtokens = [];
    p.active = true;
    const result = { isOk: true, data: p };
    result.isOk = true;
    result.data = p;

    this.token.checkToken();
    this.api.postData<APIResponse, User>('admin/AddOrUpdateUser', p).subscribe(res => {
      if (!res.isOk) {
        this.ui.show(res.message);
      } else {
        this.dialogRef.close(result);
      }
    });
  }

}
