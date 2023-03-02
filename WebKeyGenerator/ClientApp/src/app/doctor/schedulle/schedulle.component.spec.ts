import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SchedulleComponent } from './schedulle.component';

describe('SchedulleComponent', () => {
  let component: SchedulleComponent;
  let fixture: ComponentFixture<SchedulleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SchedulleComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SchedulleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
