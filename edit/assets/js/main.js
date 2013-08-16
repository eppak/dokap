jQuery(document).ready(function () {
   
});

$(window).on('beforeunload', function () {
    $('#loaderLayer').css("height", $(window).height());
    $('#loaderLayer').css("width", "100%");
    $('#loaderLayer').fadeIn(40);
    console.log("dsflsdf");
});