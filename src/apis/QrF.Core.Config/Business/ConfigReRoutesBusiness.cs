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
    public class ConfigReRoutesBusiness : IConfigReRoutesBusiness
    {
        private readonly QrfSqlSugarClient _dbContext;
        private readonly IMapper _mapper;

        public ConfigReRoutesBusiness(QrfSqlSugarClient dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        public async Task<BasePageOutput<ConfigReRoutes>> GetPageList(PageInput input)
        {
            var list = new List<ConfigReRoutes>();
            var totalNumber = 0;
            var query = await _dbContext.Queryable<ConfigReRoutes>()
                .ToPageListAsync(input.Page, input.PageSize, totalNumber);
            list = query.Key;
            totalNumber = query.Value;
            return new BasePageOutput<ConfigReRoutes> { Page = input.Page, Rows = list, Total = totalNumber };
        }
        /// <summary>
        /// 列表
        /// </summary>
        public async Task<List<ConfigReRoutes>> GetList(int? input)
        {
            var list = await _dbContext.Queryable<ConfigReRoutes>()
                .Where(o => o.KeyId == input)
                .ToListAsync();
            return list;
        }
        /// <summary>
        /// 分配路由
        /// </summary>
        public async Task<bool> ToAccReRouteAsync(ToAccReRouteInput input)
        {
            if (input.Type == 0)
            {
                await _dbContext.Deleteable<ConfigReRoutes>()
                .Where(f => f.KeyId == input.KeyId && f.ReRouteId == input.ReRouteId)
                .ExecuteCommandAsync();
            }
            else
            {
                await _dbContext.Insertable(new ConfigReRoutes { KeyId = input.KeyId, ReRouteId = input.ReRouteId })
                    .ExecuteCommandAsync();
            }
            return true;
        }
        /// <summary>
        /// 编辑信息
        /// </summary>
        public async Task EditModel(ConfigReRoutes input)
        {
            if (input.KeyId > 0)
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
            await _dbContext.Deleteable<ConfigReRoutes>()
                .Where(f => f.KeyId == input).ExecuteCommandAsync();
        }
    }
}
