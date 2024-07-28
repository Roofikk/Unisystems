import { Component } from '@angular/core';
import { Classroom } from '../../models/classroom.model';
import { ClassroomsService } from '../../services/classrooms.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-classrooms',
  templateUrl: './classrooms.component.html',
  styleUrl: './classrooms.component.css'
})
export class ClassroomsComponent {
  classrooms: Classroom[] = [];

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
}
