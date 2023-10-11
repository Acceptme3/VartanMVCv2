function uploadPhotos(url) {
    var files = document.getElementById("fileInput").files;
    var formData = new FormData();
    var totalFiles = files.length;
    var uploadedFiles = 0;
    // Создаем Promise для загрузки файлов
    var uploadPromise = new Promise((resolve, reject) => {
        for (var i = 0; i < files.length; i++) {
            formData.append("files", files[i]);
        }
        $.ajax({
            url: url,
            type: "POST",
            data: formData,
            contentType: false,
            processData: false,
            xhr: function () {
                var xhr = new window.XMLHttpRequest();
                xhr.upload.addEventListener("progress", function (evt) {
                    if (evt.lengthComputable) {
                        var percentComplete = (uploadedFiles + (evt.loaded / evt.total)) * 100 / totalFiles;
                        var progress = Math.round(percentComplete);
                        $("#progressBar").val(progress);
                    }
                }, false);
                xhr.upload.addEventListener("load", function () {
                    uploadedFiles++;
                    if (uploadedFiles === totalFiles) {
                        // Все файлы загружены, вызываем resolve для завершения Promise
                        resolve();
                    }
                }, false);
                return xhr;
            }
        });
    });
    // Отправка формы на сервер после загрузки всех файлов
    uploadPromise.then(() => {
        // Все файлы загружены, отправляем форму на сервер
        var form = document.getElementById("progressBar");
        form.submit();
    });
}