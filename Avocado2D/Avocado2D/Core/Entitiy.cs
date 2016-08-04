using Avocado2D.Components;
using Avocado2D.Managers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Avocado2D
{
    public class Entitiy : IDisposable
    {
        /// <summary>
        /// Gets the id of the entity.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Gets or sets the name of the entity.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The scene this entity belongs to.
        /// </summary>
        public Scene Scene { get; set; }

        /// <summary>
        /// Whether or not the entity is enabled.
        /// </summary>
        public bool Enabled { get; set; }

        #region EVENTS

        /// <summary>
        /// Triggers when a component was added to the entity.
        /// </summary>
        public EventHandler<ComponentEventArgs> ComponentAdded { get; set; }

        /// <summary>
        /// Triggers when a component was removed from the entity.
        /// </summary>
        public EventHandler<ComponentEventArgs> ComponentRemoved { get; set; }

        /// <summary>
        /// Triggers when a component changed his updateorder.
        /// </summary>
        public EventHandler<ComponentEventArgs> ComponentUpdateOrderChanged { get; set; }

        /// <summary>
        /// Triggers when a component changed his updateorder.
        /// </summary>
        public EventHandler<ComponentEventArgs> ComponentDrawOrderChanged { get; set; }

        #endregion EVENTS

        /// <summary>
        /// Gets all components of the entity.
        /// </summary>
        public IReadOnlyList<Component> Components => components.Values.ToArray();

        /// <summary>
        /// Gets the transform component of the entity.
        /// </summary>
        public Transform Transform { get; }

        private readonly Dictionary<Type, Component> components;
        private static int nextId;

        private RenderManager renderManager;
        private BehaviorManager behaviorManager;
        private List<Drawable> tmpDrawables;
        private List<Behavior> tmpBehavior;

        public Entitiy(string name = null)
        {
            Id = nextId++;
            if (string.IsNullOrEmpty(name))
            {
                Name = "Entity" + Id;
            }

            components = new Dictionary<Type, Component>();
            tmpDrawables = new List<Drawable>();
            tmpBehavior = new List<Behavior>();

            Transform = AddComponent<Transform>();

            ComponentUpdateOrderChanged += (sender, args) => SortByUpdateOrder();
            ComponentDrawOrderChanged += (sender, args) => SortByDrawOrder();
        }

        private void SortByDrawOrder()
        {
            renderManager?.SortByDrawOrder();
        }

        private void SortByUpdateOrder()
        {
            behaviorManager?.SortByUpdateOrder();
        }

        /// <summary>
        /// Initializes the entity and all his components.
        /// </summary>
        public void Initialize()
        {
            renderManager = Scene.RenderManager;
            behaviorManager = Scene.BehaviorManager;

            foreach (var behavior in tmpBehavior)
            {
                behaviorManager.AddBehavior(behavior);
            }
            tmpBehavior.Clear();
            tmpBehavior = null;

            foreach (var drawable in tmpDrawables)
            {
                renderManager.AddDrawable(drawable);
            }

            tmpDrawables.Clear();
            tmpDrawables = null;

            foreach (var component in Components.ToArray())
            {
                component.OnInitialize();
            }
        }

        /// <summary>
        /// Adds a component to the entity.
        /// If the type of the component is already added to the list,
        /// it will returns this type of component.
        /// </summary>
        /// <typeparam name="T">The type of the component.</typeparam>
        /// <returns>Returns the added component.</returns>
        public T AddComponent<T>() where T : Component, new()
        {
            // check if the entity owns alreday this type of component.
            if (components.ContainsKey(typeof(T)))
            {
                return (T)components[typeof(T)];
            }
            var component = new T();

            InitializeComponent(component);

            // requires the comonent other components?
            foreach (var cmpType in component.RequiredComponents)
            {
                if (components.ContainsKey(cmpType)) continue;
                var newCmp = (Component)Activator.CreateInstance(cmpType);
                InitializeComponent(newCmp);
            }

            components.Add(typeof(T), component);
            ComponentAdded?.Invoke(this, new ComponentEventArgs(component));

            return component;
        }

        private void InitializeComponent(Component component)
        {
            component.Entity = this;

            // check if the component is an updatable component
            if (component.GetType().IsSubclassOf(typeof(Behavior)))
            {
                var behavior = component as Behavior;
                tmpBehavior.Add(behavior);
            }

            // check if the component is an drawable component
            if (component.GetType().IsSubclassOf(typeof(Drawable)))
            {
                var drawable = component as Drawable;
                tmpDrawables.Add(drawable);
            }
        }

        /// <summary>
        /// Gets a component from the entity.
        /// </summary>
        /// <typeparam name="T">The type of the component.</typeparam>
        /// <returns>Returns the component.</returns>
        public T GetComponent<T>() where T : Component
        {
            if (components.ContainsKey(typeof(T)))
            {
                return (T)components[typeof(T)];
            }
            return null;
        }

        /// <summary>
        /// Removes the component from the entity.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        public void RemoveComponent<T>() where T : Component
        {
            var type = typeof(T);
            RemoveComponent(type);
        }

        /// <summary>
        /// Removes the component from the entity.
        /// </summary>
        /// <param name="type">The type of the entity.</param>
        public void RemoveComponent(Type type)
        {
            var cmp = components[type];

            if (cmp == null) return;

            if (cmp.GetType().IsSubclassOf(typeof(Behavior)))
            {
                var behavior = cmp as Behavior;
                behaviorManager.RemoveBehavior(behavior);
            }

            if (cmp.GetType().IsSubclassOf(typeof(Drawable)))
            {
                var drawable = cmp as Drawable;
                renderManager.RemoveDrawable(drawable);
            }

            ComponentRemoved?.Invoke(this, new ComponentEventArgs(cmp));
            components.Remove(type);
        }

        /// <summary>
        /// Disposes the entity.
        /// </summary>
        public void Dispose()
        {
            components.Clear();
            Scene?.EntityManager.RemoveEntity(this);
        }
    }
}