using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP.Data
{
    public class ActiveTransactionProviderArgs : Dictionary<string, object>
    {
        public static ActiveTransactionProviderArgs Empty { get; } = new ActiveTransactionProviderArgs();
    }
}
