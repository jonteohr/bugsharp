using BugSharp.Services;

namespace BugSharp
{
    /// <summary>
    /// The main bugzilla client class
    /// </summary>
    public class BugZilla
    {
        private readonly BugZillaSettings _settings;
        private readonly ServiceLocator _services;
        
        public BugZilla(ServiceLocator services, BugZillaSettings settings)
        {
            _services = services;
            _settings = settings;
        }

        public static BugZillaClientBuilder Builder()
        {
            return new BugZillaClientBuilder();
        }
        
        /// <summary>
        /// The bugzilla bugs service.
        /// </summary>
        public IBugService Bugs
        {
            get
            {
                return Services.Get<IBugService>();
            }
        }

        /// <summary>
        /// The ServiceLocator handling all services.
        /// </summary>
        public ServiceLocator Services
        {
            get
            {
                return _services;
            }
        }

        /// <summary>
        /// The configured settings for the client
        /// </summary>
        public BugZillaSettings Settings
        {
            get
            {
                return _settings;
            }
        }

        public static void ConfigureDefaultServices(ServiceLocator service, BugZilla bugZilla)
        {
            service.Register<IBugService>(() => new BugService(bugZilla));
        }
    }
}