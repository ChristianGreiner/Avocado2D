using Avocado2D.Components;
using Avocado2D.SceneManagement;
using Microsoft.Xna.Framework.Graphics;
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
        public IReadOnlyList<Component> Components => components.ToArray();

        /// <summary>
        /// Gets all drawable components of the gameobject.
        /// </summary>
        public IReadOnlyList<DrawableComponent> DrawableComponents => drawableComponents.ToArray();

        /// <summary>
        /// Gets the transform component of the gameobject.
        /// </summary>
        public Transform Transform { get; }

        private List<Component> components;
        private List<DrawableComponent> drawableComponents;

        private static int nextId;

        public GameObject(string name = null)
        {
            Id = nextId++;
            if (string.IsNullOrEmpty(name))
            {
                Name = "GameObject" + Id;
            }

            components = new List<Component>();

            Transform = new Transform { GameObject = this };
            components.Add(Transform);

            drawableComponents = new List<DrawableComponent>();

            ComponentUpdateOrderChanged += (sender, args) => SortByUpdateOrder();
            ComponentDrawOrderChanged += (sender, args) => SortByDrawOrder();
        }

        private void SortByDrawOrder()
        {
            this.drawableComponents = drawableComponents.OrderBy(x => x.DrawOrder).ToList();
        }

        private void SortByUpdateOrder()
        {
            this.components = components.OrderBy(x => x.UpdateOrder).ToList();
        }

        /// <summary>
        /// Initializes the gameobject and all his components.
        /// </summary>
        public void Initialize()
        {
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

            if (components.Contains(cmp))
            {
                return (T)components.Find(x => x == cmp);
            }
            if (cmp.GetType().IsSubclassOf(typeof(DrawableComponent)))
            {
                var drawable = cmp as DrawableComponent;
                drawableComponents.Add(drawable);
            }

            components.Add(cmp);
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
            return (T)components.Find(x => x.GetType() == typeof(T));
        }

        /// <summary>
        /// Gets a component from the gameobject.
        /// </summary>
        /// <param name="type">The type of the component.</param>
        /// <returns>Returns the component.</returns>
        public Component GetComponent(Type type)
        {
            return components.Find(x => x.GetType() == type);
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
            var cmp = components.Find(x => x.GetType() == type);

            if (cmp == null) return;

            if (cmp.GetType().IsSubclassOf(typeof(DrawableComponent)))
            {
                var drawable = cmp as DrawableComponent;
                drawableComponents.Remove(drawable);
            }

            ComponentRemoved?.Invoke(this, new ComponentEventArgs(cmp));
            components.Remove(cmp);
        }

        /// <summary>
        /// Disposes the gameobject.
        /// </summary>
        public void Dispose()
        {
            components.Clear();
            drawableComponents.Clear();
            Scene?.RemoveGameObject(this);
        }
    }
}