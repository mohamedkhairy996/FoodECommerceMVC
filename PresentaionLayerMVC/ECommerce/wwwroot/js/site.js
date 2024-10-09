// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function Edit(id, Count) {

    var data = {
        Id: id ,
    Quantity: Count
        };

    console.log("Edit product:", data);

    if (true/*confirm('Are you sure you want to delete this product?')*/) {
        $.ajax({
            url: '/Cart/Edit',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data),
            success: function (response) {
                // On success
                if (response.success) {
                    location.reload();
                } else {
                    $('#message').html('<p>' + response.message + '</p>').show();
                }
            },
            error: function (xhr, status, error) {
                // Handle error and provide user feedback
                $('#message').html('<p>Error: ' + error + '</p>').show();
                console.error("Error Editing product:", status, error);
            }
        });
            }   
    }