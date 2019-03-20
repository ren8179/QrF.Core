using AutoMapper;
using QrF.Core.Admin.Domain;
using QrF.Core.Admin.Dto;
using QrF.Core.Admin.Infrastructure.DbContext;
using QrF.Core.Admin.Interfaces;
using QrF.Core.Utils.Extension;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QrF.Core.Admin.Business
{
    public class LogBusiness : ILogBusiness
    {
        private readonly QrfSqlSugarClient _dbContext;
        private readonly IMapper _mapper;

        public LogBusiness(QrfSqlSugarClient dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BasePageQueryOutput<LogDto>> GetPageList(QueryLogsInput input)
        {
            var list = new List<LogDto>();
            var totalNumber = 0;
            var query = await _dbContext.Queryable<Log>()
                .WhereIF(input.Level.IsNotNullAndWhiteSpace(), o => o.Level == input.Level)
                .Select(o => new LogDto
                {
                    Id = o.Id,
                    Application = o.Application,
                    BusinessId = o.BusinessId,
                    BusinessType = o.BusinessType,
                    Level = o.Level,
                    Message = o.Message,
                    TimeStamp = o.TimeStamp
                })
                .ToPageListAsync(input.PageIndex, input.PageSize, totalNumber);
            list = query.Key;
            totalNumber = query.Value;
            return new BasePageQueryOutput<LogDto> { Page = input.PageIndex, Rows = list, Total = totalNumber };
        }
        /// <summary>
        /// 
        /// </summary>
        public async Task<Log> GetModelAsync(int id)
        {
            return await _dbContext.Queryable<Log>().FirstAsync(o => o.Id == id);
        }
    }
}
