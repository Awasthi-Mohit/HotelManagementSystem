function checkRoomNo() {
    var roomNo = document.getElementById('RoomNo').value;
    var roomNoError = document.getElementById('roomNoError');

    // Reset error message
    roomNoError.textContent = '';

    var xhr = new XMLHttpRequest();

    xhr.onreadystatechange = function () {
        if (xhr.readyState === XMLHttpRequest.DONE) {
            if (xhr.status === 200) {
                var response = xhr.responseText;
                if (response === 'exists') {
                    roomNoError.textContent = 'Room number already exists.';
                }
            } else {
                console.error('Error: ' + xhr.status);
            }
        }
    };

    xhr.open('GET', '/CheckRoomAvailability?roomNo=' + roomNo, true);
    xhr.send();
}
function checkInputLength(input) {
    var minLength = 3;
    var maxLength = 10;
    var currentValue = input.value.length;
    var errorSpan = document.getElementById('roomNoError');

    if (currentValue < minLength) {
        errorSpan.textContent = "Minimum 3 characters required.";
    } else if (currentValue >= 6 && currentValue > maxLength) {
        input.value = input.value.slice(0, maxLength);
        errorSpan.textContent = "Maximum 10 characters allowed.";
    } else {
        errorSpan.textContent = "";
    }
}