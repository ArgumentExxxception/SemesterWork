$("#logout").click(function() {
    $.ajax({
        url: '/logout',
        type: 'GET',
        success: function (data) {
            document.location.href = "/";
        },
    })
})
