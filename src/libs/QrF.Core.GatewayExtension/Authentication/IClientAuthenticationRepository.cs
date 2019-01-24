using System.Threading.Tasks;

namespace QrF.Core.GatewayExtension.Authentication
{
    public interface IClientAuthenticationRepository
    {
        /// <summary>
        /// 校验当前的请求地址是否有权限访问
        /// </summary>
        /// <param name="clientid">客户端ID</param>
        /// <param name="path">请求地址</param>
        /// <returns></returns>
        Task<bool> ClientAuthenticationAsync(string clientid, string path);
    }
}
