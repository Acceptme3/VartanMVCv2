document.addEventListener("DOMContentLoaded", function () {
    let hide_btn = document.querySelectorAll('.read_bt1')[0];

    var viewName = window.location.pathname.split('/').pop();
    console.log(viewName);

    if (hide_btn != null) {
        if (viewName == "Index" || viewName == "" || viewName == null || viewName == "index") {
            hide_btn.setAttribute('style', 'display: block;');
        }
        else {
            hide_btn.setAttribute('style', 'display: none;');
        }
    }
});