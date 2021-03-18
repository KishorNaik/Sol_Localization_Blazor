using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sol_Demo.Shared
{
    public partial class ChooseLanguage
    {
        #region Declaration

        private string selectedCulture = Thread.CurrentThread.CurrentCulture.Name;

        private Dictionary<string, string> cultures;

        #endregion Declaration

        #region Inject Property

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IConfiguration Configuration { get; set; }

        #endregion Inject Property

        #region Ui Event

        private Action OnLanguageChange { get; set; }

        #endregion Ui Event

        #region LifeCycle Event

        protected override void OnInitialized()
        {
            cultures = Configuration.GetSection("Cultures").GetChildren().ToDictionary(x => x.Key, x => x.Value);

            OnLanguageChange = () =>
            {
                if (string.IsNullOrWhiteSpace(selectedCulture))
                {
                    return;
                }
                var uri = new Uri(NavigationManager.Uri).GetComponents(UriComponents.PathAndQuery, UriFormat.Unescaped);

                var query = $"?culture={Uri.EscapeDataString(selectedCulture)}&" + $"redirectUri={Uri.EscapeDataString(uri)}";

                NavigationManager.NavigateTo("/Culture/SetCultures" + query, forceLoad: true);
            };
        }

        #endregion LifeCycle Event
    }
}