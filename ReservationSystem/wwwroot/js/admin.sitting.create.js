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


});