$(document).ready(function () {
    var table = $('#pDistributor-list').DataTable();
    $("#search").on('click', function () {
        var status = $('#status').find(":selected").val();
        var url = '/SML_QLNPP/PDistributor/Search'
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
                status: parseInt(status)
            },
            success: function (data) {
                //alert("goi ajax");
                if (data) {
                    $('#loading').hide();
                    table.clear();
                    var result = data.map(function (item) {
                        var rs = [];

                        rs.push(item.idDistributor);
                        rs.push(item.name);
                        rs.push(item.address);
                        rs.push(item.phone);
                        rs.push(item.Email);
                        //rs.push(item.representative.name);
                        // rs.push(item.staffName);

                        if (item.status == 0) {
                            rs.push('Chưa xử lý');
                        }
                        else if (item.status == 1) {
                            rs.push('Chưa hẹn gặp mặt');

                        }
                        else if (item.status == 2) {
                            rs.push('Chưa trao đổi');
                        }
                        else if (item.status == 3) {
                            rs.push('Đồng ý điều khoản hợp đồng');
                        }
                        else {
                            rs.push('Không đồng ý làm hợp đồng');
                        }
                        rs.push('<a class="btn btn-info btn-xs" href="/PDistributor/Detail/' + item.idDistributor + '">Xem chi tiết</a> | <a class="btn btn-warning btn-xs" href="/PDistributor/Update/' + item.idDistributor + '">Phân công</a>')
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