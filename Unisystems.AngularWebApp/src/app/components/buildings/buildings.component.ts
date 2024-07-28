import { Component, inject, OnInit } from '@angular/core';
import { Building } from '../../models/building.model';
import { Router } from '@angular/router';
import { BuildingsService } from '../../services/buildings.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DeleteModalComponent } from '../delete-modal/delete-modal.component';

@Component({
  selector: 'app-buildings',
  templateUrl: './buildings.component.html',
  styleUrl: './buildings.component.css'
})
export class BuildingsComponent implements OnInit {

  buildings: Building[] = [];

  private modalService = inject(NgbModal);

  constructor(private buildingService: BuildingsService,
    private router: Router) { }

  ngOnInit(): void {
    this.buildingService.getAllBuildings()
      .subscribe({
        next: (buildings) => {
          this.buildings = buildings;
        },
        error: (response) => {
          console.log(response);
        }
      });
  }

  showDeleteModal(buildingId: number) {
    const modalRef = this.modalService.open(DeleteModalComponent);
    modalRef.componentInstance.buildingId = buildingId;
  }

  deleteBuilding(id: number) {
    this.buildingService.deleteBuilding(id)
      .subscribe({
        next: () => {
          this.buildingService.getAllBuildings()
            .subscribe({
              next: (buildings) => {
                this.buildings = buildings;
              },
              error: (response) => {
                console.log(response);
              }
            });
        },
        error: (response) => {
          console.log(response);
        }
      });
  }
}
