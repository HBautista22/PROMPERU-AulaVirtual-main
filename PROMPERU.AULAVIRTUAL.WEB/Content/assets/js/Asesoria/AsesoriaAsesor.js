function AnularAsesoria(AsesoriaId) {
    $("#dialog-confirm")
        .dialog(
            {
                resizable: false,
                height: "auto",
                width: 400,
                modal: true,
                buttons:
                {
                    "Eliminar": function () {
                        var API = "/AsesoriaAsesor/AnularAsesoria";
                        $.getJSON
                            (API,
                                {
                                    AsesoriaId: AsesoriaId
                                }
                            )
                            .done(function (data) {
                                if (data.Estado == "OK") {
                                    CargarCalendario();
                                    $("<div>Éxito!</div>").dialog();
                                }
                                else {
                                    $("<div>Ocurrio un error!</div>").dialog();
                                }
                            });

                        $(this).dialog("close");
                    },
                    "Cancelar": function () {
                        $(this).dialog("close");
                    }
                },
                open: function () {
                    $(this).closest(".ui-dialog").find(".ui-dialog-titlebar-close").removeClass("ui-dialog-titlebar-close").html("<span class='ui-button-icon-primary ui-icon ui-icon-closethick'></span>");
                }
            });
}

function ShowDetalle(nombre, solicitud, telefono, cargo) {

    $("#solicitud-nombres").html(nombre);
    $("#solicitud-solicitud").html(solicitud);
    $("#solicitud-telefono").html(telefono);
    $("#solicitud-cargo").html(cargo);


    $("#dialog-detalle-solicitud")
        .dialog(
            {
                resizable: false,
                height: "auto",
                width: 400,
                modal: true,
                buttons:
                {
                    "Cerrar": function () {
                        $(this).dialog("close");
                    }
                },
                open: function () {
                    $(this).closest(".ui-dialog").find(".ui-dialog-titlebar-close").removeClass("ui-dialog-titlebar-close").html("<span class='ui-button-icon-primary ui-icon ui-icon-closethick'></span>");
                }
            });
}

function CargarCalendario() {
    calendar.refetchEvents();
}

function AddAsesoriaDetallado() {
    var API = "/AsesoriaAsesor/AsesoriasRemotasAddDetallado";
    $.getJSON
        (API,
            {
                Asre_Nombre: $("#titleScheduled").val(),
                Asre_Inicio: $("#dateScheduled").val() + ' ' + $("#timeScheduled").val() + ":00",
                Asre_Duracion: $("#durationScheduled").val()
            }
        )
        .done(function (data) {
            if (data.Estado == "OK") {
              CargarCalendario();
                $("<div>Éxito!</div>").dialog();
            }
            else {
                $("<div>Ocurrio un error!</div>").dialog();
            }
        });
}

function AddAsesoriaBloque() {
    var API = "/AsesoriaAsesor/AsesoriasRemotasAddBloque";
    $.getJSON
        (API,
            {
                Asre_Nombre: $("#titleScheduledBloque").val(),
                Asre_Inicio: $("#dateScheduledInicioBloque").val() + ' ' + "00:00:00",
                Asre_Fin: $("#dateScheduledFinBloque").val() + ' ' + "00:00:00",
                Asre_Duracion: $("#durationScheduledBloque").val()
            }
        )
        .done(function (data) {
            if (data.Estado == "OK") {
                CargarCalendario();
                $("<div>Éxito!</div>").dialog();
            }
            else {
                $("<div>Ocurrio un error!</div>").dialog();
            }
        });

}





function AceptarSolicitud(SolicitudId) {
    var API = "/AsesoriaAsesor/AceptarSolicitud";
    $.getJSON
        (API,
            {
                SolicitudId: SolicitudId
            }
        )
        .done(function (data) {
            if (data.Estado == "OK") {
                $("<div>Éxito!</div>").dialog();
                setTimeout(function () { location.reload(); }, 2000);
            }
            else {
                $("<div>Ocurrio un error!</div>").dialog();
            }
        });

}


function MostrarRechazar(SolicitudId) {

    $("#SolicitudId").val(SolicitudId);

    //Load Dialog
    $("#dialog-rechazar")
        .dialog(
            {
                resizable: false,
                height: "auto",
                width: 700,
                modal: true,
                buttons:
                {
                    "Aceptar": function () {

                        RechazarSolicitud($("#SolicitudId").val(), $("#Comentario").val());
                        $(this).dialog("close");
                    },
                    "Cancelar": function () {
                        $(this).dialog("close");
                    }
                },
                open: function () {
                    $(this).closest(".ui-dialog").find(".ui-dialog-titlebar-close").removeClass("ui-dialog-titlebar-close").html("<span class='ui-button-icon-primary ui-icon ui-icon-closethick'></span>");
                }
            });

    //Fill Dialog items

}





function RechazarSolicitud(SolicitudId, Comentario) {


    var API = "/AsesoriaAsesor/RechazarSolicitud";
    $.getJSON
        (API,
            {
                SolicitudId: SolicitudId,
                Comentario: Comentario
            }
        )
        .done(function (data) {
            if (data.Estado == "OK") {

                $("<div>Éxito!</div>").dialog();
                setTimeout(function () { location.reload(); }, 2000);

            }
            else {
                $("<div>Ocurrio un error!</div>").dialog();
            }
        });

}

function TerminarSolicitud(SolicitudId) {
    var API = "/AsesoriaAsesor/TerminarSolicitud";
    $.getJSON
        (API,
            {
                SolicitudId: SolicitudId
            }
        )
        .done(function (data) {
            if (data.Estado == "OK") {

                $("<div>Éxito!</div>").dialog();
                setTimeout(function () { location.reload(); }, 2000);

            }
            else {
                $("<div>Ocurrio un error!</div>").dialog();
            }
        });

}