function draw() {

    var map = new Array();
    map = $('#hiddenInput1').val();

    if (map != "null") {
        map = JSON.parse(map);
        $('.appMap').append(map[0]);
    }
    else {
        for (var i = 0; i < 65; i++) {
            for (var j = 0; j < 125; j++) {
                $('.appMap').append('<div class="square"></div>');
            }
            $('.appMap').append('<br />');
        }
    }
}

draw();

// Mouse clicks handler

$('.appParent').bind('contextmenu', function (e) {
    return false;
});

$('.appParent').on('selectstart', function (event) {
    event.preventDefault();
});

var doubleClickBlock = false;
var agriId;
var block = false;

$(document).ready(clickHandler());

function clickHandler() {

    var scr = $('.appMap');
    var click = $('.square');

    scr.mousedown(function (r) {
        if (r.button == 0 && clickCounter == (-2)) {
            var startX = this.scrollLeft + event.pageX;

            scr.mousemove(function () {

                this.scrollLeft = startX - event.pageX;
            });
        }
    });

    click.mousedown(function (r) {
        $('.context-menu').remove();

        if (r.button == 0 && clickCounter != (-2)) {

            if ($(this).attr('id') >= 0) {

                var n = $(this).index('.appMap div');
                console.log(n);
                console.log($('.appMap div').eq(n).attr('class'));
                //console.log($(":eq("+ n+ ")").attr('class'));
            }

            else if (clickCounter >= 18 && clickCounter <= 50) {

                $(this).toggleClass();
                $(this).toggleClass('square' + texturesHandler());
                $(this).attr('id', agriId);

                click.mouseenter(function () {

                    if ($(this).attr('id') >= 0) { click.off("mouseenter"); }
                    else {
                        $(this).toggleClass();
                        $(this).toggleClass('square' + texturesHandler());
                        $(this).attr('id', agriId);
                    }
                });
            }

            else {
                $(this).toggleClass();
                $(this).toggleClass('square' + texturesHandler());

                click.mouseenter(function () {

                    if ($(this).attr('id') >= 0) { click.off("mouseenter"); }
                    else {
                        $(this).toggleClass();
                        $(this).toggleClass('square' + texturesHandler());
                    }
                });
            }
        }
    });

    click.mousedown(function (r) {
        $('.context-menu').remove();

        if (r.button == 2 && $(this).attr('id') >= 0 && block == false) {

            $('<div/>', {
                class: 'context-menu'
            }).css({
                left: event.pageX + 'px',
                top: event.pageY + 'px'
            })
                .appendTo('.appParent')
                .append(
                    $('<ul/>')
                        .append('<li><a href="#" id="agriInfo">Информация</a></li>')
                        .append('<li><a href="#" id="agriDel">Удалить</a></li>'))
                .show('fast');

            var arr = new Array();
            arr[0] = Number($(this).attr('class').match(/\d+/));
            arr[1] = $(this).attr('id');
            contextMenuClick(arr);
        }
    });

    $(window).mouseup(function () {
        scr.off("mousemove");
    });

    $(window).mouseup(function () {
        click.off("mouseenter");
    });
}

function contextMenuClick(arr) {

    $('#agriInfo').click(function (e) {
        e.preventDefault();
        contextMenuInfoAjax(arr);
    });

    $('#agriDel').click(function (e) {
        e.preventDefault();

        $('.context-menu').remove();

        $('<div/>', {
            class: 'delChoice'
        }).css({
            left: event.pageX + 'px',
            top: event.pageY + 'px'
        })
            .appendTo('.appParent')
            .append(
                $('<div>')
                    .append('<p>Вы уверены?</p>')
                    .append('<button type="button" class="btn btn-info" id="delYes">Удалить</button>')
                    .append('<button type="button" class="btn btn-info" id="delNo">Отмена</button>'))
            .show('fast');
        block = true;
        delChoise(arr);
    });

    function delChoise(arr) {
        $('#delYes').click(function () {
            $('.delChoice').remove();
            contextMenuDelAjax(arr);
        });

        $('#delNo').click(function () {
            $('.delChoice').remove();
            block = false;
        });
    }
}

//Zoom

var currentZoom = 1.0;

