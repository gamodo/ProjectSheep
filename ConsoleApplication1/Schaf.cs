
using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSheep{
    class Schaf : GameObject{
        Texture textur2=new Texture("Bilder/Sheep1.png");
        public Schaf(Vector2f sPosition){
            textur = new Texture("Bilder/Sheep1mirror.png");
            sprite = new Sprite(textur);
            baseMovementSpeed = 0.1f;
            sprite.Position = sPosition;
            sprite.Scale = new Vector2f(0.5f, 0.5f);
        }
        protected void Animate(GameTime gTime){
            if (MovingDirection.X>0)
                sprite.Texture = textur;
            else
                sprite.Texture = textur2;
        }
        public override void Update(GameTime gTime){
            movementSpeedX = baseMovementSpeed * gTime.Ellapsed.Milliseconds;
            if (Game.Player.Position.X > sprite.Position.X) MovingDirection = new Vector2f(1, 0);
            else MovingDirection = new Vector2f(-1, 0);
            Move();
            if (isMoving)
                Animate(gTime);
        }
    }
}