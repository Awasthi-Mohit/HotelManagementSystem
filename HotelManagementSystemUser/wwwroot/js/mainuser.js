//phonenumber
const phoneNumberInput = document.getElementById('pickupPhoneNumber');
const phoneNumberLengthMessage = document.getElementById('phoneNumberLengthMessage');

phoneNumberInput.addEventListener('input', () => {
    const value = phoneNumberInput.value.trim();
    const onlyNumbers = /^[0-9]*$/;

    if (value.length > 0 && !onlyNumbers.test(value)) {
        phoneNumberInput.value = value.replace(/\D/g, ''); // Remove non-numeric characters
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
    setTimeout(() => {
        const pastedText = phoneNumberInput.value;
        const onlyNumbers = /^[0-9]*$/;

        if (!onlyNumbers.test(pastedText)) {
            phoneNumberInput.value = pastedText.replace(/\D/g, ''); // Remove non-numeric characters
        }
    }, 0);
});
//POSTALCODE
document.getElementById('postalCodeInput').addEventListener('input', function () {
    var postalCodeInput = this.value;
    var postalCodeError = document.getElementById('postalCodeError');

    // Regular expression to allow only alphanumeric characters
    var alphanumericRegex = /^[a-zA-Z0-9]*$/;

    if (!alphanumericRegex.test(postalCodeInput)) {
        postalCodeError.style.display = 'block';
    } else {
        postalCodeError.style.display = 'none';
    }
});
/*COUNTRY*/
document.getElementById('countryDropdown').addEventListener('change', function () {
    var countryId = this.value;
    var stateDropdown = document.getElementById('stateDropdown');
    var cityDropdown = document.getElementById('cityDropdown');

    stateDropdown.innerHTML = '<option value="">Select State</option>';
    cityDropdown.innerHTML = '<option value="">Select City</option>';

    if (countryId) {
        stateDropdown.disabled = false;

        // Filter states based on the selected country
        var states = @Json.Serialize(Model.States);
        var filteredStates = states.filter(function (state) {
            return state.countryId == countryId;
        });

        filteredStates.forEach(function (state) {
            var option = document.createElement('option');
            option.value = state.stateId;
            option.text = state.stateName;
            stateDropdown.add(option);
        });
    } else {
        stateDropdown.disabled = true;
        cityDropdown.disabled = true;
    }
});

document.getElementById('stateDropdown').addEventListener('change', function () {
    var stateId = this.value;
    var cityDropdown = document.getElementById('cityDropdown');
    cityDropdown.innerHTML = '<option value="">Select City</option>';

    if (stateId) {
        cityDropdown.disabled = false;

        // Filter cities based on the selected state
        var cities = @Json.Serialize(Model.Cities);
        var filteredCities = cities.filter(function (city) {
            return city.stateId == stateId;
        });

        filteredCities.forEach(function (city) {
            var option = document.createElement('option');
            option.value = city.cityId;
            option.text = city.cityName;
            cityDropdown.add(option);
        });
    } else {
        cityDropdown.disabled = true;
    }
});