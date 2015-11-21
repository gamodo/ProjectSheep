
using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSheep{
    class Schaf : GameObject{
        Texture textur2;
        public Schaf(string direction, Vector2f sPosition){
            textur = new Texture(direction);
            sprite = new Sprite(textur);
            baseMovementSpeed = 0.05f;
            sprite.Position = sPosition;
            sprite.Scale = new Vector2f(0.5f, 0.5f);
        }
        protected void Animate(GameTime gTime){
            if (gTime.Total.Milliseconds % 1000 < 500)
                sprite.Texture = textur;
            else
                sprite.Texture = textur2;
        }
        public override void Update(GameTime gTime){
            movementSpeed = baseMovementSpeed * gTime.Ellapsed.Milliseconds;
            MovingDirection = new Vector2f(Game.Player.Position.X - sprite.Position.X,0);
            Move();
            if (isMoving)
                Animate(gTime);
        }
    }
}