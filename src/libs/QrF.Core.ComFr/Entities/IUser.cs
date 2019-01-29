using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.Core.ComFr.Entities
{
    public interface IUser
    {
        string UserID { get; set; }
        string NickName { get; set; }
        string PassWord { get; set; }
        long Timestamp { get; set; }
        string LoginIP { get; set; }
        string PhotoUrl { get; set; }
        string UserName { get; set; }
        string ApiLoginToken { get; set; }
        string Telephone { get; set; }
        string Email { get; set; }
    }
}
