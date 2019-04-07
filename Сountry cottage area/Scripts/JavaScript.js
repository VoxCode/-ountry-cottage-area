
$('.menu-btn').click(function (e) {
    e.preventDefault();
    $(this).toggleClass('menu-btn_active');
    $('.menu').toggleClass('menu_active');
    $('.content').toggleClass('content_active');
});

// Guide Tool List

$('.vegetables').hide();
$('.fruits').hide();

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

$('.vegetables').click(function (event) {

    var id = $(event.target).attr("id");
    makeRequestAjax(id);  
    
});

$('.fruits').click(function (event) {

    var id = $(event.target).attr("id");
    makeRequestAjax(id);

});

