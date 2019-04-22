function DetailShoe(url, id, form) {
    $.get(url + id, function (data) {
        $(form).html(data).dialog("open");
        return false;
    });
}

function EditShoe(id) {
    url = "../SHOes/Edit/";
    $.get(url + id, function (data) {
        $("#dialogForm").html(data).dialog("open");
    })
}

function DeleteShoe(id) {
    url = "../SHOes/Delete/";
    $.get(url + id, function (data) {
        //btnSearchRole();
    });
}






function btnAddRole() {
    $("#CreateRole").toggle(500);
    $("#txtNameRole").val("").focus();
    $("#txtDescRole").val("");
    $("#controlNameRole").fadeOut();
};

function btnCreateRole() {
    if ($("#txtNameRole").val() == "") {
        $("#txtNameRole").focus();
        $("#controlNameRole").fadeIn();
    }
    else if ($("#txtDescRole").val() == "") {
        $("#txtDescRole").focus();
    }
    else {
        NameRole = $("#txtNameRole").val();
        DescRole = $("#txtDescRole").val();
        $.ajax({
            url: '../Tes/Create',
            data: {
                NameRole: NameRole,
                DescRole: DescRole,
            },
            beforeSend:
                function () {
                    $("#loading-role").toggle()
                },
            success:
                function () {
                    $("#txtNameRole").val("");
                    $("#txtDescRole").val("");
                    $("#CreateRole").toggle(500);
                    $("#controlNameRole").fadeOut();
                    $("#loading-role").toggle()
                    btnSearchRole();
                }

        });
    };

};

function btnSearchRole() {
    NameRole = $("#txtSearchRole").val();
    $.ajax({
        url: '../Tes/Search',
        data: {
            NameRole: NameRole,
        },
        success: function (data) {
            $("#table-role").html(data);
        }
    });
}

function txtSearchRole(event) {
    if (event.keyCode == 13) {
        btnSearchRole();
    };
};


function editrole() {
    url = "../TPROC_USER/Bejos/";
    $.get(url, function (data) {
        $("#dialog-form").html(data).dialog("open");

    })
}

