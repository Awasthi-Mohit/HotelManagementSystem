var dataTable;

$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "Hotel/GetAll"
        },
        "columns": [
            { "data": "name", "width": "60%" },
            { "data": "place", "width": "60%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                     <div class="text-center">
                        <a href="/Hotel/Edit/${data}" class="btn btn-info">
                        <i class="fas fa-edit"></i>
                        </a>
                       
                           <a class="btn btn-danger"  onclick=Delete("/Hotel/Delete/${data}")>
                           <i class="fas fa-trash-alt"></i>
                           </a>
                        </div>
                    `;
                }
            }
        ]
    })
}
function Delete(url) {
    swal({
        title: "want to delete data?",
        text: "Delete Infomation!!!",
        buttons: true,
        icon: "warning",
        dangerModel: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        $('#tblData').DataTable();
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}