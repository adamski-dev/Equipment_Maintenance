$(document).ready(function () {

    const maximumNumberOfTasks = 20;

    $(function () {

        $("#addTask").click(function (e) {

            e.preventDefault();
            $('#tasksValidationError').hide();

            var itemIndex = $("#tasksContainer input.iHidden").length;

            if (itemIndex <= maximumNumberOfTasks) {

                var newItem = $("<div class='input-group mb-3'><input id='Routine.ListOfTasks_" + itemIndex + "__Description' name='Routine.ListOfTasks[" + itemIndex + "].Description' class='form-control input-sm iHidden'/><span class='input-group-btn delete'><a href='#' class='btn btn-danger btn-sm mt-1'><i class='bi bi-trash'></i></a></span></div>");
                $("#tasksContainer").append(newItem);
            }
            else
            {
                document.getElementById('tasksValidationError').innerHTML = "You can only have maximum of 20 tasks assigned.";
                $('#tasksValidationError').show();
            }

        });

        $("#tasksContainer").on("click", ".delete", function (e) {

            e.preventDefault();
            $('#tasksValidationError').hide()
            $(this).parent('div').remove();
        })

    });
});


/* the below input is example of adding another input of id type which i didn't need for tasks as it generates automatically but i left it here as example from development
/* <input id='Routine.ListOfTasks_" + itemIndex + "__RoutineId' type='hidden' value='0' class='iHidden' name='Routine.ListOfTasks[" + itemIndex + "].RoutineId' />*/


