$(() => {

	var slider = $('#numberToSchedule');
	var endDate = $('#endDateText');
	$("p#numberToScheduleText").html(slider.val());
	$('#endDateText').html(calculateEndDate($('#start-time').val(), $('#set-recurring-type').val(), slider.val()));
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

	/*
	for (i = 0; i < 7; i++) {
		$('#recur-checkbox-day-' + i).change((e) => {
			$('#recur-hidden-day-' + i).toggle;
		});
    }
	*/

	function calculateEndDate(date, recurringType, sliderval) {
		
		date = new Date(date);
		var endDate;
		if (recurringType == "Daily") {
			var tempDate = date.getDate() + sliderval;
			date.setDate(date.getDate() + 1*sliderval);
		}
		else if (recurringType == "Weekly") {
			var tempDate = date.getDate() + 7*sliderval;
			date.setDate(date.getDate() + 7*sliderval);
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