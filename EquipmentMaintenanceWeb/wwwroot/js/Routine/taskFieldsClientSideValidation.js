
$(document).ready(function () {

    $("#submitButton").click(function (e) {

        //getting list of id by partial id name in my case geting the end of id with $ sign before =; if ^ it gets the start of id name; 
        //    if * it should be any value but i didn't test this case.

        var taskInputsList = document.querySelectorAll('[id$="__Description"]');
        var tasksPopulated = true;

        for (var task of taskInputsList.values()) {

            if ($('[id="' + task.id + '"]').val() == "" || $('[id="' + task.id + '"]').val() == null) {
                tasksPopulated = false;
                document.getElementById('tasksValidationError').innerHTML = "All task fields must be populated.";
            }

            if ($('[id="' + task.id + '"]').val().length < 5 || $('[id="' + task.id + '"]').val().length > 400) {
                tasksPopulated = false;
                document.getElementById('tasksValidationError').innerHTML = "Task description must be between 5 and 400 characters in length.";
            }
        };

        if (taskInputsList.length == 0) {
            tasksPopulated = false;
            document.getElementById('tasksValidationError').innerHTML = "At least one task is required.";
        }

        tasksPopulated ? $('#tasksValidationError').hide() : ($('#tasksValidationError').show(), e.preventDefault());

    });
})
