document.addEventListener("DOMContentLoaded", function () {
    // Получите элемент блока с изображением и заголовком
    var imageBlock = document.getElementById("imageBlock");
    var title = document.querySelector("#imageBlock h1");
    var paragraf = document.querySelector("#imageBlock p");

    // Создайте временный Image объект
    var tempImage = new Image();

    // Получите URL изображения из стиля фона
    var backgroundImageUrl = getComputedStyle(imageBlock).backgroundImage.slice(5, -2);

    // Установите URL изображения во временный Image объект
    tempImage.src = backgroundImageUrl;

    // Обработчик события загрузки изображения
    tempImage.onload = function () {
        // Используйте ColorThief для анализа цвета
        var colorThief = new ColorThief();
        var color = colorThief.getColor(tempImage);

        // Определите цвет шрифта в зависимости от яркости цвета фона
        var fontColor = (getBrightness(color) < 128) ? "white" : "black";

        // Установите цвет шрифта
        title.style.color = fontColor;
    };

    // Функция для определения яркости цвета
    function getBrightness(rgb) {
        return 0.299 * rgb[0] + 0.587 * rgb[1] + 0.114 * rgb[2];
    }
});
