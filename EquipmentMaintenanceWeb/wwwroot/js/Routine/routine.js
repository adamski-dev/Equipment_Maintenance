var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tableData').DataTable({
        "ajax": {
            "url": "/Admin/Routine/GetAll"
        },
        "columns": [
            { "data": "equipment.uniqueIdentifier", "width": "10%" },
            { "data": "routineDescription", "width": "10%" },
            {
                "data": "applicationUser",
                "render": function (data) {
                    return `${data.firstName +" "+ data.surname}`
                },
                "width": "20%"
            },
            {
                "data": "isActiveStatus",
                "render": function (data) {
                    return data == 1 ? `${"Yes"}` : `${"No"}`
                },
                "width": "5%"
            },
            {
                "data": "dateLastCompleted",
                "render": function (data) {
                    return data == null ? `${"N/A"}` : getCustomizedDate(data)
                },
                "width": "10%"
            },
            {
                "data": "nextDueDate",
                "render": function (data) {
                    return getCustomizedDate(data)
                },
                "width": "10%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                            <a href="/Admin/Routine/Upsert?id=${data}"
                                class="btn btn-primary btn-sm mx-2"> <i class="bi bi-pencil-square"></i></a>
                            <a  onClick=Delete('/Admin/Routine/Delete/${data}')
                                class="btn btn-danger btn-sm mx-2" > <i class="bi bi-trash"></i></a>
                        </div>
                    `
                },
                "width": "10%"
            }
            
        ]
    });
}

function getCustomizedDate (data) {
    var date = new Date(data);
    var customizedDate = ('0' + date.getDate()).slice(-2) + '-'
        + ('0' + (date.getMonth() + 1)).slice(-2) + '-'
        + date.getFullYear();
    return `${customizedDate}`
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
                    data.success ?
                        (dataTable.ajax.reload(), toastr.success(data.message))
                    : toastr.error(data.message);
                    
                }
            })
        }
    })
}