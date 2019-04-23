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
        /// 获取授权域列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<ApiScope>> GetScopeList()
        {
            return await _dbContext.Queryable<ApiScope>().ToListAsync();
        }
        /// <summary>
        /// 编辑信息
        /// </summary>
        public async Task EditModel(ApiResource input)
        {
            try
            {
                _dbContext.Ado.BeginTran();
                if (input.Id > 0)
                {
                    var scope = await _dbContext.Queryable<ApiScope>().FirstAsync(o => o.ApiResourceId == input.Id);
                    input.Updated = DateTime.Now;
                    await _dbContext.Updateable(input).IgnoreColumns(it => new { it.Created })
                        .ExecuteCommandAsync();
                    if (scope != null)
                    {
                        scope.Name = input.Name;
                        scope.DisplayName = input.DisplayName;
                        scope.Description = input.Description;
                        await _dbContext.Updateable(scope).IgnoreColumns(it => new { it.ApiResourceId })
                            .ExecuteCommandAsync();
                    }
                    _dbContext.Ado.CommitTran();
                    return;
                }
                input.Created = DateTime.Now;
                input.NonEditable = 0;
                var rid = await _dbContext.Insertable(input).ExecuteReturnIdentityAsync();
                await _dbContext.Insertable(new ApiScope
                {
                    ApiResourceId = rid,
                    Description = input.Description,
                    DisplayName = input.DisplayName,
                    Emphasize = false,
                    Name = input.Name,
                    Required = false,
                    ShowInDiscoveryDocument = true
                }).ExecuteCommandAsync();
                _dbContext.Ado.CommitTran();
            }
            catch (Exception ex)
            {
                _dbContext.Ado.RollbackTran();
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public async Task DelModel(int input)
        {
            try
            {
                _dbContext.Ado.BeginTran();
                await _dbContext.Deleteable<ApiResource>().Where(f => f.Id == input).ExecuteCommandAsync();
                await _dbContext.Deleteable<ApiScope>().Where(f => f.ApiResourceId == input).ExecuteCommandAsync();
                _dbContext.Ado.CommitTran();
            }
            catch (Exception ex)
            {
                _dbContext.Ado.RollbackTran();
                throw ex;
            }
        }
    }
}
