using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.Core.GatewayExtension.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public class ResultMsg
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 返回值
        /// </summary>
        public Object Result { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get; set; }

        public static ResultMsg SuccessMsg(object data)
        {
            return new ResultMsg { Code = 200, Success = true, Msg = "成功", Result = data };
        }
        public static ResultMsg ErrorMsg(string msg = "", int code = 500)
        {
            return new ResultMsg { Code = code, Success = false, Msg = msg };
        }
    }
}
