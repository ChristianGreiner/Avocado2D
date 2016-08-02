using Avocado2D.Graphics.Viewports;
using Microsoft.Xna.Framework;
using System;

namespace Avocado2D.Graphics
{
    public class Camera
    {
        /// <summary>
        /// Gets or sets the position of the camera.
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// Gets or sets thr rotation of the camera.
        /// </summary>
        public float Rotation { get; set; }

        /// <summary>
        /// Gets or sets the origin of the camera.
        /// </summary>
        public Vector2 Origin { get; set; }

        /// <summary>
        /// Gets or sets the zoom of the camera.
        /// </summary>
        public float Zoom
        {
            get { return zoom; }
            set
            {
                if (!(value < MinimumZoom || value > MaximumZoom))
                    zoom = value;
            }
        }

        /// <summary>
        /// Gets or sets the maximum zoom of the camera.
        /// </summary>
        public float MaximumZoom
        {
            get { return maximumZoom; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("MaximumZoom must be greater than zero");

                if (Zoom > value)
                    Zoom = value;

                maximumZoom = value;
            }
        }

        /// <summary>
        /// Gets or sets the minium zoom of the camera.
        /// </summary>
        public float MinimumZoom
        {
            get { return minimumZoom; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("MinimumZoom must be greater than zero");

                if (Zoom < value)
                    Zoom = MinimumZoom;

                minimumZoom = value;
            }
        }

        /// <summary>
        /// Gets the viewportadapter of the camera.
        /// </summary>
        public ViewportAdapter ViewportAdapter { get; }

        private float zoom;
        private float maximumZoom;
        private float minimumZoom;

        public Camera(ViewportAdapter viewportAdapter)
        {
            ViewportAdapter = viewportAdapter;
            MinimumZoom = 1f;
            MaximumZoom = 3f;
            Zoom = 1f;
        }

        /// <summary>
        /// Looks at a specific position.
        /// </summary>
        /// <param name="position">The position.</param>
        public void LookAt(Vector2 position)
        {
            Position = position - new Vector2(ViewportAdapter.VirtualWidth / 2f / Zoom, ViewportAdapter.VirtualHeight / 2f / Zoom);
        }

        /// <summary>
        /// Transform the the given world vector to a screenvector.
        /// </summary>
        /// <param name="worldPosition">The position in the world.</param>
        /// <returns>Returns the converted vector.</returns>
        public Vector2 WorldToScreen(Vector2 worldPosition)
        {
            var viewport = ViewportAdapter.Viewport;
            return Vector2.Transform(worldPosition + new Vector2(viewport.X, viewport.Y), GetViewMatrix());
        }

        /// <summary>
        /// Transform the the given screen vector to a worldvector.
        /// </summary>
        /// <param name="screenPosition">The position on the screen.</param>
        /// <returns>Returns the converted vector.</returns>
        public Vector2 ScreenToWorld(Vector2 screenPosition)
        {
            var matrix = Matrix.Invert(ViewportAdapter.GetScaleMatrix());
            return Vector2.Transform(screenPosition, matrix);
        }

        /// <summary>
        /// Gets the viewmatrix of the camera.
        /// </summary>
        /// <returns>Returns the viewmatrix.</returns>
        public Matrix GetViewMatrix()
        {
            return GetVirtualViewMatrix() * ViewportAdapter.GetScaleMatrix();
        }

        private Matrix GetVirtualViewMatrix()
        {
            return
                Matrix.CreateTranslation(new Vector3(-Position, 0.0f)) *
                Matrix.CreateTranslation(new Vector3(-Origin, 0.0f)) *
                Matrix.CreateRotationZ(Rotation) *
                Matrix.CreateScale(Zoom, Zoom, 1) *
                Matrix.CreateTranslation(new Vector3(Origin, 0.0f));
        }
    }
}