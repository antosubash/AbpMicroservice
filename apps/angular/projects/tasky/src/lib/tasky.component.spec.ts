import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { TaskyComponent } from './tasky.component';

describe('TaskyComponent', () => {
  let component: TaskyComponent;
  let fixture: ComponentFixture<TaskyComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ TaskyComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TaskyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
