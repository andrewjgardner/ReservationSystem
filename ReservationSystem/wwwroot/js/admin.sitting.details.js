$(() => {
    $(".draggable").draggable({ revert: true, revertDuration: 300 });
    $(".droppable").droppable({ drop: (event, ui) => handleOnDropped(event, ui) });
});

function handleOnDropped(e, ui) {
    var tableId = $(e.target).data('table-id');
    var reservationId = ui.draggable.data('reservation-id');
    alert("Table with id: " + tableId + " has been assigned to reservation with id: " + reservationId);
    $.post(`/admin/reservation/assignTables/`, { TableId: tableId, ReservationId: reservationId }, function () { alert("Success!")});
}