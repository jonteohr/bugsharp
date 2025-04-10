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
        
        /// <summary>
        /// Create a client that connects to Bugzilla with the configured dependencies.
        /// </summary>
        public BugZilla(ServiceLocator services, BugZillaSettings settings)
        {
            _services = services;
            _settings = settings;
        }

        /// <summary>
        /// The builder to create a BugZilla client
        /// </summary>
        /// <param name="url">URL to the base bugzilla instance</param>
        /// <param name="apiKey">OPTIONAL: API key if necessary to access information</param>
        public static BugZilla Create(string url, string apiKey = null)
        {
            var settings = new BugZillaSettings(url, apiKey);
            var services = new ServiceLocator();
            var client = new BugZilla(services, settings);
            ConfigureDefaultServices(services, client);
            return client;
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
        /// The service for working with comments
        /// </summary>
        public ICommentService Comments
        {
            get
            {
                return Services.Get<ICommentService>();
            }
        }

        /// <summary>
        /// The service that handles attachments
        /// </summary>
        public IAttachmentService Attachments
        {
            get
            {
                return Services.Get<IAttachmentService>();
            }
        }

        /// <summary>
        /// The service that handles components
        /// </summary>
        public IComponentService Components
        {
            get
            {
                return Services.Get<IComponentService>();
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

        /// <summary>
        /// Url to the BugZilla server
        /// </summary>
        public string Url
        {
            get
            {
                return _settings.BugZillaUrl;
            }
        }

        /// <summary>
        /// Returns a new Bug that when saved will be created on the remote BugZilla server
        /// </summary>
        public Bug CreateBug()
        {
            return new Bug(this);
        }

        /// <summary>
        /// Returns a new Comment that when saved will be created on the remote BugZilla server
        /// </summary>
        public Comment CreateComment()
        {
            return new Comment(this);
        }

        /// <summary>
        /// Returns a new Attachment that when saved will be created on the remote BugZilla server
        /// </summary>
        public Attachment CreateAttachment()
        {
            return new Attachment(this);
        }

        /// <summary>
        /// Returns a new Component that when saved will be created on the remote BugZilla server
        /// </summary>
        public Component CreateComponent()
        {
            return new Component(this);
        }

        private static void ConfigureDefaultServices(ServiceLocator service, BugZilla bugZilla)
        {
            service.Register<IBugService>(() => new BugService(bugZilla));
            service.Register<ICommentService>(() => new CommentService(bugZilla));
            service.Register<IAttachmentService>(() => new AttachmentService(bugZilla));
            service.Register<IComponentService>(() => new ComponentService(bugZilla));
        }
    }
}