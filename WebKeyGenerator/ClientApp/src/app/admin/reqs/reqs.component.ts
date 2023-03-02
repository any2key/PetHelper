import { Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { APIResponse, DataResponse, Doctor, environment } from '../../model';
import { ApiService } from '../../services/api.service';
import { TokenService } from '../../services/token.service';
import { UiService } from '../../services/ui.service';

@Component({
  selector: 'app-reqs',
  templateUrl: './reqs.component.html',
  styleUrls: ['./reqs.component.css']
})
export class ReqsComponent implements OnInit {

 

  displayedColumns: string[] = ['photo', 'name', 'startWork', 'about', 'email', 'action'];
  dataSource = new MatTableDataSource<Doctor>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator | null;
  @ViewChild(MatSort)
  sort!: MatSort | null;
  constructor(private api: ApiService, private ui: UiService, public dialog: MatDialog, private token: TokenService) { }

  ngOnInit(): void {
    this.refreshTable();
  }


  getStage(year: number) {
    let cur = new Date().getFullYear()
    return cur - year;
  }

  refreshTable() {
    this.token.checkToken();
    this.api.getData<DataResponse<Doctor[]>>('admin/reqs').subscribe(res => {
      if (!res.isOk)
        this.ui.show(res.message);
      else {
        this.dataSource = new MatTableDataSource<Doctor>(res.data);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      }
    });
  }

  confirm(id: number) {
    this.api.getData<APIResponse>(`admin/activatereq?id=${id}`).subscribe(res => {
      if (!res.isOk)
        this.ui.show(res.message);
      else {
        this.api.getData<DataResponse<number>>('admin/reqscount').subscribe(res => {
          if (res.isOk)
            this.ui.reqcount = res.data;
        });
        this.ui.show('Успешно');
      }
      this.refreshTable();

    });
  }

  download(id:number) {
    window.open(`${environment.apiUrl}/api/admin/download?id=${id}`);
  }

}
