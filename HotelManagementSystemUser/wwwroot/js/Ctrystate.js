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