using AutoMapper;
using QrF.Core.Config.Domain;
using QrF.Core.Config.Dto;
using QrF.Core.Config.Infrastructure.DbContext;
using QrF.Core.Config.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using QrF.Core.Utils.Extension;

namespace QrF.Core.Config.Business
{
    public class ClientGroupBusiness : IClientGroupBusiness
    {
        private readonly QrfSqlSugarClient _dbContext;
        private readonly IMapper _mapper;

        public ClientGroupBusiness(QrfSqlSugarClient dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        public async Task<BasePageOutput<ClientGroup>> GetPageList(PageInput input)
        {
            var list = new List<ClientGroup>();
            var totalNumber = 0;
            var query = await _dbContext.Queryable<ClientGroup>()
                .ToPageListAsync(input.Page, input.PageSize, totalNumber);
            list = query.Key;
            totalNumber = query.Value;
            return new BasePageOutput<ClientGroup> { Page = input.Page, Rows = list, Total = totalNumber };
        }
        /// <summary>
        /// 列表
        /// </summary>
        public async Task<List<ClientGroup>> GetList(int? input)
        {
            var list = await _dbContext.Queryable<ClientGroup>()
                .Where(o => o.GroupId == input)
                .ToListAsync();
            return list;
        }
        /// <summary>
        /// 分配客户端
        /// </summary>
        public async Task<bool> ToAccReRouteAsync(ToAccGroupInput input)
        {
            if (input.Type == 0)
            {
                await _dbContext.Deleteable<ClientGroup>()
                .Where(f => f.GroupId == input.GroupId && f.Id == input.KeyId)
                .ExecuteCommandAsync();
            }
            else
            {
                await _dbContext.Insertable(new ClientGroup { GroupId = input.GroupId, Id = input.KeyId })
                    .ExecuteCommandAsync();
            }
            return true;
        }
        /// <summary>
        /// 编辑信息
        /// </summary>
        public async Task EditModel(ClientGroup input)
        {
            if (input.ClientGroupId > 0)
            {
                await _dbContext.Updateable(input).ExecuteCommandAsync();
                return;
            }
            await _dbContext.Insertable(input).ExecuteCommandAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        public async Task DelModel(int input)
        {
            await _dbContext.Deleteable<ClientGroup>()
                .Where(f => f.ClientGroupId == input).ExecuteCommandAsync();
        }
    }
}
