using Avocado2D.SceneManagement;
using Microsoft.Xna.Framework;

namespace Avocado2D
{
    public class AvocadoGame : Game
    {
        /// <summary>
        /// Gets the instance of the avocado-game class.
        /// </summary>
        public static AvocadoGame Instance { get; private set; }

        /// <summary>
        /// Gets or sets the graphicsdevicemanager of the game.
        /// </summary>
        public GraphicsDeviceManager GraphicsDeviceManager { get; }

        #region BUILDIN GAMECOMPONENTS

        /// <summary>
        /// Gets the scenemanager of the game.
        /// </summary>
        public SceneManager SceneManager { get; private set; }

        #endregion BUILDIN GAMECOMPONENTS

        /// <summary>
        /// Gets the settings of the game.
        /// </summary>
        public GameSettings GameSettings { get; }

        public AvocadoGame(GameSettings gameSettings)
        {
            Instance = this;
            GameSettings = gameSettings;
            GraphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            GraphicsDeviceManager.PreferMultiSampling = false;
            GraphicsDeviceManager.SynchronizeWithVerticalRetrace = true;
            GraphicsDeviceManager.PreferredBackBufferWidth = gameSettings.WindowResolution.X;
            GraphicsDeviceManager.PreferredBackBufferHeight = gameSettings.WindowResolution.Y;
            GraphicsDeviceManager.ApplyChanges();
            Window.AllowUserResizing = true;
            IsMouseVisible = true;
        }

        /// <summary>
        /// Initializes the game.
        /// Use this method to create a new scene.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            SceneManager = new SceneManager(this)
            {
                UpdateOrder = 1,
                DrawOrder = 0
            };
            Components.Add(SceneManager);
        }
    }
}