import { Component, ViewChild, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AgGridAngular } from 'ag-grid-angular';



export class AnnouncementComponent implements OnInit {
  ngOnInit(): void {
    //this.http.get(this.url).subscribe((x) => this.rowData = x, error => error.console(error));
  }

  private gridApi;
  private gridColumnApi;
  private autoGroupColumnDef;
  private defaultColDef;
  private rowSelection;
  private rowGroupPanelShow;
  private pivotPanelShow;
  private paginationPageSize;
  private paginationNumberFormatter;
  private http;
  private url;

  @ViewChild('agGrid') agGrid: AgGridAngular;
  rowData: any;
  tmpData: any;

  /*constructor(http: HttpClient, @Inject('BASE_URL') baseUrl) {
    this.http = http;
    this.url = baseUrl + 'api/Announcements';
    this.ngOnInit();
    this.autoGroupColumnDef = {
      headerName: "Group",
      width: 200,
      field: "Пользователь",
      valueGetter(params) {
        if (params.node.group) {
          return params.node.key;
        } else {
          return params.data[params.colDef.field];
        }
      },
      headerCheckboxSelection: true,
      cellRenderer: "agGroupCellRenderer",
      cellRendererParams: { checkbox: true }
    };

    this.defaultColDef = {
      editable: true,
      enableRowGroup: true,
      enablePivot: true,
      enableValue: true,
      sortable: true,
      resizable: true,
      filter: true
    };

    this.rowSelection = "multiple";
    this.rowGroupPanelShow = "always";
    this.pivotPanelShow = "always";
    this.paginationPageSize = 10;
    this.paginationNumberFormatter = params => "[" + params.value.toLocaleString() + "]";
  }*/
  
  columnDefs = [
    {headerName: 'Номер', field: 'number', sortable: true },
    { headerName: 'Пользователь', field: 'users', valueGetter: (params)=> params.data.users.name, sortable: true, editable: true, width: 280 },
    { headerName: 'Текст', field: 'text', editable: true },
    { headerName: 'Рейтинг', field: 'rate', sortable: true, editable: true },
    { headerName: 'Дата публикации', field: 'creationDateTime', sortable: true}
  ];
}
