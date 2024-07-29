import { Component, inject, Input, OnInit, Output } from '@angular/core';
import { ClassroomModify } from '../../models/classroomModify.model';
import { ClassroomsService } from '../../services/classrooms.service';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AddRoomTypeModalComponent } from '../add-room-type-modal/add-room-type-modal.component';
import { AddBuildingModalComponent } from '../add-building-modal/add-building-modal.component';
import { Building, RoomType } from '../../models/classroom.model';

@Component({
  selector: 'app-edit-classroom',
  templateUrl: './edit-classroom.component.html',
  styleUrl: './edit-classroom.component.css'
})
export class EditClassroomComponent implements OnInit {
  classroomId: number = 0;
  classroom: ClassroomModify = {
    name: '',
    number: 0,
    capacity: 0,
    floor: 0,
    buildingId: 0,
    roomTypeId: 0,
  };

  @Output() roomTypeName: string = '';
  @Output() buildingName: string = '';

  modalService = inject(NgbModal);

  constructor(private classroomsService: ClassroomsService,
    private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (params) => {
        this.classroomId = Number(params.get('id'));

        this.classroomsService.getClassroom(this.classroomId)
          .subscribe({
            next: (data) => {
              this.classroomId = data.classroomId;
              this.classroom.name = data.name;
              this.classroom.number = data.number;
              this.classroom.capacity = data.capacity;
              this.classroom.floor = data.floor;
              this.classroom.buildingId = data.building.buildingId;
              this.classroom.roomTypeId = data.roomType.roomTypeId;

              this.roomTypeName = data.roomType.name;
              this.buildingName = data.building.name;
            },
            error: (err) => {
              console.log(err);
            }
          });
      },
      error: (err) => {
        console.log(err);
      }
    })
    
  }

  showAddRoomTypeModal() {
    const modalRef = this.modalService.open(AddRoomTypeModalComponent);
    modalRef.componentInstance.roomTypeSelected.subscribe((roomType: RoomType) => {
      this.classroom.roomTypeId = roomType.roomTypeId;
      this.roomTypeName = roomType.name;
    });
  }

  showAddBuildingModal() {
    const modalRef = this.modalService.open(AddBuildingModalComponent);
    modalRef.componentInstance.buildingSelected.subscribe((building: Building) => {
      this.classroom.buildingId = building.buildingId;
      this.buildingName = building.name;
    });
  }

  updateClassroom() {
    this.classroomsService.updateClassroom(this.classroomId, this.classroom)
      .subscribe({
        next: () => {
          this.router.navigate(['/classrooms']);
        },
        error: (err) => {
          console.log(err);
        }
      })
  }
}
