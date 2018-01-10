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
                // alert("goi ajax");
                debugger;
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
                        debugger;
                        rs.push(item.Representatives[0].name);
                        var check = item.Assignments[0]
                        if (check) {
                            rs.push(check.Staff1.staffName);
                        }
                        else
                            rs.push("Chưa phân công");
                        rs.push('<a class="btn btn-info btn-xs" href="/SML_QLNPP/PDistributor/Detail/' + item.idDistributor + '">Xem chi tiết</a>')
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


//var rs1 = "";
//$.ajax({
//    url: "/PDistributor/getStaffAssigment",
//    type: 'GET',
//    data: {
//    },
//    success: function (data1) {
//        if (data1) {
//            var result1 = data1.map(function (item1) {
//                rs1 = rs1 + "<option id='s_ " + item1.idStaff + " '>" + item1.staffName + "</option>"
//            })
//        }
//        else {
//            alert('Lỗi')
//        }
//    }
//})
//rs.push("<select id='iAssig'>" + rs1 + "</select>")