// signalr-setup.js
function setupToAdminMessageHandler(connection) {
    connection.on("sendAdminMsg", function (msg , id , name) {
        if (id == $("#txtToUser").val()) {

            document.getElementById("list").innerHTML += `
         <li class="fs-3" style="display:flex; justify-content:flex-start;">${msg}</li>
        `;

        }
        alert(name+" : "+msg);
    });
}

function setupToUserMessageHandler(connection) {
    connection.on("sendUserMsg", function (msg) {
        document.getElementById("list").innerHTML += `
         <li class="fs-3" style="display:flex; justify-content:flex-end;">${msg}</li>
        `;

        alert("Admin : "+msg);
    });
}

// Initialize connection and set up handlers
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub")
    .build();

setupToAdminMessageHandler(connection);
setupToUserMessageHandler(connection);

// Start the connection
connection.start()
    .then(() => console.log("SignalR Connected."))
    .catch(err => console.error("SignalR Connection Error: ", err));

//Get id Of Admin And User
var Admin = "b696b434-b763-431f-82d2-3c5b97450dba";
var currentUserId = document.getElementById("CurrentUserId")?.value;
console.log(currentUserId);

// Define send function for sending messages to Admin
$("#btnSend").on("click", function () {
    var msg = $("#txtmsg").val();

    // Check if the message or input fields are empty
    if (!msg) {
        console.log("Please fill in all fields.");
        return;
    }

    // Send message to the MessageController
    $.ajax({
        type: "POST",
        url: "/message/SendMessage",
        contentType: "application/json",
        data: JSON.stringify({ fromUserId: currentUserId, toUserId: Admin, msg: msg }),
        success: function () {
            console.log("Message sent!");
            document.getElementById("list").innerHTML += `
         <li class="fs-3" style="display:flex; justify-content:flex-start;">${msg}</li>
        `;
            document.getElementById("txtmsg").value = "";
        },
        error: function (err) {
            console.error("Error sending message: ", err);
        }
    });
});

// Define send function for sending messages to User
$("#btnSendToUser").on("click", function () {
    var to = $("#txtToUser").val();
    console.log(to);
    var msg = $("#txtmsg").val();

    // Check if the message or input fields are empty
    if (!to || !msg) {
        alert("Please fill in all fields.");
        return;
    }
    
    // Send message to the MessageController
    $.ajax({
        type: "POST",
        url: "/message/SendMessage",
        contentType: "application/json",
        data: JSON.stringify({ fromUserId: Admin, toUserId: to, msg: msg }),
        success: function () {
            console.log("Message sent!");
            document.getElementById("list").innerHTML += `
         <li class="fs-3" style="display:flex; justify-content:flex-end;">${msg}</li>
        `;
            document.getElementById("txtmsg").value = "";
        },
        error: function (err) {
            console.error("Error sending message: ", err);
        }
    });
});