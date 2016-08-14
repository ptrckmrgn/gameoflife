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

//grid.imagesLoaded().progress(function () {
//    grid.masonry('layout');
//});