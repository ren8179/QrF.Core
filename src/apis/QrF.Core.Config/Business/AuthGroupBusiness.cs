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
    public class AuthGroupBusiness : IAuthGroupBusiness
    {
        private readonly QrfSqlSugarClient _dbContext;
        private readonly IMapper _mapper;

        public AuthGroupBusiness(QrfSqlSugarClient dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        public async Task<BasePageOutput<AuthGroup>> GetPageList(PageInput input)
        {
            var list = new List<AuthGroup>();
            var totalNumber = 0;
            var query = await _dbContext.Queryable<AuthGroup>()
                .WhereIF(!input.Name.IsNullOrEmpty(), o => o.GroupName.Contains(input.Name))
                .ToPageListAsync(input.Page, input.PageSize, totalNumber);
            list = query.Key;
            totalNumber = query.Value;
            return new BasePageOutput<AuthGroup> { Page = input.Page, Rows = list, Total = totalNumber };
        }
        /// <summary>
        /// 编辑信息
        /// </summary>
        public async Task EditModel(AuthGroup input)
        {
            if (input.GroupId > 0)
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
            await _dbContext.Deleteable<AuthGroup>()
                .Where(f => f.GroupId == input).ExecuteCommandAsync();
        }
    }
}
