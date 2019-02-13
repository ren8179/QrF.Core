using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.Core.IdentityServer4.Dapper.Entities
{
    public class IdentityClaim : UserClaim
    {
        public IdentityResource IdentityResource { get; set; }
    }
}
