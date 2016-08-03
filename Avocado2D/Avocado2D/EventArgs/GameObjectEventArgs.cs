using System;

namespace Avocado2D
{
    public class GameObjectEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the gameobject.
        /// </summary>
        public GameObject GameObject { get; }

        public GameObjectEventArgs(GameObject gameObject)
        {
            GameObject = gameObject;
        }
    }
}