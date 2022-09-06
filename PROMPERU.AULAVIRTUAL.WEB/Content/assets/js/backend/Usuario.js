$(function () {

    $(document).ready(function () {

        $("#chkmarcaopcion").click(function () {
            $('input:checkbox').not(this).prop('checked', this.checked);
            if (this.checked) {

                llenar_hidden_option();

            }
            else {
                $("#MarcaAlgunos").val("");
            }

        });


        $("#lnkactual").click(function () {
            $('input:checkbox').prop('checked', true);
            llenar_hidden_option();
        });

        $("#lnktotal").click(function () {
            $('input:checkbox').prop('checked', true);
            $("#MarcaTodo").val("1");
        });

    });

    if ($("#MarcaTodo").val() == 1) {

        $('input:checkbox').prop('checked', true);

    }



    $("input[name='chkopcion']").on("change", function () {

        llenar_hidden_option();

    });



});


function llenar_hidden_option() {

    var ids = [];
    var parstring = "";

    $.each($("input[name='chkopcion']:checked"), function () {

        ids.push($(this).val());

    });

    parstring = btoa(JSON.stringify(ids));

    $("#MarcaAlgunos").val(parstring);

}


$("#btnreset").on("click", function () {

    location.href = "/User/ListUsuario";

});

function openChangeAvatar()
{
    

    $("#dialog-change-avatar")
        .dialog(
            {
                resizable: false,
                height: "auto",
                width: "auto",
                modal: true,
                buttons:
                {
                    "Seleccionar": function () 
                    {
                        var API = "/User/ActualizarCargo";
                        $.getJSON
                            (API,
                                {
                                    Cargo: $("input[name='cargo']:checked").val()
                                }
                            )
                            .done(function (data)
                            {
                                if (data.Estado == "OK")
                                {
                                    $("<div>Éxito!</div>").dialog();
                                    
                                }
                                else
                                {
                                    $("<div>Ocurrio un error!</div>").dialog();
                                }


                            });

                        $(this).dialog("close");
                    },
                    "Cancelar": function () 
                    {
                        $(this).dialog("close");
                    }
                }
            });
    
}