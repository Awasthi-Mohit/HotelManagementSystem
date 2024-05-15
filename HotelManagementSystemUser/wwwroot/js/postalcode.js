document.getElementById('postalCode').addEventListener('input', function (event) {
    var inputElement = event.target;
    var inputValue = inputElement.value;

    // Check if the input value is not empty
    if (inputValue.trim()) {
        var cursorPosition = inputElement.selectionStart; // Store the cursor position

        // Define the regex pattern for postal code validation
        var regex = /^[0-9A-Za-z\s]+$/; // Allow alphanumeric characters and spaces

        if (!regex.test(inputValue)) {
            var filteredValue = inputValue.replace(/[^0-9A-Za-z\s]/g, ''); // Remove any characters other than alphanumeric and spaces

            // Determine the new cursor position after filtering
            var newPos = cursorPosition - (inputValue.length - filteredValue.length);
            inputElement.value = filteredValue;

            // Restore the cursor position
            inputElement.setSelectionRange(newPos, newPos);
        }

        // Get the length of the filtered input value
        var length = inputValue.length;

        // Get the message element
        var messageElement = document.getElementById('postalCodeMessage');

        if (length < 3) {
            messageElement.textContent = 'Minimum 3 characters required.';
            messageElement.style.color = 'red'; // Set the text color to red
        } else if (length > 6) {
            messageElement.textContent = 'Maximum 6 characters required.';
            messageElement.style.color = 'red'; // Set the text color to red
        } else {
            messageElement.textContent = ''; // Clear the message
        }
    }
});