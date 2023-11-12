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
document.addEventListener("DOMContentLoaded", function () {
    let phoneInputs = document.querySelectorAll('input[data-tel-input]');

    let getInputNumbersValue = function (input)
    {
        return input.value.replace(/\D/g, "");
    }

    let onPhoneInput = function (e)
    {
        let input = e.target, inputNumbersValue = getInputNumbersValue(input),
        formattedInputValue = "",
        selectionStart = input.selectionStart;

        if (!inputNumbersValue) { return input.value = "" }

        if (input.value.length != selectionStart)
        {
            if (e.data && /\D/g.test(e.data))
            {
                input.value = inputNumbersValue;
            }
            return;
        }

        if (["7", "8", "9"].indexOf(inputNumbersValue[0]) > -1)
        {
            //russian code
            if (inputNumbersValue[0] == "9") inputNumbersValue = "7" + inputNumbersValue;
            let firstSymbols = (inputNumbersValue[0] == "8") ? "8" : "+7";
            formattedInputValue = firstSymbols + " ";
            if (inputNumbersValue.length > 1)
            {
                formattedInputValue += "(" + inputNumbersValue.substring(1, 4);
            }

            if (inputNumbersValue.length >= 5) {
                formattedInputValue += ") " + inputNumbersValue.substring(4, 7);
            }

            if (inputNumbersValue.length >= 8) {
                formattedInputValue += "-" + inputNumbersValue.substring(7, 9);
            }

            if (inputNumbersValue.length >= 10) {
                formattedInputValue += "-" + inputNumbersValue.substring(9, 11);
            }
        }
        else
        {
            //not-russian code
           formattedInputValue = "+" + inputNumbersValue.substring(0, 16);
        }
        input.value = formattedInputValue;
    }
    var onPhonePaste = function (e) {
        var input = e.target,
            inputNumbersValue = getInputNumbersValue(input);
        var pasted = e.clipboardData || window.clipboardData;
        if (pasted) {
            var pastedText = pasted.getData('Text');
            if (/\D/g.test(pastedText)) {
                // Attempt to paste non-numeric symbol � remove all non-numeric symbols,
                // formatting will be in onPhoneInput handler
                input.value = inputNumbersValue;
                return;
            }
        }
    }

    let onPhoneKeyDown = function (e)
    {
        let input = e.target;
        if (e.keyCode == 8 && getInputNumbersValue(input).length == 1) input.value = "";
    }

    for (i = 0; i < phoneInputs.length; i++)
    {
        let input = phoneInputs[i];
        input.addEventListener("input", onPhoneInput);
        input.addEventListener("keydown", onPhoneKeyDown);
        input.addEventListener("paste", onPhonePaste);
    }
});

document.addEventListener("DOMContentLoaded", function () {
    let hide_btn = document.querySelectorAll('.read_bt1')[0];

    var viewName = window.location.pathname.split('/').pop();

    if (hide_btn != null) {
        if (viewName == "Index" || viewName == "" || viewName == null || viewName == "index") {
            hide_btn.setAttribute('style', 'display: block;');
        }
        else {
            hide_btn.setAttribute('style', 'display: none;');
        }
    }
});
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
    if (openPopUp != null)
    {
        popUpClose.addEventListener('click', () => { popUp.classList.remove('active'); })
    }
    
});
document.addEventListener('DOMContentLoaded', function () {
    var rows = document.querySelectorAll('a[data-copy="true"]');
    rows.forEach(function (row) {
        row.addEventListener('click', function () {
            var text = row.innerText;
            navigator.clipboard.writeText(text).then(function () {
                alert('Строка успешно скопирована в буфер обмена!');
            }, function () {
                alert('Ошибка при копировании строки в буфер обмена.');
            });
        });
    });
});

const swiper = new Swiper('.swiper', {
    // Optional parameters
    direction: 'horizontal',
    loop: true,

    // Navigation arrows
    navigation: {
        nextEl: '.swiper-button-next',
        prevEl: '.swiper-button-prev',
    },

    // And if we need scrollbar
    scrollbar: {
        el: '.swiper-scrollbar',
    },
});