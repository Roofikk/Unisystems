import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BuildingsComponent } from './components/buildings/buildings.component';
import { ClassroomsComponent } from './components/classrooms/classrooms.component';
import { AddBuildingComponent } from './components/add-building/add-building.component';

const routes: Routes = [
  {
    path: 'buildings',
    component: BuildingsComponent
  },
  {
    path: 'buildings/add',
    component: AddBuildingComponent
  },
  {
    path: 'classrooms',
    component: ClassroomsComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
