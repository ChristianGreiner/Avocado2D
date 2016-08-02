using Avocado2D.Graphics;
using Avocado2D.Graphics.Viewports;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Avocado2D.SceneManagement
{
    public class Scene
    {
        /// <summary>
        /// Gets the intance of the game.
        /// </summary>
        public AvocadoGame Game { get; }

        /// <summary>
        /// Gets or sets the name of the scene.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets or sets the clearcolor of the scene.
        /// </summary>
        public Color ClearColor { get; set; }

        /// <summary>
        /// Gets the contentmanager of the scene.
        /// </summary>
        public ContentManager Content { get; }

        /// <summary>
        /// Gets the camera of the scene.
        /// </summary>
        public Camera Camera { get; }

        #region EVENTS

        /// <summary>
        /// Called when a new gameobject was added to the scene.
        /// </summary>
        public EventHandler<GameObjectEventArgs> GameObjectAdded { get; set; }

        /// <summary>
        /// Called when a new gameobject was removed from the scene.
        /// </summary>
        public EventHandler<GameObjectEventArgs> GameObjectRemoved { get; set; }

        #endregion EVENTS

        private readonly List<GameObject> gameObjectsToAdd;
        private readonly List<GameObject> gameObjectsToRemove;
        private readonly Dictionary<int, GameObject> gameObjects;
        private readonly GraphicsDevice graphicsDevice;

        public Scene(string name, AvocadoGame game)
        {
            Game = game;
            Name = name;
            Content = new ContentManager(game.Services);
            ClearColor = Color.Black;

            gameObjects = new Dictionary<int, GameObject>();
            graphicsDevice = game.GraphicsDevice;
            gameObjectsToAdd = new List<GameObject>();
            gameObjectsToRemove = new List<GameObject>();

            var settings = Game.GameSettings;
            ViewportAdapter adapter = null;

            switch (settings.ViewportType)
            {
                case ViewportType.Default:
                    adapter = new DefaultViewportAdapter(Game.GraphicsDevice);
                    break;

                case ViewportType.Scaling:
                    adapter = new ScalingViewportAdapter(game.GraphicsDevice, settings.VirtualResolution.X, settings.VirtualResolution.Y);
                    break;

                case ViewportType.Window:
                    adapter = new WindowViewportAdapter(game.Window, graphicsDevice);
                    break;
            }
            Camera = new Camera(adapter);
        }

        /// <summary>
        /// Adds a gameobject to the scene.
        /// </summary>
        /// <param name="gameObject">The gameobject.</param>
        public void AddGameObject(GameObject gameObject)
        {
            if (gameObject == null) return;
            gameObject.Scene = this;
            gameObject.Initialize();
            gameObjectsToAdd.Add(gameObject);
            GameObjectAdded?.Invoke(this, new GameObjectEventArgs(gameObject));
        }

        /// <summary>
        /// Removes a gameobject from the scene.
        /// </summary>
        /// <param name="gameObject">The gameobject.</param>
        public void RemoveGameObject(GameObject gameObject)
        {
            if (gameObject == null) return;
            RemoveGameObject(gameObject.Id);
        }

        /// <summary>
        /// Removes a gameobject from the scene.
        /// </summary>
        /// <param name="id">The id of the gameobject.</param>
        public void RemoveGameObject(int id)
        {
            if (gameObjects.ContainsKey(id))
            {
                gameObjectsToRemove.Add(gameObjects[id]);
            }
        }

        /// <summary>
        /// Loads the content of the scene.
        /// </summary>
        /// <param name="content">The contentmanager of the scene.</param>
        public virtual void LoadContent(ContentManager content)
        {
        }

        /// <summary>
        /// Gets called when the scene is active.
        /// </summary>
        public virtual void OnActive()
        {
        }

        /// <summary>
        /// Gets called when the scene gets destoryed / removed.
        /// </summary>
        public virtual void OnDestroyed()
        {
        }

        /// <summary>
        /// Updates the scene.
        /// </summary>
        /// <param name="gameTime">The gametime.</param>
        public virtual void Update(GameTime gameTime)
        {
            if (gameObjectsToAdd.Count > 0)
            {
                foreach (var gameObject in gameObjectsToAdd)
                {
                    gameObjects.Add(gameObject.Id, gameObject);
                    gameObject.Enabled = true;
                }
                gameObjectsToAdd.Clear();
            }

            if (gameObjectsToRemove.Count > 0)
            {
                foreach (var gameObject in gameObjectsToRemove)
                {
                    gameObjects.Remove(gameObject.Id);
                }

                gameObjectsToRemove.Clear();
            }

            foreach (var entries in gameObjects)
            {
                var gameObj = entries.Value;
                if (!gameObj.Enabled) return;
                foreach (var component in gameObj.Components)
                {
                    if (component.Enabled && component.Initialized)
                        component.Update(gameTime);
                }
            }
        }

        /// <summary>
        /// Draws the content of the scene.
        /// </summary>
        /// <param name="spriteBatch">The spritebatch.</param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(ClearColor);

            spriteBatch.Begin(samplerState: SamplerState.PointWrap, transformMatrix: Camera.GetViewMatrix());

            foreach (var entries in gameObjects)
            {
                var gameObj = entries.Value;
                if (!gameObj.Enabled) return;
                foreach (var component in gameObj.DrawableComponents)
                {
                    if (component.Enabled && component.Initialized && component.Visible)
                        component.Draw(spriteBatch);
                }
            }
            spriteBatch.End();
        }
    }
}