function OnCompleteJs(request, status) {
    $('html, body').animate({
        scrollTop: 0
    }, 500);
    return false;
}