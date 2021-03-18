using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sol_Demo.Configurations
{
    public static class RequestLocalizationMiddleware
    {
        private static RequestLocalizationOptions GetLocalizationOptions(IConfiguration configuration)
        {
            var cultures = configuration.GetSection("Cultures").GetChildren().ToDictionary(x => x.Key, x => x.Value);

            var supportedCultures = cultures.Keys.ToArray();

            var localizationOptions = new RequestLocalizationOptions()
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);

            return localizationOptions;
        }

        public static void UseRequestLocalizationConfig(this IApplicationBuilder applicationBuilder, IConfiguration configuration)
        {
            applicationBuilder.UseRequestLocalization(GetLocalizationOptions(configuration));
        }
    }
}