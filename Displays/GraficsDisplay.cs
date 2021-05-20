using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;

namespace KataGameOfLife
{
    class GraficsDisplay : IGameDisplay
    {
        [DllImport("kernel32.dll", EntryPoint = "GetConsoleWindow", SetLastError = true)]
        private static extern IntPtr GetConsoleHandle();

        [DllImport("user32.dll")]
        private static extern int GetWindowRect(IntPtr hwnd, out Rectangle rect);

        public GraficsDisplay()
        {
            _handler = GetConsoleHandle();
            Rectangle r;
            GetWindowRect(Process.GetCurrentProcess().MainWindowHandle, out r);
            _width = (int)((r.Width - r.X));
            _height = (int)((r.Height - r.Y) * 0.95);
            _bWhite = new SolidBrush(Color.White);
            _bBlack = new SolidBrush(Color.Black);
            _eWidth = _width / Program.Cols;
            _eHeight = _height / Program.Rows;

        }

        private IntPtr _handler;
        private int _width;
        private int _height;
        private Brush _bWhite;
        private Brush _bBlack;
        private int _eWidth;
        private int _eHeight;
        private double _trippyAngle;
        public double TrippyAngle
        {
            get => _trippyAngle;
            set
            {
                _trippyAngle = value;
                if (_trippyAngle > Math.PI * 2) _trippyAngle = 0;
            }
        }

        Bitmap Display;
        public void DrawFrame(string[] board)
        {
            Display = new Bitmap(_width, _height);
            Brush trippyBrush = GetTrippyBrush(TrippyAngle += 0.25);

            // In buffer zeichnen
            using (var g = Graphics.FromImage(Display))
            {
                g.FillRectangle(_bBlack, 0, 0, _width, _height);
                for (int y = 0; y < board.Length; y++)
                {
                    for (int x = 0; x < board[y].Length; x++)
                    {
                        if (board[y][x] == '0') continue;

                        g.FillEllipse(trippyBrush, x * _eWidth, y * _eHeight, _eWidth, _eHeight);
                    }
                }

            }

            // Auf Console zeichnen
            using (var grafics = Graphics.FromHwnd(_handler))
            using (Display)
                grafics.DrawImage(Display, 0, 0, _width, _height);

        }

        private Brush GetTrippyBrush(double angle)
        {
            int r = (int)(0.5 * (1 + Math.Sin(angle)) * 255);
            int g = (int)(0.5 * (1 + Math.Sin(angle + Math.PI * 0.75)) * 255);
            int b = (int)(0.5 * (1 + Math.Sin(angle + Math.PI * 1.25)) * 255);
            return new SolidBrush(Color.FromArgb(r, g, b));
        }
    }
}
