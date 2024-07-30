import { Component, inject, OnInit } from '@angular/core';
import { Building } from '../../models/building.model';
import { Router } from '@angular/router';
import { BuildingsService } from '../../services/buildings.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DeleteModalComponent } from '../delete-modal/delete-modal.component';
import { GetQueryParamsModel, Direction } from '../../models/get-query-params.modal';
import { BuildingSortBy } from '../../models/building-sort-by.model';

@Component({
  selector: 'app-buildings',
  templateUrl: './buildings.component.html',
  styleUrl: './buildings.component.css'
})
export class BuildingsComponent implements OnInit {
  sortBy = BuildingSortBy;
  direction = Direction;

  totalItems: number = 0;
  queryParams: GetQueryParamsModel = {
    pagination: {
      currentPage: 1,
      pageSize: 10,
    },
    direction: Direction.Asc,
    sortBy: BuildingSortBy.Id,
  }

  buildings: Building[] = [];
  private modalService = inject(NgbModal);

  constructor(private buildingService: BuildingsService,
    private router: Router) { }

  ngOnInit(): void {
    this.buildingService.getItemsAmount().subscribe({
      next: (itemsCount) => {
        this.totalItems = itemsCount;
        this.buildingService.getAllBuildings(this.queryParams).subscribe({
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

  getSortedBuildings(sortBy: BuildingSortBy, direction: Direction) {
    this.queryParams.sortBy = sortBy;
    this.queryParams.direction = direction;
    this.queryParams.pagination = {
      currentPage: 1,
      pageSize: 10
    }

    this.buildingService.getAllBuildings(this.queryParams).subscribe({
      next: (buildings) => {
        this.buildings = buildings;
      },
      error: (response) => {
        console.log(response);
      }
    })
  }

  getBuildings(page: number) {
    this.queryParams.pagination.currentPage = page;
    this.buildingService.getAllBuildings(this.queryParams).subscribe({
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
            this.buildingService.getAllBuildings(this.queryParams)
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
