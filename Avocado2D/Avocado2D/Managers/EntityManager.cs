using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Avocado2D.Managers
{
    public class EntityManager : Manager
    {
        #region EVENTS

        /// <summary>
        /// Called when a new entity was added to the scene.
        /// </summary>
        public EventHandler<EntityEventArgs> EntityAdded { get; set; }

        /// <summary>
        /// Called when a new entity was removed from the scene.
        /// </summary>
        public EventHandler<EntityEventArgs> EntityRemoved { get; set; }

        #endregion EVENTS

        private readonly Dictionary<int, Entity> entities;
        private readonly Scene scene;

        public EntityManager(Scene scene) : base(scene)
        {
            this.scene = scene;
            entities = new Dictionary<int, Entity>();
        }

        /// <summary>
        /// Adds a entity to the scene.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Add(Entity entity)
        {
            if (entity == null) return;
            entity.Scene = scene;
            entities.Add(entity.Id, entity);
            entity.Initialize();
            EntityAdded?.Invoke(this, new EntityEventArgs(entity));
        }

        /// <summary>
        /// Removes a entity from the scene.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Remove(Entity entity)
        {
            if (entity == null) return;
            Remove(entity.Id);
        }

        /// <summary>
        /// Removes a entity from the scene.
        /// </summary>
        /// <param name="id">The id of the entity.</param>
        public void Remove(int id)
        {
            if (entities.ContainsKey(id))
            {
                entities.Remove(id);
            }
        }

        /// <summary>
        /// Gets an entity from the scene.
        /// </summary>
        /// <param name="id">The id of the entity.</param>
        /// <returns>Returns the entity.</returns>
        public Entity Get(int id)
        {
            if (entities.ContainsKey(id))
            {
                return entities[id];
            }
            return null;
        }

        /// <summary>
        /// Gets an entity from the scene.
        /// </summary>
        /// <param name="name">The name of the entity.</param>
        /// <returns>Returns the entity.</returns>
        public Entity Get(string name)
        {
            foreach (var entity in entities)
            {
                if (entity.Value.Name.EndsWith(name))
                {
                    return entity.Value;
                }
            }
            return null;
        }
    }
}