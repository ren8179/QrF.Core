﻿using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.Core.IdentityServer4.Dapper.Entities
{
    public class ApiResourceClaim : UserClaim
    {
        public ApiResource ApiResource { get; set; }
    }
}
