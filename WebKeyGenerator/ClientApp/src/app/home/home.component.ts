import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { TokenService } from '../services/token.service';
import { UiService } from '../services/ui.service';
import { UserService } from '../services/user.service';
import { EntryComponent } from '../shared/entry/entry.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  constructor(private router: Router, private token: TokenService, private user: UserService, private ui: UiService, private tokenService: TokenService, public dialog: MatDialog) { }

  ngOnInit(): void {
    
   // this.router.navigate(['login']);
  }

  
  get isLogin() {
    return this.tokenService.getSession()?.login != null;
    }
  openEntry() {
    const dialogRef = this.dialog.open(EntryComponent, {
      width: '600px',
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result == null || result == undefined || !result.isOk)
        return;
      else {
        this.ui.show("Успешно");
      }
    });
  }

  route(path: string) {
    this.router.navigate([path]);
  }

}
