using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Shooter
{
    class DirectionDictionary
    {
        /// <summary>
        /// 1 - Left, 2 - Right, 3 - Up, 4 - Down, and Unknown direction for any other int.
        /// </summary>
        public enum Direction
        {
            Unknown,
            Left,
            Right,
            Up,
            Down
        }
        /// <summary>
        /// This function returns one of the cardinal directions, with coresponding values:
        /// 1 - Left, 2 - Right, 3 - Up, 4 - Down, and Unknown direction for any other int.
        /// </summary>
        /// <param name="x">int value to be translated to a Direction</param>
        /// <returns>Direction</returns>
        public static Direction Translate(int x)
        {
            return x switch
            {
                1 => Direction.Left,
                2 => Direction.Right,
                3 => Direction.Up,
                4 => Direction.Down,
                _ => Direction.Unknown,
            };
        }
    }
}
