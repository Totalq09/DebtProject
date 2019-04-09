$(document).ready(function () {
    $.ajax({
        url: "/Authentication/currentuserpesel",
        method: "GET",
        success: function (data) {
            if (data === "")
                return;
            $("#pesel-for-read").val(data).trigger("change");
        },
        error: function (error) {
            console.log(error);
        },
        dataType: "json",
        cache: false
    });
});
//# sourceMappingURL=controller.js.map