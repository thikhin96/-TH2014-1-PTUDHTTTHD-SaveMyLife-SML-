$(document).ready(function () {
    $('#order-list tfoot th').each(function () {
        var title = $(this).text();
        $(this).html('<input class="FClass" style="width:inherit;" type="text" id="' + title.replace(' ', '_') + '" placeholder="Search ' + title + '" />');
    });
    var oTable = $('#order-list').DataTable({
        "ordering": false,
        "lengthMenu": [[100, 250, 500, -1], [100, 250, 500, "All"]],
        "pagingType": "full_numbers",
        "scrollY": "350px",
        "scrollX": true,
        "ajax": {
            "url": "/SalesReport/LoadData",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idOrder", "autoWidth": false },
            { "data": "CreateDate", "autoWidth": true },
            { "data": "idDistributor", "autoWidth": true },
            { "data": "idStaff", "autoWidth": true },
            { "data": "Total", "autoWidth": true },
        ]
    });

    oTable.columns().every(function () {
        var that = this;
        $('input', this.footer()).on('keyup change', function () {
            if (that.search() !== this.value) {
                that.search(this.value).draw();
            }
        });
    });
});

$(document).ready(function () {
    $(".FClass").change(function () {
        var values = "order : " + $("#idOrder").val() + ", createDate : " + $("#CreateDate").val() + ", Distributors : " + $("#idDistributor").val();
    });
});