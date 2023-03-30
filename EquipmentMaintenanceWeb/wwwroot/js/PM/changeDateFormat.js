$(document).ready(function () {

    flatpickr("#createdDate", {});

    var createdDate = document.getElementById('createdDate').value;

    document.getElementById('createdDate').value = getCustomizedDate(createdDate);

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

        document.getElementById('createdDate').disabled = false;
    });
});

