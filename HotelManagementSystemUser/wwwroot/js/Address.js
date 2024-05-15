const streetAddressInput = document.getElementById('input-street-address');
const streetAddressValidationMessage = document.getElementById('streetAddressValidation');

// Function to validate input and disallow special characters
function validateInput(value) {
    const isSpecialChar = /[!@@#$%^&*(),.?":/\{}|<>]/.test(value);
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
        streetAddressInput.value = streetAddress.replace(/[!@@#$%^&*(),.?" :{}|<>]/g);
        streetAddressValidationMessage.innerText = '';
        return;
    }

    if (streetAddress.length < 5) {
        streetAddressValidationMessage.innerText = 'Minimum 5 characters are required.';
        return;
    }

    if (streetAddress.length > 100 && streetAddress.length <= 115) {
        streetAddressValidationMessage.innerText = 'Maximum 100 characters are allowed.';
        return;
    }


    if (streetAddress.length > 115) {
        streetAddressInput.value = streetAddress.substring(0, 115);
        streetAddressValidationMessage.innerText = 'Maximum 115 characters are allowed.';
        return;
    }


    streetAddressValidationMessage.innerText = '';
});
