OpenPopup = (title) => {
    $.ajax({
        type: 'GET',
        url: "/Message/Allmsg",
        success: function (res) {
            console.log(res);
            $('#form-modal .modal-body').html(res);
            $('#form-modal .modal-title').html(title);
            $('#form-modal').modal('show');
            // to make popup draggable
            //$('.modal-dialog').draggable({
            //    handle: ".modal-header"
            //});
        }
    })
}