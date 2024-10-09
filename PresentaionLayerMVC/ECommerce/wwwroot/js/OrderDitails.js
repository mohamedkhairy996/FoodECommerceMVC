$(document).ready(function () {
    var dataTable = $("#UserOrder").DataTable({
        colReorder: true,
        "ajax": {
            "url": "/Orders/UserCartList",
            "dataSrc": function (json) {
                // Update total price after loading data
                var total = json.totalPrice * 0.05 + json.totalPrice;
                console.log(total);
                document.getElementById("subtotal").value = `${json.totalPrice}`;
                document.getElementById("TotalAfterTax").value = `${total}`;
                return json.data; // Assuming the actual data is in json.data
            }
        },
        "columns": [
            { "data": "product.name", "width": "35%" },
            { "data": "product.price", "width": "15%" },
            { "data": "quantity", "width": "15%" },
            { "data": "quantityPrice", "width": "15%" }
        ]
    });
});
