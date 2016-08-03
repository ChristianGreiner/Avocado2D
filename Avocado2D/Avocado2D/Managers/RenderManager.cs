using Avocado2D.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Avocado2D.Managers
{
    public class RenderManager : Manager
    {
        public TimeSpan DrawTime { get; private set; }

        private readonly Camera camera;
        private List<Drawable> drawables;
        // List<DrawableUI> ...

        private readonly Stopwatch stopwatch;
        private readonly Scene scene;
        private readonly GraphicsDevice graphicsDevice;

        public RenderManager(Scene scene, GraphicsDevice graphicsDevice) : base(scene)
        {
            this.scene = scene;
            this.camera = scene.Camera;
            this.graphicsDevice = graphicsDevice;
            drawables = new List<Drawable>();
            stopwatch = new Stopwatch();
        }

        /// <summary>
        /// Sorts the drawables by their draw order.
        /// </summary>
        public void SortByDrawOrder()
        {
            this.drawables = drawables.OrderBy(x => x.DrawOrder).ToList();
        }

        /// <summary>
        /// Adds a drawable component to the rendermanager.
        /// </summary>
        /// <param name="drawable">The drawable.</param>
        public void AddDrawable(Drawable drawable)
        {
            drawables.Add(drawable);
        }

        /// <summary>
        /// Removes a drawable component form the rendermanager.
        /// </summary>
        /// <param name="drawable">The drawable.</param>
        public void RemoveDrawable(Drawable drawable)
        {
            drawables.Remove(drawable);
        }

        /// <summary>
        /// Draws the rendere maanger.
        /// </summary>
        /// <param name="spriteBatch">The spritebatch.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            stopwatch.Reset();
            stopwatch.Start();

            graphicsDevice.Clear(scene.BackgroundColor);

            spriteBatch.Begin(samplerState: SamplerState.PointWrap, transformMatrix: camera.GetViewMatrix());

            for (var i = 0; i < drawables.Count; i++)
            {
                var drawable = drawables[i];

                if (drawable.Enabled && drawable.Initialized && drawable.Visible)
                    drawable.Draw(spriteBatch);
            }

            spriteBatch.End();
            DrawTime = stopwatch.Elapsed;
            stopwatch.Stop();
        }
    }
}