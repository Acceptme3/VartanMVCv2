const openPopUp = document.getElementById("btn_show_entryForm");
const popUpClose = document.getElementById('pop_up_close');
const popUp = document.getElementById('pop_up');

const imagePopUp = document.getElementById('imagePop_up')

openPopUp.addEventListener('click', function (e){
    e.preventDefault();
    popUp.classList.add('active');
})

popUpClose.addEventListener('click', () => { popUp.classList.remove('active'); })

