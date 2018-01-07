$(document).ready(function () {
    $.ajax({
        url: '/SML_QLNPP/SalesReport/LoadData',
        type: 'GET',
        cache: false,
        beforeSend: function () {
            loading = true;
            $('#loading').show();

        },
        success: function (data) {
            if (data) {
                debugger;
                $('#loading').hide();
                table.clear();
                var result = data.map(function (item) {
                    var rs = [];
                    var date = new Date(item.CreatedDate);

                    rs.push(item.idOrder);
                    rs.push(item.name);
                    rs.push(item.Total);
                    rs.push(date.getHours() + ':' + date.getMinutes() + ' ' + +(date.getMonth() + 1) + '/' + date.getDate() + '/' + date.getFullYear());
                    rs.push(item.staffName);

                    if (item.Statuses == 0) {
                        rs.push('Chưa duyệt');
                    }
                    else if (item.Statuses == 1) {
                        rs.push('Đã duyệt');

                    }
                    else if (item.Statuses == 2) {
                        rs.push('Không duyệt');
                    }
                    else {
                        rs.push('Đã giao');
                    }
                    rs.push('<a class="btn btn-info btn-xs" href="/Order/Detail/' + item.idOrder + '">Xem</a> | <a class="btn btn-danger btn-xs" href="/Order/Delete/' + item.idOrder + '">Xoá</a>')
                    return rs;
                });

                table.rows.add(result);
                table.draw();



            }
            else {
                $('#loading').hide();
                alert('Tìm kiếm không thành công!')
            };
        }
    });
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