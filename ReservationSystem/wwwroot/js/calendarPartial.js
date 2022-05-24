
$(function () {
    const currentDate = new Date();
    GetSittings(currentDate);  
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

    const sittings = await getSittingsOnMonth(date);
    let s = getSittingsOnDay(date, sittings);
    if (s.length == 0) {
        $("#sittings-partial").html("");
    }
    for (let s of getSittingsOnDay(date,sittings)) {
        
        let interval = new Date(s.startTime);
        let endTime = new Date(s.endTime)

        let el = $($('#accordian-template').html());
        el.find('#sitting-times-header-button').html(`${s.title} ${s.startTime} ${s.endTime}`);
     
        while (interval < endTime.setMinutes(endTime.getMinutes() - s.resDuration)) {

            let btn = $(`<button type="button" data-sitting-time="${interval}"> ${interval} </button>`);
            btn.click((e) => selectSittingInterval($(e.target).data('sitting-time')));
            el.find('#sitting-times-accordian-body').append(btn);
            interval.setMinutes(interval.getMinutes() + 15)
        }
        $("#sittings-partial").html(el); 
    } 
}

function selectSittingInterval(date) {
    let d = new Date(date);
    const format = `${d.toLocaleDateString()} ${d.toLocaleTimeString()}`;
    $("#ReservationForm_DateTime").val(format);
}

function selectDate(date) {
    $('.calendar-container').updateCalendarOptions({
        date: date
    });
    updateSessionPartial(date);
}

async function updateSessionPartial(date) {
    var d = new Date(date);
    GetSittings(d);
        
}

const calendar = $('.calendar-container').calendar({
    weekDayLength: 1,
    date: new Date(),
    onClickDate: selectDate,
    onChangeMonth: updateSessionPartial,
    showYearDropdown: true,
    startOnMonday: false,
    prevButton: "<",
    nextButton: ">",
});