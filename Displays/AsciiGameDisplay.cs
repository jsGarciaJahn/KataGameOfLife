using System;
using System.Text;

namespace KataGameOfLife
{
    class AsciiGameDisplay : IGameDisplay
    {
        public char alive = 'X';
        public char dead = ' ';
        StringBuilder buffer;
        public void DrawFrame(string[] board)
        {
            buffer = new StringBuilder(board.Length * (board[0].Length + 2));
            for (int y = 0; y < board.Length; y++)
            {
                for (int x = 0; x < board[y].Length; x++)
                    buffer.Append(board[y][x] == '1' ? alive : dead);
                buffer.Append(Environment.NewLine);
            }
            Console.SetCursorPosition(0, 0);
            Console.Write(buffer.ToString());
        }
    }
}
