$(document).ready(function () {
    $('#btnSubmit').on('click', function () {
         var selectedItems = []; // Array to hold the IDs of checked checkboxes
        var checkboxes = document.querySelectorAll('input[name="selectedItems"]:checked');
        checkboxes.forEach(function (checkbox) {
            selectedItems.push(checkbox.id); // Push the ID of checked checkboxes
        });
        var data = $('#CoursesForm').serialize();
        // Now you can use the selectedItems array to get the IDs of the checked items

        // Rest of your AJAX code
        $.ajax({
            url: '/Booking/Bookings?selectedItems=' + selectedItems.join(','),
            type: 'POST',
            data: data ,
            success: function (response) {
                console.log('Success');
            },
            error: function (xhr) {
                console.log(xhr);
            }
        });
    });
});

document.getElementById('txtEmailId').addEventListener('input', function (event) {
    var inputElement = event.target;
    var inputValue = inputElement.value;

    // Check if the input value is not empty
    if (inputValue.trim()) {
        var cursorPosition = inputElement.selectionStart; 

        var regex = /^[A-Za-z\s,"']+$/; 
        if (!regex.test(inputValue)) {
            var filteredValue = inputValue.replace(/[^A-Za-z\s,']/g, ''); 

       
            var newPos = cursorPosition - (inputValue.length - filteredValue.length);
            inputElement.value = filteredValue;

          
            inputElement.setSelectionRange(newPos, newPos);
        }

        var length = inputValue.length;

        // Get the message element
        var messageElement = document.getElementById('message');

        if (length < 3) {
            messageElement.textContent = 'Minimum 3 characters required.';
            messageElement.style.color = 'red'; // Set the text color to red
        } else {
            messageElement.textContent = 'Maximum 50 characters required.';
            messageElement.style.color = 'black'; // Reset the text color to black
        }
    }
});

$(function () {
    $("#txtname").bind('paste', function (e) {
        var inputElement = this;
        var inputValue = inputElement.value;
        var cursorPosition = inputElement.selectionStart; // Store the cursor position

        // Get the pasted data
        var pastedData = e.originalEvent.clipboardData.getData('text');

        // Append the pasted data
        inputElement.value = inputValue.substring(0, cursorPosition) + pastedData + inputValue.substring(inputElement.selectionEnd);

        // Validate the input
        var modifiedData = inputElement.value.replace(/[^A-Za-z\s,'"]/g, '');
        inputElement.value = modifiedData;

        // Determine the new cursor position after pasting
        var newPos = cursorPosition + (pastedData.length - (inputElement.value.length - inputValue.length));

        // Restore the cursor position
        inputElement.setSelectionRange(newPos, newPos);
    });
});
$(function () {
    $("#txtname").bind('paste', function (e) {
        var inputElement = this;
        var inputValue = inputElement.value;
        var cursorPosition = inputElement.selectionStart; // Store the cursor position

        // Get the pasted data
        var pastedData = e.originalEvent.clipboardData.getData('text');

        // Append the pasted data
        inputElement.value = inputValue.substring(0, cursorPosition) + pastedData + inputValue.substring(inputElement.selectionEnd);

        // Validate the input
        var modifiedData = inputElement.value.replace(/[^A-Za-z\s,'"]/g, ''); // Allow spaces by including '\s' in the regex
        inputElement.value = modifiedData;

        // Determine the new cursor position after pasting
        var newPos = cursorPosition + (pastedData.length - (inputElement.value.length - inputValue.length));

        // Restore the cursor position
        inputElement.setSelectionRange(newPos, newPos);
    });
});
//review
        .rating {

    display: flex;

    width: 100 %;

    justify - content: center;

    overflow: hidden;

    flex - direction: row - reverse;

    height: 150px;

    position: relative;

}
 
.rating - 0 {

    filter: grayscale(100 %);

}
 
.rating > input {

    display: none;

}
 
.rating > label {

    cursor: pointer;

    width: 40px;

    height: 40px;

    margin - top: auto;

    background - image: url("data:image/svg+xml;charset=UTF-8,%3csvg xmlns='http://www.w3.org/2000/svg' width='126.729' height='126.73'%3e%3cpath fill='%23e3e3e3' d='M121.215 44.212l-34.899-3.3c-2.2-.2-4.101-1.6-5-3.7l-12.5-30.3c-2-5-9.101-5-11.101 0l-12.4 30.3c-.8 2.1-2.8 3.5-5 3.7l-34.9 3.3c-5.2.5-7.3 7-3.4 10.5l26.3 23.1c1.7 1.5 2.4 3.7 1.9 5.9l-7.9 32.399c-1.2 5.101 4.3 9.3 8.9 6.601l29.1-17.101c1.9-1.1 4.2-1.1 6.1 0l29.101 17.101c4.6 2.699 10.1-1.4 8.899-6.601l-7.8-32.399c-.5-2.2.2-4.4 1.9-5.9l26.3-23.1c3.8-3.5 1.6-10-3.6-10.5z'/%3e%3c/svg%3e");

    background - repeat: no - repeat;

    background - position: center;

    background - size: 76 %;

    transition: .3s;

}
 
.rating > input:checked ~label,

.rating > input:checked ~label ~label {

    background - image: url("data:image/svg+xml;charset=UTF-8,%3csvg xmlns='http://www.w3.org/2000/svg' width='126.729' height='126.73'%3e%3cpath fill='%23fcd93a' d='M121.215 44.212l-34.899-3.3c-2.2-.2-4.101-1.6-5-3.7l-12.5-30.3c-2-5-9.101-5-11.101 0l-12.4 30.3c-.8 2.1-2.8 3.5-5 3.7l-34.9 3.3c-5.2.5-7.3 7-3.4 10.5l26.3 23.1c1.7 1.5 2.4 3.7 1.9 5.9l-7.9 32.399c-1.2 5.101 4.3 9.3 8.9 6.601l29.1-17.101c1.9-1.1 4.2-1.1 6.1 0l29.101 17.101c4.6 2.699 10.1-1.4 8.899-6.601l-7.8-32.399c-.5-2.2.2-4.4 1.9-5.9l26.3-23.1c3.8-3.5 1.6-10-3.6-10.5z'/%3e%3c/svg%3e");

}
 
 
.rating > input: not(: checked) ~label: hover,

.rating > input: not(: checked) ~label:hover ~label {

    background - image: url("data:image/svg+xml;charset=UTF-8,%3csvg xmlns='http://www.w3.org/2000/svg' width='126.729' height='126.73'%3e%3cpath fill='%23d8b11e' d='M121.215 44.212l-34.899-3.3c-2.2-.2-4.101-1.6-5-3.7l-12.5-30.3c-2-5-9.101-5-11.101 0l-12.4 30.3c-.8 2.1-2.8 3.5-5 3.7l-34.9 3.3c-5.2.5-7.3 7-3.4 10.5l26.3 23.1c1.7 1.5 2.4 3.7 1.9 5.9l-7.9 32.399c-1.2 5.101 4.3 9.3 8.9 6.601l29.1-17.101c1.9-1.1 4.2-1.1 6.1 0l29.101 17.101c4.6 2.699 10.1-1.4 8.899-6.601l-7.8-32.399c-.5-2.2.2-4.4 1.9-5.9l26.3-23.1c3.8-3.5 1.6-10-3.6-10.5z'/%3e%3c/svg%3e");

}
 
.emoji - wrapper {

    width: 100 %;

    text - align: center;

    height: 100px;

    overflow: hidden;

    position: absolute;

    top: 0;

    left: 0;

}
 
    .emoji - wrapper: before,

    .emoji - wrapper:after {

    content: "";

    height: 15px;

    width: 100 %;

    position: absolute;

    left: 0;

    z - index: 1;

}
 
    .emoji - wrapper:before {

    top: 0;

    background: linear - gradient(to bottom, rgba(255, 255, 255, 1) 0 %, rgba(255, 255, 255, 1) 35 %, rgba(255, 255, 255, 0) 100 %);

}
 
    .emoji - wrapper:after {

    bottom: 0;

    background: linear - gradient(to top, rgba(255, 255, 255, 1) 0 %, rgba(255, 255, 255, 1) 35 %, rgba(255, 255, 255, 0) 100 %);

}
 
.emoji {

    display: flex;

    flex - direction: column;

    align - items: center;

    transition: .3s;

}
 
    .emoji > svg {

    margin: 15px 0;

    width: 70px;

    height: 70px;

    flex - shrink: 0;

}

#rating - 1:checked ~ .emoji - wrapper > .emoji,

    [id ^= "rating-1-"]:checked ~ .emoji - wrapper > .emoji {

    transform: translateY(-100px);

}

#rating - 2:checked ~ .emoji - wrapper > .emoji,

    [id ^= "rating-2-"]:checked ~ .emoji - wrapper > .emoji {

    transform: translateY(-200px);

}

#rating - 3:checked ~ .emoji - wrapper > .emoji,

    [id ^= "rating-3-"]:checked ~ .emoji - wrapper > .emoji {

    transform: translateY(-300px);

}

#rating - 4:checked ~ .emoji - wrapper > .emoji,

    [id ^= "rating-4-"]:checked ~ .emoji - wrapper > .emoji {

    transform: translateY(-400px);

}

#rating - 5:checked ~ .emoji - wrapper > .emoji,

    [id ^= "rating-5-"]:checked ~ .emoji - wrapper > .emoji {

    transform: translateY(-500px);

}
 
 
.feedback {

    max - width: 360px;

    background - color: #fff;

    width: 100 %;

    border - radius: 8px;

    display: flex;

    flex - direction: column;

    flex - wrap: wrap;

    align - items: center;

    box - shadow: 0 4px 30px rgba(0, 0, 0, .05);

}

.review_section {

    box - sizing: border - box;

    font - family: 'Raleway', sans - serif!important;

    width: 80vw;

    margin: 0 auto;

}
 
    .review_section *,
        
    .review_section *:: before,

    .review_section *::after {

    box - sizing: inherit;

}
 
        .review_section *: focus, .review_section *:active {

    outline: 0!important;

}
 
    .review_section * {

        - webkit - tap - highlight - color: rgba(0, 0, 0, 0)!important;

    }
 
    .review_section body, .review_section td, .review_section th, .review_section p {

    color: #333;

    font - family: 'Raleway', sans - serif;

}
 
    .review_section body {

    background - color: #fdfdfd;

    margin: 0;

    position: relative;

}
 
    .review_section h2 {

    display: inline - block;

}
 
    .review_section #review - add - btn {

    padding: 0;

    font - size: 1.6em;

    cursor: pointer;

}

    /* ====================== Review Form ====================== */

    .review_section #modal {

    /* position: absolute;

  left: 10vh;

  top: 10vh; */

    /* fix exactly center: https://css-tricks.com/considerations-styling-modal/ */

    /* begin css tricks */

    position: fixed;

    top: 50 %;

    left: 50 %;

    transform: translate(-50 %, -50 %);

    /* end css tricks */

    /* z-index: -10; */

    z - index: 3;

    display: flex;

    flex - direction: column;

    /* width: 80vw; */

    /* height: 80vh; */

    border: 1px solid #666;

    border - radius: 10px;

    opacity: 0;

    transition: all .3s;

    overflow: hidden;

    background - color: #eee;

    /* visibility: hidden; */

    display: none;

}
 
        .review_section #modal.show {

    /* visibility: visible;   */

    opacity: 1;

    /* z-index: 10; */

    display: flex;

}
 
    .review_section.modal - overlay {

    width: 100 %;

    height: 100 %;

    z - index: 2; /* places the modalOverlay between the main page and the modal dialog */

    background - color: #000;

    opacity: 0;

    transition: all .3s;

    position: fixed;

    top: 0;

    left: 0;

    display: none;

    margin: 0;

    padding: 0;

}
 
 
    .review_section form {

    width: 70vw;

    padding: 0 20px 20px 20px;

}
 
    .review_section input,

    .review_section select, .review_section.rate, .review_section textarea, .review_section button {

    background: #f9f9f9;

    border: 1px solid #e5e5e5;

    border - radius: 8px;

    box - shadow: inset 0 1px 1px #e1e1e1;

    font - size: 16px;

    padding: 8px;

}
 
        .review_section input[type = "radio"] {

    box - shadow: none;

    outline: 0!important;

}
 
    .review_section button {

    min - width: 48px;

    min - height: 48px;

}
 
        .review_section button:hover {

    border: 1px solid #ccc;

    background - color: #fff;

}
 
        .review_section button#review - add - btn,

        .review_section button.close - btn,

        .review_section button#submit - review - btn {

    min - height: 40px;

}
 
        .review_section button#submit - review - btn {

    font - weight: bold;

    cursor: pointer;

    padding: 0 16px;

}
 
    .review_section.fieldset {

    margin - top: 20px;

}
 
    .review_section.right {

    align - self: flex - end;

}
 
    .review_section #review - form - container {

    width: 100 %;

    /* background-color: #eee; */

    padding: 0 20px 26px;

    color: #333;

    overflow - y: auto;

}
 
        .review_section #review - form - container h2 {

    margin: 0 0 0 6px;

}
 
    .review_section #review - form {

    display: flex;

    flex - direction: column;

    background: #fff;

    border: 1px solid #e5e5e5;

    border - radius: 4px;

}
 
        .review_section #review - form label, .review_section #review - form input {

    display: block;

    /* width: 100%; */

}
 
        .review_section #review - form label {

    font - weight: bold;

    margin - bottom: 5px;

}
 
        .review_section #review - form.rate label, .review_section #review - form.rate input,

        .review_section #review - form.rate1 label, .review_section #review - form.rate1 input {

    display: inline - block;

}

    /* Modified from: https://codepen.io/tammykimkim/pen/yegZRw */

    .review_section.rate {

    /* float: left; */

    /* display: inline-block; */

    height: 36px;

    display: inline - flex;

    flex - direction: row - reverse;

    align - items: flex - start;

    justify - content: flex - end;

}
 
    .review_section #review - form.rate > label {

    margin - bottom: 0;

    margin - top: -5px;

    height: 30px;

}
 
    .review_section.rate: not(: checked) > input {

    /* position: absolute; */

    top: -9999px;

    margin - left: -24px;

    width: 20px;

    padding - right: 14px;

    z - index: -10;

}
 
    .review_section.rate: not(: checked) > label {

    float: right;

    width: 1em;

    overflow: hidden;

    white - space: nowrap;

    cursor: pointer;

    font - size: 30px;

    color: #ccc;

}

    /* #star1:focus{
 
    } */

    .review_section.rate2 {

    float: none;

}
 
    .review_section.rate: not(: checked) > label::before {

    content: '★ ';

    position: relative;

    top: -10px;

    left: 2px;

}
 
    .review_section.rate > input:checked ~label {

    color: #ffc700;

    /* outline: -webkit-focus-ring-color auto 5px; */

}
 
    .review_section.rate > input: checked: focus + label, .review_section.rate > input: focus + label {

    outline: -webkit - focus - ring - color auto 5px;

}
 
    .review_section.rate: not(: checked) > label: hover,

    .review_section.rate: not(: checked) > label:hover ~label {

    color: #deb217;

    /* outline: -webkit-focus-ring-color auto 5px; */

}


    /*PEN STYLES*/

    .review_section * {

    box- sizing: border - box;

    }
 
