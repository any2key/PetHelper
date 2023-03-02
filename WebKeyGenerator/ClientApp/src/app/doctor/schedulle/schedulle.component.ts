import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { MatCalendarCellClassFunction } from '@angular/material/datepicker';
import { DataResponse, DayOfWeek, Schedulle, SchedulleInstantiate } from '../../model';
import { ApiService } from '../../services/api.service';
import { UiService } from '../../services/ui.service';

@Component({
  selector: 'app-schedulle',
  templateUrl: './schedulle.component.html',
  styleUrls: ['./schedulle.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class SchedulleComponent implements OnInit {

  schedulle: Schedulle;

  myShedulles: SchedulleInstantiate[] =
    [
      {
        day: DayOfWeek.Monday,
        begin: { hours:0,minutes:0 },
        end: { hours: 0, minutes: 0 },
        work:false
      },
      {
        day: DayOfWeek.Tuesday,
        begin: { hours: 0, minutes: 0 },
        end: { hours: 0, minutes: 0 },
        work: false
      },
      {
        day: DayOfWeek.Wednesday,
        begin: { hours: 0, minutes: 0 },
        end: { hours: 0, minutes: 0 },
        work: false
      },
      {
        day: DayOfWeek.Thursday,
        begin: { hours: 0, minutes: 0 },
        end: { hours: 0, minutes: 0 },
        work: false
      },
      {
        day: DayOfWeek.Friday,
        begin: { hours: 0, minutes: 0 },
        end: { hours: 0, minutes: 0 },
        work: false
      },
      {
        day: DayOfWeek.Saturday,
        begin: { hours: 0, minutes: 0 },
        end: { hours: 0, minutes: 0 },
        work: false
      },
      {
        day: DayOfWeek.Sunday,
        begin: { hours: 0, minutes: 0 },
        end: { hours: 0, minutes: 0 },
        work: false
      },
    ];
  constructor(private api: ApiService, private ui: UiService) { }

  ngOnInit(): void {
    this.api.getData<DataResponse<Schedulle>>('doctor/schedulle').subscribe(res =>
    {
      if (res.isOk) {
        this.schedulle = res.data;
      }
      if (this.schedulle.schedulles.length>0)
      this.schedulle.schedulles.forEach(e =>
      {
        let ms = this.myShedulles.find(e => e.day == e.day);
        if (ms != undefined) {
          this.myShedulles[this.myShedulles.indexOf(ms)].begin = e.begin;
          this.myShedulles[this.myShedulles.indexOf(ms)].end = e.end;
          this.myShedulles[this.myShedulles.indexOf(ms)].work = true;
        }
      });
    });
  }


  selectedDate: any;


  dateClass: MatCalendarCellClassFunction<Date> = (cellDate, view) => {
    // Only highligh dates inside the month view.
    if (view === 'month') {
      const date = cellDate.getDay();

      return date === 1 || date === 3 || date === 5 ? 'example-custom-date-class' : '';
    }

    return '';
  };

  addToSchedulle() {

  }
  removeFromSchedulle() { }

}
