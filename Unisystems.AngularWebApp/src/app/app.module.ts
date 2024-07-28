import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { BuildingsComponent } from './components/buildings/buildings.component';
import { provideHttpClient } from '@angular/common/http';
import { ClassroomsComponent } from './components/classrooms/classrooms.component';
import { AddBuildingComponent } from './components/add-building/add-building.component';
import { UpdateBuildingComponent } from './components/update-building/update-building.component';
import { EditBuildingComponent } from './components/edit-building/edit-building.component';
import { DeleteModalComponent } from './components/delete-modal/delete-modal.component';
import { NgbActiveModal, NgbModal, NgbModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  declarations: [
    AppComponent,
    BuildingsComponent,
    ClassroomsComponent,
    AddBuildingComponent,
    UpdateBuildingComponent,
    EditBuildingComponent,
    DeleteModalComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    NgbModule
  ],
  providers: [
    provideHttpClient(),
    NgbActiveModal,
    NgbModal
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
