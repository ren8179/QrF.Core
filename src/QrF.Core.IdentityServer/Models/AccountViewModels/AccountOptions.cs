using System;

namespace QrF.Core.IdentityServer.Models
{
    public class AccountOptions
    {
        public AccountOptions()
        {
            RememberMeLoginDuration= TimeSpan.FromDays(30);
            InvalidCredentialsErrorMessage= "用户名或密码无效";
        }
        public bool AllowLocalLogin { get; set; }
        public bool AllowRememberLogin { get; set; }
        public TimeSpan RememberMeLoginDuration { get; set; }

        public bool ShowLogoutPrompt { get; set; }
        public bool AutomaticRedirectAfterSignOut { get; set; }

        // to enable windows authentication, the host (IIS or IIS Express) also must have 
        // windows auth enabled.
        public bool WindowsAuthenticationEnabled { get; set; }
        public bool IncludeWindowsGroups { get; set; }
        // specify the Windows authentication scheme and display name
        public string WindowsAuthenticationSchemeName { get; } = "Windows";

        public string InvalidCredentialsErrorMessage { get; set; }
    }
}
