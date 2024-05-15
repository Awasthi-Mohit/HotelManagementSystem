function previewImage() {
    var fileInput = document.getElementById("uploadbox");
    var imagePreview = document.getElementById("imagePreview");
    if (fileInput.files && fileInput.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            imagePreview.style.display = "block";
            imagePreview.src = e.target.result;
        };

        reader.readAsDataURL(fileInput.files[0]);
    } else {
        imagePreview.style.display = "none";
        imagePreview.src = "#";
    }
}
$(document).ready(function () {
    $('form').submit(function () {
        var isValid = true;

        // Check each input field
        $('.form-control').each(function () {
            if ($.trim($(this).val()) == '') {
                isValid = false;
                $(this).addClass('is-invalid'); // Add red border
            }
        });

        return isValid;
    });

    // Remove red border on input change
    $('.form-control').keyup(function () {
        if ($.trim($(this).val()) != '') {
            $(this).removeClass('is-invalid');
        }
    });
});
document.getElementById('notificationButton').addEventListener('click', function () {
    if (Notification.permission !== 'granted') {
        Notification.requestPermission().then(function (permission) {
            if (permission === 'granted') {
                showNotification();
            } else {
                console.log('Notification permission denied.');
            }
        });
    } else {
        showNotification();
    }
});

function showNotification() {
    var notification = new Notification('Hotel Notification', {
        body: 'Hotel Created.'
    });

    notification.onclick = function () {
        console.log('Notification clicked.');

        // window.location.href = 'https://example.com';
    };
}
