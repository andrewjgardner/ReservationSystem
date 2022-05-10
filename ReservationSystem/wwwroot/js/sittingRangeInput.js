$(() => {
    changeScheduleText();
});

$("#numberToSchedule").change(() => {
    changeScheduleText();
});

function changeScheduleText() {
    var str = $("#numberToSchedule").val();
    $("div#numberToScheduleText").html(str);
}