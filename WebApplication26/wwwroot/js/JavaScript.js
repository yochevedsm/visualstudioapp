$(() => {
    $("#commenterName").on('input', function () {
        ensureFormValidity();
    });

    $("#text").on('input', function () {
        ensureFormValidity();
    });

    function ensureFormValidity() {
        const commenterName = $("#commenterName").val();
        const text = $("#text").val();
        const isValid = commenterName && text;
        $("#submit").prop('disabled', !isValid);
    }
});