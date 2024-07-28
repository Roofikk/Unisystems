import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Building } from '../models/building.model';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class BuildingsService {
  baseApiUrl = 'https://localhost:7158/api/Buildings';
  constructor(private http: HttpClient) { }

  getAllBuildings(): Observable<Building[]> {
    return this.http.get<Building[]>(this.baseApiUrl);
  }

  getBuilding(id: number): Observable<Building> {
    return this.http.get<Building>(this.baseApiUrl + '/' + id);
  }

  addBuilding(model: Building): Observable<Building> {
    model.buildingId = 0;
    return this.http.post<Building>(this.baseApiUrl, model);
  }

  updateBuilding(model: Building): Observable<Building> {
    return this.http.put<Building>(this.baseApiUrl + '/' + model.buildingId, model);
  }

  deleteBuilding(id: number): Observable<Building> {
    return this.http.delete<Building>(this.baseApiUrl + '/' + id);
  }
}
