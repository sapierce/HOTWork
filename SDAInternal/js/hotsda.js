var todayDate = new Date();

$("#displayToday").html(todayDate);

if (sessionStorage.displayDate) {
    $("#displayCurrent").html(sessionStorage.displayDate);
} else {
    $("#displayCurrent").html(todayDate);
}