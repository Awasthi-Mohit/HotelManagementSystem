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


//phonemumber
const phoneNumberInput = document.getElementById('pickupPhoneNumber');
const phoneNumberLengthMessage = document.getElementById('phoneNumberLengthMessage');

phoneNumberInput.addEventListener('input', () => {
    const value = phoneNumberInput.value.trim();
    const onlyNumbers = /^[0-9]*$/;

    if (value.length > 0 && !onlyNumbers.test(value)) {
        phoneNumberInput.value = value.replace(/\D/g, '');
    }

    if (phoneNumberInput.value.length < 1) {
        phoneNumberLengthMessage.innerText = ' minimum 1 digits.';
    } else if (phoneNumberInput.value.length > 9) {
        phoneNumberLengthMessage.innerText = ' maximum 9 digits.';
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
        phoneNumberInput.value = pastedText.replace(/\D/g, '').slice(0, 10); // Replace all selected text
    } else {
        const newValue = phoneNumberInput.value + pastedText.replace(/\D/g, '').slice(0, 10 - phoneNumberInput.value.length);
        phoneNumberInput.value = newValue.slice(0, 10); // Limit to 20 characters

        if (!onlyNumbers.test(newValue)) {
            phoneNumberInput.value = newValue.replace(/\D/g, '');
        }
    }

    validatePhoneNumber(); // Validate the input after pasting
});

function validatePhoneNumber() {
    const value = phoneNumberInput.value.trim();
    const onlyNumbers = /^[0-9]*$/;

    if (value.length < 1) {
        phoneNumberLengthMessage.innerText = ' minimum 1 digits.';
    } else if (value.length > 8) {
        phoneNumberLengthMessage.innerText = 'maximum 9 digits.';
    } else {
        phoneNumberLengthMessage.innerText = '';
    }
}