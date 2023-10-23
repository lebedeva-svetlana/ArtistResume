using Microsoft.AspNetCore.Localization;

namespace Resume
{
    public class UrlRequestCultureProvider : RequestCultureProvider
    {
        public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            string cultureCode;

            cultureCode = GetCultureFromCookie(httpContext);
            bool hasCookie = cultureCode != null;

            if (httpContext.Request.Path.HasValue && httpContext.Request.Path.Value == "/" && !hasCookie)
            {
                cultureCode = GetDefaultCultureCode();
            }
            // TODO: Изменить
            else if (httpContext.Request.Path.HasValue && httpContext.Request.Path.Value.Length >= 4 && httpContext.Request.Path.Value[0] == '/' && httpContext.Request.Path.Value[3] == '/')
            {
                cultureCode = httpContext.Request.Path.Value.Substring(1, 2);

                if (CheckCultureCode(cultureCode))
                {
                    // TODO: Вынести в отдельный метод
                    httpContext.Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(cultureCode)), new CookieOptions
                    {
                        Expires = DateTimeOffset.UtcNow.AddYears(1)
                    }
                    );

                    // cultureCode = GetDefaultCultureCode(); //throw new HttpException(HttpStatusCode.NotFound);
                }
            }
            else
            {
                // throw new HttpException(HttpStatusCode.NotFound);
                //cultureCode = GetDefaultCultureCode();
            }

            // TODO: Вернуть 404 для неизвестных культур

            ProviderCultureResult requestCulture = new(cultureCode);

            return Task.FromResult(requestCulture);
        }

        private bool CheckCultureCode(string cultureCode)
        {
            return Options.SupportedCultures.Select(c => c.TwoLetterISOLanguageName).Contains(cultureCode);
        }

        private string GetDefaultCultureCode()
        {
            return Options.DefaultRequestCulture.Culture.TwoLetterISOLanguageName;
        }

        private string GetCultureFromCookie(HttpContext httpContext)
        {
            return httpContext.Request.Cookies[".AspNetCore.Culture"];
        }
    }
}