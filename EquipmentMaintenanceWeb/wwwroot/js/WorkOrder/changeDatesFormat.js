$(document).ready(function () {

    flatpickr("#dueDate", {});
    flatpickr("#overdueDate", {});
    flatpickr("#createdDate", {});

    var dueDate = document.getElementById('dueDate').value;
    var overdueDate = document.getElementById('overdueDate').value;
    var createdDate = document.getElementById('createdDate').value;

    document.getElementById('dueDate').value = getCustomizedDate(dueDate);
    document.getElementById('overdueDate').value = getCustomizedDate(overdueDate);
    document.getElementById('createdDate').value = getCustomizedDate(createdDate);

    document.getElementById('dueDate').disabled = true;
    document.getElementById('overdueDate').disabled = true;
    document.getElementById('createdDate').disabled = true;

    function getCustomizedDate(data) {

        var date = new Date(data);

        var day = date.getDate();
        var month = date.getMonth() + 1;
        var year = date.getFullYear();

        if (day < 10) day = "0" + day;
        if (month < 10) month = "0" + month;

        return year + "-" + month + "-" + day;
    }

    $("#submitButton").click(function () {

        document.getElementById('dueDate').disabled = false;
        document.getElementById('overdueDate').disabled = false;
        document.getElementById('createdDate').disabled = false;
    });
});

