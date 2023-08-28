$(document).ready(function () {
    $('#names-table').DataTable({
        "paging": true,
        "searching": true,
        "ordering": true,
        "pageLength": 20,
         stateSave: true

    });

    $('#comp_names-table').DataTable({
        "paging": true,
        "searching": true,
        "ordering": true,
        "pageLength": 20,
        stateSave: true

    });
    console.log("Таблицы из Work");
});