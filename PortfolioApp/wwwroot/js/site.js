$(document).on('click', '.project', function (event) {
    var id = $('.project').data('id');
    $.ajax({
        url: '/getprojectdetails/get/?Id=' + id,
        type: 'get',
        success: function (data) {
            $("#projectModal").html(data);
            var myModal = new bootstrap.Modal($('#projectModal'));
            myModal.show();
        },
        error: function () {
            console.error("Error");
        }
    });
});