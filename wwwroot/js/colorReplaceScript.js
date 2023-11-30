document.addEventListener("DOMContentLoaded", function () {
    // �������� ������� ����� � ������������ � ����������
    var imageBlock = document.getElementById("imageBlock");
    var title = document.querySelector("#imageBlock h1");
    var paragraf = document.querySelector("#imageBlock p");

    // �������� ��������� Image ������
    var tempImage = new Image();

    // �������� URL ����������� �� ����� ����
    var backgroundImageUrl = getComputedStyle(imageBlock).backgroundImage.slice(5, -2);

    // ���������� URL ����������� �� ��������� Image ������
    tempImage.src = backgroundImageUrl;

    // ���������� ������� �������� �����������
    tempImage.onload = function () {
        // ����������� ColorThief ��� ������� �����
        var colorThief = new ColorThief();
        var color = colorThief.getColor(tempImage);

        // ���������� ���� ������ � ����������� �� ������� ����� ����
        var fontColor = (getBrightness(color) < 128) ? "white" : "black";

        // ���������� ���� ������
        title.style.color = fontColor;
    };

    // ������� ��� ����������� ������� �����
    function getBrightness(rgb) {
        return 0.299 * rgb[0] + 0.587 * rgb[1] + 0.114 * rgb[2];
    }
});
