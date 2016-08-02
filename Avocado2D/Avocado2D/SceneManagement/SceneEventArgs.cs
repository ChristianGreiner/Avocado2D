using System;

namespace Avocado2D.SceneManagement
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