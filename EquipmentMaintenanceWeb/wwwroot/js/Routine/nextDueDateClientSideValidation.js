
$(document).ready(function () {

    $('#nextDueDate').change(function () {

        $('#nextDueDate').val() < todays_date() ? $('#dueDate').show() : $('#dueDate').hide();

    })
});





