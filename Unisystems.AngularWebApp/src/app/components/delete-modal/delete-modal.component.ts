import { Component, inject, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { BuildingsService } from '../../services/buildings.service';

@Component({
  selector: 'app-delete-modal',
  templateUrl: './delete-modal.component.html',
  styleUrl: './delete-modal.component.css'
})

export class DeleteModalComponent {
  activeModal = inject(NgbActiveModal);
  buildingId: number = 0;

  constructor(private buildibgService: BuildingsService) { }
  
  delete() {
    this.buildibgService.deleteBuilding(this.buildingId).subscribe({
      next: () => {
        console.log('deleted');

      },
      error: (error) => {
        console.log(error);
      },
      complete: () => {
        this.activeModal.close();
      }
    })
  }
}
