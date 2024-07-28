import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BuildingsComponent } from './components/buildings/buildings.component';
import { provideHttpClient } from '@angular/common/http';
import { ClassroomsComponent } from './components/classrooms/classrooms.component';
import { AddBuildingComponent } from './components/add-building/add-building.component';
import { UpdateBuildingComponent } from './components/update-building/update-building.component';

@NgModule({
  declarations: [
    AppComponent,
    BuildingsComponent,
    ClassroomsComponent,
    AddBuildingComponent,
    UpdateBuildingComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
  ],
  providers: [provideHttpClient()],
  bootstrap: [AppComponent]
})
export class AppModule { }
