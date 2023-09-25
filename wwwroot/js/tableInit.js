$(document).ready(function () {
    $('#names-table').DataTable({
        "autoWidth": false,
        "columnDefs": [
            { "width": "90px", "targets": 4 }
        ],
        "paging": true,
        "searching": true,
        "ordering": true,
        "pageLength": 20,
        "stateSave": true
    });

    $('#comp_names-table').DataTable({
        "autoWidth": false,
        "columnDefs": [
            { "width": "90px", "targets": 4 }
        ],
        "paging": true,
        "searching": true,
        "ordering": true,
        "pageLength": 20,
        "stateSave": true
    });

    $('#feedback_table').DataTable({
        "paging": true,
        "searching": true,
        "ordering": true,
        "pageLength": 20,
        "stateSave": true
    });

    $('#cp-table').DataTable({
        "paging": true,
        "searching": true,
        "ordering": true,
        "pageLength": 10,
        "stateSave": true
    });

    $('#ws-table').DataTable({
        "paging": true,
        "searching": true,
        "ordering": true,
        "pageLength": 10,
        "stateSave": true
    });
    console.log("Таблицы статус Work");
});