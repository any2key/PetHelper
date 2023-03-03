import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { DataResponse, Doctor } from '../../model';
import { ApiService } from '../../services/api.service';
import { TokenService } from '../../services/token.service';
import { UiService } from '../../services/ui.service';

@Component({
  selector: 'app-spec-doctors',
  templateUrl: './spec-doctors.component.html',
  styleUrls: ['./spec-doctors.component.css']
})
export class SpecDoctorsComponent implements OnInit {
  specId: string | null = null;
  docs: Doctor[] = [];
  private subscription: Subscription;
  constructor(private api: ApiService, private ui: UiService, private token: TokenService, public dialog: MatDialog, private activateRoute: ActivatedRoute, private route: Router) {
    this.subscription = activateRoute.params.subscribe(
      (params) => {
        console.log(params);
        (this.specId = params['specId']);
        if (this.specId != undefined) {
          this.api.getData<DataResponse<Doctor[]>>(`admin/docsbyspec?id=${this.specId}`).subscribe(res => {
            this.docs = res.data;
          });
        }
        else {
          this.api.getData<DataResponse<Doctor[]>>(`admin/docs`).subscribe(res => {
          });
            }


      }
    );
  }

  ngOnInit(): void {
  }
  getStage(year: number) {
    let cur = new Date().getFullYear()
    return cur - year;
  }
}
