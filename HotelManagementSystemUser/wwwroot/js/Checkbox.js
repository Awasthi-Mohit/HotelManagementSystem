// Get the checkboxes and price input element
const checkboxes = document.querySelectorAll('input[name="SelectedHotel"]');
const priceInput = document.getElementById('booking-price');

// Add an event listener to each checkbox
checkboxes.forEach((checkbox) => {
    checkbox.addEventListener('change', (event) => {
        // Get the price of the selected room
        const price = event.target.getAttribute('data-price');

        // Update the price input value
        priceInput.value = price;
    });
});
checkboxes.forEach((checkbox) => {
    checkbox.addEventListener('change', (event) => {
        // Check if the checkbox is checked
        const isChecked = event.target.checked;

        // If the checkbox is checked, update the price input value with the price of the selected room
        if (isChecked) {
            const price = event.target.getAttribute('data-price');
            priceInput.value = price;
        }
        // If the checkbox is not checked, clear the price input value
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