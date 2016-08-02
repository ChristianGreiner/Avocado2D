using System;

namespace Avocado2D
{
    public class ComponentEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the component.
        /// </summary>
        public Component Component { get; }

        public ComponentEventArgs(Component component)
        {
            Component = component;
        }
    }
}