/* 
    INITIALISATIONS
*/

/* Masonry */

$('.grid').packery({
    percentPosition: true,
    columnWidth: '.grid-sizer',
    itemSelector: '.grid-item',
    gutter: '.grid-gutter-sizer',
});

$(document).ready(function () {
    $('#loading').fadeOut(700);
    $('.quote').fadeIn(2000);
});

//$.jInvertScroll(['#cloud1', '#cloud2', '#cloud3']);
$("#cloud1").click(function () {
    
});


//grid.imagesLoaded().progress(function () {
//    grid.masonry('layout');
//});