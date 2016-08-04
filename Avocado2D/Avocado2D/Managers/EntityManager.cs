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

        private readonly List<Entitiy> entitiesToAdd;
        private readonly List<Entitiy> entitiesToRemove;
        private Dictionary<int, Entitiy> entities;
        private Scene scene;

        public EntityManager(Scene scene) : base(scene)
        {
            this.scene = scene;
            entities = new Dictionary<int, Entitiy>();
            entitiesToAdd = new List<Entitiy>();
            entitiesToRemove = new List<Entitiy>();
        }

        /// <summary>
        /// Adds a entity to the scene.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void AddEntity(Entitiy entity)
        {
            if (entity == null) return;
            entity.Scene = scene;
            entitiesToAdd.Add(entity);
            entity.Initialize();
            EntityAdded?.Invoke(this, new EntityEventArgs(entity));
        }

        /// <summary>
        /// Removes a entity from the scene.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void RemoveEntity(Entitiy entity)
        {
            if (entity == null) return;
            RemoveEntity(entity.Id);
        }

        /// <summary>
        /// Removes a entity from the scene.
        /// </summary>
        /// <param name="id">The id of the entity.</param>
        public void RemoveEntity(int id)
        {
            if (entities.ContainsKey(id))
            {
                entitiesToRemove.Add(entities[id]);
            }
        }

        /// <summary>
        /// Gets a entity from the scene.
        /// </summary>
        /// <param name="id">The id of the entity.</param>
        /// <returns>Returns the entity.</returns>
        public Entitiy GetEntity(int id)
        {
            if (entities.ContainsKey(id))
            {
                return entities[id];
            }
            return null;
        }
    }
}