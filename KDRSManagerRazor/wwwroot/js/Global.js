//Karina
var karvar = 5;
console.log(karvar);
console.log(getDate("from"));

$(document).ready(function () {
    console.log('global ready');
    console.log(karvar);
});

function saveDate(type, date) {
    if (typeof (Storage) !== "undefined") {
        localStorage.setItem(type, date);
    }
}
function getDate(type) {
    var date = localStorage.getItem(type);
    return date;
}