using System;
using System.Collections.Concurrent;

namespace BugSharp
{
    /// <summary>
    /// The main object that handles service registration and handling
    /// </summary>
    public class ServiceLocator
    {
        private readonly ConcurrentDictionary<Type, object> _services;
        private readonly ConcurrentDictionary<Type, object> _factories;
        
        /// <summary>
        /// Constructor for the service locator class
        /// </summary>
        public ServiceLocator()
        {
            _services = new ConcurrentDictionary<Type, object>();
            _factories = new ConcurrentDictionary<Type, object>();
        }
        
        /// <summary>
        /// Gets a service
        /// </summary>
        public TService Get<TService>()
        {
            var serviceType = typeof(TService);
            object factoryObj = null;
            object serviceObj = null;

            if (_services.TryGetValue(serviceType, out serviceObj))
            {
                return (TService)serviceObj;
            }
            else if (_factories.TryGetValue(serviceType, out factoryObj))
            {
                serviceObj = ((Func<TService>)factoryObj).Invoke();
                _services.TryAdd(serviceType, serviceObj);

                return (TService)serviceObj;
            }
            else
            {
                throw new InvalidOperationException(String.Format("Service '{0}' not found.", typeof(TService)));
            }
        }

        /// <summary>
        /// Registers a service
        /// </summary>
        /// <param name="factory">Factory that creates the service instance.</param>
        public void Register<TService>(Func<TService> factory)
        {
            var serviceType = typeof(TService);
            object factoryObj = null;

            _factories.AddOrUpdate(serviceType, factory, (s, f) => factory);
            _services.TryRemove(serviceType, out factoryObj);
        }

        /// <summary>
        /// Clears all registered services.
        /// </summary>
        public void Clear()
        {
            _factories.Clear();
            _services.Clear();
        }
    }
}