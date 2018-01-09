$(document).ready(function () {
    
    var table = $('#order-list').DataTable();
    
    $("#searchListOrder").on('click', function () {
        var idDistributor = $("#idDistributor").val();
        if (!idDistributor) {
            idDistributor = "0";
        }
        var month = $("#month").val();
        if (!month) {
            month = "0";
        }
        var quartar = $("#quartar").val();
        if (!quartar) {
            quartar = "0";
        }
        var year = $("#year").val();
        if (!year) {
            year = "0";
        }
        debugger;
        var url = 'SearchListOrder';
        console.log(status);
        $.ajax({
            url: url,
            type: 'GET',
            cache: false,
            beforeSend: function () {
                loading = true;
                $('#loading').show();

            },
            //int? idDistributor, int month, int quartar, int year
            data: {
                month: parseInt(month),
                quartar: parseInt(quartar),
                year: parseInt(year),
                idDistributor: parseInt(idDistributor),
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
                        rs.push(date.getHours() + ':' + date.getMinutes() + ' ' + +(date.getMonth() + 1) + '/' + date.getDate() + '/' + date.getFullYear());
                        //rs.push(item.CreateDate);
                        rs.push(item.idDistributor);
                        rs.push(item.idStaff);
                        rs.push(item.Total);
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
    //$('#order-list tfoot th').each(function () {
    //    var title = $(this).text();
    //    $(this).html('<input class="FClass" style="width:inherit;" type="text" id="' + title.replace(' ', '_') + '" placeholder="Search ' + title + '" />');
    //});
    //var oTable = $('#order-list').DataTable({
    //    "ordering": false,
    //    "lengthMenu": [[100, 250, 500, -1], [100, 250, 500, "All"]],
    //    "pagingType": "full_numbers",
    //    "scrollY": "350px",
    //    "scrollX": true,
    //    "columns": [
    //        { "data": "idOrder", "autoWidth": false },
    //        { "data": "CreateDate", "autoWidth": true },
    //        { "data": "idDistributor", "autoWidth": true },
    //        { "data": "idStaff", "autoWidth": true },
    //        { "data": "Total", "autoWidth": true },
    //    ]
    //});

    //oTable.columns().every(function () {
    //    var that = this;
    //    $('input', this.footer()).on('keyup change', function () {
    //        if (that.search() !== this.value) {
    //            that.search(this.value).draw();
    //        }
    //    });
    //});
});

$(document).ready(function () {

    var table = $('#bill-list').DataTable();

    $("#searchListBill").on('click', function () {
        var idDistributor = $("#idDistributor").val();
        var month = $("#month").val();
        var quartar = $("#quartar").val();
        var year = $("#year").val();
        if (!idDistributor && !year && !month && !quartar) {
            year = "0";
            idDistributor = "0";
            month = "0";
            year = "0";
        }
        debugger;
        var url = 'SearchListBill';
        console.log(status);
        $.ajax({
            url: url,
            type: 'GET',
            cache: false,
            beforeSend: function () {
                loading = true;
                $('#loading').show();

            },
            //int? idDistributor, int month, int quartar, int year
            data: {
                month: parseInt(month),
                quartar: parseInt(quartar),
                year: parseInt(year),
                idDistributor: parseInt(idDistributor),
            },
            success: function (data) {
                if (data) {
                    debugger;
                    $('#loading').hide();
                    table.clear();
                    var result = data.map(function (item) {
                        var rs = [];
                        var date = new Date(item.CreatedDate);

                        rs.push(item.idBill);
                        //rs.push(item.CreateDate);
                        rs.push(item.idStaff);
                        rs.push(item.idDistributor);
                        rs.push(date.getHours() + ':' + date.getMinutes() + ' ' + +(date.getMonth() + 1) + '/' + date.getDate() + '/' + date.getFullYear());
                        rs.push(item.purchase);
                        rs.push(item.types);
                        rs.push(item.description)
                        return rs;
                        //x.idBill, x.idStaff, x.idDistributor, x.createdDate, x.purchase, x.types, x.description
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