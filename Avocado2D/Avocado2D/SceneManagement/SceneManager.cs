using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;

namespace Avocado2D.SceneManagement
{
    public class SceneManager : DrawableGameComponent
    {
        /// <summary>
        /// Gets the active scene of the scenemanager.
        /// </summary>
        public Scene ActiveScene { get; private set; }

        #region EVENTS

        /// <summary>
        /// Rises when the active scene of the scenemanager chanaged.
        /// </summary>
        public EventHandler<SceneEventArgs> ActiveSceneChanged { get; set; }

        /// <summary>
        /// Rises when a new scene was added to the scenemanager.
        /// </summary>
        public EventHandler<SceneEventArgs> SceneAdded { get; set; }

        /// <summary>
        /// Rises when a scene was removed from the scenemanager.
        /// </summary>
        public EventHandler<SceneEventArgs> SceneRemoved { get; set; }

        #endregion EVENTS

        private readonly Dictionary<string, Scene> scenes;
        private readonly SpriteBatch spriteBatch;

        public SceneManager(AvocadoGame game) : base(game)
        {
            scenes = new Dictionary<string, Scene>();
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        /// <summary>
        /// Adds a new scene to the scenemanager.
        /// </summary>
        /// <param name="scene">The scene.</param>
        public void AddScene(Scene scene)
        {
            if (scene == null) return;

            if (!scenes.ContainsKey(scene.Name))
            {
                scenes.Add(scene.Name, scene);
                SceneAdded?.Invoke(this, new SceneEventArgs(scene));
            }
            else throw new Exception("The scene '" + scene.Name + "' is already in the scene manager.");
        }

        /// <summary>
        /// Sets the active scene of the scenemanager.
        /// </summary>
        /// <param name="name">The name of the scene that is already in the scenemanager.</param>
        public void SetActiveScene(string name)
        {
            if (scenes.ContainsKey(name))
            {
                ActiveScene = scenes[name];
                ActiveScene.LoadContent(ActiveScene.Content);
                ActiveSceneChanged?.Invoke(this, new SceneEventArgs(ActiveScene));
            }
            else throw new Exception("The scene '" + name + "' isn't in the scene manager.");
        }

        /// <summary>
        /// Removes a scene from the scenemanager.
        /// </summary>
        /// <param name="name">The name of the scene.</param>
        public void RemoveScene(string name)
        {
            if (scenes.ContainsKey(name))
            {
                var scene = scenes[name];
                scene.OnDestroyed();
                scene.Content.Unload();
                SceneRemoved?.Invoke(this, new SceneEventArgs(scene));
                scenes.Remove(name);
            }
            else throw new Exception("The scene '" + name + "' isn't in the scene manager.");
        }

        /// <summary>
        /// Gets the a scene by the name.
        /// </summary>
        /// <param name="name">The name of the scene.</param>
        /// <returns>Returns the scene.</returns>
        public Scene GetScene(string name)
        {
            if (scenes.ContainsKey(name))
            {
                return scenes[name];
            }
            throw new Exception("The scene '" + name + "' isn't in the scene manager.");
        }

        /// <summary>
        /// Updates the active scene.
        /// </summary>
        /// <param name="gameTime">The gametime.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            ActiveScene?.Update(gameTime);
        }

        /// <summary>
        /// Drawws the active scene.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            ActiveScene?.Draw(spriteBatch);
        }
    }
}