import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { last, Subscription } from 'rxjs';
import { AdminSettings, APIResponse, CrmClient, DataResponse, Key, mSettings, UpdateKeyRequest } from '../../model';
import { ApiService } from '../../services/api.service';
import { TokenService } from '../../services/token.service';
import { UiService } from '../../services/ui.service';

const floatNumericNumberReg = '^-?[0-9]\\d*(\\.\\d{1,2})?$';
const numericNumberReg = "^[0-9]*$";

@Component({
  selector: 'app-key',
  templateUrl: './key.component.html',
  styleUrls: ['./key.component.css']
})
export class KeyComponent {

}
