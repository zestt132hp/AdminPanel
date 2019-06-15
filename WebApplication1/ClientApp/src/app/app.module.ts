import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { AgGridModule } from 'ag-grid-angular';
import { GridComponent } from "./GridComponent.component";
import * as Announceservice from "./services/announce.service";
import AnnService = Announceservice.AnnService;
import Userservice = require("./services/user.service");
import UserService = Userservice.UserService;
import { UserComponent } from './users/userComponent';

@NgModule({
  declarations: [
    AppComponent,
    GridComponent,
    UserComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([{ path: '', component: AppComponent }]),
    AgGridModule.withComponents([])
  ],
  providers: [AnnService, UserService],
  bootstrap: [AppComponent]
})
export class AppModule { }
