import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DataResponse, Speciality } from '../../model';
import { ApiService } from '../../services/api.service';
import { UiService } from '../../services/ui.service';

@Component({
  selector: 'app-spec-list',
  templateUrl: './spec-list.component.html',
  styleUrls: ['./spec-list.component.css']
})
export class SpecListComponent implements OnInit {

  constructor(private api: ApiService, private ui: UiService, private router: Router) { }


  specs: Speciality[]=[];

  ngOnInit(): void {
    this.api.getData<DataResponse<Speciality[]>>('admin/fetchspec').subscribe(res =>
    {
      if (!res.isOk)
        this.ui.show(res.message);
      else this.specs = res.data;
    });
  }

  route(id: number)
  {
    this.router.navigate(['docs', `${id}`])
  }

}
