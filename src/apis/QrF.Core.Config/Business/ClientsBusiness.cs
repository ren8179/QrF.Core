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
    public class ClientsBusiness : IClientsBusiness
    {
        private readonly QrfSqlSugarClient _dbContext;
        private readonly IMapper _mapper;

        public ClientsBusiness(QrfSqlSugarClient dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        public async Task<BasePageOutput<ClientsDto>> GetPageList(PageInput input)
        {
            var list = new List<Clients>();
            var totalNumber = 0;
            var query = await _dbContext.Queryable<Clients>()
                .WhereIF(!input.Name.IsNullOrEmpty(), o => o.ClientId.Contains(input.Name))
                .ToPageListAsync(input.Page, input.PageSize, totalNumber);
            list = query.Key;
            totalNumber = query.Value;
            var result = new List<ClientsDto>();
            if (list != null && list.Count > 0)
            {
                list.ForEach(o =>
                {
                    result.Add(new ClientsDto
                    {
                        ClientId = o.ClientId,
                        ClientName = o.ClientName,
                        Description = o.Description,
                        Enabled = o.Enabled,
                        Id = o.Id
                    });
                });
            }
            return new BasePageOutput<ClientsDto> { Page = input.Page, Rows = result, Total = totalNumber };
        }
        /// <summary>
        /// 编辑信息
        /// </summary>
        public async Task EditModel(ClientsDto input)
        {
            var model = _mapper.Map<ClientsDto, Clients>(input);
            if (model.Id > 0)
            {
                await _dbContext.Updateable(model).ExecuteCommandAsync();
                return;
            }
            await _dbContext.Insertable(model).ExecuteCommandAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        public async Task DelModel(int input)
        {
            await _dbContext.Deleteable<Clients>()
                .Where(f => f.Id == input).ExecuteCommandAsync();
        }
    }
}
