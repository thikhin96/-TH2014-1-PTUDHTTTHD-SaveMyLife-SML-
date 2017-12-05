$(document).ready(function () {
    $("#created_date").datepicker({
        dateFormat: 'yy/mm/dd'
    }).datepicker("setDate", new Date());;
    var table = $('#order-list').DataTable();
    $("#search").on('click', function () {
        var idpro = $("#idpro").val();
        if (!idpro) {
            idpro = "0";
        }

        var startDate = $("#startDate").val();
        var endDate = $("#endDate").val();
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
                idpro: parseInt(idpro),
                startDate: startDate,
                endDate: endDate
            },
            success: function (data) {
                if (data) {
                    $('#loading').hide();
                    table.clear();
                    var result = data.map(function (item) {
                        var rs = [];
                        var sdate = new Date(item.startDate);
                        var edate = new Date(item.endDate);
                        rs.push(item.idPromotion);
                        rs.push(sdate.getHours() + ':' + sdate.getMinutes() + ' ' + +(sdate.getMonth() + 1) + '/' + sdate.getDate() + '/' + sdate.getFullYear());
                        rs.push(edate.getHours() + ':' + edate.getMinutes() + ' ' + +(edate.getMonth() + 1) + '/' + edate.getDate() + '/' + edate.getFullYear());
                        rs.push('<a href="/Promotion/Detail/' + item.idOrder + '">Xem</a>')
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