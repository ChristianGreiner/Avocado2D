using Avocado2D.Components;
using Avocado2D.Test.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Avocado2D.Test
{
    public class Game1 : AvocadoGame
    {
        public Game1() : base(new GameSettings() { })
        {
        }

        protected override void Initialize()
        {
            base.Initialize();

            var scene = new Scene("Test", this) { BackgroundColor = Color.CornflowerBlue };
            SceneManager.AddScene(scene);
            SceneManager.SetActiveScene("Test");

            scene.AddGameObject(CreatePlayer(Vector2.Zero));
        }

        private GameObject CreatePlayer(Vector2 position)
        {
            var player = new GameObject();
            player.Transform.Position = position;
            player.AddComponent<TestComponent>();
            player.AddComponent<Rotator>();

            var sprite = player.AddComponent<Sprite>();
            sprite.Texture = Content.Load<Texture2D>("spaceship");
            return player;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            var state = Mouse.GetState();

            if (state.LeftButton == ButtonState.Pressed)
            {
                var player = CreatePlayer(state.Position.ToVector2());
                SceneManager.GetScene("Test").AddGameObject(player);
            }
        }
    }
}