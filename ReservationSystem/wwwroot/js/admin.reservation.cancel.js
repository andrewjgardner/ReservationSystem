function confirmDelete(id) {
	confirmed = confirm("Are you sure you want to cancel this reservation?");
	if (confirmed) {
		window.location.href = '@Url.Action("Delete", "Reservation")/' + id;
		return window;
	}
}