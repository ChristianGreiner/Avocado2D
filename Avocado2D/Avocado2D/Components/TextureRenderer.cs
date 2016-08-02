using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Avocado2D.Components
{
    public class TextureRenderer : DrawableComponent
    {
        /// <summary>
        /// Gets or sets the texture that should be rendered.
        /// </summary>
        public Texture2D Texture
        {
            get { return texture; }
            set
            {
                texture = value;
                TextureSource = new Rectangle(0, 0, value.Width, value.Height);
            }
        }

        /// <summary>
        /// Gets or sets the origin of the texture.
        /// </summary>
        public Vector2 TextureOrigin { get; set; }

        /// <summary>
        /// Gets or sets the opacity of the texture.
        /// </summary>
        public float Alpha { get; set; }

        /// <summary>
        /// Gets or sets the color tint of the texture.
        /// </summary>
        public Color ColorTint { get; set; }

        /// <summary>
        /// Sets or gets the spriteeffect of the texture.
        /// </summary>
        public SpriteEffects Flip { get; set; }

        /// <summary>
        /// Gets or sets the source of the texture.
        /// </summary>
        public Rectangle TextureSource { get; set; }

        private Texture2D texture;

        public TextureRenderer()
        {
            Flip = SpriteEffects.None;
            Alpha = 1f;
            Visible = true;
            ColorTint = Color.White;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Texture == null) return;
            spriteBatch.Draw(Texture, GameObject.Transform.Position, null, TextureSource, TextureOrigin, GameObject.Transform.Rotation, GameObject.Transform.Scale, ColorTint, Flip, 0f);
        }
    }
}