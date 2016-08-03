using Avocado2D.Components;
using Avocado2D.Managers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Avocado2D
{
    public class GameObject : IDisposable
    {
        /// <summary>
        /// Gets the id of the gameobject.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Gets or sets the name of the gameobject.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The scene this gameobject belongs to.
        /// </summary>
        public Scene Scene { get; set; }

        /// <summary>
        /// Whether or not the gameobject is enabled.
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
        /// Gets all components of the gameobject.
        /// </summary>
        public IReadOnlyList<Component> Components => components.Values.ToArray();

        /// <summary>
        /// Gets the transform component of the gameobject.
        /// </summary>
        public Transform Transform { get; }

        private readonly Dictionary<Type, Component> components;
        private static int nextId;

        private RenderManager renderManager;
        private BehaviorManager behaviorManager;
        private List<Drawable> tmpDrawables;
        private List<Behavior> tmpBehavior;

        public GameObject(string name = null)
        {
            Id = nextId++;
            if (string.IsNullOrEmpty(name))
            {
                Name = "GameObject" + Id;
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
            //this.drawableComponents = drawableComponents.OrderBy(x => x.DrawOrder).ToList();
        }

        private void SortByUpdateOrder()
        {
            behaviorManager?.SortByUpdateOrder();
        }

        /// <summary>
        /// Initializes the gameobject and all his components.
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
                component.Initialize();
            }
        }

        /// <summary>
        /// Adds a component to the gameobject.
        /// If the type of the component is already added to the list,
        /// it will returns this type of component.
        /// </summary>
        /// <typeparam name="T">The type of the component.</typeparam>
        /// <returns>Returns the added component.</returns>
        public T AddComponent<T>() where T : Component, new()
        {
            var cmp = new T()
            {
                GameObject = this
            };

            if (components.ContainsKey(typeof(T)))
            {
                return (T)components[typeof(T)];
            }

            if (cmp.GetType().IsSubclassOf(typeof(Behavior)))
            {
                var behavior = cmp as Behavior;
                tmpBehavior.Add(behavior);
            }

            if (cmp.GetType().IsSubclassOf(typeof(Drawable)))
            {
                var drawable = cmp as Drawable;
                tmpDrawables.Add(drawable);
            }

            components.Add(typeof(T), cmp);
            ComponentAdded?.Invoke(this, new ComponentEventArgs(cmp));

            return cmp;
        }

        /// <summary>
        /// Gets a component from the gameobject.
        /// </summary>
        /// <typeparam name="T">The type of the component.</typeparam>
        /// <returns>Returns the component.</returns>
        public T GetComponent<T>() where T : Component
        {
            return (T)components[typeof(T)];
        }

        /// <summary>
        /// Gets a component from the gameobject.
        /// </summary>
        /// <param name="type">The type of the component.</param>
        /// <returns>Returns the component.</returns>
        public Component GetComponent(Type type)
        {
            return components[type];
        }

        /// <summary>
        /// Removes the component from the gameobject.
        /// </summary>
        /// <typeparam name="T">The type of the gameobject.</typeparam>
        public void RemoveComponent<T>() where T : Component
        {
            var type = typeof(T);
            RemoveComponent(type);
        }

        /// <summary>
        /// Removes the component from the gameobject.
        /// </summary>
        /// <param name="type">The type of the gameobject.</param>
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
        /// Disposes the gameobject.
        /// </summary>
        public void Dispose()
        {
            components.Clear();
            Scene?.RemoveGameObject(this);
        }
    }
}