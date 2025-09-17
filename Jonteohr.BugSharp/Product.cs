using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugSharp.Exceptions;
using BugSharp.Remote;

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
        /// <exception cref="BugZillaRequestException">Thrown if a comment with that ID already exists. This property must be unset to properly function.</exception>
        public async Task<Product> SaveChangesAsync()
        {
            if (!string.IsNullOrEmpty(_originalProduct.id.ToString()))
                throw new BugZillaRequestException("The product with id " + Id + " already exists on the server.");

            // var newId = await _bugZilla.Products.CreateProductAsync(this);
            // var product = await _bugZilla.Products.GetProduct(newId);

            // return product;
            return this;
        }
    }
}