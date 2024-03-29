﻿document.addEventListener("DOMContentLoaded", function () {

    // Hide sideNav
    $('.button-collapse').sideNav({
        menuWidth: 300, // Default is 300
        edge: 'left', // Choose the horizontal origin
        closeOnClick: false, // Closes side-nav on <a> clicks, useful for Angular/Meteor
        draggable: true // Choose whether you can drag to open on touch screens
    });
    $(document).ready(function () {
        $('.tooltipped').tooltip({ delay: 50 });
    });
    $('select').material_select();
    $('.collapsible').collapsible();

});