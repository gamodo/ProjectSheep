using SFML.Graphics;
using SFML.Window;
using System.Drawing;
using System;
using System.Diagnostics;

namespace ProjectSheep{
    class Game {
        public static Player Player { get; set; }
        public static Schaf Schaf1 { get; set; }
        public static Schaf Schaf2 { get; set; }
        public static Schaf Schaf3 { get; set; }
        public static Schaf Schaf4 { get; set; }
        public static TileMap map { get; private set; }
        public static GameTime gTime { get; private set; }
        public static View view { get; private set; }
        static Stopwatch watch;

        static void Main(string[] args)
        {
            RenderWindow win = new RenderWindow(new VideoMode(1200, 1000), "ProjectSheep", Styles.Default | Styles.Fullscreen);
            win.Closed += (sender, e) => { ((RenderWindow)sender).Close(); };
            Initialize();

            while (win.IsOpen())
            {
                Update();
                Draw(win);
                win.DispatchEvents();
                if (checkCollision())
                    Player.disable();
            }
        }
        public static bool checkCollision()
        {
            Rectangle hBox = new Rectangle((int)Player.sprite.Position.X - 10, (int)Player.sprite.Position.Y - 30, 70, 100);
            Rectangle hBoxS1 = new Rectangle((int)Schaf1.sprite.Position.X + 20, (int)Schaf1.sprite.Position.Y, 35, 70);
            Rectangle hBoxS2 = new Rectangle((int)Schaf2.sprite.Position.X + 20, (int)Schaf2.sprite.Position.Y, 35, 70);
            // fix all sheeps in edge problem
            if (hBoxS1.IntersectsWith(hBoxS2))
                Schaf1.sprite.Position = new Vector2f(Schaf1.sprite.Position.X - 30, Schaf1.sprite.Position.Y);

            if (hBox.IntersectsWith(hBoxS1)||hBox.IntersectsWith(hBoxS2))
                {
                    Console.WriteLine("Game Over");
                    Console.WriteLine("You survived " + watch.Elapsed.Minutes +" minutes "+ watch.Elapsed.Seconds + " seconds " + watch.Elapsed.Milliseconds + " milliseconds");
                    Console.WriteLine("Press 'n' to restart or 'Esc' to close the game");
                    return true;
                }
            
            return false;

        }

        public static void resetComponents()
        {
            watch.Restart();
            Player.sprite.Position = new Vector2f(600, 900);
            Schaf1.sprite.Position = new Vector2f(50, 900);
            Schaf2.sprite.Position = new Vector2f(1000, 900);
        }


        public static void Initialize()
        {
            int[] level = new int[2550];
            for (int i = 0; i < level.Length; i++)
            {
                level[i] = 0;
            }
            map = new TileMap();
            map.load("Bilder/background.bmp", new Vector2u(1200, 1000), level, (uint)50, (uint)50);
            gTime = new GameTime();
            Player = new Player(new Vector2f(600, 900));
            Schaf1 = new Schaf(new Vector2f(50, 900));
            Schaf2 = new Schaf(new Vector2f(800, 900));
            view = new View();
            watch = new Stopwatch();
            watch.Start();
        }

        static void Draw(RenderWindow window)
        {
            window.Clear(new SFML.Graphics.Color(100, 100, 100));
            window.Draw(map);
            Player.Draw(window);
            Schaf1.Draw(window);
            Schaf2.Draw(window);
            window.Display();
            window.SetView(view);
        }

        static void Update()
        {
            gTime.Update();
            Player.Update(gTime);
            Schaf1.Update(gTime);
            Schaf2.Update(gTime);
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                System.Environment.Exit(1);
        }
    }
}