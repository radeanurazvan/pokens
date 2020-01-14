import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ArenaDetailsComponent } from './arena-details.component';

describe('ArenaDetailsComponent', () => {
  let component: ArenaDetailsComponent;
  let fixture: ComponentFixture<ArenaDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ArenaDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArenaDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
