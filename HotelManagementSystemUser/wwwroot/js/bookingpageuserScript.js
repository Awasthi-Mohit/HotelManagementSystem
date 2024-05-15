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

    //nameInput.addEventListener("paste", function (event) {
    //    event.preventDefault();
    //    const pastedText = (event.clipboardData || window.clipboardData).getData('text');
    //    const filteredText = pastedText.replace(/[^a-zA-Z\s,'"]/g, '');
    //    document.execCommand("insertText", false, filteredText);
    //    validateName();
    //});

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

//postalcode
document.getElementById("postalCode").addEventListener("input", function () {
    var postalCodeInput = this.value;
    var lengthMessage = document.getElementById("postalCodeLengthMessage");

    if (postalCodeInput.length < 3) {
        lengthMessage.textContent = "PinCode must be at least 3 characters.";
    } else {
        lengthMessage.textContent = "";
    }
});

document.getElementById("postalCode").addEventListener("paste", function (event) {
    var pastedText = (event.clipboardData || window.clipboardData).getData("text");
    var resultingText = this.value + pastedText;

    if (resultingText.length > 6) {
        event.preventDefault();
        alert("PinCode cannot exceed 6 characters.");
    }
});

function validateEmail(input) {

    const emailRegex = /^[a-zA-Z0-9._%+-]+@@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    const isValid = emailRegex.test(input.value);
    const validationMessage = document.getElementById('emailValidationMessage');

    if (!isValid) {
        validationMessage.innerText = "Please enter a valid email address.";
        input.setCustomValidity("Invalid email address.");
    } else {
        validationMessage.innerText = "";
        input.setCustomValidity("");
    }
}