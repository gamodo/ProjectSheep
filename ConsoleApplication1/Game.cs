
using SFML.Graphics;
using SFML.Window;
using System.Drawing;
using System;

namespace ProjectSheep{
    class Game{
        static Schaf schaf1 { get; set; }
        static Schaf schaf2 { get; set; }
        public static Player Player { get; private set; }
        public static TileMap map { get; private set; }
        static GameTime gTime;
        static View view;

            static void Main(string[] args){
                RenderWindow win = new RenderWindow(new VideoMode(900,900), "ProjectSheep",Styles.Default);
                win.Closed += (sender, e) => { ((RenderWindow)sender).Close(); };
                Initialize();

                while (win.IsOpen()){
                    Update();
                    Draw(win);
                    win.DispatchEvents();
                }
            }

       public static void Initialize(){
            int[] level =new int[2550];
            for(int i=0; i< level.Length; i++){
                level[i] = 0;
            }
            map = new TileMap();
            map.load("Bilder/map.bmp", new Vector2u(1200,1000), level, (uint)50, (uint)50);
            gTime = new GameTime();
            Player = new Player(new Vector2f(500, 500));
            schaf1 = new Schaf("Bilder/Sheep1.png", new Vector2f(900,5));
            schaf2 = new Schaf("Bilder/Sheep1mirror.png", new Vector2f(100,5));
            view = new View();
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
