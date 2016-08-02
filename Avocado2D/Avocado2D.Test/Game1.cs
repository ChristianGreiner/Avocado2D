using Avocado2D.Components;
using Avocado2D.SceneManagement;
using Avocado2D.Test.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Avocado2D.Test
{
    public class Game1 : AvocadoGame
    {
        public Game1() : base(new GameSettings())
        {
        }

        protected override void Initialize()
        {
            base.Initialize();

            var scene = new Scene("Test", this) { ClearColor = Color.CornflowerBlue };
            SceneManager.AddScene(scene);
            SceneManager.SetActiveScene("Test");

            var player = new GameObject("Player");
            player.AddComponent<TestComponent>();
            var renderer = player.AddComponent<TextureRenderer>();

            renderer.Texture = Content.Load<Texture2D>("spaceship");

            scene.AddGameObject(player);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }
    }
}