.blog - card {

    display: flex;

    flex - direction: column;

    margin 5px 0;

    box - shadow: 0 3px 7px - 1px rgba(0, 0, 0, .1);

    margin - bottom: 1.6 %;

    background: #fff;

    line - height: 1.4;

    font - family: 'Raleway', sans - serif;

    border - radius: 5px;

    overflow: hidden;

    z - index: 0;

}
 
    .blog - card a {

    color: inherit;

}
 
    .blog - card.meta {

    position: relative;

    z - index: 0;

    height: 180px;

}
 
    .blog - card.photo {

    height: 200px;

    width: 200px;

    margin: 0;

}
 
 
    .blog - card.description {

    padding: 1rem;

    background: #fff;

    position: relative;

    z - index: 1;

}
 
        .blog - card.description h1, .blog - card.description h2 {

    font - family: 'Raleway', sans - serif;

}
 
        .blog - card.description h1 {

    line - height: 1;

    margin: 0;

    font - size: 1.7rem;

}
 
        .blog - card.description h2 {

    font - size: 1rem;

    font - weight: 300;

    text - transform: uppercase;

    color: #a2a2a2;

    margin - top: 5px;

}
 
 
    .blog - card p: first - of - type {

    margin - top: 1.25rem;

}
 
        .blog - card p: first - of - type:before {

    content: "";

    position: absolute;

    height: 5px;

    background: #5ad67d;

    width: 35px;

    margin - top: -1rem;

    border - radius: 3px;

}

@media(min - width: 640px) {

    .blog - card {

        flex - direction: row;

        max - width: 550px;

    }
 
        .blog - card.meta {

        flex - basis: 0 %;

        height: 200px;

    }
 
        .blog - card.description {

        flex - basis: 95 %;

    }
 
            .blog - card.description:before {

        content: "";

        background: #fff;

        width: 30px;

        position: absolute;

        left: -10px;

        top: 0;

        bottom: 0;

        z - index: -1;

    }
 
        .blog - card.alt {

        flex - direction: row - reverse;

    }
 
            .blog - card.alt.details {

        padding - left: 25px;

    }

}
