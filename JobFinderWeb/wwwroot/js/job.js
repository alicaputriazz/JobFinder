let dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Job/GetAll"
        },
        "columns": [
            { "data": "name"},
            { "data": "workingMethod.name"},
            { "data": "category.name" }
        ],
        "autoWidth": false,
    });
}