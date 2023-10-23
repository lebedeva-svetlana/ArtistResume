using System.Text.RegularExpressions;

namespace Resume
{
    public class CultureConstraint : IRouteConstraint
    {
        private readonly string _defaultCulture;
        private readonly string _pattern;

        public CultureConstraint(string defaultCulture, string pattern)
        {
            _defaultCulture = defaultCulture;
            _pattern = pattern;
        }

        bool IRouteConstraint.Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (routeDirection == RouteDirection.UrlGeneration &&
    _defaultCulture.Equals(values[routeKey]))
            {
                return false;
            }
            else
            {
                return Regex.IsMatch((string)values[routeKey], "^" + _pattern + "$");
            }
        }
    }
}