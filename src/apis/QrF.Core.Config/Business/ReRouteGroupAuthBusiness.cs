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
    public class ReRouteGroupAuthBusiness : IReRouteGroupAuthBusiness
    {
        private readonly QrfSqlSugarClient _dbContext;
        private readonly IMapper _mapper;

        public ReRouteGroupAuthBusiness(QrfSqlSugarClient dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        public async Task<BasePageOutput<ReRouteGroupAuth>> GetPageList(PageInput input)
        {
            var list = new List<ReRouteGroupAuth>();
            var totalNumber = 0;
            var query = await _dbContext.Queryable<ReRouteGroupAuth>()
                .ToPageListAsync(input.Page, input.PageSize, totalNumber);
            list = query.Key;
            totalNumber = query.Value;
            return new BasePageOutput<ReRouteGroupAuth> { Page = input.Page, Rows = list, Total = totalNumber };
        }
        /// <summary>
        /// 列表
        /// </summary>
        public async Task<List<ReRouteGroupAuth>> GetList(int? input)
        {
            var list = await _dbContext.Queryable<ReRouteGroupAuth>()
                .Where(o => o.GroupId == input)
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
                await _dbContext.Deleteable<ReRouteGroupAuth>()
                .Where(f => f.GroupId == input.KeyId && f.ReRouteId == input.ReRouteId)
                .ExecuteCommandAsync();
            }
            else
            {
                await _dbContext.Insertable(new ReRouteGroupAuth { GroupId = input.KeyId, ReRouteId = input.ReRouteId })
                    .ExecuteCommandAsync();
            }
            return true;
        }
        /// <summary>
        /// 编辑信息
        /// </summary>
        public async Task EditModel(ReRouteGroupAuth input)
        {
            if (input.AuthId > 0)
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
            await _dbContext.Deleteable<ReRouteGroupAuth>()
                .Where(f => f.AuthId == input).ExecuteCommandAsync();
        }
    }
}
