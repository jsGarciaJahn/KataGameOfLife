using System;
using System.Linq;

namespace KataGameOfLife
{
    class RawGameDisplay : IGameDisplay
    {
        public void DrawFrame(string[] board)
        {
            Console.SetCursorPosition(0, 0);
            Console.Write(board.Aggregate((s1, s2) => s1 + Environment.NewLine + s2));
        }
    }
}
