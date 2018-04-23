using QrF.ABP.Modules;
using QrF.ABP.Reflection.Extensions;
using QrF.ABP.AutoMapper;

namespace QrF.Core.Tasks
{
    [DependsOn(typeof(AutoMapperModule))]
    public class TaskModule : BaseModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(TaskModule).GetAssembly());
        }
    }
}
