$(() => {
    $(".draggable").draggable({ revert: true, revertDuration: 300 });
    $(".droppable").droppable({ drop: (event, ui) => handleOnDropped(event, ui) });
});

function handleOnDropped(e, ui) {
    var tableId = $(e.target).data('table-id');
    var reservationId = ui.draggable.data('reservation-id');
    $.post(`/api/admin/reservations/assign-table/${reservationId}/${tableId}`)
        .then(function (response) {
            var tableName = response.tableName;
            var reservationCustomerName = response.reservationCustomerName;
            var reservationTime = response.reservationTime;
            if (!response.tableAlreadyAssigned) {
                alert("Table " + tableName + " was assigned to " + reservationCustomerName + "'s reservation at " + reservationTime + ".");
            }
            else {
                alert("Table " + tableName + " is already assigned to " + reservationCustomerName + "'s reservation at " + reservationTime + ".");
            }
        });
}