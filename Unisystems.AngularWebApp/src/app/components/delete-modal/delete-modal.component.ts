import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { BuildingsService } from '../../services/buildings.service';
import { DeleteModal } from '../../models/delete-modal.model';

@Component({
  selector: 'app-delete-modal',
  templateUrl: './delete-modal.component.html',
  styleUrl: './delete-modal.component.css'
})

export class DeleteModalComponent {
  activeModal = inject(NgbActiveModal);

  model: DeleteModal = {
    entityId: 0,
    title: '',
    message: '',
    deleteAction: () => { }
  }
}
