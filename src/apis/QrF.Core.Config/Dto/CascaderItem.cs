using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.Config.Dto
{
    public class CascaderItem : CascaderItem<int>
    {
        public new IEnumerable<CascaderItem> children { get; set; }
    }
    public class CascaderItem<T>
    {
        public T value { get; set; }
        public string label { get; set; }
        public virtual IEnumerable<CascaderItem<T>> children { get; set; }
    }
}
