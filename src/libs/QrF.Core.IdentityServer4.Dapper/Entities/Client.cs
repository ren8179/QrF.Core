using QrF.Core.Storage.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.Core.IdentityServer4.Dapper.Entities
{
    public class Client : Clients
    {
        public List<ClientGrantType> AllowedGrantTypes { get; set; }
        public List<ClientRedirectUri> RedirectUris { get; set; }
        public List<ClientPostLogoutRedirectUri> PostLogoutRedirectUris { get; set; }
        public List<ClientScope> AllowedScopes { get; set; }
        public List<ClientIdPRestriction> IdentityProviderRestrictions { get; set; }
        public List<ClientClaim> Claims { get; set; }
        public List<ClientCorsOrigin> AllowedCorsOrigins { get; set; }
        public List<ClientProperty> Properties { get; set; }
        public List<ClientSecret> ClientSecrets { get; set; }
    }
}
