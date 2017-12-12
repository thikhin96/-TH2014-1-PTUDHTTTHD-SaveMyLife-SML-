$(document).ready(function () {
    var url = '/aj/Product/Search'
    var table = $('#product-list').DataTable();
    $.ajax({
        url: url,
        type: 'GET',
        cache: false,
        beforeSend: function () {
            loading = true;
            $('#loading').show();

        },
        data: {
            keyword: ""
        },
        success: function (data) {
            if (data) {
                $('#loading').hide();
                table.clear();
                var result = data.map(function (item) {
                    var rs = [];
                    //var date = new Date(item.CreatedDate);

                    rs.push(item.IdProduct);
                    rs.push(item.ProductName);
                    rs.push(item.Price);

                    if (item.Statuses) {
                        rs.push('Ngừng');
                    }
                    else {
                        rs.push('Còn bán');
                    }

                    rs.push('<a href="/Product/Detail/' + item.IdProduct + '">Xem</a>')
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


    $("#search").on('click', function () {
        var keyword = $("#keyword").val();
       
        $.ajax({
            url: url,
            type: 'GET',
            cache: false,
            beforeSend: function () {
                loading = true;
                $('#loading').show();

            },
            data: {
                keyword: keyword
            },
            success: function (data) {
                if (data) {
                    $('#loading').hide();
                    table.clear();
                    var result = data.map(function (item) {
                        var rs = [];
                        //var date = new Date(item.CreatedDate);

                        rs.push(item.IdProduct);
                        rs.push(item.ProductName);
                        rs.push(item.Price);

                        if (item.Statuses) {
                            rs.push('Ngừng');
                        }
                        else {
                            rs.push('Còn bán');
                        }
                        
                        rs.push('<a href="/Order/Detail/' + item.IdProduct + '">Xem</a>')
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

    //$('.form-control').change(function () {
    //    alert('test')
    //})
});