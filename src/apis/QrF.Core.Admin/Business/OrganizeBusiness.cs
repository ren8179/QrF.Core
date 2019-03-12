using AutoMapper;
using QrF.Core.Admin.Domain;
using QrF.Core.Admin.Dto;
using QrF.Core.Admin.Infrastructure.DbContext;
using QrF.Core.Admin.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QrF.Core.Admin.Business
{
    public class OrganizeBusiness : IOrganizeBusiness
    {
        private readonly QrfSqlSugarClient _dbContext;
        private readonly IMapper _mapper;

        public OrganizeBusiness(QrfSqlSugarClient dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BasePageQueryOutput<QueryOrganizeDto>> GetPageList(QueryOrganizesInput input)
        {
            var list = new List<QueryOrganizeDto>();
            var totalNumber = 0;
            var query = await _dbContext.Queryable<Organize>()
                .WhereIF(input.ParentId.HasValue, o => o.ParentId == input.ParentId.Value)
                .Select(o => new QueryOrganizeDto
                {
                    KeyId = o.KeyId,
                    CreateTime = o.CreateTime,
                    ParentId = o.ParentId,
                    Name = o.Name,
                    BizCode = o.BizCode,
                    Sort = o.Sort,
                    Status = o.Status,
                    CreateId = o.CreateId,
                })
                .ToPageListAsync(input.PageIndex, input.PageSize, totalNumber);
            list = query.Key;
            totalNumber = query.Value;
            return new BasePageQueryOutput<QueryOrganizeDto> { CurrentPage = input.PageIndex, Data = list, Total = totalNumber };
        }
    }
}
