import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SpecDoctorsComponent } from './spec-doctors.component';

describe('SpecDoctorsComponent', () => {
  let component: SpecDoctorsComponent;
  let fixture: ComponentFixture<SpecDoctorsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SpecDoctorsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SpecDoctorsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
