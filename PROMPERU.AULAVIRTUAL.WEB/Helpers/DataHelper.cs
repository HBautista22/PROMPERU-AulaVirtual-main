using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace PROMPERU.AULAVIRTUAL.WEB.Helpers
{
    public class DataHelper<TModel>
    {
        private AppWebViewPage<TModel> dataWebViewPage { get; set; }

        public DataHelper(AppWebViewPage<TModel> dataWebViewPage)
        {
            if (dataWebViewPage == null)
            {
                throw new ArgumentNullException("dataWebViewPage");
            }
            this.dataWebViewPage = dataWebViewPage; 
        }
        /*
         * data-type="modal-link" data-source-url="@Url.Action("EditAutor", new { ExperienciaExitosaId = Model.ExperienciaExitosaId, AutorId = item.AutorId})"
         *  new  data-type="modal-link" data-source-url="@Url.Action("EditAutor", new { ExperienciaExitosaId = Model.ExperienciaExitosaId, AutorId = item.AutorId})"
         */

        public IHtmlString ModalLink(String action)
        {
            return ModalLink(action, null, null);
        }

        public IHtmlString ModalLink(String action, String controller)
        {
            return ModalLink(action, controller, null);
        }

        public IHtmlString ModalLink(String action, object routeValues)
        {
            return ModalLink(action, null, routeValues);
        }

        public IHtmlString ModalLink(String action, String controller, object routeValues)
        {
            var result = "data-type=\"modal-link\" data-source-url=\"" + dataWebViewPage.Url.Action(action, controller, routeValues) + "\"";
            return new HtmlString(result);
        }

        public IHtmlString ValidationFor<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression)
        {
            var html = dataWebViewPage.Html;
            var propertyName = html.NameFor(propertyExpression).ToString();
            var metadata = ModelMetadata.FromLambdaExpression(propertyExpression, html.ViewData);
            var attributes = html.GetUnobtrusiveValidationAttributes(propertyName, metadata);
            return new HtmlString(String.Join(" ", attributes.Select(x => String.Format("{0}=\"{1}\"", x.Key, x.Value)).ToArray()));
        }


        public IHtmlString ModalDialog(String action, String controller)
        {
            return ModalDialog(action, controller, null, null, null, null);
        }
        public IHtmlString ModalDialog(String action, object routeValues)
        {
            return ModalDialog(action, null, routeValues, null, null, null);
        }
        public IHtmlString ModalDialog(String textoTitulo, String textoBoton, String iconoBoton)
        {
            return ModalDialog(null, null, null, textoTitulo, textoBoton, null);
        }
        public IHtmlString ModalDialog(String action, String controller, object routeValues)
        {
            return ModalDialog(action, controller, routeValues, null, null, null);
        }
        public IHtmlString ModalDialog(String action, String controller, object routeValues, String textoTitulo)
        {
            return ModalDialog(action, controller, routeValues, textoTitulo, null, null);
        }
        public IHtmlString ModalDialog(String action, String controller, object routeValues, String textoTitulo, String textoBoton)
        {
            return ModalDialog(action, controller, routeValues, textoTitulo, textoBoton, null);
        }
        public IHtmlString ModalDialog(String action, String controller, object routeValues, String textoTitulo, String textoBoton, String iconoBoton)
        {
            var result ="";
            if (action != null && controller != null && routeValues != null)
            {
                result = "onclick=\"doModal( '" + textoTitulo + "','" + dataWebViewPage.Url.Action(action, controller, routeValues) + "','" + (textoBoton ?? "Aceptar") + "','" + (iconoBoton ?? "fa-check") + "')\"";
            }
            else
            {
                result = "onclick=\"doModal( '" + textoTitulo + "','','" + (textoBoton ?? "Aceptar") + "','" + (iconoBoton ?? "fa-check") + "')\"";
            }
            return new HtmlString(result);
        }

    }
}