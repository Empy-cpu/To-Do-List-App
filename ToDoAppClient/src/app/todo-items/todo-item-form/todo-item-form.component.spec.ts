import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TodoItemFormComponent } from './todo-item-form.component';

describe('TodoItemFormComponent', () => {
  let component: TodoItemFormComponent;
  let fixture: ComponentFixture<TodoItemFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TodoItemFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TodoItemFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
