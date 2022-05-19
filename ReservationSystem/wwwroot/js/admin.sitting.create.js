$(() => {

	var slider = $('#numberToSchedule');
	var endDate = $('#endDateText');
	$("p#numberToScheduleText").html(slider.val());
	disableRecurringDay();

	$("#numberToSchedule").change(() => {
		$("p#numberToScheduleText").html(slider.val());
		$('#endDateText').html(calculateEndDate($('#start-time').val(), $('#set-recurring-type').val(), slider.val()));
	});

	$('#cb-show-recurring').change((e) => {

		if (e.currentTarget.checked) {
			$('#recurring-form-container').show();
		}
		else {
			$('#recurring-form-container').hide();
		}
	});

	$('#set-recurring-type').change((e) => {

		let recurringtype = $('#set-recurring-type').val();

		if (recurringtype==="Weekly") {
			$('#weekly-days-select').show();
		}
		else {
			$('#weekly-days-select').hide();
		}

		$('#endDateText').html(calculateEndDate($('#start-time').val(), $('#set-recurring-type').val(), slider.val()));
	});

	$('#start-time').change((e) => {
		disableRecurringDay();
	});

	//If there is a change in a variable with id starting with 'recur-checkbox-day-'
	//Update the hidden input with the corresponding number to be the same value
	$("[id^='recur-checkbox-day-']").change(function(e) {
		var id = "#recur-hidden-day-" + this.id.slice(19);
		if (e.currentTarget.checked) {
			$("#recur-hidden-day-" + this.id.slice(19)).val(true);
		}
		else {
			$("#recur-hidden-day-" + this.id.slice(19)).val(false);
		}
	});

	function calculateEndDate(date, recurringType, sliderval) {
		
		date = new Date(date);
		if (recurringType == "Daily") {
			date.setDate(date.getDate() + (sliderval-1));
		}
		else if (recurringType == "Weekly") {
			date.setDate(date.getDate() + 7*(sliderval-1));
		}
		return date;
	}

	function disableRecurringDay() {
		let startday = new Date($('#start-time').val()).getDay();
		for (i = 0; i < 7; i++) {
			$('#recur-checkbox-day-' + i).attr("disabled", false);
		}
		$('#recur-checkbox-day-' + startday).prop("checked", true);
		$('#recur-hidden-day-' + startday).val(true);
		$('#recur-checkbox-day-' + startday).attr("disabled", true);

		$('#endDateText').html(calculateEndDate($('#start-time').val(), $('#set-recurring-type').val(), slider.val()));

    }
});