$(document).ready(function () {
    var table = $('#returnRequest-list').DataTable();
    $("#search").on('click', function () {
        var keyword = $("#keyword").val();
        var status = $("#status").val();
        var url = '/ReturnRequest/Search'
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
                status: parseInt(status)
            },
            success: function (data) {
                if (data) {
                    $('#loading').hide();
                    table.clear();
                    var result = data.map(function (item) {
                        var rs = [];
                        var date = new Date(item.DateCreate);
                        rs.push(item.IdReturnRequest);
                        rs.push(item.idDistributor);
                        rs.push(item.name);
                        rs.push((date.getMonth() + 1) + '/' + date.getDate() + '/' + date.getFullYear());
                        if (item.Status === 0)
                            rs.push("Chưa xử lý")
                        else if (item.Status === 1)
                            rs.push("Đã xử lý")
                        else
                            rs.push("Đã từ chối")
                        rs.push('<a class="btn btn-info btn-xs" href="/ReturnRequest/Detail/' + item.IdReturnRequest + '">Chi tiết</a>')
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
})