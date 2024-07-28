import { Component } from '@angular/core';
import { Building } from '../../models/building.model';
import { BuildingsService } from '../../services/buildings.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-building',
  templateUrl: './add-building.component.html',
  styleUrl: './add-building.component.css'
})
export class AddBuildingComponent {
  newBuilding: Building = {
    buildingId: 0,
    name: '',
    address: '',
    floorCount: 0
  };

  constructor(private buildingService: BuildingsService, private router: Router) { }

  addBuilding(): void {
    this.buildingService.addBuilding(this.newBuilding)
      .subscribe({
        next: (data) => {
          this.router.navigate(['/buildings']);
        },
        error: (error) => {
          console.log(error);
        }
      });
  }
}
