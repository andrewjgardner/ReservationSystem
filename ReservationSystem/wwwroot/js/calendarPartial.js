$(function () {
    var date = new Date();
    const sittings = filterSittingsOPL(date);  
    console.log(sittings)
});

async function getSittingsOnMonth(date) {
    var jsonDate = date.toJSON();
    const sittings = await fetch(`/api/sittings/${jsonDate}`, { cache: 'force-cache' })
        .then(res => res.json())
        .then(d => { return d });
    sessionStorage.setItem('sittings', JSON.stringify(sittings));
    return sittings;
}

function getSittingsOnDay(selectedDate, sittings) {

    return sittingsFilter = sittings.filter(s => {
        var date = new Date(s.startTime);
        return date.getDate() == selectedDate.getDate();
    });
}

async function filterSittingsOPL(date) {
    const sittings = await getSittingsOnMonth(date);
    const sittingsOnDay = getSittingsOnDay(date, sittings);
    $(".sittings-partial").html(sittingsOnDay.map(s => {
        return `${s.startTime} - ${s.endTime}`
    }));
    return sittingsOnDay;
}

function selectDate(date) {
    $('.calendar-container').updateCalendarOptions({
        date: date
    });
    updateSessionPartial(date);
}

async function updateSessionPartial(date) {
    const selectedDate = new Date(date);
    const sittings = JSON.parse(sessionStorage.getItem('sittings'))
    const sittingsOnDay = getSittingsOnDay(selectedDate, sittings);
    $(".sittings-partial").html(sittingsOnDay.map(s => {
        return `${s.startTime} - ${s.endTime}`
    }));        
}


var calendar = $('.calendar-container').calendar({
    weekDayLength: 1,
    date: new Date(),
    onClickDate: selectDate,
    showYearDropdown: true,
    startOnMonday: false,
    prevButton: "<",
    nextButton: ">",
});