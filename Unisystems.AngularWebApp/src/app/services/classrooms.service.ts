import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Classroom, RoomType } from '../models/classroom.model';
import { Building } from '../models/building.model';
import { ClassroomModify } from '../models/classroomModify.model';

@Injectable({
  providedIn: 'root'
})
export class ClassroomsService {
  baseApiUrl = 'https://localhost:7289/api/'
  constructor(private http: HttpClient) { }

  getAllClassrooms(): Observable<Classroom[]> {
    return this.http.get<Classroom[]>(this.baseApiUrl + 'Classrooms');
  }

  getClassroom(id: number): Observable<Classroom> {
    return this.http.get<Classroom>(`${this.baseApiUrl}Classrooms/${id}`);
  }

  createClassroom(classroom: ClassroomModify): Observable<Classroom> {
    return this.http.post<Classroom>(this.baseApiUrl + 'Classrooms', classroom);
  }

  updateClassroom(id: number, classroom: ClassroomModify): Observable<Classroom> {
    return this.http.put<Classroom>(`${this.baseApiUrl}Classrooms/${id}`, classroom);
  }

  deleteClassroom(id: number): Observable<Classroom> {
    return this.http.delete<Classroom>(`${this.baseApiUrl}Classrooms/${id}`);
  }

  getAllBuildings(): Observable<Building[]> {
    return this.http.get<Building[]>(this.baseApiUrl + 'Buildings');
  }

  getBuilding(id: number): Observable<Building> {
    return this.http.get<Building>(`${this.baseApiUrl}Buildings/${id}`);
  }

  getRoomTypes(): Observable<RoomType[]> {
    return this.http.get<RoomType[]>(this.baseApiUrl + 'RoomTypes');
  }

  getRoomType(id: number): Observable<RoomType> {
    return this.http.get<RoomType>(`${this.baseApiUrl}RoomTypes/${id}`);
  }
}
