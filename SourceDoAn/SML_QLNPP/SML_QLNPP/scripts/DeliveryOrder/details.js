//$(function () {
//    $.fn.datepicker.dates['vi'] = {
//            days: 'chủ nhật_thứ hai_thứ ba_thứ tư_thứ năm_thứ sáu_thứ bảy'.split('_'),
//            daysShort: 'CN_T2_T3_T4_T5_T6_T7'.split('_'),
//            daysMin: 'CN_T2_T3_T4_T5_T6_T7'.split('_'),
//            months: 'Tháng 1_Tháng 2_Tháng 3_Tháng 4_Tháng 5_Tháng 6_Tháng 7_Tháng 8_Tháng 9_Tháng 10_Tháng 11_Tháng 12'.split('_'),
//            monthsShort: 'Th01_Th02_Th03_Th04_Th05_Th06_Th07_Th08_Th09_Th10_Th11_Th12'.split('_'),
//            today: "Today",
//            clear: "Clear",
//            format: "dd/mm/yyyy",
//            titleFormat: "MM yyyy",
//            weekStart: 0
//    };
//    $("#deliveryDate").datepicker({
//        locale: 'vi',
//        language: 'vi'
//    });
//});

$(function () {
    var table = $('#dorder-list').DataTable({
        "oLanguage": {
            "oPaginate": {
                //"sPrevious": "<span class=\"fa fa-backward\"> Trước</span> ❮❮",
                //"sNext": "Sau <span class=\"fa fa-forward\"></span>",
                "sPrevious": "❮❮ Trước",
                "sNext": "Sau ❯❯",
            },
            "sInfo": "Hiển thị _START_ đến _END_ trên _TOTAL_",
            "sZeroRecords": "Không tìm thấy dữ liệu",
            "sInfoEmpty": "Không tìm thấy dữ liệu",
            "sLengthMenu": "Số dòng mỗi trang _MENU_"
        },
        "searching": true,
        "bLengthChange": true,
    });
    var updateDate = document.getElementById('updateDate');
    updateDate.valueAsDate = new Date($('#updateDate').attr("strDate"))

    var deliveryDate = document.getElementById('deliveryDate');
    deliveryDate.valueAsDate = new Date($('#deliveryDate').attr("strDate"))

    var estimateDateOfDelivery = document.getElementById('estimateDateOfDelivery');
    estimateDateOfDelivery.valueAsDate = new Date($('#estimateDateOfDelivery').attr("strDate"))
    
});
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