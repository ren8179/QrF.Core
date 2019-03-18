using System;
using System.Collections.Generic;

namespace QrF.Core.Admin.Dto
{
    public class CascaderItem : CascaderItem<Guid>
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
