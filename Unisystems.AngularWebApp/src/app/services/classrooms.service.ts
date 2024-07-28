import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Classroom } from '../models/classroom.model';

@Injectable({
  providedIn: 'root'
})
export class ClassroomsService {
  baseApiUrl = 'https://localhost:7289/api/Classrooms'
  constructor(private http: HttpClient) { }

  getAllClassrooms(): Observable<Classroom[]> {
    return this.http.get<Classroom[]>(this.baseApiUrl);
  }

  getClassroom(id: number): Observable<Classroom> {
    return this.http.get<Classroom>(`${this.baseApiUrl}/${id}`);
  }

  createClassroom(classroom: Classroom): Observable<Classroom> {
    return this.http.post<Classroom>(this.baseApiUrl, classroom);
  }

  updateClassroom(id: number, classroom: Classroom): Observable<Classroom> {
    return this.http.put<Classroom>(`${this.baseApiUrl}/${id}`, classroom);
  }

  deleteClassroom(id: number): Observable<Classroom> {
    return this.http.delete<Classroom>(`${this.baseApiUrl}/${id}`);
  }
}
