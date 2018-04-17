using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP
{
    /// <summary>
    /// Implements <see cref="IGuidGenerator"/> by using <see cref="Guid.NewGuid"/>.
    /// </summary>
    public class RegularGuidGenerator : IGuidGenerator
    {
        public virtual Guid Create()
        {
            return Guid.NewGuid();
        }
    }
}
