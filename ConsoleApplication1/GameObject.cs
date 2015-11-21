using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSheep{
    abstract class GameObject{
        protected Sprite sprite;
        protected Texture textur;
        protected float baseMovementSpeed;
        protected float movementSpeed;
        protected bool isMoving = false;
        public Vector2f MovingDirection { get; protected set; }
        public Vector2f Position { get { return sprite.Position; } }
        public Vector2f Size { get { return new Vector2f(textur.Size.X * sprite.Scale.X, textur.Size.Y * sprite.Scale.Y); } }
        protected void Move(){
            float length = (float)Math.Sqrt(MovingDirection.X * MovingDirection.X + MovingDirection.Y * MovingDirection.Y);
            if (length != 0)
                MovingDirection = MovingDirection / length;
            MovingDirection *= movementSpeed;
            isMoving = Game.map.IsWalkable(this);

            if (isMoving)
                sprite.Position += MovingDirection;
        }
        public abstract void Update(GameTime gTime);
        public void Draw(RenderWindow win){
            win.Draw(sprite);
        }
    }
}