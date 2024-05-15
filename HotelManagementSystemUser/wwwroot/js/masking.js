var phoneInput = document.getElementById('phone');
var countryCodeSelect = document.getElementById('countryCode');
var phoneInput = document.getElementById('phone');
phoneInput.addEventListener('input', function (e) {
    var x = e.target.value.replace(/\D/g, '').match(/(\d{0,3})(\d{0,3})(\d{0,4})/);
    e.target.value = !x[2] ? x[1] : 'z(' + x[1] + ') ' + x[2] + (x[3] ? '-' + x[3] : '');
});

phoneInput.addEventListener('input', function (e) {
    var countryCode = countryCodeSelect.value; // Get the selected country code
    var phoneNumber = e.target.value.replace(/\D/g, ''); // Remove non-numeric characters

    // Apply mask based on country code
    if (countryCode === '+91') {
        // US Phone Number Mask (e.g., (123) 456-7890)
        var formattedNumber = phoneNumber.match(/(\d{0,3})(\d{0,3})(\d{0,4})/);
        e.target.value = !formattedNumber[2] ? formattedNumber[1] : '(' + formattedNumber[1] + ') ' + formattedNumber[2] + (formattedNumber[3] ? '-' + formattedNumber[3] : '');
    } else if (countryCode === '+1') {
        // UK Phone Number Mask (e.g., 01234 567890)
        var formattedNumber = phoneNumber.match(/(\d{0,5})(\d{0,6})/);
        e.target.value = !formattedNumber[2] ? formattedNumber[1] : formattedNumber[1] + ' ' + formattedNumber[2];
    }
  
});