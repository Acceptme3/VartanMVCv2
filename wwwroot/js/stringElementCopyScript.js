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
