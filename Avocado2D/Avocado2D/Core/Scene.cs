using Avocado2D.Graphics;
using Avocado2D.Graphics.Viewports;
using Avocado2D.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Avocado2D
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
        public Color BackgroundColor { get; set; }

        /// <summary>
        /// Gets the contentmanager of the scene.
        /// </summary>
        public ContentManager Content { get; }

        /// <summary>
        /// Gets the camera of the scene.
        /// </summary>
        public Camera Camera { get; }

        /// <summary>
        /// Gets the rendermanager of the scene.
        /// </summary>
        public RenderManager RenderManager { get; }

        /// <summary>
        /// Gets the behaviormanager of the scene.
        /// </summary>
        public BehaviorManager BehaviorManager { get; }

        /// <summary>
        /// Gets the collision manager of the scene.
        /// </summary>
        public CollisionManager CollisionManager { get; }

        /// <summary>
        /// Gets the entity manager of the scene.
        /// </summary>
        public EntityManager EntityManager { get; }

        private readonly GraphicsDevice graphicsDevice;

        public Scene(string name, AvocadoGame game)
        {
            Game = game;
            Name = name;
            graphicsDevice = game.GraphicsDevice;
            Content = new ContentManager(game.Services);
            BackgroundColor = Color.Black;

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

            EntityManager = new EntityManager(this);
            BehaviorManager = new BehaviorManager(this);
            RenderManager = new RenderManager(this, graphicsDevice);
            CollisionManager = new CollisionManager(this);
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
            CollisionManager.Update(gameTime);
            BehaviorManager.Update(gameTime);
        }

        /// <summary>
        /// Draws the content of the scene.
        /// </summary>
        /// <param name="spriteBatch">The spritebatch.</param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            RenderManager.Draw(spriteBatch);
        }
    }
}