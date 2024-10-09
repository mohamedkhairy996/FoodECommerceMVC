$(document).ready(function () {
    var adminUser = $("#ifAdmin").val();
    dataTable = $("#myOrderTable").DataTable({
        colReorder: true,
        "ajax": {
            "url": "/Orders/OrderListAll?status"
        },
        "columns": [
            {
                "data": "dateOfOrder", "render": function (data) { return moment(data).format('Do MMMM YYYY'); }, "width": "35%" },
            { "data": "name", "width": "15%" },
            { "data": "totalOrderAmount", "width": "15%" },
            {
                "data": "orderStatus", "render": function (data) {
                    if (data == "Canceled") {
                        return '<p class="text-danger"> ' + data + '</p>'
                    } else {
                        return '<p class="text-success"> ' + data + '</p>'
                    }
                }, "width": "15%"
            },
            {
                "data": "id", "width": "15%", "render": function (data) {
                    if (adminUser === "1") {
                        return `
            <div class="row">
              <div class="col-12">
                <a href="/Orders/OrderDescription/${data}" class="btn btn-success text-white form-control" style="cursor:pointer">
                  <i class="bi bi-pencil-square">Details</i>
                </a>
              </div>
            </div>
          `;
                    } else {
                        return `
            <div class="row">
              <div class="col-12">
                <a href="/Orders/OrderDescription/${data}" class="btn btn-success text-white form-control" style="cursor:pointer">
                  <i class="bi bi-credit-card">Details</i>
                </a>
              </div>
            </div>
          `;
                    }
                }
            }
        ]
    });
});
