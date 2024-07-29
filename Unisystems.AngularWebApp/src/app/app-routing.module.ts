import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BuildingsComponent } from './components/buildings/buildings.component';
import { ClassroomsComponent } from './components/classrooms/classrooms.component';
import { AddBuildingComponent } from './components/add-building/add-building.component';
import { EditBuildingComponent } from './components/edit-building/edit-building.component';
import { AddClassroomComponent } from './components/add-classroom/add-classroom.component';
import { EditClassroomComponent } from './components/edit-classroom/edit-classroom.component';

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
    path: 'buildings/edit/:id',
    component: EditBuildingComponent
  },
  {
    path: 'classrooms',
    component: ClassroomsComponent
  },
  {
    path: 'classrooms/add',
    component: AddClassroomComponent
  },
  {
    path: 'classrooms/edit/:id',
    component: EditClassroomComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
