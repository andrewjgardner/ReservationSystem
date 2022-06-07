
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
    $("#sittings-partial").html("");
    const sittings = await getSittingsOnMonth(date);
    let s = getSittingsOnDay(date, sittings);
    if (s.length == 0) {
        $("#sittings-partial").html("");
    }
    let i = 0;
    for (let s of getSittingsOnDay(date,sittings)) {
        i++;
        let interval = new Date(s.startTime);
        let endTime = new Date(s.endTime)

        let el = $($('#accordionExample').html());
        el.find('#sitting-times-header-button').html(`${s.title} ${s.startTime} ${s.endTime}`);
        el.find('#sitting-times-header-button').attr('data-bs-target', `#collpase${i}`);
        el.find('#sitting-times-header-button').attr('aria-controls', `#collpase${i}`);
        el.find('.accordion-collapse.collapse.show').attr('id', `collpase${i}`);
        while (interval < endTime.setMinutes(endTime.getMinutes() - s.resDuration)) {

            let btn = $(`<button type="button" class="btn btn-outline-primary m-1" data-sitting-time="${interval}" data-sitting-id="${s.id}"> ${interval} </button>`);
            btn.click((e) => selectSittingInterval($(e.target).data('sitting-time'),$(e.target).data('sitting-id')));
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