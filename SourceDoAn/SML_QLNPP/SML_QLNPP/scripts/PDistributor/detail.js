$(document).ready(function () {
    $("#date").datepicker({
        dateFormat: 'dd/mm/yyyy'
    }).datepicker("setDate", new Date());;

    $(".form-control").change(function () {
        $("#btn_submit").removeAttr("disabled");
    })

    $("#status").change(function () {
        if ($("#status").val() != 4) {
            $("#reason").attr("style", "display: block");
            $("#txbReason").attr("required", false);
        }
        else {
            $("#reason").attr("style", "display: display");
            $("#txbReason").attr("required", "required");
        }
    })

    
})
