var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable =  $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Job/GetAll"
        },
        "columns": [
            { "data": "name"},
            { "data": "workingMethod.name"},
            { "data": "category.name" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <a class="btn btn-warning btn-sm" href="/Admin/Job/Upsert?id=${data}">Edit</a>
                        <a onClick="Delete('/Admin/Job/Delete/${data}')" class="btn btn-danger btn-sm">Delete</a>
                    `
                }
            }
        ],
        "autoWidth": false,
    });
}

function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, just delete'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        Swal.fire(
                            'Deleted!',
                            `${data.message}`,
                            'success'
                        )
                    } else {
                        console.log(data.message);
                    }
                }
            })
        }
    })
}