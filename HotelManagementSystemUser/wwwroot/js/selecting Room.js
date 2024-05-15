let selectedRooms = {};

document.addEventListener('DOMContentLoaded', function () {
    document.querySelectorAll('input[name="selectedItems"]').forEach(function (checkbox) {
        checkbox.addEventListener('change', function () {
            let roomId = checkbox.value;
            let roomPrice = checkbox.getAttribute('data-price');

            if (checkbox.checked) {
                selectedRooms[roomId] = roomPrice;
            } else {
                delete selectedRooms[roomId];
            }

            updateSelectedRoomsDisplay();
        });
    });
});
function updateSelectedRoomsDisplay() {
    let selectedRoomsDisplay = document.getElementById("selectedRoomsDisplay");
    selectedRoomsDisplay.innerHTML = ""; // clean old card 

    let totalPaymentAllRooms = 0;

    Object.keys(selectedRooms).forEach(function (roomId) {
        let roomPrice = selectedRooms[roomId];
        let card = createRoomCard(roomId, roomPrice);

        selectedRoomsDisplay.appendChild(card);

        totalPaymentAllRooms += parseFloat(selectedRooms[roomId]);
        let checkInDate = document.getElementById("checkInDate");
        let checkOutDate = document.getElementById("checkOutDate");
        let checkInDateInput = document.getElementById('checkInDateselected_' + roomId);
        let checkOutDateInput = document.getElementById('checkOutDateselected_' + roomId);

        if (checkInDateInput.value && checkOutDateInput.value) {
            checkInDate.value = checkInDateInput.value;
            checkOutDate.value = checkOutDateInput.value;
        }
    });

    let grandTotalLabel = document.getElementById("totalPaymentLabeluser");
    if (grandTotalLabel) {
        grandTotalLabel.innerText = " Total Payment: " + totalPaymentAllRooms.toFixed(2);
    }
}


function createRoomCard(roomId, roomPrice) {
    let card = document.createElement("div");
    card.className = "card";
    card.style = "width: 18rem; margin: 10px;";

    let cardContent = `
        <div class="card-body">
            <p class="card-text">RoomId: ${roomId}</p>
            <h5 class="card-title">Room No: ${roomId}</h5>
            <p id="priceid_${roomId}" class="card-text">Price: ${roomPrice}</p>
            <p id="totalPaymentLabel_${roomId}" class="card-text">Total: 0.00</p>

            <div class="col-md-8 text-center">
                <div class="mb-4">
                    <label class="form-label">Check-In Date</label>
                    <input id="checkInDateselected_${roomId}" class="form-control" type="date"
                           onchange="updateTotalPayment('${roomId}')" />
                </div>
            </div>
            <div class="col-md-8 text-center">
                <div class="mb-4">
                    <label class="form-label">Check-Out Date</label>
                    <input id="checkOutDateselected_${roomId}" class="form-control" type="date"
                           onchange="updateTotalPayment('${roomId}')" />
                </div>
            </div>
        </div>
    `;
    card.innerHTML = cardContent;

    return card;
}
function updateTotalPayment(roomId) {
    let checkInDateInput = document.getElementById('checkInDateselected_' + roomId);
    let checkOutDateInput = document.getElementById('checkOutDateselected_' + roomId);
    let checkInDate = new Date(checkInDateInput.value);
    let checkOutDate = new Date(checkOutDateInput.value);
    let roomPrice = selectedRooms[roomId];

    if (checkInDate && checkOutDate && checkOutDate > checkInDate) {
        let difference = checkOutDate.getTime() - checkInDate.getTime();
        let duration = difference / (1000 * 60 * 60 * 24);

        if (duration > 30) {
            checkInDateInput.value = '';
            checkOutDateInput.value = '';
            alert("You can't select a duration more than 30 days.");
            return;
        }

        let totalPrice = roomPrice * duration;

        let totalPaymentLabelRoom = document.getElementById("totalPaymentLabel_" + roomId);
        totalPaymentLabelRoom.innerText = "Total: " + totalPrice.toFixed(2);
        // Retrieve existing booking data from session storage
        let bookings = JSON.parse(sessionStorage.getItem('bookings')) || [];

       //storing the data in the array
        let newBooking = {
            roomId: roomId,
            checkInDate: checkInDate.toISOString(),
            checkOutDate: checkOutDate.toISOString()
        };
        bookings.push(newBooking);

        sessionStorage.setItem('bookings', JSON.stringify(bookings));
        
        updateGrandTotal();    
    } else {
        let errorMessage = "Please select valid check-in and check-out dates.";
        console.error(errorMessage);
    }
}



