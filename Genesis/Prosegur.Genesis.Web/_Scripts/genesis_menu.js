///
/// Implementação do menu suspenso das aplicações unificadas Genesis web
///

var menu_visible = false;
var menu_isAnimating = false;
var menu_timeout = 250;
var menu_overflow = 0;

function menu_hide() {
    if (menu_visible && !menu_isAnimating) {
        menu_visible = false;
        menu_isAnimating = true;
        $(".menu_container").animate({ marginTop: '+=' + menu_overflow }, menu_timeout, function () { menu_isAnimating = false; });
    }
}

function menu_show() {

    if (!menu_visible && !menu_isAnimating) {
        menu_visible = true;
        menu_isAnimating = true;
        $(".menu_container").animate({ marginTop: '-=' + menu_overflow }, menu_timeout, function () { menu_isAnimating = false; });
    }
}

function menu_init(title, itens) {

    // em popups nao devemo gerar o menu suspenso
    if (window.open != null)
        return;

    $(document).mouseleave(function () { menu_hide(); });

    $("<div class='menu_container' />")
        .appendTo("body")
        .mouseenter(function () { menu_show(); })
        .mouseleave(function () { menu_hide(); });

    $("<div class='menu_header' />")
        .appendTo(".menu_container")
        .text(title);

    $.each(itens, function (i, v) {
        $("#" + v.app_lnkid)
            .text(v.app_name)
            .attr("class", "menu_item")
            .attr("href", v.app_postback)
            .attr("target", "_blank")
            .appendTo(".menu_container");
    });

    menu_overflow = $(".menu_container").height() - $(".menu_item").height();

}