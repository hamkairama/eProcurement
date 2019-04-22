var fullOrigin = window.location.href;
var linkProc = location.origin + "/eprocurement";

function ConvertDate(dt) {
    var split = dt.split('-');
    var result = split[1] + '-' + split[0] + '-' + split[2]

    return result;
}

function RegexDate(str) {
    var regexValid = 0;
    var m = str.match(/^(\d{1,2})-(\d{1,2})-(\d{4})$/);
    if (m == null) {
        regexValid = 1;
    }
    return regexValid;
}

function RegexNumber(str) {
    var regexValid = 0;
    var m = str.match(/^\d+$/);
    if (m == null) {
        regexValid = 1;
    }
    return regexValid;
}

function IsValidDate() {
    var fields = document.getElementsByClassName("dateText");
    var valid = 0;

    for (i = 0; i < fields.length; ++i) {
        var field = document.getElementById('required_' + fields[i].id);
        var str = fields[i].innerHTML;
        if (RegexDate(str) == 1) {
            field.innerHTML = 'format required dd-mm-yyyy';
            field.style.color = 'red';
            valid = 1;
        } else {
            field.innerHTML = '';
        };
    };

    return valid;
}


function IsValidLength() {
    var fields = $(".item-required").find("span");
    var valid = 0;

    for (i = 0; i < fields.length; ++i) {
        if (fields[i].id != "") {
            var maxlength = $("#" + fields[i].id).attr("maxlenght");
            var field = document.getElementById('required_' + fields[i].id);
            if (fields[i].innerText.length > maxlength) {
                field.innerHTML = 'max lenght ' + maxlength;
                field.style.color = 'red';
                valid = 1;
            } else {
                field.innerHTML = '';
            };
        }       
    };

    return valid;
};

function IsValidLengthNonMandatory() {
    var fields = $(".freeText");
    var valid = 0;

    for (i = 0; i < fields.length; ++i) {
        var maxlength = $("#" + fields[i].id).attr("maxlenght");
        var field = document.getElementById('required_' + fields[i].id);
        if (fields[i].innerHTML.length > maxlength) {
            field.innerHTML = 'max lenght ' + maxlength;
            field.style.color = 'red';
            valid = 1;
        } else {
            field.innerHTML = '';
        };
    };

    return valid;
};

function IsValidChecked() {
    var fields = document.getElementsByClassName("mandatoryChecked");
    var valid = 0;

    for (i = 0; i < fields.length; ++i) {
        var field = document.getElementById('mandatory_checked_' + fields[i].id);
        //var isChecked = fields[i].is(":checked");
        if (!fields[i].checked) {
            field.innerHTML = 'Input must be checked';
            field.style.color = 'red';
            valid = 1;
        } else {
            field.innerHTML = '';
        };
    };

    return valid;
};

function IsOnlyNumberInput() {
    var fields = document.getElementsByClassName("onlyNumber");
    var valid = 0;

    for (i = 0; i < fields.length; ++i) {
        var field = document.getElementById('only_number_' + fields[i].id);
        var str = fields[i].innerHTML;
        if (RegexNumber(str) == 1) {
            field.innerHTML = 'Input only number';
            field.style.color = 'red';
            valid = 1;
        } else {
            field.innerHTML = '';
        };
    };

    return valid;
};

function ReadyToExec() {
    var fields = $(".profile-info-value").find("span");

    for (i = 0; i < fields.length; ++i) {
        var field = document.getElementById(fields[i].id);
        if (fields[i].innerHTML == "Empty" || fields[i].innerHTML == "NaN") {
            field.innerHTML = '';
        };
    };
};


function Comma(nStr) {
    nStr = nStr.replace(/,/g, "")
    nStr += '';
    x = nStr.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1))
        x1 = x1.replace(rgx, '$1' + ',' + '$2');

    var result = x1 + x2;
    return result;
}

function fnExcelReport(idButton, idTable, nmReport) {
    var tab_text = '<html xmlns:x="urn:schemas-microsoft-com:office:excel">';
    tab_text = tab_text + '<head><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet>';

    tab_text = tab_text + '<x:Name>' + nmReport + '</x:Name>';

    tab_text = tab_text + '<x:WorksheetOptions><x:Panes></x:Panes></x:WorksheetOptions></x:ExcelWorksheet>';
    tab_text = tab_text + '</x:ExcelWorksheets></x:ExcelWorkbook></xml></head><body>';

    //tab_text = tab_text + "<table>";
    //tab_text = tab_text + $('#' + idTableHead).html();
    //tab_text = tab_text + '</table>';

    tab_text = tab_text + "<table>";
    tab_text = tab_text + $('#' + idTable).html();
    tab_text = tab_text + '</table></body></html>';

    var data_type = 'data:application/vnd.ms-excel';

    var ua = window.navigator.userAgent;
    var msie = ua.indexOf("MSIE ");

    if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./)) {
        if (window.navigator.msSaveBlob) {
            var blob = new Blob([tab_text], {
                type: "application/csv;charset=utf-8;"
            });
            navigator.msSaveBlob(blob, nmReport + '.xls');
        }
    } else {
        $('#' + idButton).attr('href', data_type + ', ' + encodeURIComponent(tab_text));
        $('#' + idButton).attr('download', nmReport + '.xls');
    }

};

//function selectElementContents() {
//    var table = document.getElementById('dynamic-table');
//    // Below line is essential !!!
//    table.contentEditable = 'true';

//    var controlRange = document.body.createControlRange();
//    controlRange.addElement(table);
//    controlRange.execCommand("Copy");

//}


function selectElementContents(el) {
    var body = document.body, range, sel;
    if (document.createRange && window.getSelection) {
        range = document.createRange();
        sel = window.getSelection();
        sel.removeAllRanges();
        try {
            range.selectNodeContents(el);
            sel.addRange(range);
        } catch (e) {
            range.selectNode(el);
            sel.addRange(range);
        }
    } else if (body.createTextRange) {
        range = body.createTextRange();
        range.moveToElementText(el);
        range.select();
        range.execCommand("Copy");
    }
}

$.extend({
    xResponse: function (url, data) {
        // local var
        var theResponse = null;
        // jQuery ajax
        $.ajax({
            url: url,
            cache: false,
            traditional: true,
            type: 'POST',
            data: data,
            dataType: "html",
            async: false,
            success: function (respText) {
                theResponse = respText;
            }
        });
        // Return the response text
        return theResponse;
    },
})

function GetRowIndex(el) {
    while ((el = el.parentNode) && el.nodeName.toLowerCase() !== 'tr');

    return el.rowIndex;
}

function GetRow(el) {
    while ((el = el.parentNode) && el.nodeName.toLowerCase() !== 'tr');

    return el
}

function ConvertDateToJsFormat(dt) {
    var newDate = new Date();
    var arrDt = dt.split("-");

    newDate.setDate(arrDt[0]);
    newDate.setMonth(arrDt[1] - 1); //i dont know why
    newDate.setYear(arrDt[2]);

    newDate.setMilliseconds(0);
    newDate.setSeconds(0);
    newDate.setMinutes(0);
    newDate.setHours(0);

    return newDate;
}