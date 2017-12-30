$(document).ready(function () {
    $("#created_date").datepicker({
        dateFormat: 'yy/mm/dd'
    }).datepicker("setDate", new Date());;
    var table = $('#order-list').DataTable();
    $("#search").on('click', function () {
        var keyword = $("#keyword").val();
        var created_date = $("#created_date").val();
        var status = $('#status').find(":selected").val();
        var url = '/Order/Search'
        console.log(status);
        $.ajax({
            url: url,
            type: 'GET',
            cache: false,
            beforeSend: function () {
                loading = true;
                $('#loading').show();

            },
            data: {
                keyword: keyword,
                created_date: created_date,
                status: parseInt(status)
            },
            success: function (data) {
                if (data) {
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
                        rs.push('<a class="btn btn-info btn-xs" href="/Order/Detail/' + item.idOrder + '">Xem chi tiết</a>')
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
    })
});