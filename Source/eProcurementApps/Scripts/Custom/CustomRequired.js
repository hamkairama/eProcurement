function IsValidForm() {
    var fields = $(".item-required").find("span");
    var valid = 0;

    for (i = 0; i < fields.length; ++i) {
        if (fields[i].id != "") {
            var field = document.getElementById('required_' + fields[i].id);
            if (fields[i].innerHTML == "Empty" || fields[i].innerHTML == "NaN" || fields[i].innerHTML == "" || fields[i].innerHTML == "0") {
                field.innerHTML = 'must be filled';
                field.style.color = 'red';
                valid = 1;
            } else {
                field.innerHTML = '';
            };
        }        
    };

    return valid;
}

function IsValidSelect() {
    var valid = 0;
    var fieldx = $(".item-required-select").find(".search-choice");
    var field = document.getElementById('required_form-field-select-4');

    if (fieldx.length == 0) {
        field.innerHTML = 'must be filled';
        field.style.color = 'red';
        valid = 1;
    } else {
        field.innerHTML = '';
    };

    return valid;
}

function IsValidTextbox() {
    var fields = $(".item-required").find("input");
    var valid = 0;

    for (i = 0; i < fields.length; ++i) {
        var field = document.getElementById('required_' + fields[i].id);
        if (fields[i].value == "Empty" || fields[i].value == "NaN" || fields[i].value == "" || fields[i].value == "0") {
            field.innerHTML = 'must be filled';
            field.style.color = 'red';
            valid = 1;
        } else {
            field.innerHTML = '';
        };
    };

    return valid;
}

function IsValidApproval() {
    var r_user_id = document.getElementById("txt_user_id2");
    var r_user_name = document.getElementById("txt_user_name");
    var r_mail = document.getElementById("txt_mail");

    var valid = 0;

    var required = document.getElementById("required_txt_approval");
    if (r_user_id.value == "" || r_user_name.value == "" || r_mail.value == "") {
        required.innerHTML = 'Approval must be selected';
        required.style.color = 'red';
        valid = 1;
    } else {
        required.innerHTML = '';
    };

    return valid;
}