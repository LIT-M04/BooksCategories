$(function () {
    var categories = $("form").data('categories').split(',');
    $("#category-input").on('input', function() {
        var value = $(this).val();
        var match = categories.filter(function (cat) {
            return cat.toLowerCase() === value.toLowerCase();
        });
        $("#submit-button").prop('disabled', match.length);
        if (match.length) {
            $("#stuff").addClass('has-error');
        } else {
            $("#stuff").removeClass('has-error');
        }
    });
});