using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP.Domain.Entities
{
    public interface IEntity<TPrimaryKey>
    {
        TPrimaryKey Id { get; set; }

        bool IsTransient();
    }
}
