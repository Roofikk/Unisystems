import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddRoomTypeModalComponent } from './add-room-type-modal.component';

describe('AddRoomTypeModalComponent', () => {
  let component: AddRoomTypeModalComponent;
  let fixture: ComponentFixture<AddRoomTypeModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddRoomTypeModalComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddRoomTypeModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
