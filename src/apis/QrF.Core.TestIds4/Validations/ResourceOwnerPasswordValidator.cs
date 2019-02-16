using IdentityModel;
using IdentityServer4.Validation;
using QrF.Core.TestIds4.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QrF.Core.IdentityServer4.Validations
{
    /// <summary>
    /// 自定义用户名密码校验
    /// </summary>
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUsersBusServices _services;
        public ResourceOwnerPasswordValidator(IUsersBusServices services)
        {
            _services = services;
        }
        /// <summary>
        /// 验证用户身份
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = _services.FindUserByuAccount(context.UserName, context.Password);
            if (user != null)
            {
                context.Result = new GrantValidationResult(
                    user.KeyId.ToString(),
                    OidcConstants.AuthenticationMethods.Password,
                    DateTime.UtcNow,
                    user.Claims);
            }
            return Task.CompletedTask;
        }
    }
}
