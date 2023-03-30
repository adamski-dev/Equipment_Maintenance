
$(document).ready(function () {

    $("#submitButton").click(function (e) {

        var taskCompletedList = document.querySelectorAll('[id$="_completeTask"]');
        var allCompleted = true;

        var uncheckedFields = [].filter.call(taskCompletedList, function (task) {
            return !task.checked
        });

        if (uncheckedFields.length > 0) {
            allCompleted = false;
            document.getElementById('tasksValidationError').innerHTML = "All tasks must be completed.";
        }
        else
        {
            document.getElementById('completedWorkOrder').checked = true;
        }

        allCompleted ? $('#tasksValidationError').hide() : ($('#tasksValidationError').show(), e.preventDefault());

    });
})
