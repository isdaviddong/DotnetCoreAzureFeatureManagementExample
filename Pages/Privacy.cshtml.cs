using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;

namespace webapp.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;
        public readonly IFeatureManager _featureManager;
        public bool featureFlag01 = false;

        public PrivacyModel(ILogger<PrivacyModel> logger, IFeatureManager featureManager)
        {
            _logger = logger;
            _featureManager = featureManager;
        }

        public void OnGet()
        {
            Program.ConfigurationRefresher.TryRefreshAsync();
            featureFlag01 = _featureManager.IsEnabledAsync("featureFlag01").Result;
        }
    }
}
