using Avocado2D.Components;
using Avocado2D.Test.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Avocado2D.Test
{
    public class Game1 : AvocadoGame
    {
        private SpriteFont font;
        private SpriteBatch batch;

        public Game1() : base(new GameSettings() { })
        {
        }

        protected override void Initialize()
        {
            base.Initialize();
            batch = new SpriteBatch(GraphicsDevice);
            var scene = new Scene("Test", this) { BackgroundColor = Color.CornflowerBlue };
            SceneManager.AddScene(scene);
            SceneManager.SetActiveScene("Test");

            scene.EntityManager.AddEntity(CreatePlayer(Vector2.Zero));
            scene.EntityManager.AddEntity(CreatePlayer(new Vector2(100, 100)));
        }

        private Entitiy CreatePlayer(Vector2 position)
        {
            var player = new Entitiy();
            player.Transform.Position = position;
            player.AddComponent<TestComponent>();
            player.AddComponent<Rotator>();
            var collider = player.AddComponent<Collider>();
            collider.SetBounds(64, 64);

            var sprite = player.AddComponent<Sprite>();
            sprite.Texture = Content.Load<Texture2D>("spaceship");
            return player;
        }

        protected override void LoadContent()
        {
            font = Content.Load<SpriteFont>("DebugFont");
            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            var keyboard = Keyboard.GetState();

            var state = Mouse.GetState();
            var scene = SceneManager.GetScene("Test");
            if (state.LeftButton == ButtonState.Pressed)
            {
                var player = CreatePlayer(state.Position.ToVector2());
                scene.EntityManager.AddEntity(player);
            }

            if (keyboard.IsKeyDown(Keys.Right))
            {
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            batch.Begin();
            batch.End();
        }
    }
}