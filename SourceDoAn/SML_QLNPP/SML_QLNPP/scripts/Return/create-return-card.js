$(function () {
    $('#product-list tbody').on('click', '.add-product', function () {
        var productId = $(this).data('id');
        var productName = $(this).data('name');
        var productPrice = $(this).data('price');
        $('#add-product-btn').data('orderid', $(this).data('returnid'));
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
        $currentRow.append('<td><input type="hidden"' + 'name="ReturnDetails[' + lastIndex.toString() + '].ProductMoneyRefunding" value="' + total.toString() + '" class="table-input"/><input type="number"' + ' name="ReturnDetails[' + lastIndex.toString() + '].ProductMoneyRefunding" value="' + total.toString() + '" class="table-input" readonly="readonly"/></td>');
        //$currentRow.append('<td>' + total.toString() + '</td>');
        $currentRow.append('<td>' + '<button type="button" class="btn btn-danger delete-product">Xóa</button>' + '</td>');

        var totalValue = $('#Total').val();
        if (totalValue === "")
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
                $('#idStorage option').remove();
                return {
                    results: data
                };
            }
        }
    });

    $('#return-product-list tbody').on('change', '.product-quantity', function () {
        var $thisRow = $(this).closest('tr');
        var $pricePerItem = $thisRow.children('td').eq(3);
        var $price = $thisRow.children('td').eq(4);
        var oldPrice = parseInt($price.find('input').val());
        var newPrice = parseInt($pricePerItem.html()) * $(this).val();
        $price.find('input').val(newPrice);
        updateTotal(oldPrice, newPrice);
    });

    function updateTotal(substraction, addition) {
        var totalValue = $('#Total').val();
        var Total = parseInt(totalValue) - substraction + addition;
        $('#Total').val(Total);
    }

    var InitialTotal = 0;
    $('.total-per-product').each(function () {
        InitialTotal += parseInt($(this).val());
    });
    $('#Total').val(InitialTotal);

    //$("#btnTao").on('click', function () {
    //    var id = $("#idReturn").val();
    //    var url = ""
    //    $.ajax({
    //        url: url,
    //        type: 'POST',
    //        cache: false,
    //        beforeSend: function () {
    //            loading = true;
    //            $('#loading').show();
    //        },
    //        data: {
    //            id: id
    //        },
    //        success: function (rs) {
    //            if (rs === 0) {
    //                $('#loading').hide();
    //                alert('Đã xảy ra lỗi!')
    //            }
    //            else {
    //                window.location.href = "/Debt/Create?ReturnId=" + id;
    //            };
    //        }
    //    });
    //})
})