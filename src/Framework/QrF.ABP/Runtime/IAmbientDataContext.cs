using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP.Runtime
{
    public interface IAmbientDataContext
    {
        void SetData(string key, object value);

        object GetData(string key);
    }
}
