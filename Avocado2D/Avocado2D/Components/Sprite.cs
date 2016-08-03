using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Avocado2D.Components
{
    public class Sprite : Drawable
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
                Source = new Rectangle(0, 0, value.Width, value.Height);
                TextureOrigin = new Vector2((float)texture.Width / 2, (float)texture.Height / 2);
            }
        }

        private Texture2D texture;

        /// <summary>
        /// Gets or sets the origin of the sprite.
        /// </summary>
        public Vector2 TextureOrigin { get; set; }

        /// <summary>
        /// Gets or sets the opacity of the sprite.
        /// </summary>
        public float Alpha { get; set; }

        /// <summary>
        /// Gets or sets the color tint of the sprite.
        /// </summary>
        public Color ColorTint { get; set; }

        /// <summary>
        /// Gets or sets the source of the texture.
        /// </summary>
        public Rectangle Source { get; set; }

        /// <summary>
        /// Sets or gets the spriteeffect of the sprite.
        /// </summary>
        public SpriteEffects SpriteEffect { get; set; }

        public Sprite()
        {
            Alpha = 1f;
            ColorTint = Color.White;
            SpriteEffect = SpriteEffects.None;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (texture == null) return;
            spriteBatch.Draw(Texture, GameObject.Transform.Position, null, Source, TextureOrigin, GameObject.Transform.Rotation, GameObject.Transform.Scale, ColorTint, SpriteEffect, 0f);
        }
    }
}