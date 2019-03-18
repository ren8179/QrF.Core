using AutoMapper;
using QrF.Core.Admin.Domain;
using QrF.Core.Admin.Dto;
using QrF.Core.Admin.Infrastructure.DbContext;
using QrF.Core.Admin.Interfaces;
using QrF.Core.Utils.Extension;
using QrF.Core.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QrF.Core.Admin.Business
{
    public class RoleBusiness : IRoleBusiness
    {
        private readonly QrfSqlSugarClient _dbContext;
        private readonly IMapper _mapper;

        public RoleBusiness(QrfSqlSugarClient dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BasePageQueryOutput<QueryRoleDto>> GetPageList(QueryRolesInput input)
        {
            var list = new List<Role>();
            var totalNumber = 0;
            var query = await _dbContext.Queryable<Role>()
                .WhereIF(input.DeptId.HasValue, o => o.DeptId == input.DeptId.Value)
                .ToPageListAsync(input.PageIndex, input.PageSize, totalNumber);
            list = query.Key;
            totalNumber = query.Value;
            var result = new List<QueryRoleDto>();
            if (list != null && list.Count > 0)
            {
                list.ForEach(o =>
                {
                    var parent = _dbContext.Queryable<Organize>().First(it => it.KeyId == o.DeptId);
                    result.Add(new QueryRoleDto
                    {
                        DeptName = parent?.Name,
                        KeyId = o.KeyId,
                        CreateTime = o.CreateTime,
                        DeptId = o.DeptId,
                        Name = o.Name,
                        Codes = o.Codes,
                        CreateId = o.CreateId
                    });
                });
            }
            return new BasePageQueryOutput<QueryRoleDto> { Page = input.PageIndex, Rows = result, Total = totalNumber };
        }
        /// <summary>
        /// 编辑信息
        /// </summary>
        public async Task EditModel(RoleDto input)
        {
            input.KeyId = input.KeyId ?? Guid.Empty;
            var model = _mapper.Map<RoleDto, Role>(input);
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
            await _dbContext.Deleteable<Role>()
                .Where(f => f.KeyId == input).ExecuteCommandAsync();
        }
    }
}
