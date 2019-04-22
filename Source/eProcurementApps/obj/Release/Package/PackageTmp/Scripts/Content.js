function GetSidebar(route, action, child, parent) {
    //get data
    url = "../" + route + "/" + action + "/";
    $.get(url, function (data) {
        $("#dialog-form").html(data);
    });

    //set active parent
    var elms = document.getElementsByClassName('active open')
    for (var i = 0; i < elms.length; i++) {
        if (elms[i].getAttribute("Class") === "active open") {
            elms[i].setAttribute("Class", "no aktive open");
        }
    }
    document.getElementById(parent).setAttribute("Class", "active open");

    //set active child
    var elms = document.getElementsByClassName('active')
    for (var j = 0; j < elms.length; j++) {
        if (elms[j].getAttribute("Class") === "active") {
            elms[j].setAttribute("Class", "no aktive");
        }
    }
    document.getElementById(child).setAttribute("Class", "active");

    //set breadcrumbs
    document.getElementById('breadcrumbsParent').innerHTML = parent;
    document.getElementById('breadcrumbsChild').innerHTML = child;

    //set header title
    document.getElementById("headerTitle").innerHTML = child;
   
}

function Control(route, action) {
    url = "../" + route + "/" + action + "/";
    $.get(url, function (data) {
        $("#dialog-form").html(data);
    });

}