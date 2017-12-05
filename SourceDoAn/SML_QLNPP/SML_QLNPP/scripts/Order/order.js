$(document).ready(function () {
    $("#created_date").datepicker({
        dateFormat: 'yy/mm/dd'
    }).datepicker("setDate", new Date());;
    var table = $('#order-list').DataTable();
    $("#search").on('click', function () {
        var keyword = $("#keyword").val();
        var created_date = $("#created_date").val();
        var status = $('#status').find(":selected").val();
        var url = '/aj/Order/Search'
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
                        var date = new Date(item.NgayLap);

                        rs.push(item.ID_DonHang);
                        rs.push(item.TenNPP);
                        rs.push(item.TongTien);
                        rs.push(date.getHours() + ':' + date.getMinutes() + ' ' + +(date.getMonth() + 1) + '/' + date.getDate() + '/' + date.getFullYear());
                        rs.push(item.TenNV);

                        if (item.TinhTrang == 0) {
                            rs.push('Chưa duyệt');
                        }
                        else if (item.TinhTrang == 1) {
                            rs.push('Đã duyệt');

                        }
                        else if (item.TinhTrang == 2) {
                            rs.push('Không duyệt');
                        }
                        else {
                            rs.push('Đã giao');
                        }
                        rs.push('<a href="/Order/Detail/' + item.ID_DonHang + '">Xem</a>')
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