


document.addEventListener("DOMContentLoaded", function () {
    const account = document.getElementById("account");


    account.addEventListener("submit", function (event) {
        let isValid = true;

        const requiredFields = document.querySelectorAll(
            "#account input[required], #account select[required]"
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

    document.querySelectorAll("#account input, #account select").forEach((field) => {
        field.addEventListener("input", function () {
            if (field.value.trim() !== "") {
                field.classList.remove("border-danger"); 
            }
        });
    });
  

});
