using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Avocado2D.Input
{
    public class InputManager : GameComponent
    {
        /// <summary>
        /// Returns the current keyboard state.
        /// </summary>
        public KeyboardState KeyboardState { get; private set; }

        /// <summary>
        /// Returns the last known keyboard state.
        /// </summary>
        public KeyboardState PreviousKeyboardState { get; private set; }

        /// <summary>
        /// Gets the current mousestate.
        /// </summary>
        public MouseState MouseState { get; private set; }

        /// <summary>
        /// Gets the previous mousestate.
        /// </summary>
        public MouseState PreviousMouseState { get; private set; }

        private Dictionary<string, List<Keys>> keybinds { get; }

        public InputManager(AvocadoGame game) : base(game)
        {
            PreviousKeyboardState = Keyboard.GetState();
            PreviousMouseState = Mouse.GetState();
            keybinds = new Dictionary<string, List<Keys>>();
        }

        /// <summary>
        /// Adds keybindings to the inputmanager.
        /// </summary>
        /// <param name="name">The name of the keybinding.</param>
        /// <param name="keys">The keys.</param>
        public void AddKeybind(string name, params Keys[] keys)
        {
            if (!keybinds.ContainsKey(name))
            {
                keybinds.Add(name, new List<Keys>());
                InsertKeys(name, keys);
            }
            else
            {
                InsertKeys(name, keys);
            }
        }

        private void InsertKeys(string name, Keys[] keys)
        {
            foreach (var key in keys)
            {
                keybinds[name].Add(key);
            }
        }

        /// <summary>
        /// Updates the inputmanager.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            // updates keyboardstates
            PreviousKeyboardState = KeyboardState;
            KeyboardState = Keyboard.GetState();

            // updates mousestates
            PreviousMouseState = MouseState;
            MouseState = Mouse.GetState();

            base.Update(gameTime);
        }

        /// <summary>
        /// Check for releases by comparing the previous state to the current state.
        /// In the event of a key release it will have been down, and currently its up
        /// </summary>
        /// <param name="key">This is the key to check for release</param>
        /// <returns></returns>
        public bool KeyReleased(Keys key)
        {
            return KeyboardState.IsKeyUp(key) && PreviousKeyboardState.IsKeyDown(key);
        }

        /// <summary>
        /// Gets the released key by the name of the key.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool KeyReleased(string name)
        {
            if (!keybinds.ContainsKey(name)) return false;
            foreach (var key in keybinds[name])
            {
                if (KeyboardState.IsKeyUp(key) && PreviousKeyboardState.IsKeyDown(key))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Given a previous key state of up determine if its been pressed.
        /// </summary>
        /// <param name="key">key to check</param>
        /// <returns></returns>
        public bool KeyHit(Keys key)
        {
            return KeyboardState.IsKeyDown(key) && PreviousKeyboardState.IsKeyUp(key);
        }

        /// <summary>
        /// Given a previous key state of up determine if its been pressed.
        /// </summary>
        /// <param name="name">The name of the keybind.</param>
        /// <returns></returns>
        public bool KeyHit(string name)
        {
            if (!keybinds.ContainsKey(name)) return false;
            foreach (var key in keybinds[name])
            {
                if (KeyboardState.IsKeyDown(key) && PreviousKeyboardState.IsKeyUp(key))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Don't examine last state just check if a key is down
        /// </summary>
        /// <param name="key">key to check</param>
        /// <returns></returns>
        public bool KeyDown(Keys key)
        {
            // check if a key is down regardless of current/past state
            return KeyboardState.IsKeyDown(key);
        }

        /// <summary>
        /// Don't examine last state just check if a key is down
        /// </summary>
        /// <param name="name">The name of the keybind.</param>
        /// <returns></returns>
        public bool KeyDown(string name)
        {
            if (!keybinds.ContainsKey(name)) return false;

            foreach (var key in keybinds[name])
            {
                if (KeyboardState.IsKeyDown(key))
                    return true;
            }
            return false;
        }
    }
}