import { Component, EventEmitter, inject, OnInit } from '@angular/core';
import { RoomType } from '../../models/classroom.model';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ClassroomsService } from '../../services/classrooms.service';

@Component({
  selector: 'app-add-room-type-modal',
  templateUrl: './add-room-type-modal.component.html',
  styleUrl: './add-room-type-modal.component.css'
})
export class AddRoomTypeModalComponent implements OnInit {
  activeModal = inject(NgbActiveModal);
  modalService = inject(NgbModal);
  roomTypes: RoomType[] = [];

  roomTypeSelected: EventEmitter<RoomType> = new EventEmitter<RoomType>();

  constructor(private classroomService: ClassroomsService) { }

  ngOnInit(): void {
    this.classroomService.getRoomTypes().subscribe({
      next: (roomTypes) => {
        this.roomTypes = roomTypes;
      },
      error: (error) => {
        console.log(error);
      }
    })
  }

  selectRoomType(roomType: RoomType) {
    this.roomTypeSelected.emit(roomType);
    this.activeModal.close();
  }

  showCreateNewRoomType() {
    //TODO
  }

  showDeleteRoomType(roomType: RoomType) {
    //TODO
  }
}
