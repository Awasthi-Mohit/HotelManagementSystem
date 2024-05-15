
$(document).ready(function () {
    $("#showNotificationBtn").click(function () {
        if ("Notification" in window) {
            Notification.requestPermission().then(function (permission) {
                if (permission === "granted") {
                    var notification = new Notification("Hotel Deleted ", {
                        body: "Hotel Deleted ",
                    });
                }
            });
        }
    });
});