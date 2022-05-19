
$(function () {
    const currentDate = new Date();
    onPageLoadGetSittings(currentDate);  
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

async function onPageLoadGetSittings(date) {

    const sittings = await getSittingsOnMonth(date);

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
        $("#sittings-partial").append(el); 
    } 
}

function selectSittingInterval(date) {
 
    let d = new Date(date);
    //2022-05-19T09:21:00.000
    let x = `${d.getFullYear()}-${d.getMonth() + 1}-${d.getDate()}T${d.getHours()}:${d.getMinutes()}:00.000`;
    debugger;
    $("#ReservationForm_DateTime").val(x);
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
    var sittingmap = sittingsOnDay.map(s => {
        var intervals = new Date(s.startTime);
        var endTime = new Date(s.endTime)
        var intervalList = ``;
        while (intervals < endTime.setMinutes(endTime.getMinutes() - s.resDuration)) {
            intervalList += `<button> ${intervals} </button>`
            intervals.setMinutes(intervals.getMinutes() + 15)
        }
        return `<div class="accordion" id="accordionExample">
                <div class="accordion-item">
                    <h2 class="accordion-header" id="headingOne">
                        <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                        ${s.title} - ${s.startTime} - ${s.endTime}
                        </button>
                    </h2>
                    <div id="collapseOne" class="accordion-collapse collapse show" aria-labelledby="headingOne" data-bs-parent="#accordionExample">
                        <div class="accordion-body">
                        ${intervalList}
                        </div>
                    </div>
                 </div>
                 </div>`

    });
    $(".sittings-partial").html(sittingmap);        
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