let state = false; // Tracks if password is visible
let password = document.getElementById("password");
let passwordStrength = document.getElementById("password-strength");
let hidden = document.getElementById("popover-password");
let passwordStrengthContainer = document.getElementById("password-strength-container"); // Element that contains the password strength components
let lowUpperCase = document.querySelector(".low-upper-case i");
let number = document.querySelector(".one-number i");
let specialChar = document.querySelector(".one-special-char i");
let eightChar = document.querySelector(".eight-character i");

// Toggle password visibility
function toggle() {
    if (state) {
        password.setAttribute("type", "password");
        state = false;
    } else {
        password.setAttribute("type", "text");
        state = true;
    }
}

// Toggle the eye icon
function myFunction(show) {
    show.classList.toggle("fa-eye-slash");
}

// Show the password strength bar and validation messages
function showPasswordStrengthBar() {
    passwordStrengthContainer.classList.remove("hidden");
}

// Check password strength based on various conditions
function checkStrength(password) {
    let strength = 0;

    // Check for lowercase and uppercase
    if (password.match(/([a-z].*[A-Z])|([A-Z].*[a-z])/)) {
        strength += 1;
        lowUpperCase.classList.remove('fa-circle');
        lowUpperCase.classList.add('fa-check');
    } else {
        lowUpperCase.classList.add('fa-circle');
        lowUpperCase.classList.remove('fa-check');
    }

    // Check for numbers
    if (password.match(/([0-9])/)) {
        strength += 1;
        number.classList.remove('fa-circle');
        number.classList.add('fa-check');
    } else {
        number.classList.add('fa-circle');
        number.classList.remove('fa-check');
    }

    // Check for special characters
    if (password.match(/([!,%,&,@@,#,$,^,*,?,_,~])/)) {
        strength += 1;
        specialChar.classList.remove('fa-circle');
        specialChar.classList.add('fa-check');
    } else {
        specialChar.classList.add('fa-circle');
        specialChar.classList.remove('fa-check');
    }

    // Check for length (between 8 and 15 characters)
    if (password.length >= 8 && password.length <= 15) {
        strength += 1;
        eightChar.classList.remove('fa-circle');
        eightChar.classList.add('fa-check');
    } else {
        eightChar.classList.add('fa-circle');
        eightChar.classList.remove('fa-check');
    }

    if (strength === 4) {
        // Clear validation messages and hide strength bar when complete
        // var messageElement = document.getElementById("password-strength-container");
        hidden.classList.add("hidden"); // Hide the whole container
    } else {
        // Show validation elements
        hidden.classList.remove("hidden"); // Ensure it's visible
    }
    updateProgressBar(strength);
}

function updateProgressBar(strength) {
    // Update the progress bar based on the strength level
    if (strength < 2) {
        passwordStrength.classList.remove("progress-bar-success", "progress-bar-warning");
        passwordStrength.classList.add("progress-bar-danger");
        passwordStrength.style.width = '10%';
    } else if (strength === 3) {
        passwordStrength.classList.remove("progress-bar-success", "progress-bar-danger");
        passwordStrength.classList.add("progress-bar-warning");
        passwordStrength.style.width = '60%';
    } else if (strength === 4) {
        passwordStrength.classList.remove("progress-bar-warning", "progress-bar-danger");
        passwordStrength.classList.add("progress-bar-success");
        passwordStrength.style.width = '100%';
    }
}

password.addEventListener("keyup", function () {
    let pass = password.value;

    if (pass.length === 0) {
        // Hide the strength bar and reset if password is empty
        passwordStrength.style.width = '0%';
        passwordStrengthContainer.classList.add("hidden");
    } else {
        showPasswordStrengthBar(); // Show the strength bar when text is entered
        checkStrength(pass); // Check the password's strength
    }
});
function handlePasswordChange() {
    let pass = password.value;

    if (pass.length === 0) {
        // Hide the strength bar and reset if the password is empty
        passwordStrength.style.width = '0%';
        passwordStrengthContainer.classList.add("hidden");
    } else {
        // Show the strength bar when text is entered
        passwordStrengthContainer.classList.remove("hidden");
        checkStrength(pass); // Check the password's strength
    }

    validatePassword(); // Trigger validation to display messages
}

// Listen to events that might indicate text removal
password.addEventListener("keyup", handlePasswordChange);
password.addEventListener("cut", handlePasswordChange);
password.addEventListener("input", handlePasswordChange);
function validatePassword() {
    var password = password.value;
    var message = "";

    // Validate password length
    if (password.length < 8) {
        message = "Password must be at least 8 characters long";
    } else if (password.length > 15) {
        message = "Password must be at most 15 characters long";
    } else {
        message = "Password is of appropriate length"; // A general message
    }

    document.getElementById("passwordValidationMessage").innerText = message;
}

//RegisterButton
document.addEventListener("DOMContentLoaded", function () {
    const registrationForm = document.getElementById("registrationForm");


    registrationForm.addEventListener("submit", function (event) {
        let isValid = true;

        const requiredFields = document.querySelectorAll(
            "#registrationForm input[required], #registrationForm select[required]"
        );

        requiredFields.forEach((field) => {
            if (field.value.trim() === "") {
                field.classList.add("border-danger");
                isValid = false; // Form is invalid
            } else {
                field.classList.remove("border-danger");
            }
        });

        if (!isValid) {
            event.preventDefault();
        }
    });

    document.querySelectorAll("#registrationForm input, #registrationForm select").forEach((field) => {
        field.addEventListener("input", function () {
            if (field.value.trim() !== "") {
                field.classList.remove("border-danger"); // Remove red border when user types
            }
        });
    });
});


// Email
function EmailCheck() {
    // Clear status text after 5 seconds


    //$.post("@Url.Action("CheckEmailAvailability", "EmailVarification")",
    //    {
    //        Email: $("#Email").val()
    //    },
    //    function (data) {
    //        if (data == 0) {
    //            setTimeout(function () {
    //                $("#Statu").html('<font color="Green">Available! You can take it.</font>');
    //                $("#Email").css("border-color", "Green");
    //            }, 7000);

    //        } else {
    //            $("#Statu").html('<font color="Red">Email already in use. Try another.</font>');
    //            $("#Email").css("border-color", "Red");
    //        }
    //    }
    //);

    // Improved code with suggestions
    $.ajax({
        type: "POST",
        url: "CheckEmailAvailability, EmailVarification",
        data: { Email: $("#Email").val() },
        success: function (response) {
            const statusElement = $("#Statu");
            const emailElement = $("#Email");
            const delay = 7000; // define delay as a constant

            if (response === 0) {
                setTimeout(() => {
                    statusElement.html(`<span style="color: green">Available! You can take it.</span>`); // use span instead of font
                    emailElement.css("border-color", "green");
                }, delay);
            } else {
                statusElement.html(`<span style="color: red">Email already in use. Try another.</span>`); // use span instead of font
                emailElement.css("border-color", "red");
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            // Handle errors here
        }
    });
}
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

//UserName
function userCheck(input) {

    // Improved code with suggestions
  

    $.ajax({
        type: "POST",
        url: "CheckuserAvailability, EmailVarification",
        data: { username: $("#username").val() },
        success: function (response) {
            const statusElement = $("#Status");
            const usernameElement = $("#username");

            if (response === 0) {
                statusElement.html(`<span style="color: green">Available !. you can take it.</span>`); // use span instead of font
                usernameElement.css("border-color", "green");
            } else {
                statusElement.html(`<span style="color: red">username already in use. Try another.</span>`); // use span instead of font
                usernameElement.css("border-color", "red");
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            // Handle errors here
        }
    });
}


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

//PhoneNumber
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

    validatePhoneNumber(); // Validate the input after pasting
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

//ctrystatecity
// Cascading dropdown logic
