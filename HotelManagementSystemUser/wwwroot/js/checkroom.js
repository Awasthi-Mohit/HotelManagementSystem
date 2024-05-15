function RoomCheck() {
    $("#Statu").html("Checking...");
    var roomNumber = $("#Roomno").val();

    $.post({
        url: "@Url.Action("CheckRoom", "Hotel")", // URL for the server-side action
        data: { room: roomNumber }, // Data to send to the server

        success: function (data) { // Function to handle successful response
            if (data == 0) { // Assuming 0 means available, adjust as necessary
                $("#Statu").html('<font color="green">Available! You can take it.</font>');
                $("#Roomno").css("border-color", "green");
            } else {
                $("#Statu").html('<font color="red">Room already in use. Try another.</font>');
                $("#Roomno").css("border-color", "red");
            }
        },
        error: function () { // Function to handle errors
            $("#Statu").html('<font color="red">Error checking room availability.</font>');
            $("#Roomno").css("border-color", "red");
        }
    });
}
<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
