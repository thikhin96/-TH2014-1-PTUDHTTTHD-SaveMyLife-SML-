$(document).ready(function () {
    $(".form-control").change(function () {
        $("#btn_submit").removeAttr("disabled");
    })

    $("#price").change(function () {
        //console.log(old_price)
        //console.log($("#new_price").val())
        if ($("#new_price").val() == old_price) {
            $("#lblreason").attr("style", "display: none");
            $("#reason").attr("style", "display: none");
            $("#txbReason").attr("required", false);
        }
        else {
            $("#lblreason").attr("style", "display: display");
            $("#reason").attr("style", "display: display");
            $("#txbReason").attr("required", "required");
        }
    })
})