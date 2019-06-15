"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var GridComponent = /** @class */ (function () {
    // inject the athleteService
    function GridComponent(annservice) {
        this.annservice = annservice;
        this.columnDefs = [
            { headerName: 'Номер', field: 'number', sortable: true },
            { headerName: 'Пользователь', field: 'users', valueGetter: function (params) { return params.data.users.name; }, sortable: true, editable: true, width: 280 },
            { headerName: 'Текст', field: 'text', editable: true },
            { headerName: 'Рейтинг', field: 'rate', sortable: true, editable: true },
            { headerName: 'Дата публикации', field: 'creationDateTime', sortable: true }
        ];
    }
    // on init, subscribe to the athelete data
    GridComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.annservice.findAll().subscribe(function (athletes) {
            _this.rowData = athletes;
        }, function (error) {
            console.log(error);
        });
    };
    GridComponent.prototype.onGridReady = function (params) {
        this.api = params.api;
        this.columnApi = params.columnApi;
        this.api.sizeColumnsToFit();
    };
    // create some simple column definitions
    GridComponent.prototype.createColumnDefs = function () {
        return [
            { field: 'id' },
            { field: 'name' },
            { field: 'country' },
            { field: 'results' }
        ];
    };
    return GridComponent;
}());
exports.GridComponent = GridComponent;
//# sourceMappingURL=grid.component.js.map