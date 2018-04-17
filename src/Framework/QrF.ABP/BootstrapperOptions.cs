using QrF.ABP.Dependency;
using QrF.ABP.PlugIns;

namespace QrF.ABP
{
    public class BootstrapperOptions
    {
        /// <summary>
        /// Used to disable all interceptors added by ABP.
        /// </summary>
        public bool DisableAllInterceptors { get; set; }

        /// <summary>
        /// IIocManager that is used to bootstrap the ABP system. If set to null, uses global <see cref="Abp.Dependency.IocManager.Instance"/>
        /// </summary>
        public IIocManager IocManager { get; set; }

        /// <summary>
        /// List of plugin sources.
        /// </summary>
        public PlugInSourceList PlugInSources { get; }

        public BootstrapperOptions()
        {
            IocManager = QrF.ABP.Dependency.IocManager.Instance;
            PlugInSources = new PlugInSourceList();
        }
    }
}
