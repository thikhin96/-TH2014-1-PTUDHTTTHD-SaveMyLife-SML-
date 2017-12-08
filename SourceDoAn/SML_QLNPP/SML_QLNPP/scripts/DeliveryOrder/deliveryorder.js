$(document).ready(function () {
    var date_input = document.getElementById('delivery_date');
    date_input.valueAsDate = new Date();
    var delivery_date = date_input.value;
    date_input.onchange = function () {
        $('delivery_date').attr('value', this.value);
        delivery_date = this.value;
    }


    var table = $('#dorder-list').DataTable();
    $("#search").on('click', function () {
        console.log("Tìm kiếm đơn giao hàng bắt đầu");
        console.log(delivery_date);
        var url = '/aj/DeliveryOrder/Search';
        
        $.ajax({
            url: url,
            type: 'GET',
            cache: false,
            data: {},
            success: function (data) {
                console.log("Ajax gọi thành công");
                if (data) {
                    console.log("có dữ liệu");
                }
                else {
                    console.log("Không có dữ liệu");
                    alert('Tìm kiếm không thành công!');
                };
            },
            failure: function (response) {
                alert("thất bại");
                alert(response.responseText);
            },
            error: function (response) {
                alert("Lỗi");
                alert(response.responseText);
            }
        });
    })
});
