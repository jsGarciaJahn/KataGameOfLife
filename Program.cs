using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KataGameOfLife
{
    class Program
    {
        const int FPS_MAXVAL = 120;
        private static int _fps;
        public static int Fps
        {
            get => _fps;
            set
            {
                _fps = value;
                if (_fps < 1) _fps = 1;
                if (_fps > FPS_MAXVAL) _fps = FPS_MAXVAL;
            }
        }

        private static bool _running;

        private static List<IGameDisplay> _display;
        private static int _width;
        public static int Cols => _width;
        private static int _height;
        public static int Rows => _height;
        private static bool _gameRun;
        public static int CurrentDisplay
        {
            get => _currentDisplay;
            set
            {
                _currentDisplay = value;
                if (_currentDisplay < 0) _currentDisplay = _display.Count - 1;
                if (_currentDisplay > _display.Count - 1) _currentDisplay = 0;
            }
        }
        private static int _currentDisplay;


        static void Main(string[] args)
        {
            Setup();
            Run();
        }

        static void Setup()
        {
            Console.CursorVisible = false;
            _width = 100;
            _height = 40;
            Console.WindowHeight = _height+1;
            Console.WindowWidth = _width;
            Fps = 30;
            _gameRun = true;
            _display = new List<IGameDisplay>();
            _display.Add(new AsciiGameDisplay());
            _display.Add(new RawGameDisplay());
            _display.Add(new GraficsDisplay());
            _currentDisplay = 0;
            GameOfLife.Threshold = 0.5;
            WriteTitle();
            Task.Run(KeyHandler);
        }

        static void KeyHandler()
        {
            while (_gameRun)
            {
                if (Console.KeyAvailable)
                {
                    var k = Console.ReadKey(true).Key;
                    switch (k)
                    {
                        case ConsoleKey.Escape: _gameRun = false; _running = false; break;
                        case ConsoleKey.Enter: _running = false; break;
                        case ConsoleKey.RightArrow: Fps++; break;
                        case ConsoleKey.LeftArrow: Fps--; break;
                        case ConsoleKey.UpArrow: GameOfLife.Threshold += 0.1; break;
                        case ConsoleKey.DownArrow: GameOfLife.Threshold -= 0.1; break;
                        case ConsoleKey.D: CurrentDisplay++; Console.Clear(); break;
                        case ConsoleKey.A: CurrentDisplay--; Console.Clear(); break;
                    }
                    WriteTitle();
                }
            }
        }

        static void Run()
        {
            while (_gameRun)
            {
                GameOfLife.BuildBoard(_width, _height);
                _running = true;
                while (_running)
                {
                    DateTime t = DateTime.Now;
                    _display[_currentDisplay].DrawFrame(GameOfLife.Board);
                    GameOfLife.NextGen();
                    TimeSpan tFps = new TimeSpan(0, 0, 0, 0, 1000 / Fps);
                    while((DateTime.Now - t) < tFps) { }
                }
            }
        }

        static void WriteTitle() => Console.Title = $"Kata Game Of Life. T: {GameOfLife.Threshold} F: {Fps} D:{_currentDisplay}";
    }
}
