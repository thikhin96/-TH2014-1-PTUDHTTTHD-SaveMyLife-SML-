//$('#btnComfirm').click(function (e) {
//    e.preventDefault();

//    // $('#status :selected').text() lấy giá trị text của opption
//    // $('#status :selected').val() lất giá tị value cỉa opption
//    var data = {
//        idDeliveryOrder: parseInt($('#idDelivery').val()),
//        status: parseInt($('#status :selected').val()),
//        description: $('#description').val()
//    };

//    //alert("mã giao hàng: " + data['idDeliveryOrder']);
//    //alert("tinhd trạng hiện tại :" + data['status']);
//    //alert("Mô tả :" + data['description']);

//    //alert("data: " + data);
//    //alert(JSON.stringify(data));
//    if (data.status == 5) {
//        alert("Chưa cập nhật tình trạng");
//        //$('#status').focus(function () {
//        //   $(this).append("span").attr("Chưa cập nhật trạng thai");
//        //   $(this).fadeIn(100000);
//        //});
//        return;
//    }
//    var url = '/DeliveryOrder/UpdateStatus';
//    $.ajax({
//        type: "POST",
//        url: url,
//       // content: "application/json; charset=utf-8",
//        dataType: "json",
//        cache: false,
//        data: { idDeliveryOrder: data.idDeliveryOrder, status: data.status, description: data.description },
//        success: function (d) {
//            if (d.success == 'true') {
//                alert("Cập nhật thành công");
//                $('#btnControll').empty();
//                $('#btnControll').append('<input type="button" value="Cập nhật" class="btn" style= "margin-right: 5px" id="btnUpdate"/>');
//                $('#btnControll').append('<input type="button" value="Lập hóa đơn" class="btn" style= "margin-left: 5px" id="btnCreateBill"/>');
//                $('#status').empty();
//                $('#status').append('<option value="1">Đang giao</option>');
//                $('#status').append('<option value="2">Đã giao</option>');
//                $('#status').append('<option value="3">Kiểm kê thiếu</option>');
//                $('#status').append('<option value="4">Bị Từ chối giao</option>');
//            }
//            else {
//                alert("Cập nhật thất bại");
//            }
//        },
//        error: function (xhr, textStatus, errorThrown) {
//            alert("Kết nối với server bị gián đoạn, vui lòng thử lại sau");
//            alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
//            alert("responseText: " + xhr.responseText);
//        }
//    });
//});

//$('#btnUpdate').click(function (e) {
//    e.preventDefault();

//    // $('#status :selected').text() lấy giá trị text của opption
//    // $('#status :selected').val() lất giá tị value cỉa opption
//    var data = {
//        idDeliveryOrder: parseInt($('#idDelivery').attr('value')),
//        status: parseInt($('#status :selected').val()),
//        description: $('#description').val()
//    };


//    var url = '/DeliveryOrder/UpdateStatus';
//    $.ajax({
//        type: "POST",
//        url: url,
//        // content: "application/json; charset=utf-8",
//        dataType: "json",
//        cache: false,
//        data: { idDeliveryOrder: data.idDeliveryOrder, status: data.status, description: data.description },
//        success: function (d) {
//            if (d.success == 'true') {
//                alert("Cập nhật thành công");
//                $('#btnControll').empty();
//                $('#btnControll').append('<input type="button" value="Cập nhật" class="btn" style= "margin-right: 5px" id="btnUpdate"/>');
//                $('#btnControll').append('<input type="button" value="Lập hóa đơn" class="btn" style= "margin-left: 5px" id="btnCreateBill"/>');
//                // thay đổi selected của select
//                $('#status').val(data.status);
//            }
//            else {
//                alert("Cập nhật thất bại");
//            }
//        },
//        error: function (xhr, textStatus, errorThrown) {
//            alert("Kết nối với server bị gián đoạn, vui lòng thử lại sau");
//            alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
//            alert("responseText: " + xhr.responseText);
//        }
//    });
//});