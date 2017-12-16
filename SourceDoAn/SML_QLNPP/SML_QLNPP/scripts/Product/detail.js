$(document).ready(function () {
    $(".form-control").change(function () {
        $("#btn_submit").removeAttr("disabled");
    })
})