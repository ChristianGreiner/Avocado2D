using System;

namespace Avocado2D
{
    public class SceneEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the scene.
        /// </summary>
        public Scene Scene { get; }

        public SceneEventArgs(Scene scene)
        {
            Scene = scene;
        }
    }
}