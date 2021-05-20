using System;
using System.Linq;
using System.Text;

namespace KataGameOfLife
{
    public class GameOfLife
    {

        private static char[][] _board;
        private static Random r = new Random();
        public static double Threshold
        {
            get => _threshold;
            set 
            {
                _threshold = value;
                if (_threshold < 0) _threshold = 0;
                if (_threshold > 1) _threshold = 1;
            }
        }
        private static double _threshold;

        public static void BuildBoard(int w, int h)
        {
            
            string[] tmp = new string[h];
            for(int i = 0; i< h; i++)
            {
                StringBuilder sw = new StringBuilder(w);
                for(int j = 0; j< w; j++)
                {
                    char next = r.NextDouble() > Threshold ? '1' : '0';
                    sw.Append(next);
                }
                tmp[i] = sw.ToString();
            }
            Board = tmp;
        }
        public static string[] Board
        {
            get
            {
                string[] board = new string[_board.Length];
                for (int i = 0; i < _board.Length; i++)
                {
                    board[i] = new string(_board[i]);
                }
                return board;
            }
            set
            {
                _board = new char[value.Length][];
                for (int i = 0; i < value.Length; i++)
                {
                    _board[i] = value[i].ToCharArray();
                }
            }
        }
        public static void NextGen()
        {
            char[][] NextGen = new char[_board.Length][];
            for (int y = 0; y < _board.Count(); y++)
            {
                NextGen[y] = new char[_board[y].Length];
                for (int x = 0; x < _board[y].Length; x++)
                {
                    int liveNeighbors = CountNeighbors(x, y);
                    // apply rules
                    NextGen[y][x] = SetCellstate(liveNeighbors, _board[y][x]);
                }
            }
            _board = NextGen;
        }
        private static int CountNeighbors(int x, int y)
        {
            int liveNeighbors = 0;
            if (y > 0)
            {
                if (x > 0 && _board[y - 1][x - 1] == '1') liveNeighbors++;
                if (_board[y - 1][x] == '1') liveNeighbors++;
                if (x < _board[y - 1].Length - 1 && _board[y - 1][x + 1] == '1') liveNeighbors++;
            }
            if (x > 0 && _board[y][x - 1] == '1') liveNeighbors++;
            if (x < _board[y].Length - 1 && _board[y][x + 1] == '1') liveNeighbors++;
            if (y < _board.Length - 1)
            {
                if (x > 0 && _board[y + 1][x - 1] == '1') liveNeighbors++;
                if (_board[y + 1][x] == '1') liveNeighbors++;
                if (x < _board[y + 1].Length - 1 && _board[y + 1][x + 1] == '1') liveNeighbors++;
            }
            return liveNeighbors;
        }

        private static char SetCellstate(int LiveNeighbors, char current)
        {
            if (LiveNeighbors < 2 || LiveNeighbors > 3) return '0';
            if (LiveNeighbors == 2 && current == '1') return '1';
            if (LiveNeighbors == 3) return '1';
            return '0';
        }
    }
}
