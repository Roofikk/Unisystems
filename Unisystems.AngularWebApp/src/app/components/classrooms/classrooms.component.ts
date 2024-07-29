import { Component, inject } from '@angular/core';
import { Classroom } from '../../models/classroom.model';
import { ClassroomsService } from '../../services/classrooms.service';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DeleteModalComponent } from '../delete-modal/delete-modal.component';

@Component({
  selector: 'app-classrooms',
  templateUrl: './classrooms.component.html',
  styleUrl: './classrooms.component.css'
})
export class ClassroomsComponent {
  classrooms: Classroom[] = [];
  private modalService = inject(NgbModal);

  constructor(private classroomService: ClassroomsService,
    private router: Router) { }

  ngOnInit(): void {
    this.classroomService.getAllClassrooms()
      .subscribe({
        next: (classrooms) => {
          this.classrooms = classrooms;
        },
        error: (error) => {
          console.log(error);
        }
      });
  }

  showDeleteModal(classroomId: number) {
    const modalRef = this.modalService.open(DeleteModalComponent);

    modalRef.componentInstance.model = {
      entityId: classroomId,
      title: 'Удаление заведения',
      message: 'Вы действительно хотите удалить заведение? Все кабинеты этого заведения будут так же удалены',
      deleteAction: () => {
        this.classroomService.deleteClassroom(classroomId).subscribe({
          next: () => {
            this.classroomService.getAllClassrooms()
              .subscribe({
                next: (classrooms) => {
                  this.classrooms = classrooms;
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
