//Karina
var karvar = 5;
console.log(karvar);
console.log(getDate("from"));

$(document).ready(function () {
    console.log('global ready');
    console.log(karvar);
    //location.reload();
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

function getClicked(string) {
    var Clicked = localStorage.getItem("clicked");
    var clickedstr = Clicked.split(";");
    if (string === "srv") {
        return clickedstr[0];
    } else if (string === "cmpid") {
        return clickedstr[1];
    } else if (string === "rpid") {
        return clickedstr[2];
    } else {
        return "not found";
    }
}