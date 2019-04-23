using AutoMapper;
using QrF.Core.Config.Domain;
using QrF.Core.Config.Dto;
using QrF.Core.Config.Infrastructure.DbContext;
using QrF.Core.Config.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using QrF.Core.Utils.Extension;
using System;
using System.Security.Cryptography;
using System.Text;

namespace QrF.Core.Config.Business
{
    public class ClientsBusiness : IClientsBusiness
    {
        private readonly QrfSqlSugarClient _dbContext;
        private readonly IMapper _mapper;

        public ClientsBusiness(QrfSqlSugarClient dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        public async Task<BasePageOutput<ClientsDto>> GetPageList(PageInput input)
        {
            var list = new List<Clients>();
            var totalNumber = 0;
            var query = await _dbContext.Queryable<Clients>()
                .WhereIF(!input.Name.IsNullOrEmpty(), o => o.ClientId.Contains(input.Name))
                .ToPageListAsync(input.Page, input.PageSize, totalNumber);
            list = query.Key;
            totalNumber = query.Value;
            var result = new List<ClientsDto>();
            if (list != null && list.Count > 0)
            {
                foreach (var o in list)
                {
                    var grantTypes = await _dbContext.Queryable<ClientGrantType>()
                    .Where(t => t.ClientId == o.Id).Select(t => t.GrantType).ToListAsync();
                    var scopes = await _dbContext.Queryable<ClientScope>()
                    .Where(t => t.ClientId == o.Id).Select(t => t.Scope).ToListAsync();
                    result.Add(new ClientsDto
                    {
                        ClientId = o.ClientId,
                        ClientName = o.ClientName,
                        Description = o.Description,
                        Enabled = o.Enabled,
                        AllowedGrantTypes = grantTypes,
                        AllowedScopes = scopes,
                        Id = o.Id
                    });
                };
            }
            return new BasePageOutput<ClientsDto> { Page = input.Page, Rows = result, Total = totalNumber };
        }
        /// <summary>
        /// 编辑信息
        /// </summary>
        public async Task EditModel(ClientsDto input)
        {
            try
            {
                _dbContext.Ado.BeginTran();
                var model = _mapper.Map<ClientsDto, Clients>(input);
                if (model.Id > 0)
                {
                    await _dbContext.Updateable(model).ExecuteCommandAsync();
                    await SaveClientGrantTypes(input.AllowedGrantTypes, model.Id, true);
                    await SaveClientScope(input.AllowedScopes, model.Id, true);
                    await SaveClientSecret(input.ClientSecrets, model.Id, true);
                    _dbContext.Ado.CommitTran();
                    return;
                }
                var mid = await _dbContext.Insertable(model).ExecuteReturnIdentityAsync();
                await SaveClientGrantTypes(input.AllowedGrantTypes, mid);
                await SaveClientScope(input.AllowedScopes, mid);
                await SaveClientSecret(input.ClientSecrets, mid);
                _dbContext.Ado.CommitTran();
            }
            catch (Exception ex)
            {
                _dbContext.Ado.RollbackTran();
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public async Task DelModel(int input)
        {
            try
            {
                _dbContext.Ado.BeginTran();
                await _dbContext.Deleteable<ClientScope>().Where(f => f.ClientId == input).ExecuteCommandAsync();
                await _dbContext.Deleteable<ClientGrantType>().Where(f => f.ClientId == input).ExecuteCommandAsync();
                await _dbContext.Deleteable<Clients>()
                    .Where(f => f.Id == input).ExecuteCommandAsync();
                _dbContext.Ado.CommitTran();
            }
            catch (Exception ex)
            {
                _dbContext.Ado.RollbackTran();
                throw ex;
            }
        }

        /// <summary>
        /// 保存客户端授权域
        /// </summary>
        /// <param name="AllowedScopes">授权域</param>
        /// <param name="clientId">客户端编号</param>
        /// <param name="isUpdate">是否更新操作</param>
        /// <returns></returns>
        private async Task SaveClientScope(List<string> AllowedScopes, int clientId, bool isUpdate = false)
        {
            if (AllowedScopes != null && AllowedScopes.Count > 0)
            {
                if (isUpdate)
                    await _dbContext.Deleteable<ClientScope>().Where(f => f.ClientId == clientId).ExecuteCommandAsync();
                AllowedScopes.ForEach(o =>
                {
                    _dbContext.Insertable(new ClientScope { ClientId = clientId, Scope = o }).ExecuteCommand();
                });
            }
        }

        /// <summary>
        /// 保存客户端授权类型
        /// </summary>
        /// <param name="AllowedScopes">授权类型</param>
        /// <param name="clientId">客户端编号</param>
        /// <param name="isUpdate">是否更新操作</param>
        /// <returns></returns>
        private async Task SaveClientGrantTypes(List<string> AllowedGrantTypes, int clientId, bool isUpdate = false)
        {
            if (AllowedGrantTypes != null && AllowedGrantTypes.Count > 0)
            {
                if (isUpdate)
                    await _dbContext.Deleteable<ClientGrantType>().Where(f => f.ClientId == clientId).ExecuteCommandAsync();
                AllowedGrantTypes.ForEach(o =>
                {
                    _dbContext.Insertable(new ClientGrantType { ClientId = clientId, GrantType = o }).ExecuteCommand();
                });
            }
        }
        /// <summary>
        /// 保存客户端授权密钥
        /// </summary>
        /// <param name="secret">密钥</param>
        /// <param name="clientId">客户端编号</param>
        /// <param name="isUpdate">是否更新操作</param>
        /// <returns></returns>
        private async Task SaveClientSecret(string secret, int clientId, bool isUpdate = false)
        {
            if (!secret.IsNullOrEmpty())
            {
                using (var sha = SHA256.Create())
                {
                    var bytes = Encoding.UTF8.GetBytes(secret);
                    var hash = sha.ComputeHash(bytes);
                    secret = Convert.ToBase64String(hash);
                }
                if (isUpdate)
                {
                    var model = await _dbContext.Queryable<ClientSecret>().FirstAsync(o => o.ClientId == clientId);
                    model.Value = secret;
                    await _dbContext.Updateable(model).IgnoreColumns(it => new { it.Created }).ExecuteCommandAsync();
                }
                _dbContext.Insertable(new ClientSecret { ClientId = clientId, Value = secret, Type = "SharedSecret", Created = DateTime.Now }).ExecuteCommand();
            }
        }
    }
}
