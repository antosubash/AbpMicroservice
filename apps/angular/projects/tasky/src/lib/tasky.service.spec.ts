import { TestBed } from '@angular/core/testing';

import { TaskyService } from './tasky.service';

describe('TaskyService', () => {
  let service: TaskyService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TaskyService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
