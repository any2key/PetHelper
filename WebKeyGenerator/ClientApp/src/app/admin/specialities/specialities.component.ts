import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AddOrUpdate, APIResponse, DataResponse, Speciality } from '../../model';
import { ApiService } from '../../services/api.service';
import { TokenService } from '../../services/token.service';
import { UiService } from '../../services/ui.service';
import { AddSpecialtyComponent } from './add-specialty/add-specialty.component';

@Component({
  selector: 'app-specialities',
  templateUrl: './specialities.component.html',
  styleUrls: ['./specialities.component.css']
})
export class SpecialitiesComponent implements OnInit {

  displayedColumns: string[] = ['id', 'name', 'action'];
  dataSource = new MatTableDataSource<Speciality>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator | null;
  @ViewChild(MatSort)
  sort!: MatSort | null;
  constructor(private api: ApiService, private ui: UiService, public dialog: MatDialog, private token: TokenService) { }

  ngOnInit(): void {
    this.refreshTable();
  }

  refreshTable() {
    this.token.checkToken();
    this.api.getData<DataResponse<Speciality[]>>('admin/fetchspec').subscribe(res => {
      if (!res.isOk)
        this.ui.show(res.message);
      else {
        this.dataSource = new MatTableDataSource<Speciality>(res.data);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      }
    });
  }


  openDialog(sign: AddOrUpdate, spec: Speciality = new Speciality()) {
    const data: Speciality = spec;
    if (sign == AddOrUpdate.add) {

    }

    const dialogRef = this.dialog.open(AddSpecialtyComponent, {
      width: '400px',
      data: data,
    });
    dialogRef.afterClosed().subscribe(result => {
      if (!result.isOk || result == undefined || result == null)
        return;
      else {
        this.ui.show("Успешно");
        this.refreshTable();
       
      }
    });
  }

  delete(id: number) {
    this.ui.confirmation().subscribe(res => {
      if (res) {
        this.token.checkToken();
        this.api.getData<APIResponse>(`admin/Deletespec?userId=${id}`).subscribe(res => {
          if (!res.isOk)
            this.ui.show(res.message);
          else this.ui.show("Успешно");

          this.refreshTable();
        });
      }
    })
  }

}
