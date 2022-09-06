
var ajaxCall;

function cancelRequest() {
    ajaxCall.abort();
}

$(function () {

    //let mySelect = new vanillaSelectBox("#cbosector", {
    //    translations: {
    //        "all": "All",
    //        "items": "items"
    //    },
    //    placeHolder: "[- Seleccionar -]",
    //});

    $('input').attr('autocomplete', 'off');

    $(function () {

        var value = $(this).val();
        var section = $("#empresaSection");

        
            //section.hide('slow');
            $(".texto-sector").text("Cuál es el sector de interés");
            //$("#cbosector").removeAttr("multiple");
            //$("#cbosector").removeAttr("size");

        
        //let mySelect = new vanillaSelectBox("#cbosector");
        //mySelect.empty();


    });

    $('#DNI').blur(function () {

        var $this = $(this);
        var value = $this.val();

        if (value && value.length === 8 && $("[name='DNI']").valid() && $("#cbodocumento").find(".active").text() == "DNI") {

            ajaxCall = $.ajax({
                data: { dni: value },
                method: 'post',
                //url: '@Url.Action("FindPersonByDNI")',
                url: 'FindPersonByDNI',
                beforeSend: function () {
                    $('.loader-container').show();
                    document.activeElement.blur();
                },
                success: function (data) {
                    if (data.status !== "200") {
                        toastr.error('DNI incorrecto', data.message);
                        return false;
                    }
                    console.log(data);
                    toastr.success('DNI correcto', 'Por favor, completa los campos en blanco')
                    $('#Nombres').val(data.result[0].nombres);
                    $('#Apellidos').val(data.result[0].paterno + ' ' + data.result[0].materno);
                    $('#Direccion').val(data.result[0].direccion);

                },
                error: function (jqXHR, exception) {
                    if (jqXHR.statusText === "abort") {
                        toastr.warning('Ha cancelado la consulta', '');
                        return false;
                    }
                    toastr.error('Ha ocurrido un error en la consulta Reniec', '');
                    console.log(jqXHR);
                },
                complete: function () {
                    $('.loader-container').hide()
                }
            })

        }
    });
    
    $(".dropdown-item").on("click", function () {
        $("#cbodocumento").find("a").removeClass("active");
        $(this).addClass("active");

        if ($(this).text() == "DNI") {
            $("#DNI").attr('maxlength', '8');
            $("form").removeData("validator");
            // change the range
            $("#DNI").attr("data-val-length-min", "8");
            // reapply the form's validator
            $.validator.unobtrusive.parse(document);

            $("#lbldocumento").text("DOCUMENTO DNI");
            
        }
        else if ($(this).text() == "OTROS") {
            $("#DNI").attr('maxlength', '20');
            // reset the form's validator
            $("form").removeData("validator");

            // change the range
            $("#DNI").attr("data-val-length-min", "6");

            // reapply the form's validator
            $.validator.unobtrusive.parse(document);

            $("#lbldocumento").text("DOCUMENTO EXTRANJERO");

        }

        $("#cbodocumento_val").val($(this).text());
        $("#Nacionalidad").val($(this).text());

        $("#DNI").attr("readonly", false);
        $("#DNI").val("");
        $("#DNI").focus();


    });

    $("#DNI").on("keypress", function (e, pid) {

        var identifica = $("#cbodocumento_val").val();
        var patt = new RegExp("^[0-9]+$");
        var code = e.charCode || e.keyCode;
        var input = $(this).val() + String.fromCharCode(code);

        if (identifica == "DNI") {
            return patt.test(input);
        }


    });

    $("#Email").blur(function () {

        if ($('input[name="TipoRegistro"]:checked').val() == "0") {
            $.post("Consultar_Cuenta", { correo: $(this).val() }, function (data) {
                if (data == 1) {
                    alertas();
                }
            });
        }

    });


    function alertas() {
        $('#mydialog').modal('show');
    }


    $("#btnconversion").on("click", function () {

        $("#Conversion").val("1");

    });


    $("#btnrechazo").on("click", function () {

        location.href = "/login";

        $("#Conversion").val("0");

    });



    $.validator.addMethod('dni', function (value, element) {
        var pattern = "^[0-9]+$";
        return value.test(pattern);
    }, 'Number must be divisible by 5');


    $("#UsuarioAsesor_AceptaTerminosCondiciones").on("click", function () {
        
        if ($(this).is(":checked") == true) {
            $("#UsuarioAsesor_AceptaTerminosCondiciones").val(false);
            $('#UsuarioAsesor_AceptaTerminosCondiciones2').val("1");
        }
        else {
            $("#UsuarioAsesor_AceptaTerminosCondiciones").val(true);
            $('#UsuarioAsesor_AceptaTerminosCondiciones2').val("0");
        }

    });


});