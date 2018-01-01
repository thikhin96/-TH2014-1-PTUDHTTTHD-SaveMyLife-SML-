$(function () {
    $('#product-list tbody').on('click', '.add-product', function () {
        var productId = $(this).data('id');
        var productName = $(this).data('name');
        var productPrice = $(this).data('price');
        $('#add-product-btn').data('orderid', $(this).data('orderid'));
        var $modal = $('#addProduct');
        var $idField = $('#product-id');
        $idField.val(productId);
        var $nameField = $('#product-name');
        $nameField.val(productName);
        var $priceField = $('#product-price');
        $priceField.val(productPrice);
    });

    $('#add-product-btn').on('click', function () {
        var $tableBody = $('#return-product-list tbody');
        var lastIndex = $('#return-product-list tbody tr').length;
        if ($('#return-product-list tbody tr').find('.dataTables_empty').length > 0) {
            $('#return-product-list tbody tr').eq(0).remove();
            lastIndex = 0;
        }

        $tableBody.append('<tr></tr>');
        var $currentRow = $('#return-product-list tbody tr').eq(lastIndex);
        $currentRow.append('<td><input type="hidden"' + 'name="ReturnDetails[' + lastIndex.toString() + '].idReturn" value="' + $(this).data('orderid') + '" class="table-input"/><input type="number"' + ' name="ReturnDetails[' + lastIndex.toString() + '].idProduct" value="' + $('#product-id').val() + '" class="table-input"/></td>');
        $currentRow.append('<td>' + $('#product-name').val() + '</td>');
        $currentRow.append('<td><input type="number"' + ' name="ReturnDetails[' + lastIndex.toString() + '].Quantity" value="' + $('#product-quantity').val() + '" class="table-input product-quantity" /></td>');
        $currentRow.append('<td>' + $('#product-price').val() + '</td>');


        var total = $('#product-price').val() * $('#product-quantity').val();
        $currentRow.append('<td>' + total.toString() + '</td>');
        $currentRow.append('<td>' + '<button type="button" class="btn btn-danger delete-product">Xóa</button>' + '</td>');

        var totalValue = $('#Total').val();
        if (totalValue == "")
            totalValue = '0';
        var Total = parseInt(totalValue) + parseInt(total);
        $('#Total').val(Total);
    });

    $('#DistributorId').select2({
        minimumInputLength: 2,
        ajax: {
            url: '../DataSource/GetDistributors',
            dataType: 'json',
            data: function (params) {
                var query = {
                    search: params.term
                }
                return query;
            },
            processResults: function (data) {
                return {
                    results: data
                };
            }
        }
    });

    $('#idStorage').select2({
        minimumInputLength: 2,
        ajax: {
            url: '../DataSource/GetStorages',
            dataType: 'json',
            data: function (params) {
                var query = {
                    search: params.term,
                    distributorId: $('#DistributorId').val()
                }
                return query;
            },
            processResults: function (data) {
                return {
                    results: data
                };
            }
        }
    });

    

})