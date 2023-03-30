var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tableData').DataTable({
        "ajax": {
            "url": "/Admin/WorkOrder/GetAll"
        },
        "columns": [
            { "data": "workOrderNumber", "width": "10%", "className": "text-center" },
            { "data": "equipment.uniqueIdentifier", "width": "10%", "className": "text-center" },
            { "data": "jobDescription", "width": "10%", "className": "text-center" },
            { 
                "data": "applicationUser",
                "render": function (data) {
                    return `${data.firstName +" "+ data.surname}`
                },
                "width": "15%",
                "className": "text-center"
            },
            {
                "data": "dateDue",
                "render": function (data) {
                    return data == null ? `${"N/A"}` : getCustomizedDate(data)
                },
                "width": "10%",
                "className": "text-center"
            },
            {
                "data": "dateOverdue",
                "render": function (data) {
                    return data == null ? `${"N/A"}` : getCustomizedDate(data)
                },
                "width": "10%",
                "className": "text-center"
            },
            {
                "data": "unplannedFlag",
                "render": function (data) {
                    return data == 1 ? `${"Unplanned"}` : `${"Planned"}`
                },
                "width": "10%",
                "className": "text-center"
            },
            {
                "data": {
                    "completedFlag": "completedFlag",
                    "dateCompleted": "dateCompleted"
                }, 
                "render": function (data) {
                    return data.completedFlag == 1 ? getCustomizedDate(data.dateCompleted) : `${"No"}`
                },
                "width": "3%",
                "className": "text-center"
            },
            {
                "data": {
                    "id": "id",
                    "equipmentId": "equipmentId",
                    "workOrderRoutineId": "RoutineId",
                    "completedFlag": "completedFlag"
                },
                "render": function (data) {
                    return data.completedFlag == 1 ? `` :
                    `
                        <div class="w-75 btn-group" role="group">
                            <a href="/Admin/WorkOrder/Edit?id=${data.id}&equipmentId=${data.equipmentId}"
                                class="btn btn-primary btn-sm mx-2"> <i class="bi bi-pencil-square"></i></a>

                            <a  href="/Admin/WorkOrder/Complete?id=${data.id}&RoutineId=${data.workOrderRoutineId}"
                                class="btn btn-success btn-sm"> <i class="bi bi-play-btn"></i></a>
                        </div>
                    `
                },
                "width": "10%",
            }
        ],
        "createdRow": function (row, data) {

            if ((checkDate(data.dateDue) <= checkDate(new Date())) && (data.completedFlag == 0)) {
                $(row).css('background-color', '#ffcccc');
            }

            if ((checkDate(data.dateCompleted) > checkDate(data.dateOverdue)) && (data.completedFlag == 1) && (checkDate(data.dateOverdue) != null)) {
                $(row).css('background-color', '#ffcccc');
            };

            if ((data.unplannedFlag == 1) && (data.completedFlag == 1)) {
                $(row).css('background-color', '#e6fff3');
            };

            if ((checkDate(data.dateCompleted) <= checkDate(data.dateOverdue)) && (data.completedFlag == 1)) {
                $(row).css('background-color', '#e6fff3');
            };
        }
    });
}

function getCustomizedDate (data) {
    var date = new Date(data);
    var customizedDate = ('0' + date.getDate()).slice(-2) + '-'
        + ('0' + (date.getMonth() + 1)).slice(-2) + '-'
        + date.getFullYear();
    return `${customizedDate}`
}


function checkDate(data) {

    var today = new Date(data);

    var day = today.getDate();
    var month = today.getMonth() + 1;
    var year = today.getFullYear();

    if (day < 10) day = "0" + day;
    if (month < 10) month = "0" + month;

    return year + "-" + month + "-" + day;
}