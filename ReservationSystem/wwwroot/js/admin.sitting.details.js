$(() => {
    $(".draggable").draggable({ revert: true, revertDuration: 300 });
    $(".droppable").droppable({ drop: (event, ui) => handleOnDropped(event, ui) });
});

function handleOnDropped(e, ui) {
    alert("Table with id: " + ui.draggable.data('table-id') + " has been assigned to reservation with id: " + $(e.target).data('reservation-id'));
}