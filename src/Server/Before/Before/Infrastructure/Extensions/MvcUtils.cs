using System.Web.Mvc;
using System.Web.Routing;

namespace Before.Infrastructure.Extensions
{
    public static class MvcUtils
    {
        // http://www.codeproject.com/Articles/786085/ASP-NET-MVC-List-Editor-with-Bootstrap-Modals

        // As the text the: "<span class='glyphicon glyphicon-plus'></span>" can be entered
        public static MvcHtmlString NoEncodeActionLink(this HtmlHelper htmlHelper,
                                             string text, string title, string action,
                                             string controller,
                                             object routeValues = null,
                                             object htmlAttributes = null)
        {
            UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            TagBuilder builder = new TagBuilder("a");
            builder.InnerHtml = text;
            builder.Attributes["title"] = title;
            builder.Attributes["href"] = urlHelper.Action(action, controller, routeValues);
            builder.MergeAttributes(new RouteValueDictionary(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes)));

            return MvcHtmlString.Create(builder.ToString());
        }


        public static MvcHtmlString AlertLevelClass(this HtmlHelper htmlHelper, int alertLevel)
        {
            string alertLevelClass = "";
            switch (alertLevel)
            {
                case 0:
                    alertLevelClass = "status-ok";
                    break;
                case 1:
                    alertLevelClass = "status-warning";
                    break;
                case 2:
                    alertLevelClass = "status-critical";
                    break;
                default:
                    alertLevelClass = "status-unknown";
                    break;
            }

            return MvcHtmlString.Create(alertLevelClass);
        }


        public static MvcHtmlString AlertLevelValue(this HtmlHelper htmlHelper, int alertLevel)
        {
            string alertLevelValue = "";
            switch (alertLevel)
            {
                case 0:
                    alertLevelValue = "Ok";
                    break;
                case 1:
                    alertLevelValue = "Warning";
                    break;
                case 2:
                    alertLevelValue = "Critical";
                    break;
                default:
                    alertLevelValue = "Unknown";
                    break;
            }

            return MvcHtmlString.Create(alertLevelValue);
        }
    }
}