import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatButton } from '@angular/material/button';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { APIResponse, Speciality } from '../../../model';
import { ApiService } from '../../../services/api.service';
import { TokenService } from '../../../services/token.service';
import { UiService } from '../../../services/ui.service';

@Component({
  selector: 'app-add-specialty',
  templateUrl: './add-specialty.component.html',
  styleUrls: ['./add-specialty.component.css']
})
export class AddSpecialtyComponent implements OnInit {

  @ViewChild('btnRef')
  buttonRef!: MatButton;
  constructor(
    public dialogRef: MatDialogRef<AddSpecialtyComponent>, private ui: UiService, private token: TokenService, private api: ApiService,
    @Inject(MAT_DIALOG_DATA) public data: Speciality) { }
  ngAfterViewInit(): void {
    this.buttonRef?.focus();
  }

  header!: string;
  ngOnInit(): void {
    this.header = 'Редактор специальности';
    console.log(this.data);
    this.spec.patchValue(this.data);
  }

  keyUpFunction(res: any) {
    this.apply();
  }

  spec = new FormGroup(
    {
      name: new FormControl('', [Validators.required]),
    });

  onNoClick(): void {
    const result: any = { isOk: false };
    this.dialogRef.close(result);
  }

  apply() {

    if (!this.spec.valid) {
      this.ui.show("Форма заполнена не корректно");
      return;
    }
    const p: Speciality = this.spec.value;
    p.id = this.data.id;
    p.doctors = [];
    const result = { isOk: true, data: p };
    result.isOk = true;
    result.data = p;

    this.token.checkToken();
    this.api.postData<APIResponse, Speciality>('admin/addspec', p).subscribe(res => {
      if (!res.isOk) {
        this.ui.show(res.message);
      } else {
        this.dialogRef.close(result);
      }
    });





  }

}
