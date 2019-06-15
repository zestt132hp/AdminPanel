import { Component, OnInit } from '@angular/core';
import { GridApi, ColumnApi } from "ag-grid-community";
import * as Announcementmodel from "./models/announcement.model";
import Announcer = Announcementmodel.Announcer;
import * as Announceservice from "./services/announce.service";
import AnnService = Announceservice.AnnService;
import Userservice = require("./services/user.service");
import UserService = Userservice.UserService;
import Usermodel = require("./models/user.model");
import User = Usermodel.User;


@Component({
  selector: 'data-grid',
  templateUrl: './grid.component.html'
})

export class GridComponent implements OnInit {
  private user: User[];
  private rowData: Announcer[];
  private columnDefs = [
    { headerName: 'Номер', field: 'number', sortable: true },
    {
      headerName: 'Пользователь', field: 'users', cellRenderer:(params)=>params.data.user.name, sortable: true, width: 280 },
    { headerName: 'Текст', field: 'text', editable: true },
    { headerName: 'Рейтинг', field: 'rate', sortable: true, editable: true },
    { headerName: 'Дата публикации', field: 'creationDateTime', sortable: true }
  ];

  private api: GridApi;
  private columnApi: ColumnApi;

  constructor(private annservice: AnnService, private userservice: UserService) {
  }
  
  ngOnInit() {
    this.annservice.findAll().subscribe(
      x => {
        this.rowData = x;
      },
      error => {
        console.log(error);
      }
    );
  }

  onGridReady(params): void {
    this.api = params.api;
    this.columnApi = params.columnApi;
    this.api.sizeColumnsToFit();
  }
}
