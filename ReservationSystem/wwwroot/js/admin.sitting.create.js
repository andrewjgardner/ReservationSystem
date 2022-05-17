$(() => {

	var slider = $('#numberToSchedule');
	var output = $('numberToScheduleText');
	output.value = slider.value;

	slider.oninput = function () {
		output.value = this.value;
	}

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
	});

	$('#start-time').change((e) => {
		let startday = new Date($('#start-time').val()).getDay();
		for (i = 0; i < 7; i++) {
			$('#recur-day-' + i).attr("disabled", false);
		}
		$('#recur-checkbox-day-' + startday).prop("checked", true);
		$('#recur-hidden-day-' + startday).val(true);
		$('#recur-checkbox-day-' + startday).attr("disabled", true);
	});

	for (i = 0; i < 7; i++) {
		$('#recur-checkbox-day-' + i).change((e) => {
			$('#recur-hidden-day-' + i).toggle;
		});
    }

	function calculateEndDate(date, recurringType, sliderval) {
		if (recurringType == "Daily") {
			const addDays = sliderval;
		}
		else if (recurringType == "Weekly") {
			const addD

        }
	}

	function disableRecurringSittingDay() {
		let startday = new Date($('#start-time').val()).getDay();
		for (i = 0; i < 7; i++) {
			$('#recur-day-' + i).attr("disabled", false);
		}
		$('#recur-checkbox-day-' + startday).prop("checked", true);
		$('#recur-hidden-day-' + startday).val(true);
		$('#recur-checkbox-day-' + startday).attr("disabled", true);
    }

});