$(document).ready(function () {
    $('#ZoomIn').click(function () {
        if (currentZoom < 1.5) {
            $('.appMap').css({ 'zoom': currentZoom += .1 });
        }
    })
    $('#ZoomOut').click(function () {
        if (currentZoom > 0.5) {
            $('.appMap').css({ 'zoom': currentZoom -= .1 });
        }
    })
    $('#ZoomReset').click(function () {
        currentZoom = 1.0
        $('.appMap').animate({ 'zoom': 1 }, 'slow');
    })
});

//All map Save

$('#addArr').click(function () {
    if (agriAddBlocker == true && doubleClickBlock == false) {
        doubleClickBlock = true;
        saveMapAjax();
    }
});

//Tool panel 

$('.toolBtn').click(function (e) {
    $('.toolPanel').toggleClass('toolPanel_active');
    $('.tool-Btn').toggleClass('tool-Btn_active');
    $('.toolBtn').toggleClass('toolBtn_active');
    $('.appMap').toggleClass('appMap_active');
});

$('.appLandscape').hide();
$('.appAgricultures').hide();

$('.textures h3').click(function () {

    var findArticle = $(this).next();
    var findWrapper = $(this).closest('.textures');

    if (findArticle.is(':visible')) {
        findArticle.slideUp('fast');
    }
    else {
        findWrapper.find('>div').slideUp('fast');
        findArticle.slideDown('fast');
    }
});

//Agriculture Save

$('#addAgri').click(function () {
    if (doubleClickBlock == false) {
        doubleClickBlock = true;
        var agriArr = new Array();
        agriArr[0] = $('#agriVarIn').val();
        agriArr[1] = $('#agriDateIn').val();
        agriArr[2] = clickCounter;
        var agriClass = '.squareExample' + clickCounter;
        agriArr[3] = idGenerator(agriClass);
        if (agriArr[3] == agriId) {
            console.log(agriArr);
            agricultureSaveAjax(agriArr);
        }
        else {
            alert("Не было добавлено новых культур!");
            doubleClickBlock = false;
        }
    }
    for (i = 0; i < cordArr.length; i++) {

        $('.appMap div').eq(cordArr[i]).css("border", "");
    }
    cordArr = [];
});

//Agriculture Cancel

$('#cancelAgri').click(function () {
    $(".chosenItem").toggleClass(texturesHandler());
    agriAddBlocker = true;
    clickCounter = (-2);
    $('.appMap').empty();
    $('.appMap').append(backupMap[0]);
    clickHandler();
    $(".textureInfoPannel").toggleClass("textureInfoPannel_active");
    block = false;
    cordArr = [];
});

//Id generator

function idGenerator(agriClass) {

    var arr = new Array();
    var id = 0;

    $(agriClass).each(function (index) {

        arr[index] = $(this).attr("id");

        if (arr[index] > id) {
            id = arr[index];
        }
    });
    return (id);
}

//Planning Stages Panel

var stageBlock = true;
var choiceBlock = true;

switchPlanningStagePannel();

function switchPlanningStagePannel() {

    $('#planningStagePanelButton').click(function () {


        $('.context-menu').remove();
        $(".planningStagePanel").toggleClass("planningStagePanel_active");
        if (stageBlock == true) {
            stageBlock = false;
            stagesListClear();
            getStagesListAjax();
        }
    });
}

//Add new Planning Stage

function addNewStage() {
    var arr = new Array();
    arr[0] = $('.appMap').html();
    addNewStageAjax(arr);
}

function stagesListClear() {
    $(".planningStagesList").empty();
}

function stagesListChoice() {

    if (agriAddBlocker == true) {
        $('.planningStagesList div a').click(function (event) {
            $('.context-menu').remove();
            if (choiceBlock == true) {
                backupMap[0] = $('.appMap').html();
                var id = $(event.target).attr("id");
                stagesListAjax(id);
            }
            else {
                var id = $(event.target).attr("id");
                stagesListAjax(id);
            }
        });
    }
}

function swmBack() {

    $('#swmBack').click(function () {
        $('.swichStagesMode').remove();
        clickCounter = (-2);
        $('.appMap').empty();
        $('.appMap').append(backupMap[0]);
        clickHandler();
        choiceBlock = true;
        agriAddBlocker = true;
    });
}

//CompatibilityCheck

var cordArr = new Array();

