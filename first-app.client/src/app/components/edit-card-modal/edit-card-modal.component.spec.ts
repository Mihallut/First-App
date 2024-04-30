import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditCardModalComponent } from './edit-card-modal.component';

describe('EditCardModalComponent', () => {
  let component: EditCardModalComponent;
  let fixture: ComponentFixture<EditCardModalComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [EditCardModalComponent]
    });
    fixture = TestBed.createComponent(EditCardModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
