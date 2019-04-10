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
    public class ReRoutesItemBusiness : IReRoutesItemBusiness
    {
        private readonly QrfSqlSugarClient _dbContext;
        private readonly IMapper _mapper;

        public ReRoutesItemBusiness(QrfSqlSugarClient dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        public async Task<BasePageOutput<ReRoutesItem>> GetPageList(PageInput input)
        {
            var list = new List<ReRoutesItem>();
            var totalNumber = 0;
            var query = await _dbContext.Queryable<ReRoutesItem>()
                .WhereIF(!input.Name.IsNullOrEmpty(), o => o.ItemName.Contains(input.Name))
                .ToPageListAsync(input.Page, input.PageSize, totalNumber);
            list = query.Key;
            totalNumber = query.Value;
            return new BasePageOutput<ReRoutesItem> { Page = input.Page, Rows = list, Total = totalNumber };
        }
        /// <summary>
        /// 查询所有分类
        /// </summary>
        public async Task<List<ReRoutesItem>> GetAllList()
        {
            var list = await _dbContext.Queryable<ReRoutesItem>()
                .ToListAsync();
            return list;
        }
        /// <summary>
        /// 编辑信息
        /// </summary>
        public async Task EditModel(ReRoutesItem input)
        {
            if (input.ItemId > 0)
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
            await _dbContext.Deleteable<ReRoutesItem>()
                .Where(f => f.ItemId == input).ExecuteCommandAsync();
        }
    }
}
