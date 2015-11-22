
using SFML.Graphics;
using SFML.Window;
using System.Drawing;
using System;
using System.Diagnostics;

namespace ProjectSheep{
    class Game {
        public static Schaf schaf1 { get; set; }
        public static Schaf schaf2 { get; set; }
        public static Player Player { get; set; }
        public static TileMap map { get; private set; }
        public static GameTime gTime {get; private set;}
        public static View view { get; private set; }
        static Stopwatch watch;

        static void Main(string[] args){
                RenderWindow win = new RenderWindow(new VideoMode(1200,1000), "ProjectSheep",Styles.Default|Styles.Fullscreen);
                win.Closed += (sender, e) => { ((RenderWindow)sender).Close(); };
                Initialize();

                while (win.IsOpen()){
                    Update();
                    Draw(win);
                    win.DispatchEvents();
                    if (checkCollision())
                    Player.disable();
                }
        }
        public static bool checkCollision()
        {
            Vector2f a = new Vector2f(20, 20);
            Rectangle hBox = new Rectangle((int)Player.sprite.Position.X - 10, (int)Player.sprite.Position.Y - 30, 70, 100);
            Rectangle hBoxS1 = new Rectangle((int)schaf1.sprite.Position.X, (int)schaf1.sprite.Position.Y - 30, 100, 50);
            Rectangle hBoxS2 = new Rectangle((int)schaf2.sprite.Position.X, (int)schaf2.sprite.Position.Y - 30, 100, 50);
            if (hBox.IntersectsWith(hBoxS1) || hBox.IntersectsWith(hBoxS2))
            {
                Console.WriteLine("Game Over");
                Console.WriteLine("You survived " + watch.Elapsed.Seconds + " seconds " + watch.Elapsed.Milliseconds + " milliseconds");
                Console.WriteLine("Press 'n' to restart or 'Esc' to close the game");
                return true;
            }
            return false;

        }

        public static void resetComponents()
        {
            watch.Restart();
            Player.sprite.Position = new Vector2f(500, 500);
            schaf1.sprite.Position = new Vector2f(900, 5);
            schaf2.sprite.Position = new Vector2f(100, 5);
        }


        public static void Initialize(){
            int[] level =new int[2550];
            for(int i=0; i< level.Length; i++){
                level[i] = 0;
            }
            map = new TileMap();
            map.load("Bilder/map.bmp", new Vector2u(1200,1000), level, (uint)50, (uint)50);
            gTime = new GameTime();
            Player = new Player(new Vector2f(500, 950));
            schaf1 = new Schaf(new Vector2f(20,950));
            schaf2 = new Schaf(new Vector2f(900,950));
            view = new View();
            watch = new Stopwatch();
            watch.Start();
        }

        static void Draw(RenderWindow window){
            window.Clear(new SFML.Graphics.Color(100, 100, 100));
            window.Draw(map);
            schaf1.Draw(window);
            schaf2.Draw(window);
            Player.Draw(window);
            window.Display();
            window.SetView(view);
        }

        static void Update(){
            gTime.Update();
            schaf1.Update(gTime);
            schaf2.Update(gTime);
            Player.Update(gTime);
        }
    }
}
