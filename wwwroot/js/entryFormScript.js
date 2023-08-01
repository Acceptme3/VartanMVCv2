document.addEventListener("DOMContentLoaded", function () {

    const openPopUp = document.getElementById("btn_show_entryForm");
    const openPopUp_2 = document.getElementById("btn_show_entryForm_2");
    const popUpClose = document.getElementById("pop_up_close");
    const popUp = document.getElementById("pop_up");

    if (openPopUp != null) {
        openPopUp.addEventListener('click', function (e) {
            e.preventDefault();
            popUp.classList.add('active');
        })
    }

    popUpClose.addEventListener('click', () => { popUp.classList.remove('active'); })

    if (openPopUp_2 != null) {
        openPopUp_2.addEventListener('click', function (e) {
            e.preventDefault();
            popUp.classList.add('active');
        })
    }

    popUpClose.addEventListener('click', () => { popUp.classList.remove('active'); })

});