
//Name
document.addEventListener("DOMContentLoaded", function () {
    const nameInput = document.getElementById("txtName");
    const messageDiv = document.getElementById("message");

    nameInput.addEventListener("input", function () {
        validateName();
    });

    nameInput.addEventListener("keydown", function (event) {
        const allowedChars = /^[a-zA-Z\s,'"]+$/;
        if (!allowedChars.test(event.key)) {
            event.preventDefault();
        }
    });

    nameInput.addEventListener("paste", function (event) {
        event.preventDefault();
        const pastedText = (event.clipboardData || window.clipboardData).getData('text');
        const filteredText = pastedText.replace(/[^a-zA-Z\s,'"]/g, '');
        document.execCommand("insertText", false, filteredText);
        validateName();
    });

    function validateName() {
        const name = nameInput.value.trim();
        const nameRegex = /[^A-Za-z\s,'"]/g;

        if (name.length < 3) {
            messageDiv.textContent = "Name must be at least 3 characters long.";
            nameInput.setCustomValidity("Name must be at least 3 characters long.");
        } else if (name.length > 50) {
            messageDiv.textContent = "Name must not exceed 50 characters.";
            nameInput.setCustomValidity("Name must not exceed 50 characters.");
        } else if (name.match(nameRegex)) {
            messageDiv.textContent = "Name contains invalid characters.";
            nameInput.setCustomValidity("Name contains invalid characters.");
        } else {
            messageDiv.textContent = "";
            nameInput.setCustomValidity("");
        }
    }
});
document.getElementById('txtEmailId').addEventListener('input', function (event) {
    var inputElement = event.target;
    var inputValue = inputElement.value;

    // Check if the input value is not empty
    if (inputValue.trim()) {
        var cursorPosition = inputElement.selectionStart; // Store the cursor position

        var regex = /^[A-Za-z\s,"']+$/; // Include allowed symbols: \s for space, , for comma, ' for apostrophe, and ; for semicolon
        if (!regex.test(inputValue)) {
            var filteredValue = inputValue.replace(/[^A-Za-z\s,']/g, ''); // Remove any characters other than letters, spaces, commas, apostrophes, and semicolons

            // Determine the new cursor position after filtering
            var newPos = cursorPosition - (inputValue.length - filteredValue.length);
            inputElement.value = filteredValue;

            // Restore the cursor position
            inputElement.setSelectionRange(newPos, newPos);
        }

        // Get the length of the filtered input value
        var length = inputValue.length;

        // Get the message element
        var messageElement = document.getElementById('message');

        if (length < 3) {
            messageElement.textContent = 'Minimum 3 characters required.';
            messageElement.style.color = 'red'; // Set the text color to red
        } else if (length > 50) {
            messageElement.textContent = 'Maximum 50 characters required.';
            messageElement.style.color = 'red'; // Set the text color to red
        } else {
            messageElement.textContent = ''; // Clear the message
        }
    }
});

$(function () {
    $("#txtname").bind('paste', function (e) {
        var inputElement = this;
        var inputValue = inputElement.value;
        var cursorPosition = inputElement.selectionStart; // Store the cursor position

        // Get the pasted data
        var pastedData = e.originalEvent.clipboardData.getData('text');

        // Append the pasted data
        inputElement.value = inputValue.substring(0, cursorPosition) + pastedData + inputValue.substring(inputElement.selectionEnd);

        // Validate the input
        var modifiedData = inputElement.value.replace(/[^A-Za-z\s,'"]/g, ''); // Allow spaces by including '\s' in the regex
        inputElement.value = modifiedData;

        // Determine the new cursor position after pasting
        var newPos = cursorPosition + (pastedData.length - (inputElement.value.length - inputValue.length));

        // Restore the cursor position
        inputElement.setSelectionRange(newPos, newPos);
    });
});

//Phonenumber
const phoneNumberInput = document.getElementById('pickupPhoneNumber');
const phoneNumberLengthMessage = document.getElementById('phoneNumberLengthMessage');

phoneNumberInput.addEventListener('input', () => {
    const value = phoneNumberInput.value.trim();
    const onlyNumbers = /^[0-9]*$/;

    if (value.length > 0 && !onlyNumbers.test(value)) {
        phoneNumberInput.value = value.replace(/\D/g, '');
    }

    if (phoneNumberInput.value.length < 10) {
        phoneNumberLengthMessage.innerText = 'Phone number should be minimum 10 digits.';
    } else if (phoneNumberInput.value.length > 15) {
        phoneNumberLengthMessage.innerText = 'Phone number should be maximum 15 digits.';
    } else {
        phoneNumberLengthMessage.innerText = '';
    }
});

phoneNumberInput.addEventListener('paste', (event) => {
    event.preventDefault(); // Prevent default paste behavior
    const pastedText = (event.clipboardData || window.clipboardData).getData('text');
    const onlyNumbers = /^[0-9]*$/;

    // Check if all text is selected
    const isAllTextSelected = document.activeElement.selectionStart === 0 && document.activeElement.selectionEnd === phoneNumberInput.value.length;

    if (isAllTextSelected) {
        phoneNumberInput.value = pastedText.replace(/\D/g, '').slice(0, 20); // Replace all selected text
    } else {
        const newValue = phoneNumberInput.value + pastedText.replace(/\D/g, '').slice(0, 20 - phoneNumberInput.value.length);
        phoneNumberInput.value = newValue.slice(0, 20); // Limit to 20 characters

        if (!onlyNumbers.test(newValue)) {
            phoneNumberInput.value = newValue.replace(/\D/g, '');
        }
    }

    validatePhoneNumber();
});

function validatePhoneNumber() {
    const value = phoneNumberInput.value.trim();
    const onlyNumbers = /^[0-9]*$/;

    if (value.length < 10) {
        phoneNumberLengthMessage.innerText = 'Phone number should be minimum 10 digits.';
    } else if (value.length > 15) {
        phoneNumberLengthMessage.innerText = 'Phone number should be maximum 15 digits.';
    } else {
        phoneNumberLengthMessage.innerText = '';
    }
}

//EmailValidation
function validateEmail(input) {
    const email = input.value.toLowerCase();
    const emailPattern = /^[a-z0-9]+@@[a-z0-9]+\.[a-z]{2,}$/; // Updated email pattern
    const isValid = emailPattern.test(email);
    const validationMessage = document.getElementById('emailValidationMessage');

    if (!isValid) {
        validationMessage.textContent = "Enter a valid email address with lowercase letters and a valid format.";
    } else {
        validationMessage.textContent = "";
    }
}

// Function to clean and validate email input
function cleanAndValidateEmail(input) {
    const value = input.value.trim();
    const cleanedValue = value.replace(/[^a-z0-9@@.]/gi, ''); // Remove invalid characters
    input.value = cleanedValue; // Update input value
    validateEmail(input); // Validate email
}

// Add an event listener to the input element for input event
const inputEmail = document.getElementById('Input_Email');
inputEmail.addEventListener('input', () => {
    cleanAndValidateEmail(inputEmail);
});

// Prevent pasting invalid characters and validate email after pasting
inputEmail.addEventListener('paste', (event) => {
    event.preventDefault(); // Prevent default paste behavior
    const pastedText = (event.clipboardData || window.clipboardData).getData('text');
    const cleanedText = pastedText.replace(/[^a-z0-9@@.] in,co com/gi, ''); // Remove invalid characters
    document.execCommand("insertText", false, cleanedText); // Insert cleaned text
    cleanAndValidateEmail(inputEmail); // Validate email after pasting
});
//Address
const streetAddressInput = document.getElementById('input-street-address');
const streetAddressValidationMessage = document.getElementById('streetAddressValidation');

// Function to validate input and disallow special characters
function validateInput(value) {
    const isSpecialChar = /[!@@#$%^&*(),.?":{}|<>]/.test(value);
    if (isSpecialChar) {
        streetAddressValidationMessage.innerText = '';
        return false;
    }
    return true;
}

streetAddressInput.addEventListener('input', () => {
    const streetAddress = streetAddressInput.value.trim();

    // Check for special characters
    if (!validateInput(streetAddress)) {
        streetAddressInput.value = streetAddress.replace(/[!@@#$%^&*(),.?":{}|<>]/g, '');
        streetAddressValidationMessage.innerText = 'Special characters are not allowed.';
        return;
    }

    // Check if street address length is less than 5
    if (streetAddress.length < 5) {
        streetAddressValidationMessage.innerText = 'Minimum 5 characters are required.';
        return;
    }

    // Check if street address length is more than 50 but less than or equal to 115
    if (streetAddress.length > 100 && streetAddress.length <= 115) {
        streetAddressValidationMessage.innerText = 'Maximum 100 characters are allowed.';
        return;
    }

    // Check if street address length is more than 115
    if (streetAddress.length > 115) {
        streetAddressInput.value = streetAddress.substring(0, 115);
        streetAddressValidationMessage.innerText = 'Maximum 115 characters are allowed.';
        return;
    }

    // If all conditions pass, clear validation message
    streetAddressValidationMessage.innerText = '';
});
