import { TestBed } from '@angular/core/testing';

import { TodoAppService } from './todo-app.service';

describe('TodoAppService', () => {
  let service: TodoAppService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TodoAppService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
