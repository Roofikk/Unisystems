import { Component, OnInit } from '@angular/core';
import { Building } from '../../models/building.model';
import { BuildingsService } from '../../services/buildings.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-edit-building',
  templateUrl: './edit-building.component.html',
  styleUrl: './edit-building.component.css'
})
export class EditBuildingComponent implements OnInit {
  editBuilding: Building = {
    buildingId: 0,
    name: '',
    address: '',
    floorCount: 0
  };

  constructor(private router: Router,
    private buildingService: BuildingsService,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (params) => {
        const id = Number(params.get('id'));

        this.buildingService.getBuilding(id).subscribe({
          next: (building) => {
            this.editBuilding = building;
          },
          error: (err) => {
            console.log(err);
          }
        });
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  updateBuilding() {
    this.buildingService.updateBuilding(this.editBuilding).subscribe({
      next: () => {
        this.router.navigate(['/buildings']);
      },
      error: (err) => {
        console.log(err);
      }
    });
  }
}
