using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.ABP.Domain.Entities
{
    public interface ISoftDelete
    {
        /// <summary>
        /// Used to mark an Entity as 'Deleted'. 
        /// </summary>
        bool IsDeleted { get; set; }
    }
}
