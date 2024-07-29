import { Component, EventEmitter, inject, OnInit } from '@angular/core';
import { Building } from '../../models/building.model';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ClassroomsService } from '../../services/classrooms.service';

@Component({
  selector: 'app-add-building-modal',
  templateUrl: './add-building-modal.component.html',
  styleUrl: './add-building-modal.component.css'
})
export class AddBuildingModalComponent implements OnInit {
  buildings: Building[] = [];
  activeModal = inject(NgbActiveModal);

  buildingSelected: EventEmitter<Building> = new EventEmitter<Building>();

  constructor(private classroomService: ClassroomsService) { }

  ngOnInit(): void {
    this.classroomService.getAllBuildings().subscribe({
      next: (buildings) => {
        this.buildings = buildings;
      },
      error: (error) => {
        console.log(error);
      }
    });
  }

  selectBuilding(building: Building) {
    this.buildingSelected.emit(building);
    this.activeModal.close();
  }
}
