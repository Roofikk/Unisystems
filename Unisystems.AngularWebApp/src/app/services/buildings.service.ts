import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Building } from '../models/building.model';
import { HttpClient } from '@angular/common/http';
import { GetQueryParamsModel } from '../models/get-query-params.modal';

@Injectable({
  providedIn: 'root'
})
export class BuildingsService {
  baseApiUrl = 'http://localhost:7158/api/Buildings';
  constructor(private http: HttpClient) { }

  getItemsAmount(): Observable<number> {
    return this.http.get<number>(this.baseApiUrl + '/total-items');
  }

  getAllBuildings(queryParams: GetQueryParamsModel): Observable<Building[]> {
    return this.http.get<Building[]>(this.baseApiUrl + '?currentPage=' + queryParams.pagination.currentPage +
      '&pageSize=' + queryParams.pagination.pageSize + '&sortBy=' + queryParams.sortBy + '&direction=' + queryParams.direction);
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
