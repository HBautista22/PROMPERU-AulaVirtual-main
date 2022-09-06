function MostrarOpciones(inicio, fin) 
{
    //Load Asesorias from given dates
    var API = "/AsesoriaAsesorado/AsesoriasRemotasListarDisponible/";
    $.getJSON
        (API,
            {
                start: inicio,
                end: fin
            }
        )
        .done(function (data)
        {
            $('#Asesores').empty();
                $.each(data, function (index, item)
                {
                    //obj.Asre_Id = (int)(dr["Asre_Id"]);
                    //obj.Asre_Nombre = (string)(dr["Asre_Nombre"]);
                    //obj.Asre_AsesorId = (int)(dr["Asre_AsesorId"]);
                    //obj.Apellido = (string)(dr["Apellido"]);
                    //obj.Nombre = (string)(dr["Nombre"]);
                    //obj.Cargo = (string)(dr["Cargo"]);
                    
                    //Llenar el dropdown de asesorias
                    $('#Asesores')
                        .append
                        (
                            $('<option></option>').val(item.Asre_Id).html(item.Asre_Nombre + " - " + item.Nombre + " " + item.Apellido + "(" + item.Cargo + ")")
                        );

                });
            //if (data.lenght > 0)
            //{
            //}
            //else
            //{
            //    $("<div>Ocurrio un error!</div>").dialog();
            //}
        });
    




    //Load Dialog
    $("#dialog-confirm")
        .dialog(
            {
                resizable: false,
                height: "auto",
                width: 700,
                modal: true,
                buttons:
                {
                    "Aceptar": function () 
                    {
                        if ($("#Consulta").val() == '') {
                            $("<div>Por favor ingrese motivo de la consulta!</div>").dialog();
                            return false;
                        }

                        SolicitarAsesoria($("#Asesores").val(), $("#Consulta").val());
                        $(this).dialog("close");
                    },
                    "Cancelar": function () 
                    {
                        $(this).dialog("close");
                    }
                },
                open: function () {
                    $(this).closest(".ui-dialog").find(".ui-dialog-titlebar-close").removeClass("ui-dialog-titlebar-close").html("<span class='ui-button-icon-primary ui-icon ui-icon-closethick'></span>");
                }
            });

    //Fill Dialog items

}


function SolicitarAsesoria(idAsesoria,txtConsulta)
{
    var API = "/AsesoriaAsesorado/SolicitarAsesoria/";
    $.getJSON
        (API,
            {
                AsesoriaId: idAsesoria,
                AsesoriaConsulta: txtConsulta
            }
        )
        .done(function (data)
        {
            if (data.Estado == "OK")
            {
                //Refresh page

                location.reload();
                //calendar.refetchEvents();

            }
            else
            {
                $("<div>Ocurrio un error!</div>").dialog();
            }
        });
}


function AnularSolicitud(SolicitudId)
{
    var API = "/AsesoriaAsesorado/AnularSolicitud/";
    $.getJSON
        (API,
            {
                SolicitudId: SolicitudId
            }
        )
        .done(function (data)
        {
            if (data.Estado == "OK")
            {
                //Refresh page

                location.reload();
                //calendar.refetchEvents();

            }
            else
            {
                $("<div>Ocurrio un error!</div>").dialog();
            }
        });
}


