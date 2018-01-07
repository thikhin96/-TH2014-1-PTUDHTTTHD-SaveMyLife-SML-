$(document).ready(function () {
    //var date_input = document.getElementById('createdDate');
    //date_input.valueAsDate = new Date();
    //var delivery_date = date_input.value;
    //date_input.onchange = function () {
    //    $('delivery_date').attr('value', this.value);
    //    delivery_date = this.value;
    //}
    var createdDate = document.getElementById('createdDate');
    createdDate.valueAsDate = new Date($('#createdDate').attr("strDate"));
});
