using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Avocado2D.Managers
{
    public class BehaviorManager : Manager
    {
        private List<Behavior> behaviors;

        public BehaviorManager(Scene scene) : base(scene)
        {
            behaviors = new List<Behavior>();
        }

        /// <summary>
        /// Adds a behavior to the behavior manager.
        /// </summary>
        /// <param name="behavior">The behavior.</param>
        public void AddBehavior(Behavior behavior)
        {
            behaviors.Add(behavior);
        }

        /// <summary>
        /// Removes a behavior frtom the behavior manager.
        /// </summary>
        /// <param name="behavior">The behavior.</param>
        public void RemoveBehavior(Behavior behavior)
        {
            behaviors.Add(behavior);
        }

        /// <summary>
        /// Sorts the behaviors by theire update order.
        /// </summary>
        public void SortByUpdateOrder()
        {
            this.behaviors = behaviors.OrderBy(x => x.UpdateOrder).ToList();
        }

        /// <summary>
        /// Updates the behavior manager.
        /// </summary>
        /// <param name="gameTime">The gametime.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            for (var i = 0; i < behaviors.Count; i++)
            {
                var behavior = behaviors[i];
                if (behavior.Enabled && behavior.Initialized)
                    behavior.Update(gameTime);
            }
        }
    }
}