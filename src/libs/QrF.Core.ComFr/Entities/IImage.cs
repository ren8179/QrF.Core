using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.Core.ComFr.Entities
{
    public interface IImage
    {
        string ImageUrl { get; set; }
        string ImageThumbUrl { get; set; }
    }
}
