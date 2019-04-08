
$('.menu-btn').click(function (e) {
    e.preventDefault();
    $(this).toggleClass('menu-btn_active');
    $('.menu').toggleClass('menu_active');
    $('.content').toggleClass('content_active');
});

// Guide Tool List

$('.vegetables').hide();
$('.greenery').hide();
$('.berries').hide();

$('.guideToolList h3').click(function () {

    var findArticle = $(this).next();
    var findWrapper = $(this).closest('.guideToolList');

    if (findArticle.is(':visible')) {
        findArticle.slideUp('fast');
    }
    else {
        findWrapper.find('>div').slideUp('fast');
        findArticle.slideDown('fast');
    }
});

// Guide Click Handler

$('.vegetables a').click(function (event) {

    var id = $(event.target).attr("id");
    makeRequestAjax(id);  
    
});

$('.greenery a').click(function (event) {

    var id = $(event.target).attr("id");
    makeRequestAjax(id);

});

$('.berries a').click(function (event) {

    var id = $(event.target).attr("id");
    makeRequestAjax(id);

});

