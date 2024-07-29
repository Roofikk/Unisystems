import { Component, EventEmitter, inject, OnInit } from '@angular/core';
import { Building } from '../../models/building.model';
import { Router } from '@angular/router';
import { BuildingsService } from '../../services/buildings.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DeleteModalComponent } from '../delete-modal/delete-modal.component';
import { DeleteModal } from '../../models/delete-modal.model';

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
    modalRef.componentInstance.model = {
      entityId: buildingId,
      title: 'Удаление заведения',
      message: 'Вы действительно хотите удалить заведение? Все кабинеты этого заведения будут так же удалены',
      deleteAction: () => {
        this.buildingService.deleteBuilding(buildingId).subscribe({
          next: () => {
            this.buildingService.getAllBuildings()
              .subscribe({
                next: (buildings) => {
                  this.buildings = buildings;
                  modalRef.close();
                },
                error: (response) => {
                  console.log(response);
                }
              });
          },
          error: (err) => {
            console.log(err)
          },
          complete: () => {
            modalRef.close();
          }
        })
      }
    }
  }
}