function getdetailsofroom() {
    

    var jsonDataString = sessionStorage.getItem('bookings');

    // Parse the JSON string into a JavaScript array of objects
    var dataArray = JSON.parse(jsonDataString);
    var sessionDataArray = [];

  
     dataArray.forEach(function (item) {
        console.log("Room ID:", item.roomId);
        console.log("Check In Date:", item.checkInDate);
         console.log("Check Out Date:", item.checkOutDate);
         var sessionData = {
             roomId: item.roomId,
             checkInDate: item.checkInDate,
             checkOutDate: item.checkOutDate
         };
         sessionDataArray.push(sessionData);
    });
  
    fetch('/Userbooking/SaveSessionData', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(sessionDataArray)
    })
        .then(response => {
            if (response.ok) {
                
            } else {
             
            }
        })
        .catch(error => {
            console.error('Error:', error);
        });

    let bookings = JSON.parse(sessionStorage.getItem('bookings')) || [];
}


function updateGrandTotal() {
    let totalPaymentAllRooms = 0;
   
    Object.keys(selectedRooms).forEach(function (roomId) {
        let totalPriceElement = document.getElementById("totalPaymentLabel_" + roomId);
        totalPaymentAllRooms += parseFloat(totalPriceElement.innerText.split(":")[1].trim());
    });

    let grandTotalLabel = document.getElementById("totalPaymentLabeluser");
    grandTotalLabel.innerText = " Total Payment: " + totalPaymentAllRooms.toFixed(2);

    let hiddenInput = document.getElementById("totalAmountInput");
    hiddenInput.value = totalPaymentAllRooms.toFixed(2);

    let totalAmountDisplay = document.getElementById("totalAmountDisplay");
    if (totalAmountDisplay) {
        totalAmountDisplay.innerText = totalPaymentAllRooms.toFixed(2);
    }
  
}


document.getElementById("searchButton").addEventListener("click", function () {
  
    var fromDate = document.getElementById("fromDate").value;
    var toDate = document.getElementById("toDate").value;

    var sessionData = {
        fromDate: fromDate,
        toDate: toDate
    };
    fetch('/Userbooking/CheckAvailability', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(sessionData) 
              

    })
        .then(response => response.json())
        .then(data => {
            console.log(data);
        })
        .catch(error => {
            console.error('Error:', error);
        });
});














/*Calendar using the JavaScript*/
document.addEventListener('DOMContentLoaded', function () {
    var calendarEl = document.getElementById('calendar');
    var calendar = new FullCalendar.Calendar(calendarEl, {

        initialView: 'dayGridMonth',
    });
    calendar.render();
});


//fix Code
//function updateTotalPaymentuser(roomId) {
//    let checkInDate = new Date(document.getElementById('checkInDateselected_' + roomId).value);
//    let checkOutDate = new Date(document.getElementById('checkOutDateselected_' + roomId).value);
//    let roomPrice = selectedRooms[roomId];
//    let duration = (checkOutDate - checkInDate) / (1000 * 60 * 60 * 24);
//    let totalPrice = roomPrice * duration;

