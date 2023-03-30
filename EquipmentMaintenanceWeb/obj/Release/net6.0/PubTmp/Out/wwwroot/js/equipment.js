var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tableData').DataTable({
        "ajax": {
            "url": "/Admin/Equipment/GetAll"
        },
        "columns": [
            { "data": "uniqueIdentifier", "width": "10%" },
            { "data": "description", "width": "20%" },
            { "data": "modelNumber", "width": "10%" },
            { "data": "serialNumber", "width": "10%" },
            {
                "data": "inServiceStatus",
                "render": function (data) {
                    return data == 1 ? `${"Yes"}` : `${"No"}`
                },
                "width": "5%"
            },
            { "data": "equipmentType.name", "width": "10%" },
            { "data": "location.name", "width": "10%" },
            {
                "data": "createdDateTime",
                "render": function (data) {
                    var date = new Date(data);
                    var customizedDate = ('0' + date.getDate()).slice(-2) + '-'
                        + ('0' + (date.getMonth() + 1)).slice(-2) + '-'
                        + date.getFullYear();
                    return `${customizedDate}`
                },
                "width": "15%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                            <a href="/Admin/Equipment/Upsert?id=${data}"
                                class="btn btn-primary btn-sm mx-2"> <i class="bi bi-pencil-square"></i></a>
                            <a  onClick=Delete('/Admin/Equipment/Delete/${data}')
                                class="btn btn-danger btn-sm mx-2" > <i class="bi bi-trash"></i></a>
                        </div>
                    `
                },
                "width": "10%"
            }
        ]
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
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    data.success ? (dataTable.ajax.reload(), toastr.success(data.message))
                    : toastr.error(data.message);
                }
            })
        }
    })
}