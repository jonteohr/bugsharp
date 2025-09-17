using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugSharp.Remote;
using Newtonsoft.Json;

namespace BugSharp
{
    public class Product
    {
        private readonly BugZilla _bugZilla;
        private RemoteProduct _originalProduct;

        public Product(BugZilla bugZilla) : this(bugZilla, new RemoteProduct())
        {
        }

        public Product(BugZilla bugZilla, RemoteProduct remoteProduct)
        {
            _bugZilla = bugZilla;
            Initialize(remoteProduct);
        }

        private void Initialize(RemoteProduct remoteProduct)
        {
            _originalProduct = remoteProduct;
            
            Id = remoteProduct.id;
            Name = remoteProduct.name;
            Description = remoteProduct.description;
            IsActive = remoteProduct.is_active;
            DefaultMilestone = remoteProduct.default_milestone;
            HasUnconfirmed = remoteProduct.has_unconfirmed;
            Classification = remoteProduct.classification;
            Components =  remoteProduct.components != null ? remoteProduct.components.ToList() : new List<Component>();
            Versions = remoteProduct.versions != null ? remoteProduct.versions.ToList() : new List<Version>();
            Milestones = remoteProduct.milestones != null ? remoteProduct.milestones.ToList() : new List<Version>();
        }
        
        /// <summary>
        /// An integer ID uniquely identifying the product in this installation only.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// The name of the product. This is a unique identifier for the product.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// A description of the product, which may contain HTML.
        /// </summary>
        public string Description  { get; set; }
        
        /// <summary>
        /// A boolean indicating if the product is active.
        /// </summary>
        public bool IsActive  { get; set; }
        
        /// <summary>
        /// The name of the default milestone for the product.
        /// </summary>
        public string DefaultMilestone { get; set; }
        
        /// <summary>
        /// Indicates whether the UNCONFIRMED bug status is available for this product.
        /// </summary>
        public bool HasUnconfirmed { get; set; }
        
        /// <summary>
        /// The classification name for the product.
        /// </summary>
        public string Classification { get; set; }
        
        /// <summary>
        /// Each component object has the items described in the Component object below.
        /// </summary>
        /// <seealso cref="Component"/>
        public List<Component> Components { get; set; }
        
        /// <summary>
        /// Each object describes a version, and has the following items: name, sort_key and is_active.
        /// </summary>
        /// <seealso cref="Version"/>
        public List<Version> Versions { get; set; }
        
        /// <summary>
        /// Each object describes a milestone, and has the following items: name, sort_key and is_active.
        /// </summary>
        /// <seealso cref="Version"/>
        public List<Version> Milestones { get; set; }

        /// <summary>
        /// Saves a product on the remote BugZilla server.
        /// </summary>
        public async Task<Product> SaveChangesAsync()
        {
            await _bugZilla.Products.UpdateProduct(this);
            var product = await _bugZilla.Products.GetProduct(Id);

            return product;
        }
        
        internal Dictionary<string, object> CompareToRemote()
        {
            var diffs = new Dictionary<string, object>();

            void Compare<T>(T current, T original, string remoteName)
            {
                if (!EqualityComparer<T>.Default.Equals(current, original))
                {
                    diffs[remoteName] = current;
                }
            }

            Compare(Id, _originalProduct.id, "id");
            Compare(Name, _originalProduct.name, "name");
            Compare(Description, _originalProduct.description, "description");
            Compare(IsActive, _originalProduct.is_active, "is_active");
            Compare(DefaultMilestone, _originalProduct.default_milestone, "default_milestone");
            Compare(HasUnconfirmed, _originalProduct.has_unconfirmed, "has_unconfirmed");
            Compare(Classification, _originalProduct.classification, "classification");

            if (!AreListsEqual(Components, _originalProduct.components.ToList()))
                diffs["components"] = Components;
            
            if (!AreListsEqual(Versions, _originalProduct.versions.ToList()))
                diffs["versions"] = Versions;
            
            if (!AreListsEqual(Milestones, _originalProduct.milestones.ToList()))
                diffs["milestones"] = Milestones;

            return diffs;
        }

        private static bool AreListsEqual<T>(List<T> a, List<T> b)
        {
            if (a == null && b == null) return true;
            if (a == null || b == null) return false;
            if (a.Count != b.Count) return false;
            return a.SequenceEqual(b);
        }
        
        internal string SerializeChanges()
        {
            var changes = CompareToRemote();
            if (changes.Count == 0)
                return string.Empty;

            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            
            var json = JsonConvert.SerializeObject(changes, jsonSettings);

            return json;
        }
    }
}