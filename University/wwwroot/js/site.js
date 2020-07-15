

// Write your JavaScript code.

$(document).ready(function () {
    $("a.ajaxLink").on('click', function (e) {
        e.preventDefault();
        var $this = $(this);
        $.ajax({
            type: "GET",
            url: $this.attr('href')
        }).then(function (result) {
            $('#InstructorCoursePlaceholder').html(result);
        });
    });

});