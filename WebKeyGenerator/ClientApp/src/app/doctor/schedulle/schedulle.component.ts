import { AfterViewInit, Component, OnChanges, OnInit, SimpleChanges, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatCalendar, MatCalendarCellClassFunction, MatDatepicker } from '@angular/material/datepicker';
import { APIResponse, DataResponse, DayOfWeek, Schedulle, SchedulleInstantiate, TimePeriod } from '../../model';
import { ApiService } from '../../services/api.service';
import { UiService } from '../../services/ui.service';

@Component({
  selector: 'app-schedulle',
  templateUrl: './schedulle.component.html',
  styleUrls: ['./schedulle.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class SchedulleComponent implements OnInit {
  @ViewChild('calendar') calendar: MatCalendar<Date>;
  schedulle: Schedulle;

  myShedulles: SchedulleInstantiate[] =
    [
      {
        day: DayOfWeek.Monday,
        begin: { hours:0,minutes:0 },
        end: { hours: 0, minutes: 0 },
        work: false,
        dateBegin: new Date(0, 0, 0, 0),
        dateEnd: new Date(0, 0, 0, 0)
      },
      {
        day: DayOfWeek.Tuesday,
        begin: { hours: 0, minutes: 0 },
        end: { hours: 0, minutes: 0 },
        work: false,
        dateBegin: new Date(0,0,0,0),
        dateEnd: new Date(0,0,0,0)
      },
      {
        day: DayOfWeek.Wednesday,
        begin: { hours: 0, minutes: 0 },
        end: { hours: 0, minutes: 0 },
        work: false,
        dateBegin: new Date(0, 0, 0, 0),
        dateEnd: new Date(0, 0, 0, 0)
      },
      {
        day: DayOfWeek.Thursday,
        begin: { hours: 0, minutes: 0 },
        end: { hours: 0, minutes: 0 },
        work: false,
        dateBegin: new Date(0, 0, 0, 0),
        dateEnd: new Date(0, 0, 0, 0)
      },
      {
        day: DayOfWeek.Friday,
        begin: { hours: 0, minutes: 0 },
        end: { hours: 0, minutes: 0 },
        work: false,
        dateBegin: new Date(0, 0, 0, 0),
        dateEnd: new Date(0, 0, 0, 0)
      },
      {
        day: DayOfWeek.Saturday,
        begin: { hours: 0, minutes: 0 },
        end: { hours: 0, minutes: 0 },
        work: false,
        dateBegin: new Date(0, 0, 0, 0),
        dateEnd: new Date(0, 0, 0, 0)
      },
      {
        day: DayOfWeek.Sunday,
        begin: { hours: 0, minutes: 0 },
        end: { hours: 0, minutes: 0 },
        work: false,
        dateBegin: new Date(0, 0, 0, 0),
        dateEnd: new Date(0, 0, 0, 0)
      },
    ];


  hours: string[]=[];
  minutes: string[]=[];

  constructor(private api: ApiService, private ui: UiService) { }
  
 

  changed() {
    this.calendar.updateTodaysDate();

  }

  ngOnInit(): void {


    for (var i = 0; i < 24; i++) {
      let s = i.toString();
      if (s.length < 2)
        s = `0${s}`;
      this.hours.push(s);
    }
    for (var i = 0; i < 60; i++) {
      let s = i.toString();
      if (s.length < 2)
        s = `0${s}`;
      this.minutes.push(s);
    }

    this.api.getData<DataResponse<Schedulle>>('doctor/schedulle').subscribe(res =>
    {
      if (res.isOk) {
        this.schedulle = res.data;
      }
      if (this.schedulle.schedulles.length>0)
      this.schedulle.schedulles.forEach(e =>
      {
        let ms = this.myShedulles.find(x => x.day == e.day);
        if (ms != undefined) {
          this.myShedulles[this.myShedulles.indexOf(ms)].begin = e.begin;
          this.myShedulles[this.myShedulles.indexOf(ms)].end = e.end;
          this.myShedulles[this.myShedulles.indexOf(ms)].dateBegin = new Date(0, 0, 0, e.begin.hours, e.begin.minutes);
          this.myShedulles[this.myShedulles.indexOf(ms)].dateEnd = new Date(0, 0, 0, e.end.hours, e.end.minutes);
          this.myShedulles[this.myShedulles.indexOf(ms)].work = true;
        }
          this.calendar.updateTodaysDate();
      });
    });

  }


  selectedDate: any;


  dateClass: MatCalendarCellClassFunction<Date> = (cellDate, view) => {
    // Only highligh dates inside the month view.
    if (view === 'month') {
      const date = cellDate.getDay();

      return this.myShedulles.find(e => (e.day == date) && e.work) != undefined ? 'example-custom-date-class' : '';
      //return date === 1 || date === 3 || date === 5 ? 'example-custom-date-class' : '';
    }

    return '';
  };

  save() {
    let schedulles: SchedulleInstantiate[] = [];
    this.myShedulles.forEach(e =>
    {
      if ((e.dateBegin >= e.dateEnd) && e.work)
      {
        this.ui.show(`Время начала работы должно быть раньше время завершения`);
        return;
      }
      if (e.work)
      {
        let s = new SchedulleInstantiate();
        s.day = e.day;
        s.begin = new TimePeriod();
        s.begin.hours = e.dateBegin.getHours();
        s.begin.minutes = e.dateBegin.getMinutes();

        s.end = new TimePeriod();
        s.end.hours = e.dateEnd.getHours();
        s.end.minutes = e.dateEnd.getMinutes();

        schedulles.push(s);
      }
    });

    console.log(schedulles);
    this.api.postData<APIResponse, SchedulleInstantiate[]>('doctor/saveschedulle', schedulles).subscribe(res =>
    {
      if (!res.isOk)
        this.ui.show(res.message);
      else this.ui.show("Успешно");
    });
  }

}
