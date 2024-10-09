$(document).ready(function () {
    var orderHeaderId = document.getElementById("orderHeaderId").innerText
    document.getElementById("orderHeaderId").hidden=true;

    console.log(orderHeaderId);
    var dataTable = $("#UserOrderDescription").DataTable({
        colReorder: true,
        searching: false,
        
        "ajax": {
            "url": "/Orders/UserOrderDetailsJson/" + orderHeaderId,
            "dataSrc": function (json) {
                return json.data; // Assuming the actual data is in json.data
            }
        },
        "columns": [
            { "data": "product.name", "width": "35%" },
            { "data": "product.price", "width": "15%" },
            { "data": "count", "width": "15%" },
            { "data": "price", "width": "15%" }
        ]
    });
});

