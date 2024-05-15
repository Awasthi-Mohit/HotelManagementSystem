$(document).ready(function () {
    const slides = $('.slide');
    const prevBtn = $('#prevBtn');
    const nextBtn = $('#nextBtn');
    let currentIndex = 0;
    let intervalId;

    function showSlide(index) {
        slides.fadeOut(); // Fade out all slides
        slides.eq(index).fadeIn(); // Fade in the slide at the given index
    }

    function nextSlide() {
        currentIndex = (currentIndex === slides.length - 1) ? 0 : currentIndex + 1;
        showSlide(currentIndex);
    }

    function prevSlide() {
        currentIndex = (currentIndex === 0) ? slides.length - 1 : currentIndex - 1;
        showSlide(currentIndex);
    }

    function startAutoSlide() {
        intervalId = setInterval(rotateSlides, 3000); // Change slide every 3 seconds (3000 milliseconds)
    }

    function stopAutoSlide() {
        clearInterval(intervalId);
    }

    function rotateSlides() {
        nextSlide();
    }

    prevBtn.click(function () {
        prevSlide();
        stopAutoSlide(); // Stop auto sliding when manual navigation occurs
    });

    nextBtn.click(function () {
        nextSlide();
        stopAutoSlide(); // Stop auto sliding when manual navigation occurs
    });

    showSlide(currentIndex);
    startAutoSlide(); // Start automatic sliding when the page loads
});

