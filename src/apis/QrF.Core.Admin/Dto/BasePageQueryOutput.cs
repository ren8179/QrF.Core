using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.Admin.Dto
{
    public class BasePageQueryOutput<T>: BasePageOutput
    {
        public List<T> Data { set; get; }
    }
}
