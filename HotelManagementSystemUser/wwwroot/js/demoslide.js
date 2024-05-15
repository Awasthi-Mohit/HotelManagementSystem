$(document).ready(function() {
  $('.sidebarCollapse').on('click', function() {
    if ($('#sidebar').hasClass('active')) {
      $('.collapse').collapse('show');
      $('li').addClass('active');
    } else {
      $('li').removeClass('active');
      $('.collapse.in').collapse('hide');
    }
    $('#sidebar').toggleClass('active');
  });
  $('[data-toggle="collapse"]').click(function() {
    if ($('#sidebar').hasClass('active')) {
      $('#sidebar').toggleClass('active');
    }
    if ($('li').hasClass('active')) {
      $('li').removeClass('active');
    }
    $('.collapse.in').collapse('hide');
  });
});
