function ShowLoading(elem, screen, mode) {

    var div = document.createElement('div');
    if (document.getElementById(elem) != null) {
        document.getElementById(elem).appendChild(div);
        div.className = 'ajax-loader';
        div.id = screen.substring(1, screen.length);
        switch (mode) {
            case 0:
                $(screen).css('top', document.getElementById(elem).offsetTop);
                $(screen).css('left', document.getElementById(elem).offsetLeft);
                $(screen).css('width', document.getElementById(elem).offsetWidth);
                $(screen).css('height', document.getElementById(elem).offsetHeight);
                break;
            case 1:
                $(screen).css('top', document.getElementById(elem).clientTop);
                $(screen).css('left', document.getElementById(elem).clientLeft);
                $(screen).css('width', document.getElementById(elem).clientWidth);
                $(screen).css('height', document.getElementById(elem).clientHeight);
                break;
            default:
                $(screen).css('top', document.getElementById(elem).offsetTop);
                $(screen).css('left', document.getElementById(elem).offsetLeft);
                $(screen).css('width', document.getElementById(elem).offsetWidth);
                $(screen).css('height', document.getElementById(elem).offsetHeight);
                break;
        }

        $(screen).show();
    }

}
function HideLoading(screen) {
    $(screen).remove();
}