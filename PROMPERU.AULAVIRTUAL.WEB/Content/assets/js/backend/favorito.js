$(function () {
    $('.favorite-card input').click(function () {

        var pCursoOnlineId = $(this).attr('id').replace('favorite-', '');
        var pestadoFavorito = $(this).is(':checked');
        //alert(pCursoOnlineId);
        //alert(pestadoFavorito);
        var API = "/CursoOnlineFavorito/favorito";
        $.getJSON
            (API,
                {
                    CursoOnlineId: pCursoOnlineId,
                    estadoFavorito: pestadoFavorito
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
    })

    $('.favorite-card input').click(function () {

        var pCursoOnlineId = $(this).attr('id').replace('favorite-2-', '');
        var pestadoFavorito = $(this).is(':checked');
        //alert(pCursoOnlineId);
        //alert(pestadoFavorito);
        var API = "/CursoOnlineFavorito/favorito";
        $.getJSON
            (API,
                {
                    CursoOnlineId: pCursoOnlineId,
                    estadoFavorito: pestadoFavorito
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
    })
});