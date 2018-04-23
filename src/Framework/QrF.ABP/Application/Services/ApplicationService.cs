using System.Collections.Generic;

namespace QrF.ABP.Application.Services
{
    /// <summary>
    /// This class can be used as a base class for application services. 
    /// </summary>
    public abstract class ApplicationService : ServiceBase, IApplicationService, IAvoidDuplicateCrossCuttingConcerns
    {
        public static string[] CommonPostfixes = { "AppService", "ApplicationService" };
        
        /// <summary>
        /// Gets the applied cross cutting concerns.
        /// </summary>
        public List<string> AppliedCrossCuttingConcerns { get; } = new List<string>();

        /// <summary>
        /// Constructor.
        /// </summary>
        protected ApplicationService()
        {

        }
        
    }
}
