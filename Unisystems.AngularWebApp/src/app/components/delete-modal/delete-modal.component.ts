import { Component, inject, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-delete-modal',
  templateUrl: './delete-modal.component.html',
  styleUrl: './delete-modal.component.css'
})

export class DeleteModalComponent {
  private modalService = inject(NgbActiveModal);
  
  delete() {
    console.log('delete');
    this.modalService.close();
  }
}
