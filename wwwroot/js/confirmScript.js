
const openPopUp_confirm = document.getElementById("accept");
const popUpClose_confirm = document.getElementById("pop_up_confirm_close");
const popUp_confirm = document.getElementById("pop_up_confirm");

openPopUp_confirm.addEventListener('click', function (e) {
    e.preventDefault();
    popUp_confirm.classList.add('active');
})

popUpClose_confirm.addEventListener('click', () => { popUp_confirm.classList.remove('active'); })
