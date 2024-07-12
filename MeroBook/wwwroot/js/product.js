$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    $('#productTable').DataTable({
        "ajax": {
            url: '/Admin/Product/GetAll',
            "type": "GET",
            "dataSrc": "data"
        },
        "columns": [
            { data: 'title', "width": "25%"},
            { data: 'isbn', "width": "15%"},
            { data: 'price', "width": "10%"},
            { data: 'author', "width": "15%"},
            { data: 'categoryName', "width": "10%"},
            {
                data: 'productId',
                "render": function (data) {
                    return `<div class = "w-75 btn-group" role = "group">
                        <a href="/admin/product/addedit?id=${data}" data-productid="${data}" class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i> Edit</a>
                        <a onClick = Delete('/admin/product/delete/${data}') data-productid="${data}" class="btn btn-danger mx-2"><i class="bi bi-trash3-fill"></i> Delete</a>
                    </div>`
                },
                "width": "25%"
            }
        ]
    });
}
function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    $('#productTable').DataTable().ajax.reload();
                    toastr.success(data.message);
                }
            });
        }
    });
}
