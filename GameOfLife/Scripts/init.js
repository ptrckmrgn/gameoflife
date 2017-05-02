﻿/* 
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

/* Google Analytics */

(function (i, s, o, g, r, a, m) {
    i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
        (i[r].q = i[r].q || []).push(arguments)
    }, i[r].l = 1 * new Date(); a = s.createElement(o),
    m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
})(window, document, 'script', 'https://www.google-analytics.com/analytics.js', 'ga');

ga('create', 'UA-83011739-1', 'auto');
ga('send', 'pageview');