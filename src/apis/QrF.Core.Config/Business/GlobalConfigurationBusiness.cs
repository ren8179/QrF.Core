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
    public class GlobalConfigurationBusiness : IGlobalConfigurationBusiness
    {
        private readonly QrfSqlSugarClient _dbContext;
        private readonly IMapper _mapper;

        public GlobalConfigurationBusiness(QrfSqlSugarClient dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        public async Task<BasePageOutput<GlobalConfiguration>> GetPageList(PageInput input)
        {
            var list = new List<GlobalConfiguration>();
            var totalNumber = 0;
            var query = await _dbContext.Queryable<GlobalConfiguration>()
                .WhereIF(!input.Name.IsNullOrEmpty(), o => o.GatewayName.Contains(input.Name))
                .ToPageListAsync(input.Page, input.PageSize, totalNumber);
            list = query.Key;
            totalNumber = query.Value;
            return new BasePageOutput<GlobalConfiguration> { Page = input.Page, Rows = list, Total = totalNumber };
        }
        /// <summary>
        /// 编辑信息
        /// </summary>
        public async Task EditModel(GlobalConfiguration input)
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
            await _dbContext.Deleteable<GlobalConfiguration>()
                .Where(f => f.KeyId == input).ExecuteCommandAsync();
        }
    }
}
