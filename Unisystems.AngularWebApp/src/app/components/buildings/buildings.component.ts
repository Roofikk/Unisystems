import { Component, OnInit } from '@angular/core';
import { Building } from '../../models/building.model';
import { Router } from '@angular/router';
import { BuildingsService } from '../../services/buildings.service';

@Component({
  selector: 'app-buildings',
  templateUrl: './buildings.component.html',
  styleUrl: './buildings.component.css'
})
export class BuildingsComponent implements OnInit {
  buildings: Building[] = [];

  constructor(private buildingService: BuildingsService,
              private router: Router) { }

  ngOnInit(): void {
    this.buildingService.getAllBuildings()
      .subscribe({
        next: (buildings) => {
          this.buildings = buildings;
        },
        error: (response) => {
          console.log(response);
        }
      });
  }
}
