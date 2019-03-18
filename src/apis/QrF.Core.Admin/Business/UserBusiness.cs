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
    public class UserBusiness : IUserBusiness
    {
        private readonly QrfSqlSugarClient _dbContext;
        private readonly IMapper _mapper;

        public UserBusiness(QrfSqlSugarClient dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BasePageQueryOutput<QueryUserDto>> GetPageList(QueryUsersInput input)
        {
            var list = new List<User>();
            var totalNumber = 0;
            var query = await _dbContext.Queryable<User>()
                .WhereIF(input.DeptId.HasValue, o => o.DeptId == input.DeptId.Value)
                .WhereIF(input.Account.IsNotNullAndWhiteSpace(), o => o.Account == input.Account)
                .WhereIF(input.RealName.IsNotNullAndWhiteSpace(), o => o.RealName == input.RealName)
                .WhereIF(input.Mobile.IsNotNullAndWhiteSpace(), o => o.Mobile == input.Mobile)
                .WhereIF(input.Email.IsNotNullAndWhiteSpace(), o => o.Email == input.Email)
                .WhereIF(input.Status.HasValue, o => o.Status == input.Status.Value)
                .ToPageListAsync(input.PageIndex, input.PageSize, totalNumber);
            list = query.Key;
            totalNumber = query.Value;
            var result = new List<QueryUserDto>();
            if (list != null && list.Count > 0)
            {
                list.ForEach(o =>
                {
                    var parent = _dbContext.Queryable<Organize>().First(it => it.KeyId == o.DeptId);
                    result.Add(new QueryUserDto
                    {
                        DeptName = parent?.Name,
                        KeyId = o.KeyId,
                        Account = o.Account,
                        CreateTime = o.CreateTime,
                        DeptId = o.DeptId,
                        Email = o.Email,
                        HeadPic = o.HeadPic,
                        Mobile = o.Mobile,
                        NickName = o.NickName,
                        RealName = o.RealName,
                        Sex = o.Sex,
                        Status = o.Status,
                        UpLoginDate = o.UpLoginDate,
                        StatusText = o.Status ? "启用" : "停用"
                    });
                });
            }
            return new BasePageQueryOutput<QueryUserDto> { Page = input.PageIndex, Rows = result, Total = totalNumber };
        }
        /// <summary>
        /// 编辑信息
        /// </summary>
        public async Task EditModel(UserDto input)
        {
            input.KeyId = input.KeyId ?? Guid.Empty;
            var model = _mapper.Map<UserDto, User>(input);
            if (model.KeyId != Guid.Empty)
            {
                model.UpdateTime = DateTime.Now;
                if (model.Password.IsNotNullAndWhiteSpace())
                {
                    model.Salt = Randoms.CreateRandomValue(8, false);
                    model.Password = $"{model.Password}{model.Salt}".ToMd5();
                    await _dbContext.Updateable(model)
                                    .IgnoreColumns(it => new { it.Account, it.CreateTime })
                                    .ExecuteCommandAsync();
                }
                else
                {
                    await _dbContext.Updateable(model)
                                    .IgnoreColumns(it => new { it.Account, it.Password, it.Salt, it.CreateTime })
                                    .ExecuteCommandAsync();
                }
            }
            else
            {
                model.CreateTime = DateTime.Now;
                model.UpdateTime = DateTime.Now;
                model.Salt = Randoms.CreateRandomValue(8, false);
                model.Password = $"{model.Password}{model.Salt}".ToMd5();
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
            await _dbContext.Deleteable<User>()
                .Where(f => f.KeyId == input).ExecuteCommandAsync();
        }
    }
}
