function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "Delete Post",
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
                    if (data.success) {
                        location.reload();
                    }
                    else {
                    }
                }
            })
        }
    });
}

function Create(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "Create New Post",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'POST',
                success: function (data) {
                    if (data.success) {
                        location.reload();
                    }
                    else {
                    }
                }
            })
        }
    });
}