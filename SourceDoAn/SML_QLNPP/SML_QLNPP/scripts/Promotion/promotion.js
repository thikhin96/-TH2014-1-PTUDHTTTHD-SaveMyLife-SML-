$(document).ready(function () {
    $(".date").datepicker({
        dateFormat: 'yy/mm/dd'
    });
    var table = $('#order-list').DataTable();
    $("#search").on('click', function () {
        var idpro = $("#idpro").val();
        if (!idpro) {
            idpro = "0";
        }

        var startDate = $("#startDate").val();
        var endDate = $("#endDate").val();
        var url = '/aj/Promotion/Search'
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
                        rs.push(sdate.getHours() + ':' + sdate.getMinutes() + ' ' + +(sdate.getDate()) + '/' + (sdate.getMonth() + 1) + '/' + sdate.getFullYear());
                        rs.push(edate.getHours() + ':' + edate.getMinutes() + ' ' + +(edate.getDate()) + '/' + (edate.getMonth() +1) + '/' + edate.getFullYear());
                        rs.push('<a href="/Promotion/Detail/' + item.idPromotion + '">Xem</a>')
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