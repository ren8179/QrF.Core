using QrF.Core.ComFr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace QrF.Core.CMS
{
    public class ApplicationContextAccessor : IApplicationContextAccessor
    {
        private readonly IServiceProvider _serviceProvider;
        public ApplicationContextAccessor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        private CMSApplicationContext current;
        public CMSApplicationContext Current
        {
            get
            {
                return current ?? (current = _serviceProvider.GetService<IApplicationContext>().As<CMSApplicationContext>());
            }
        }
    }
}
