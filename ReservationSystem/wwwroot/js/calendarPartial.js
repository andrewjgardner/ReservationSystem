$(function () {
    const currentDate = new Date();
    filterSittingsOPL(currentDate);  
});

async function getSittingsOnMonth(date) {
    const sittings = await fetch(`/api/sitting/${date.toJson()}`, { cache: 'force-cache' })
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

async function onPageLoadGetSittings(date) {
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

const calendar = $('.calendar-container').calendar({
    weekDayLength: 1,
    date: new Date(),
    onClickDate: selectDate,
    showYearDropdown: true,
    startOnMonday: false,
    prevButton: "<",
    nextButton: ">",
});