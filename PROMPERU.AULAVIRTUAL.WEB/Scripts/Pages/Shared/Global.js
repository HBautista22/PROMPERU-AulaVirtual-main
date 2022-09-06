
$(document).on('click', '[data-type="modal-link"]', function (e) {
    e.preventDefault();
    var $link = $(this);
    // Url de vista a cargar
    var sourceUrl = $(this).attr('data-source-url');
    var onClose = $(this).attr('data-on-close');

    var $modalLoading = $('#default-modal-loading');
    var $modal = $('<div class="modal fade"><div class="modal-dialog"><div class="modal-content"></div></div></div>');
    var $modalContent = $modal.find('.modal-content');

    $modalContent.html($modalLoading.html());

    var $container = $('#default-modal-container');
    $container.append($modal);

    var bootModal = $modal.modal('show');

    //.on('shown.bs.modal', function () {
    $modalContent.load(sourceUrl, function (response, status, xhr) {
        if (status == "error") {
            $modalContent.html($('#default-modal-loading-error').html());
        }
        else {
            RebindJquery($modalContent)
        }
    });
    // });

    bootModal.on('hidden.bs.modal', function () {
        var $this = $(this);

        try {
            if (window[onClose]) {
                window[onClose]($link, $this);
            } else {
                eval(onClose);
            }
        }
        catch (err) {
            console.log('Error en hidden.bs.modal: ' + err.message)
        }

        $this.remove();
    });
});

$('[data-type="cascade-dropdown-list"]').on('change', function (e) {
    // Url de datos json
    var sourceUrl = $(this).attr('data-source-url');

    // Nombre del parámetro de filtro
    var dataFilter = $(this).attr('data-filter');

    // Valor seleccionado
    var selectedId = $(this).val();

    // Select a actualizar
    var updateTarget = $(this).attr('data-update-target');

    var postData = {};
    postData[dataFilter] = selectedId;
    var $updateTarget = $(updateTarget);
    $updateTarget.attr('disabled', 'disabled')
    $updateTarget.find('option[value!=""]').remove();
    $.post(sourceUrl, postData, function (data) {
        var $updateTarget = $(updateTarget);
        var valueMember = $updateTarget.attr('data-value-member')
        var textMember = $updateTarget.attr('data-text-member')
        for (i = 0; i < data.length; i++) {
            $updateTarget.append($('<option>').text(data[i][textMember]).attr('value', data[i][valueMember]));
        }
        $updateTarget.removeAttr('disabled');
        $updateTarget.trigger('change');
    });
});

function doModal(textoTitulo, enlaceBoton, textoBoton, iconoBoton) {
    $('#generic-modal [data-container="titulo"]').html(textoTitulo);
    if (enlaceBoton === '') {
        $('#generic-modal [data-element="boton"]').attr('data-dismiss', 'modal');
    } else {
        $('#generic-modal [data-element="boton"]').attr('href', enlaceBoton);
    }
    $('#generic-modal [data-element="icono"]').removeClass().addClass('fa ' + iconoBoton);
    $('#generic-modal [data-container="texto-boton"]').text(textoBoton);

    $("#generic-modal").modal('show');
}

function AutoTargetAjaxCerrarModal() {
    var element = arguments[3];
    $($(element).data('ajax-update')).closest('.modal').nextAll().modal('hide');

}

function RebindJquery($element) {
    $.validator.unobtrusive.parse($element);
    $element.find('.datepicker').datepicker({ format: "dd/mm/yyyy", autoclose: true, clearBtn: true, language: "es", defaultDate: new Date() });
    $element.find('.datepicker.datepicker-dropdown.dropdown-menu').css('z-index', 9999);
    $element.find('.wysiwyg').wysihtml5({
        "html": false,
        "link": false,
        "image": false,
        "color": false
    });

    $element.find('.select2').select2();

    /*   $element.find('.datepicker').datepicker({
           clearBtn: true,
           language: "es",
           autoclose: true
       });
   
       $element.find('.timepicker').timepicker({
           minuteStep: 5,
           disableFocus: true,
           defaultTime: false
       });*/
}

$(function () {
    $('.select2').select2();

    $('.frameReport').on('load', function () {
        var $this = $(this);
        $('#' + $this.data('target')).hide();
    });

    $('.frameReport').on('error', function () {
        var $this = $(this);
        $('#' + $this.data('target')).hide();
    });

    setTimeout(function () { $('.alert').hide('slow') }, 3000);

});

function reload() {
    location.reload();
}

function KeepAlive() {
    http_request = new XMLHttpRequest();
    http_request.open('GET', "/Home/KeepAlive");
    http_request.send(null);
};

//setInterval(KeepAlive, 200000);
RebindJquery($(document));



(function () {
    try {
        var $_console$$ = console;
        Object.defineProperty(window, "console", {
            get: function () {
                if ($_console$$._commandLineAPI)
                    throw "Sorry, for security reasons, the script console is deactivated on netflix.com";
                return $_console$$
            },
            set: function ($val$$) {
                $_console$$ = $val$$
            }
        })
    } catch ($ignore$$) {
    }
})();