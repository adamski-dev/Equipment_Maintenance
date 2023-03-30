function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You are deleting an existing task which could have been referenced in some history data!",
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
                    data.success ? (window.setTimeout(function () { location.reload(); }, 2000), toastr.success(data.message))
                        : toastr.error(data.message);
                }
            })
        }
    })
}





