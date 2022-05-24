$(() => {

    $("#submit").click(() => {
        if ($("#password").val() != $("#confirmPassword").val()) {
            //alert("Passwords do not match.");
            $("#password").setCustomValidity("Passwords do not match");
            return false;
        }
        $("#password").setCustomValidity("");
        return true;
    });
});