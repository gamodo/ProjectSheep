﻿using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSheep{ 
    abstract class GameObject{
        public Sprite sprite;
        protected Texture textur;
        protected float baseMovementSpeed;
        protected float movementSpeedX;
        protected float movementSpeedY;
        protected bool isMoving = false;
        public Vector2f MovingDirection { get; protected set; }
        public Vector2f Position { get { return sprite.Position; } }
        public Vector2f Size { get { return new Vector2f(textur.Size.X * sprite.Scale.X, textur.Size.Y * sprite.Scale.Y); } }
        protected void Move(){
            float length = (float)Math.Sqrt(MovingDirection.X * MovingDirection.X + MovingDirection.Y * MovingDirection.Y);
            MovingDirection = new Vector2f(MovingDirection.X, MovingDirection.Y);
            if (length != 0)
                MovingDirection = MovingDirection / length;
            MovingDirection = new Vector2f(MovingDirection.X * movementSpeedX, MovingDirection.Y * movementSpeedY);
            isMoving = Game.map.IsWalkable(this);
            if (isMoving)
            {
                sprite.Position += MovingDirection;
                if (sprite.Position.X > 950)
                    this.sprite.Position = new Vector2f(950, this.sprite.Position.Y);
                if (sprite.Position.X < 0)
                    this.sprite.Position = new Vector2f(0, this.sprite.Position.Y);
                if (sprite.Position.Y < 0)
                    this.sprite.Position = new Vector2f(this.sprite.Position.X, 0);
                if (sprite.Position.Y > 900)
                    this.sprite.Position = new Vector2f(this.sprite.Position.X, 900);
            }
        }
        public abstract void Update(GameTime gTime);
        public void Draw(RenderWindow win){
            win.Draw(sprite);
        }
    }
}