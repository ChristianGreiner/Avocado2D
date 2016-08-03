using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Avocado2D
{
    public class Component : IDisposable
    {
        /// <summary>
        /// Gets or sets the gameobject of the component.
        /// </summary>
        public GameObject GameObject { get; set; }

        /// <summary>
        /// Whether or not the component is initialized.
        /// </summary>
        public bool Initialized { get; private set; }

        /// <summary>
        /// Whether or not the component is enabled.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets a list of all required components for this component.
        /// </summary>
        public IReadOnlyList<Type> RequiredComponents
        {
            get { return requiredComponents; }
        }

        private readonly List<Type> requiredComponents;

        public Component()
        {
            requiredComponents = new List<Type>();

            var requiredAttributes = (RequiredComponentAttribute[])Attribute.GetCustomAttributes(this.GetType(), typeof(RequiredComponentAttribute));

            foreach (var attribute in requiredAttributes)
            {
                requiredComponents.Add(attribute.RequiredComponentType);
            }
        }

        /// <summary>
        /// Initializes the component.
        /// </summary>
        public virtual void Initialize()
        {
            Initialized = true;
            Enabled = true;
        }

        /// <summary>
        /// Gets called when the component gets removed from the gameobject.
        /// </summary>
        public virtual void OnRemove()
        {
        }

        /// <summary>
        /// Removes the component from the gameobject.
        /// </summary>
        public void Dispose()
        {
            GameObject?.RemoveComponent(GetType());
        }
    }
}