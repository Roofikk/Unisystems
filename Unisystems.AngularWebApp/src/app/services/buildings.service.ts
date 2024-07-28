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
}
