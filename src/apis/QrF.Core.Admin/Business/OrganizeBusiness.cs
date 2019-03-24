using AutoMapper;
using QrF.Core.Admin.Domain;
using QrF.Core.Admin.Dto;
using QrF.Core.Admin.Infrastructure.DbContext;
using QrF.Core.Admin.Interfaces;
using System;
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
            var list = new List<Organize>();
            var totalNumber = 0;
            var query = await _dbContext.Queryable<Organize>()
                .WhereIF(input.ParentId.HasValue, o => o.ParentId == input.ParentId.Value)
                .ToPageListAsync(input.Page, input.PageSize, totalNumber);
            list = query.Key;
            totalNumber = query.Value;
            var result = new List<QueryOrganizeDto>();
            if (list != null && list.Count > 0)
            {
                list.ForEach(o =>
                {
                    var parent = _dbContext.Queryable<Organize>().First(it => it.KeyId == o.ParentId);
                    result.Add(new QueryOrganizeDto
                    {
                        ParentName = parent?.Name,
                        KeyId = o.KeyId,
                        CreateTime = o.CreateTime,
                        ParentId = o.ParentId,
                        Name = o.Name,
                        BizCode = o.BizCode,
                        Sort = o.Sort,
                        Status = o.Status,
                        CreateId = o.CreateId,
                        StatusText = o.Status ? "启用" : "停用"
                    });
                });
            }
            return new BasePageQueryOutput<QueryOrganizeDto> { Page = input.Page, Rows = result, Total = totalNumber };
        }
        /// <summary>
        /// 查询列表
        /// </summary>
        public async Task<IEnumerable<QueryOrganizeDto>> GetListAsync(Guid? deptId)
        {
            var list = await _dbContext.Queryable<Organize>()
                                 .WhereIF(deptId.HasValue, o => o.ParentId == deptId.Value)
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
                                 .ToListAsync();
            return list;
        }
        /// <summary>
        /// 
        /// </summary>
        public async Task<OrgDto> GetModelAsync(Guid id)
        {
            var model = await _dbContext.Queryable<Organize>().FirstAsync(o => o.KeyId == id);
            return new OrgDto
            {
                KeyId = model.KeyId,
                ParentId = model.ParentId,
                Name = model.Name,
                BizCode = model.BizCode,
                Sort = model.Sort,
                Status = model.Status
            };
        }
        /// <summary>
        /// 编辑信息
        /// </summary>
        public async Task EditModel(OrgDto input)
        {
            input.KeyId = input.KeyId ?? Guid.Empty;
            var model = _mapper.Map<OrgDto, Organize>(input);
            if (model.KeyId != Guid.Empty)
            {
                await _dbContext.Updateable(model)
                                .IgnoreColumns(it => new { it.CreateTime })
                                .ExecuteCommandAsync();
            }
            else
            {
                model.CreateTime = DateTime.Now;
                await _dbContext.Insertable(model).ExecuteCommandAsync();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task DelModel(Guid input)
        {
            await _dbContext.Deleteable<Organize>()
                .Where(f => f.KeyId == input).ExecuteCommandAsync();
        }
    }
}
