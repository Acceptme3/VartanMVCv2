document.addEventListener("DOMContentLoaded", function () {

    const openPopUp = document.getElementById("feedbackForm_open");
    const popUpClose = document.getElementById("feedback_pop_up_close");
    const popUp = document.getElementById("feedback_pop_up");

    if (openPopUp != null) {
        openPopUp.addEventListener('click', function (e) {
            e.preventDefault();
            popUp.classList.add('active');
        })
    }

    popUpClose.addEventListener('click', () => { popUp.classList.remove('active'); })
});