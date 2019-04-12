using AutoMapper;
using QrF.Core.Config.Domain;
using QrF.Core.Config.Dto;
using QrF.Core.Config.Infrastructure.DbContext;
using QrF.Core.Config.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using QrF.Core.Utils.Extension;
using System;

namespace QrF.Core.Config.Business
{
    public class ApiResourceBusiness : IApiResourceBusiness
    {
        private readonly QrfSqlSugarClient _dbContext;
        private readonly IMapper _mapper;

        public ApiResourceBusiness(QrfSqlSugarClient dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        public async Task<BasePageOutput<ApiResource>> GetPageList(PageInput input)
        {
            var list = new List<ApiResource>();
            var totalNumber = 0;
            var query = await _dbContext.Queryable<ApiResource>()
                .WhereIF(!input.Name.IsNullOrEmpty(), o => o.Name.Contains(input.Name))
                .ToPageListAsync(input.Page, input.PageSize, totalNumber);
            list = query.Key;
            totalNumber = query.Value;
            return new BasePageOutput<ApiResource> { Page = input.Page, Rows = list, Total = totalNumber };
        }
        /// <summary>
        /// 编辑信息
        /// </summary>
        public async Task EditModel(ApiResource input)
        {
            if (input.Id > 0)
            {
                input.Updated = DateTime.Now;
                await _dbContext.Updateable(input).IgnoreColumns(it => new { it.Created })
                    .ExecuteCommandAsync();
                return;
            }
            input.Created = DateTime.Now;
            input.NonEditable = 0;
            await _dbContext.Insertable(input).ExecuteCommandAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        public async Task DelModel(int input)
        {
            await _dbContext.Deleteable<ApiResource>()
                .Where(f => f.Id == input).ExecuteCommandAsync();
        }
    }
}
