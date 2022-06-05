$(() => {

    $('.btn-close-sitting').click((e) => closeSitting($(e.target).data('sitting-id')));
}); 

function closeSitting(id) {

    //debugger; 
    $.get(`/admin/sitting/close/${id}`, (data) => {
        $('.modal-footer').html(data);
    });
    $("#closeSittingModal").modal("show");
}