//    let totalPaymentLabelRoom = document.getElementById("totalPaymentLabel_" + roomId);
//    totalPaymentLabelRoom.innerText = "Total: " + totalPrice.toFixed(2)
//}
// Function to update the Grand Total
//function updateGrandTotal() {
//    // Get all selected room elements
//    var selectedRooms = document.querySelectorAll("#selectedRoomsDisplay div");

//    // Initialize total payment
//    var totalPayment = 0;

//    // Loop through each selected room and add its price to total payment
//    selectedRooms.forEach(function (room) {
//        var price = parseFloat(room.getAttribute("data-price"));
//        totalPayment += price;
//    });

//    // Update the Grand Total display
//    document.getElementById("grandtotal").textContent = totalPayment.toFixed(2);
//}

// Call the function initially to display the Grand Total


//function updateGrandTotal() {
//    let totalPaymentAllRooms = 0;

//    // Calculate the total payment for all selected rooms
//    Object.keys(selectedRooms).forEach(function (roomId) {
//        let totalPriceElement = document.getElementById("totalPaymentLabel_" + roomId);
//        totalPaymentAllRooms += parseFloat(totalPriceElement.innerText.split(":")[1].trim());
//    });

//    let grandTotalLabel = document.getElementById("totalPaymentLabeluser");
//    grandTotalLabel.innerText = "Grand Total: " + totalPaymentAllRooms.toFixed(2);
//}


//function updateTotalPayment(roomId) {
//    let checkInDate = new Date(document.getElementById('checkInDateselected_' + roomId).value);
//    let checkOutDate = new Date(document.getElementById('checkOutDateselected_' + roomId).value);
//    let roomPrice = selectedRooms[roomId];

//    // Check if both dates are selected and valid then if satement otherwise else condition
//    if (checkInDate && checkOutDate && checkOutDate > checkInDate) {
//        let duration = (checkOutDate - checkInDate) / (1000 * 60 * 60 * 24);
//        let totalPrice = roomPrice * duration;

//        let totalPaymentLabelRoom = document.getElementById("totalPaymentLabel_" + roomId);  //the totalPaymentLabel_ is the id of price
//        totalPaymentLabelRoom.innerText = "Total: " + totalPrice.toFixed(2);

//        updateGrandTotal();
//    } else {
//        console.error("Invalid dates");
//    }
//}


//function updateTotalPayment(roomId) {
//    let checkInDateInput = document.getElementById('checkInDateselected_' + roomId);
//    let checkOutDateInput = document.getElementById('checkOutDateselected_' + roomId);
//    let checkInDate = new Date(checkInDateInput.value);
//    let checkOutDate = new Date(checkOutDateInput.value);
//    let roomPrice = selectedRooms[roomId];

//    if (checkInDate && checkOutDate && checkOutDate > checkInDate) {

//        let difference = checkOutDate.getTime() - checkInDate.getTime();

//        let duration = difference / (1000 * 60 * 60 * 24);


//        if (duration > 30) {

//            checkInDateInput.value = '';
//            checkOutDateInput.value = '';

//            alert("You can't select a duration more than 30 days.");

//            return;
//        }

//        let totalPrice = roomPrice * duration;

//        let totalPaymentLabelRoom = document.getElementById("totalPaymentLabel_" + roomId);
//        totalPaymentLabelRoom.innerText = "Total: " + totalPrice.toFixed(2);

//        updateGrandTotal();
//    } else {
//        let errorMessage = "Please select valid check-in and check-out dates.";
//        console.error(errorMessage);
//    }
//}

//function updateLocalStorage(roomId, checkInDate, checkOutDate) {
//    //// Retrieve existing data from local storage
//    //let bookings = JSON.parse(localStorage.getItem('bookings')) || [];

//    //// Add new booking data
//    //bookings.push({ roomId: roomId, checkInDate: checkInDate, checkOutDate: checkOutDate });

//    //// Save updated data back to local storage
//    //localStorage.setItem('bookings', JSON.stringify(bookings));
//}


