﻿$(document).ready(function () {
    $("#btnDuyet").on('click', function () {
        var note = $("#iNote").val();
        var id = $("#idReturnRequest").val();
        var url = "approveReturnRequest"
        $.ajax({
            url: url,
            type: 'POST',
            cache: false,
            beforeSend: function () {
                loading = true;
                $('#loading').show();

            },
            data: {
                id: id,
                note: note
            },
            success: function (rs) {
                if (rs === 0) {
                    $('#loading').hide();
                    alert('Đã xảy ra lỗi!')
                }
                else {
                    window.location.href = "/SML_QLNPP/Return/Create?ReturnRequestId=" + id;
                };
            }
        });
    })

    $("#btnTuChoi").on('click', function () {
        if ($("#iNote").val() === "") {
            alert("Vui lòng nhập lý do từ chối vào phần ghi chú");
        }
        else {
            var note = $("#iNote").val();
            var id = $("#idReturnRequest").val();
            var url = "denyReturnRequest"
            $.ajax({
                url: url,
                type: 'POST',
                cache: false,
                beforeSend: function () {
                    loading = true;
                    $('#loading').show();

                },
                data: {
                    id: id,
                    note: note
                },
                success: function (rs) {
                    if (rs === 0) {
                        $('#loading').hide();
                        alert('Đã xảy ra lỗi!')
                    }
                    else {
                        alert("Từ chối đơn thành công");
                        location.reload();
                    };
                }
            });
        }
    })
})