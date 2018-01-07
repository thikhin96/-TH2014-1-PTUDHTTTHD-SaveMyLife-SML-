$(function () {
    $('#product-list tbody').on('click', '.add-product', function () {
        var productId = $(this).data('id');
        var productName = $(this).data('name');
        var Reason = $(this).data('reason');
        $('#add-product-btn').data('returnRequestid', $(this).data('returnRequestid'));
        var $modal = $('#addProduct');
        var $idField = $('#product-id');
        $idField.val(productId);
        var $nameField = $('#product-name');
        $nameField.val(productName);
        var $reasonField = $('#returnRequest-reason');
        $reasonField.val(Reason);
    });

    $('#add-product-btn').on('click', function () {
        var $tableBody = $('#returnRequest-detail tbody');
        var lastIndex = $('#returnRequest-detail tbody tr').length;
        if ($('#returnRequest-detail tbody tr').find('.dataTables_empty').length > 0) {
            $('#returnRequest-detail tbody tr').eq(0).remove();
            lastIndex = 0;
        }

        $tableBody.append('<tr></tr>');
        var $currentRow = $('#returnRequest-detail tbody tr').eq(lastIndex);
        $currentRow.append('<td>' + $('#product-id').val() + '</td>');
        $currentRow.append('<td><input type="hidden" id="ReturnRequestDetails[' + lastIndex.toString() + '].IdReturnRequest" name="ReturnRequestDetails[' + lastIndex.toString() + '].IdReturnRequest" value="' + $(this).data('returnRequestid') + '" class="table-input"/><input type="number" id="ReturnRequestDetails[' + lastIndex.toString() + '].IdProduct" name="ReturnRequestDetails[' + lastIndex.toString() + '].IdProduct" value="' + $('#product-id').val() + '" class="table-input"/></td>');
        $currentRow.append('<td>' + $('#product-name').val() + '</td>');
        $currentRow.append('<td><input type="number" id="ReturnRequestDetails[' + lastIndex.toString() + '].Quantity" name="ReturnRequestDetails[' + lastIndex.toString() + '].Quantity" value="' + $('#product-quantity').val() + '" class="table-input" /></td>');
        $currentRow.append('<td><input type="text" id="ReturnRequestDetails[' + lastIndex.toString() + '].Reason" name="ReturnRequestDetails[' + lastIndex.toString() + '].Reason" value="' + $('#returnRequest-reason ').val() + '" class="table-input" /></td>');
        $currentRow.append('<td><button type="button" class="btn btn-warning" data-dismiss="modal" id="remove-product-btn" onclick="deleterow(this)">Xóa</button></td>')
    })

   
})

function deleterow(element) {
    console.log("try remove");
    $(element).parents("tr").remove();
}

$("#searchStorage").change(function () {
    var url = "getStorageInfo"
    var id = $("#searchStorage").val();
    
    $.ajax({
        url: url,
        type: 'GET',
        data: {
            id: id
        },
        success: function (rs) {
            if (rs){
                $("#street").val(rs.HouseNumber_Street)
                $("#ward").val(rs.Ward_Commune)
                $("#district").val(rs.District)
                $("#city").val(rs.City)
            }
            else {
                alert("lỗi")
            }
        }
    });
});
