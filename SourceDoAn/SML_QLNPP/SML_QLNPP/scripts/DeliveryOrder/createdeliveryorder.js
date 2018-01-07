$(document).ready(function () {
    //var rowCount = $('#ddorder-list >tbody >tr').length;
    //for (var i = 0; i <= rowCount -1; i++)
    //{
    //    var row = $("#ddorder-list >tbody >tr[row-id='" + i + "']");
    //    var promoQuantity = row.find("td input:eq(3)").val() * 1;
    //    if(promoQuantity >0)
    //    {
    //        row.find("td input:eq(2)").attr("readonly", "readonly");
    //        row.find("td input:eq(3)").attr("readonly", "readonly");
    //    }
    //    if (promoQuantity == 0) {
    //        row.find("td input:eq(3)").attr("readonly", "readonly");
    //    }
    //}

    //cập nhật số lượng
    $('#ddorder-list >tbody >tr >td >input:first-child').change(function () {
        var totalPurchase = 0;
        var rowid = $(this).attr('id-quantity') * 1;
        var row = $("#ddorder-list >tbody >tr[row-id='" + rowid + "']");
        var totalPurchase = $('#nowtotalPurchase').text().replace(/,/g, '') * 1;
        var price = row.find("td:eq(4)").text().replace(/,/g, '') * 1;
        var total = row.find("td:eq(7)").text().replace(/,/g, '') * 1;
        totalPurchase -= total;
        total = price * $(this).val();
        totalPurchase += total;
        row.find("td:eq(7)").text(total.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"));
        $('#totalPurchase').val(totalPurchase);
        $('#nowtotalPurchase').text(totalPurchase.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"));
    });
});
