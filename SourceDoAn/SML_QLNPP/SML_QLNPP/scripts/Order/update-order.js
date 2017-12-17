$(function () {
    $('#order-detail tbody').on('click', '.delete-product', function () {
        var $thisRow = $(this).closest('tr');
        updateRowsName($thisRow, 'OrderDetails');
        var $price = $thisRow.closest('tr').children('td').eq(4);
        var price = parseInt($price.html());
        $thisRow.remove();
        updateTotal(price, 0);

    });

    $('#order-detail tbody').on('change', '.product-quantity', function () {
        var $thisRow = $(this).closest('tr');
        var $pricePerItem = $thisRow.closest('tr').children('td').eq(3);
        var $price = $thisRow.closest('tr').children('td').eq(4);
        var oldPrice = parseInt($price.html());
        var newPrice = parseInt($pricePerItem.html()) * $(this).val();
        $price.html(newPrice.toString());
        updateTotal(oldPrice, newPrice);
    });

    function updateTotal(substraction, addition) {
        var totalValue = $('#Total').val();
        var Total = parseInt(totalValue) - substraction + addition;
        $('#Total').val(Total);
    }
})

function updateRowsName($currentRow, prefixString) {
    var index = $('#order-detail tbody tr').index($currentRow);
    var count = $('#order-detail tbody tr').length;
    for (i = index + 1; i < count; i ++)
    {
        var $tableBody = $currentRow.parent();
        var $row = $tableBody.children('tr').eq(i);
        $row.find('input').each(function () {
            var $thisColumn = $(this)
            var name = $thisColumn.attr('name');
            if (name != undefined)
            {
                if (name.indexOf(prefixString) != -1) {
                    var newName = prefixString + '[' + (i-1).toString() + ']' + name.substr(name.indexOf('.'), name.length);
                    $thisColumn.attr('name', newName);
                }
            }

        });
    }
}

