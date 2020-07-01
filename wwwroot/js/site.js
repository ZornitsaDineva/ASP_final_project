// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

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