$('#compatibilityСheck').click(function () {

    var agriClass = '.squareExample' + clickCounter;
    var i = 0;

    $(agriClass).each(function () {

        if ($(this).attr('id') == agriId) {
            cordArr[i] = $(this).index('.appMap div');
            i++;

        }
    });
    console.log(cordArr);
    lastThreeYearsAjax();


    //cordArr[0] = $(this).index('.appMap div');
    //console.log(n);
    //console.log($('.appMap div').eq(n).attr('class'));
});


//FinishTheYear

$('#finishTheYear').click(function () {
    block = true;
    if (agriAddBlocker == true && doubleClickBlock == false) {
        doubleClickBlock = true;
        addNewStage();
    }
    finishTheYearAjax();
});

//LastThreeYears

function lastThreeYears(arr) {

    arr[0] = JSON.parse(arr[0]);
    arr[1] = JSON.parse(arr[1]);
    arr[2] = JSON.parse(arr[2]);

    yearCords = [[], [], [], []];
    var cords = new Array();

    $('.firstYear').append(arr[0]);
    $('.secondYear').append(arr[1]);
    $('.thirdYear').append(arr[2]);

    for (i = 0; i < cordArr.length; i++) {

        yearCords[0][i] = $('.firstYear div').eq(cordArr[i]).attr('class');
        yearCords[1][i] = $('.secondYear div').eq(cordArr[i]).attr('class');
        yearCords[2][i] = $('.thirdYear div').eq(cordArr[i]).attr('class');
        yearCords[3][i] = $('.appMap div').eq(cordArr[i]).attr('class');
    }
    $('.firstYear').empty();
    $('.secondYear').empty();
    $('.thirdYear').empty();

    var c = 0;
    var d = 0;
    for (i = 0; i < cordArr.length; i++) {

        if (yearCords[3][i] == yearCords[0][i] || yearCords[3][i] == yearCords[1][i] || yearCords[3][i] == yearCords[2][i]) {
            $('.appMap div').eq(cordArr[i]).css("border", "3px solid red");
        }
        else {
            d++;
        }
        cords[c] = yearCords[0][i];
        c++;
    }
    cords[c] = yearCords[3][0];
    if (d > 0) {
        incompatibleCheckAjax(cords);
    }
}

function incompatibleCheck(response) {
    for (i = 0; i < cordArr.length; i++) {

        if (yearCords[3][i] == yearCords[0][i] || yearCords[3][i] == yearCords[1][i] || yearCords[3][i] == yearCords[2][i]) {
            $('.appMap div').eq(cordArr[i]).css("border", "3px solid red");
        }
        else if (response[i] == "inc") {
            $('.appMap div').eq(cordArr[i]).css("border", "3px solid red");
        }
        else {

            $('.appMap div').eq(cordArr[i]).css("border", "3px solid green");
        }
    }
}



















// Textures handler

var agriAddBlocker = true;
var clickCounter = -2;
var backupMap = new Array();

$('.appAgricultures div').click(function (event) {

    $('.context-menu').remove();
    if (agriAddBlocker == true) {
        var f = $(event.target).attr("class");
        f = Number(f.match(/\d+/));
        clickCounter = f;
        var agriClass = '.squareExample' + clickCounter;
        var id = idGenerator(agriClass);
        agriId = ++id;
        agriAddBlocker = false;
        stagesListClear();
        $(".textureInfoPannel").toggleClass("textureInfoPannel_active");
        $(".chosenItem").toggleClass(texturesHandler());
        backupMap[0] = $('.appMap').html();
        block = true;
    }
});

$('.appLandscape div').click(function (event) {

    $('.context-menu').remove();
    if (agriAddBlocker == true) {
        var f = $(event.target).attr("class");
        f = Number(f.match(/\d+/));
        clickCounter = f;
    }
});

$('.moveArrow').click(function () {

    $('.context-menu').remove();
    if (agriAddBlocker == true) {
        clickCounter = -2;
    }
});

$('.squareEraser').click(function () {

    $('.context-menu').remove();
    if (agriAddBlocker == true) {
        clickCounter = 0;
    }
});

$('.square_click_Ex').click(function () {

    $('.context-menu').remove();
    if (agriAddBlocker == true) {
        clickCounter = -1;
    }
});

function texturesHandler() {

    var texture;

    if (clickCounter == 0) {
        texture = '';
    }

    else if (clickCounter == -1) {
        texture = ' square_click';
    }

    else
        texture = ' squareExample' + clickCounter;

    return texture;
}


