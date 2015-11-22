
using SFML.Graphics;
using SFML.Window;
using System.Drawing;
using System;
using System.Diagnostics;

namespace ProjectSheep{
    class Game {
        public static Player Player { get; set; }
        public static TileMap map { get; private set; }
        public static GameTime gTime {get; private set;}
        public static View view { get; private set; }
        static Stopwatch watch;
        static Schafgenerator schafgenerator;

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
            Rectangle[] hbox = new Rectangle[100];
            Rectangle hBoxP = new Rectangle((int)Player.sprite.Position.X - 10, (int)Player.sprite.Position.Y - 30, 70, 100);
            for (int i= 0;i< 100;i++)
            {
                hbox[i] = new Rectangle((int)Schafgenerator.schafArray[i].sprite.Position.X, (int)Schafgenerator.schafArray[i].sprite.Position.Y - 30, 100, 50);
                if (hBoxP.IntersectsWith(hbox[i]))
                {
                    Console.WriteLine("Game Over");
                    Console.WriteLine("You survived " + watch.Elapsed.Seconds + " seconds " + watch.Elapsed.Milliseconds + " milliseconds");
                    Console.WriteLine("Press 'n' to restart or 'Esc' to close the game");
                    return true;
                }
            }
            return false;

        }

        public static void resetComponents()
        {
            watch.Restart();
            Player.sprite.Position = new Vector2f(600, 900);
        }


        public static void Initialize(){
            int[] level =new int[2550];
            for(int i=0; i< level.Length; i++){
                level[i] = 0;
            }
            map = new TileMap();
            map.load("Bilder/background.bmp", new Vector2u(1200,1000), level, (uint)50, (uint)50);
            gTime = new GameTime();
            Player = new Player(new Vector2f(600, 900));
            schafgenerator = new Schafgenerator(1);
            view = new View();
            watch = new Stopwatch();
            watch.Start();
        }

        static void Draw(RenderWindow window){
            window.Clear(new SFML.Graphics.Color(100, 100, 100));
            window.Draw(map);
            schafgenerator.Draw(window);
            Player.Draw(window);
            window.Display();
            window.SetView(view);
        }

        static void Update(){
            gTime.Update();
            schafgenerator.Update(gTime);
            Player.Update(gTime);
        }
    }
}
