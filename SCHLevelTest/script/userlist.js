var uri = 'http://localhost:16947/api/usuario';

document.domain = "localhost";

$(document).ready(function () {
    jQuery.support.cors = true;

    var strBtoa = $("#hdUser").val() + ":" + $("#hdPwd").val();

    $.ajax({
        url: uri,
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            $.each(data, function (key, item) {
                $('<li>').html(formatItem(item)).appendTo($('#usuarios'));
            });
        },
        beforeSend: function (xhr) {
            xhr.setRequestHeader("Authorization", "Basic " + btoa(strBtoa));
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });

    $(".add").click(function () {
        $("#form").show();
        $("#txid").val('');
        $("#txnombre").val('');
        $("#txhome").val('');
        $("#txid").prop("disabled", false);
        $("#cbroles option").prop('selected', false);
    });

    $(document).on("click", ".item", function () {
        var id = $(this).attr("id");
        $.ajax({
            url: uri + "/" + id,
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                formatDetalle(data);
            },
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", "Basic " + btoa(strBtoa));
            },
            error: function (x, y, z) {
                alert(x + '\n' + y + '\n' + z);
            }
        });
    });

    $(document).on("click", "#btnClose", function () {
        $("#form").hide();
    });

    $(document).on("click", "#btnAdd", function () {

        if (!FormUserValid()) {
            alert("Wrong data");
            return;
        }

        var user = {
            Id: $('#txid').val(),
            Nombre: $('#txnombre').val(),
            Home: $('#txhome').val()
        };

        user.Roles = [];
        $('#cbroles option:selected').each(function () {
            user.Roles.push($(this).val())
        });

        var _verb = "POST";
        var _auxURI = uri

        if ($('#txid').is(':disabled')) {
            _verb = "PUT";
            _auxURI = uri + "/" + user.Id;
        }

        $.ajax({
            url: _auxURI,
            type: _verb,
            data: JSON.stringify(user),
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                document.location.reload();
            },
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", "Basic " + btoa(strBtoa));
            },
            error: function (x, y, z) {
                debugger;
                alert(x + '\n' + y + '\n' + z);
            }
        });
    });

    $(document).on("click", ".rem", function () {
        var id = $(this).attr("id");
        $.ajax({
            url: uri + "/" + id,
            type: 'DELETE',
            dataType: 'json',
            success: function (data) {
                document.location.reload();
            },
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", "Basic " + btoa(strBtoa));
            },
            error: function (x, y, z) {
                alert(x + '\n' + y + '\n' + z);
            }
        });
    });
});

function formatItem(item) {
    return "<a href='#' id='" + item.Id + "' class='rem' title='Borrar'>B</a> - <a href='#' id='" + item.Id + "' class='item'>" + item.Id + " - " + item.Nombre + "</a>";
}
function formatDetalle(item) {
    $("#txid").val(item.Id);
    $("#txid").prop("disabled", true);
    $("#txnombre").val(item.Nombre);
    $("#txhome").val(item.Home);
    $.each(item.Roles, function (key, value) {
        $("#cbroles option[value='" + value + "']").prop('selected', true);
    });
    $("#form").show();
}
function FormUserValid() {
    if ($('#txid').val() == '')
        return false;
    if ($('#txnombre').val() == '')
        return false;
    if ($('#txhome').val() == '')
        return false;

    return true;
}