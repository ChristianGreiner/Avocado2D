using Microsoft.Xna.Framework;
using System;

namespace Avocado2D.Graphics
{
    public static class ColorConverter
    {
        /// <summary>
        /// Converts a hexcode to a color.
        /// </summary>
        /// <param name="hexcode">The hexcode.</param>
        /// <returns>Returns the color.</returns>
        public static Color HexToColor(string hexcode)
        {
            return new Color(
                             Convert.ToInt32(hexcode.Substring(1, 2), 16),
                             Convert.ToInt32(hexcode.Substring(3, 2), 16),
                             Convert.ToInt32(hexcode.Substring(5, 2), 16));
        }
    }
}