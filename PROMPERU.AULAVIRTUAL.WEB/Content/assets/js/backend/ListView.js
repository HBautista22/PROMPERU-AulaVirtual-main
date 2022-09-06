    $(function () {

        $(".connectedSortable").sortable({
            connectWith: ".connectedSortable",
            items: ':not(.col-lg-6):not(.emptyMessage)',
            opacity: 0.6,
            cursor: 'move',
            'dropOnEmpty': true,
            'scroll': true,

            receive: function (event, ui) {
                var items = $("#enablesCourse").sortable("toArray");
                var jitems = JSON.stringify(items);
                $("#hitems").val(btoa(jitems));
            }


        }).disableSelection();
    });



    $(document).ready(function () {

        $("#Usuario_Id").val($("#MarcaAlgunos").val());
        $("#MarcarTodo").val($("#MarcaTodo").val());

        $('#txtbusqueda2').bind('keyup', function () {

            var searchString = $(this).val();

            $("#disabledCourse li").each(function (index, value) {

                var currentName = $(value).text();
                if (currentName.toUpperCase().indexOf(searchString.toUpperCase()) > -1) {
                    $(value).show();
                } else {
                    $(value).hide();
                }

            });

        });

        $("#btnatras").on("click", function () {

            // $('.loader-container').show();

            var html_li = "";
            var arr_li = []

            $("#disabledCourse li").each(function (index, value) {

                arr_li.push($(value).prop("id"));
                if ($(value).is(":visible")) {
                    html_li += "<li class='list-group-item ui-sortable-handle' data-rol='" + $(value).data("rol") + "' id='" + $(value).prop("id") + "'>" + $(value).text() + "</li>";
                }


            });

            $("#disabledCourse li").remove(":visible");

            $("#enablesCourse").append(html_li);

            var items = $("#enablesCourse").sortable("toArray");
            var jitems = JSON.stringify(items);
            $("#hitems").val(btoa(jitems));

            $('.loader-container').hide();


        });


        $("#btnadelante").on("click", function () {

            var html_li = "";
            var arr_li = [];

            $("#enablesCourse li").each(function (index, value) {

                arr_li.push($(value).prop("id"));
                if ($(value).is(":visible")) {
                    html_li += "<li class='list-group-item ui-sortable-handle' data-rol='" + $(value).data("rol") + "' id='" + $(value).prop("id") + "'>" + $(value).text() + "</li>";
                }
            });

            $("#enablesCourse li").remove(":visible");

            $("#disabledCourse").append(html_li);

            var items = $("#enablesCourse").sortable("toArray");
            var jitems = JSON.stringify(items);
            $("#hitems").val(btoa(jitems));

        });


        $("#cbofiltro2").on("change", function () {


            var seleccion = $(this).children("option:selected").val();


            $("#disabledCourse").children().hide()
                .filter(function () {
                    return $(this).data('rol') === seleccion;
                })
                .show();
        });


        $("#cbofiltro1").on("change", function () {

            var seleccion = $(this).children("option:selected").val();

            $("#enablesCourse").children().hide()
                .filter(function () {
                    return $(this).data('rol') === seleccion;
                })
                .show();
        });


        $('#txtbusqueda1').bind('keyup', function () {

            var searchString = $(this).val();

            $("#enablesCourse li").each(function (index, value) {

                var currentName = $(value).text();
                if (currentName.toUpperCase().indexOf(searchString.toUpperCase()) > -1) {
                    $(value).show();
                } else {
                    $(value).hide();
                }

            });

        });

    });
