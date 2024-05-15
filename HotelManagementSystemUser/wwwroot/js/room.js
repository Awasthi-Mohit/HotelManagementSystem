const checkboxes = document.querySelectorAll('input[name="SelectedHotel"]');
const priceInput = document.getElementById('booking-price');

checkboxes.forEach((checkbox) => {
    checkbox.addEventListener('change', (event) => {
        const price = event.target.getAttribute('data-price');

        priceInput.value = price;
    });
});
checkboxes.forEach((checkbox) => {
    checkbox.addEventListener('change', (event) => {
        const isChecked = event.target.checked;

        if (isChecked) {
            const price = event.target.getAttribute('data-price');
            priceInput.value = price;
        }
        else {
            priceInput.value = '';
        }
    });
});
let totalPrice = 0;

checkboxes.forEach((checkbox) => {
    checkbox.addEventListener('change', (event) => {
        const isChecked = event.target.checked;

        if (isChecked) {
            const price = event.target.getAttribute('data-price');
            totalPrice += parseInt(price);
        }
        else {
            const price = event.target.getAttribute('data-price');
            totalPrice -= parseInt(price);
        }

        priceInput.value = totalPrice;
    });
});
document.getElementById('roomCount').addEventListener('change', function () {
    // Update the number of rooms here
    console.log('Number of rooms changed to:', this.value);
});
document.querySelectorAll('.card').forEach(card => {
    card.addEventListener('click', function () {
        // Toggle the selection by adding/removing a CSS class
        this.classList.toggle('selected');
    });
});

$(document).ready(function () {
    // Apply phone number mask
    $('#phone').mask('(000) 000-0000', { placeholder: '(___) ___-____' });

    // Update hidden field on input change
    $('#phone').on('input', function () {
        // Remove non-digit characters and update hidden field
        var formattedPhoneNumber = $(this).val().replace(/\D/g, '');
        $('input[name="Input.PhoneNumber"]').val(formattedPhoneNumber);
    });
});