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
    public class PermissionsBusiness : IPermissionsBusiness
    {
        private readonly QrfSqlSugarClient _dbContext;
        private readonly IMapper _mapper;

        public PermissionsBusiness(QrfSqlSugarClient dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        public async Task<IEnumerable<Permissions>> GetListByUserTypeAsync(Guid userId, int type = 2)
        {
            var query = await _dbContext.Queryable<Permissions>()
                .Where(o => o.UserId == userId)
                .Where(o => o.Types == type)
                .ToListAsync();
            return query;
        }
        /// <summary>
        /// 用户授权角色
        /// </summary>
        public async Task<bool> ToRole(ToRoleInput input)
        {
            if (input.Status)
            {
                await _dbContext.Insertable(new Permissions
                {
                    RoleId = input.RoleId,
                    UserId = input.UserId,
                    MenuId = input.MenuId,
                    BtnFunId = input.BtnFunId,
                    Types = input.Types,
                    CreateTime = DateTime.Now
                }).ExecuteCommandAsync();
            }
            else
            {
                switch (input.Types)
                {
                    case 2:
                        await _dbContext.Deleteable<Permissions>()
                            .Where(m => m.UserId == input.UserId && m.RoleId == input.RoleId && m.Types == 2)
                            .ExecuteCommandAsync();
                        break;
                    case 3:
                        await _dbContext.Deleteable<Permissions>()
                            .Where(m => m.BtnFunId == input.BtnFunId && m.RoleId == input.RoleId && m.MenuId == input.MenuId && m.Types == 3)
                            .ExecuteCommandAsync();
                        break;
                }
            }
            return true;
        }
    }
}
