document.addEventListener("DOMContentLoaded", function () {

    const popUp = document.getElementById("pop_up");
    const popUpPar = document.getElementById("pop_up_paragraph");
    const openPopUpGreen = document.querySelectorAll("div.btn-toolbar button.btn.green");
    console.log("Колличество зеленых кнопок "+openPopUpGreen.length);
    const openPopUpRed = document.querySelectorAll("div.btn-toolbar button.btn.red");
    console.log("Колличество красных кнопок " + openPopUpRed.length);  
    const popUpNo = document.getElementById("pop_up_No");


    if (openPopUpGreen != null) {
        openPopUpGreen.forEach(function (button) {
            button.addEventListener('click', function () {
                popUpPar.innerText = "Пометить данную запись как исполненную?";
                popUp.classList.add('active');
                var clientId = button.getAttribute('data-id');
                if (document.getElementById('moveForm') != null) {
                    var clientOperation = document.getElementById('moveForm').querySelector('input[name="operation"]').value;
                    var currentViewName = document.getElementById('moveForm').querySelector('input[name="viewName"]').value;
                }
                console.log("Операции над клиентом " + clientOperation);
                console.log("View " + currentViewName);
                showModal(clientId, clientOperation, currentViewName);
            });
        });
    }

    if (popUpNo!=null) popUpNo.addEventListener('click', () => { popUp.classList.remove('active'); })
    
    if (openPopUpRed != null) {
        openPopUpRed.forEach(function (button) {
            button.addEventListener('click', function () {
                popUpPar.innerText = "Удалить данную запись?";
                popUp.classList.add('active');
                var clientId = button.getAttribute('data-id');
                if (document.getElementById('deleteForm') != null) {
                    var clientOperation = document.getElementById('deleteForm').querySelector('input[name="operation"]').value;
                    var currentViewName = document.getElementById('deleteForm').querySelector('input[name="viewName"]').value;
                }
                console.log("Операции над клиентом " + clientOperation);
                console.log("View " + currentViewName);
                showModal(clientId, clientOperation, currentViewName);
            });
        });
    }
    if (document.getElementById('confirm-form') != null) {
        document.getElementById('confirm-form').addEventListener('submit', function () {
            popUp.classList.remove('active');
        });
    }

    if (popUpNo != null) popUpNo.addEventListener('click', () => { popUp.classList.remove('active'); })

});

function showModal(clientId, clientOperation, currentViewName) {
    var modal = document.getElementById('confirm-form');
    modal.querySelector('input[name="id"]').value = clientId;
    modal.querySelector('input[name="operation"]').value = clientOperation;
    modal.querySelector('input[name="viewName"]').value = currentViewName;

}
