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
    public class ReRouteBusiness : IReRouteBusiness
    {
        private readonly QrfSqlSugarClient _dbContext;
        private readonly IMapper _mapper;

        public ReRouteBusiness(QrfSqlSugarClient dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        public async Task<BasePageOutput<ReRoute>> GetPageList(ReRoutePageInput input)
        {
            var list = new List<ReRoute>();
            var totalNumber = 0;
            var query = await _dbContext.Queryable<ReRoute>()
                .WhereIF(!input.Name.IsNullOrEmpty(), o => o.UpstreamPathTemplate.Contains(input.Name))
                .WhereIF(input.ItemId.HasValue, o => o.ItemId==input.ItemId.Value)
                .ToPageListAsync(input.Page, input.PageSize, totalNumber);
            list = query.Key;
            totalNumber = query.Value;
            return new BasePageOutput<ReRoute> { Page = input.Page, Rows = list, Total = totalNumber };
        }
        /// <summary>
        /// 编辑信息
        /// </summary>
        public async Task EditModel(ReRoute input)
        {
            if (input.ReRouteId > 0)
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
            await _dbContext.Deleteable<ReRoute>()
                .Where(f => f.ReRouteId == input).ExecuteCommandAsync();
        }
    }
}
