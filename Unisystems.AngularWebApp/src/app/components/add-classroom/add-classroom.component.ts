import { Component, inject } from '@angular/core';
import { Classroom, Building, RoomType } from '../../models/classroom.model';
import { ClassroomsService } from '../../services/classrooms.service';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AddRoomTypeModalComponent } from '../add-room-type-modal/add-room-type-modal.component';
import { AddBuildingModalComponent } from '../add-building-modal/add-building-modal.component';
import { ClassroomModify } from '../../models/classroomModify.model';

@Component({
  selector: 'app-add-classroom',
  templateUrl: './add-classroom.component.html',
  styleUrl: './add-classroom.component.css'
})
export class AddClassroomComponent {
  newClassroom: ClassroomModify = {
    name: '',
    number: 0,
    capacity: 0,
    floor: 0,
    roomTypeId: '',
    buildingId: 0
  };

  roomTypeName: string = '';
  buildingName: string = '';

  private modalService = inject(NgbModal);

  constructor(private classroomsService: ClassroomsService, private router: Router) { }

  addClassroom() {
    this.classroomsService.createClassroom(this.newClassroom).subscribe({
      next: (data) => {
        this.router.navigate(['/classrooms']);
      },
      error: (error) => {
        console.log(error);
      }
    })
  }

  showAddRoomTypeModal() {
    const modalRef = this.modalService.open(AddRoomTypeModalComponent);
    modalRef.componentInstance.roomTypeSelected.subscribe((roomType: RoomType) => {
      this.newClassroom.roomTypeId = roomType.roomTypeId;
      this.roomTypeName = roomType.name;
    });
  }

  showAddBuildingModal() {
    const modalRef = this.modalService.open(AddBuildingModalComponent);
    modalRef.componentInstance.buildingSelected.subscribe((building: Building) => {
      this.newClassroom.buildingId = building.buildingId;
      this.buildingName = building.name;
    });
  }
}
