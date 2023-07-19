$(document).ready(function () {
    $('#customtable').DataTable({
        "scrollY": "450px",
        "text-align": "right",
        "scrollCollapse": true,
        "paging": true
    });
});

showInPopup = (url, title) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $('#form-modal .modal-body').html(res);
            $('#form-modal .modal-title').html(title);
            $('#form-modal').modal('show');
            // to make popup draggable
            $('.modal-dialog').draggable({
                handle: ".modal-header"
            });
        }
    })
};

$(document).ready(function () {
    $('.dtHorizontal').DataTable({
        "scrollX": true
    });
    $('.dataTables_length').addClass('bs-select');
});