using Microsoft.AspNetCore.Mvc.Filters;
using System.Globalization;

namespace Resume.Filters
{
    public class CultureFilter : IAuthorizationFilter
    {
        //private readonly string _defaultCulture;

        //public CultureFilter(string defaultCulture)
        //{
        //    _defaultCulture = defaultCulture;
        //}

        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            RouteValueDictionary? values = filterContext.RouteData.Values;

            //string culture = (string)values["culture"] ?? _defaultCulture;
            string? culture = (string)values["culture"];

            if (culture is null)
            {
                return;
            }

            CultureInfo cultureInfo = new(culture);

            ////
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(cultureInfo.Name);
        }
    }
}