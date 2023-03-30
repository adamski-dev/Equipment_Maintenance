
flatpickr("#dueDateBy", {});

$('#dueDateBy').change(function () {

    $('#dueDateBy').val() <= todays_date() ? $('#dueDate').show() : $('#dueDate').hide();

});
