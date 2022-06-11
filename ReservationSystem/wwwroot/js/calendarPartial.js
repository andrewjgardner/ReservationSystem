
$(async function () {
    const currentDate = new Date();
    sittings = await getSittingsOnMonth(currentDate);
    $('.calendar-container').updateCalendarOptions({
        date: currentDate,
        disable: disableDates
    });
});

async function getSittingsOnMonth(date) {
    const sittings = await fetch(`/api/sitting/${date.toJSON()}`, { cache: 'force-cache' })
        .then(response => response.json())
        .then(data => { return data });
    sessionStorage.setItem('sittings', JSON.stringify(sittings));
    return sittings;
}

function getSittingsOnDay(selectedDate, sittings) {
    return sittingsFilter = sittings.filter(s => {
        const date = new Date(s.startTime);
        return date.getDate() == selectedDate.getDate();
    });
}

async function GetSittings(date) {
    $("#sittings-partial").html("");
    const sittings = JSON.parse(sessionStorage.getItem('sittings'));
    let s = getSittingsOnDay(date, sittings);
    if (s.length == 0) {
        $("#sittings-partial").html("");
    }
    let i = 0;
    for (let s of getSittingsOnDay(date, sittings)) {
        i++;
        let interval = new Date(s.startTime);
        let endTime = new Date(s.endTime)

        let el = $($('#accordionExample').html());
        el.find('#sitting-times-header-button').html(`${s.title} `);
        el.find('#sitting-times-header-button').attr('data-bs-target', `#collpase${i}`);
        el.find('#sitting-times-header-button').attr('aria-controls', `#collpase${i}`);
        el.find('.accordion-collapse.collapse').attr('id', `collpase${i}`);
        while (interval < endTime.setMinutes(endTime.getMinutes() - s.resDuration)) {

            let btn = $(`<button type="button" class="btn btn-secondary m-1 time-slot" data-sitting-time="${interval}" data-sitting-id="${s.id}"> ${interval.toLocaleTimeString('en-AU', { hour: 'numeric', minute: 'numeric', hour12: true })} </button>`);
            btn.click((e) => {
                $('.time-slot').removeClass('btn-success');
                $(e.target).addClass('btn-success');
                selectSittingInterval($(e.target).data('sitting-time'), $(e.target).data('sitting-id'));
            });
            el.find('#sitting-times-accordian-body').append(btn);
            interval.setMinutes(interval.getMinutes() + 15)
        }
        $("#sittings-partial").append(el);
    }
}

function selectSittingInterval(date, id) {
    let d = new Date(date);
    const format = `${d.toLocaleDateString()} ${d.toLocaleTimeString()}`;
    $("#ReservationForm_DateTime").val(format);
    $("#SittingId").val(id);
    console.log(format);
    console.log(id);
}

function selectDate(date) {
    let d = new Date(date);
    $('.calendar-container').updateCalendarOptions({
        date: date
    });
    $('.calendar-container').hide();
    $('#calendar-show').show();
    $('#calendar-show').html(d.toLocaleDateString());
    updateSessionPartial(date);
}

$("#calendar-show").click(() => {
    $('#calendar-show').hide();
    $('.calendar-container').show();
    $("#sittings-partial").html("");
})

async function updateSessionPartial(date) {
    var d = new Date(date);
    d.setHours(14);
    const sittings = await getSittingsOnMonth(d);
    $('.calendar-container').updateCalendarOptions({
        date: date,
        disable: disableDates
    });
    GetSittings(d);
}

const calendar = $('.calendar-container').calendar({
    weekDayLength: 1,
    date: new Date(),
    onClickDate: selectDate,
    onChangeMonth: updateSessionPartial,
    onClickMonthNext: updateSessionPartial,
    onClickMonthPrev: updateSessionPartial,
    startOnMonday: false,
    prevButton: "<",
    nextButton: ">",
    disable: disableDates,
});

function disableDates(date) {
    const sittings = JSON.parse(sessionStorage.getItem('sittings'));
    for (var i = 0; i < sittings.length; i++) {
        let day = new Date(sittings[i].startTime);
        if (day.getDate() == date.getDate()) {
            return false;
        }
    }
    return true;
}