$(document).ready(function () {
    
    var dOrderDataTable = $('#dorder-list').DataTable()
    var totalPurchase = 0;
    var dorderTable = document.getElementById("dorder-list");
    var rowCount = $('#dorder-list >tbody >tr').length;

    
    for (var i = 1; i <= rowCount; i++)
    {
        //console.log(dOrderTable.rows[i]);
        var row = dorderTable.rows[i];
        var price = row.cells[2].innerHTML.replace(/,/g, '');
        var quantity = row.cells[3].innerHTML;
       // var quantity = row.cells[3].children[0].value;
        var total = price * quantity;
        row.cells[5].innerHTML = total.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,");
        totalPurchase += total;
    }
    totalPurchase -= $('#totalPromotion').val();
    $('#totalPurchase').text(totalPurchase.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"));
    //console.log(total);
    
    // cập nhật số lượng
    //$('#dorder-list >tbody >tr >td >input:first-child').change(function () {
    //    var rowid = $(this).attr('row-id') * 1;
    //    var row = document.getElementById("dorder-list").rows[rowid];
    //    var totalPurchase = $('#totalPurchase').text().replace(/,/g, '') * 1;
    //    var price = row.cells[2].innerHTML.replace(/,/g, '');
    //    var total = row.cells[4].innerHTML.replace(/,/g, '') * 1;
    //    totalPurchase -= total;
    //    total = price * $(this).val();
    //    totalPurchase += total;
    //    row.cells[4].innerHTML = total.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,");
    //    $('#totalPurchase').text(totalPurchase.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"));
    //});
});
