using IdentityServer4.Stores;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.Ids4.Controllers
{
    [Route("Ids4API/values")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientStore _clientStore;
        public ClientsController(IClientStore clientStore)
        {
            _clientStore = clientStore;
        }


    }
}
