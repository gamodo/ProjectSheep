using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using System.Diagnostics;

namespace ProjectSheep{
    class Player : GameObject {
        public Texture textur2 { get; private set; }
        public Texture texturAtt { get; private set; }
        public Texture texturAtt2 { get; private set; }
        public Texture texturJump { get; private set; }
        public Texture texturJump2 { get; private set; }
        public bool isAttacking;
        public Stopwatch jumpwatch = new Stopwatch();
        public Stopwatch attwatch = new Stopwatch();
        private bool isJumping;
        public float jumpheight;
        public float jumppos;
        public bool jumpstate;
        public Player(Vector2f startPosition) {
            textur = new Texture("Bilder/Old_Man.png");
            textur2 = new Texture("Bilder/Old_ManMirror.png");
            texturAtt = new Texture("Bilder/Old_Man.png");
            texturAtt2 = new Texture("Bilder/Old_ManMirror.png");
            texturJump = new Texture("Bilder/Old_Man.png");
            texturJump2 = new Texture("Bilder/Old_ManMirror.png");
            sprite = new Sprite(textur);
            sprite.Position = startPosition;
            baseMovementSpeed = 0.5f;
            sprite.Scale = new Vector2f(0.2f, 0.2f);
            isAttacking = false;
            jumpheight = 0;
            jumppos = 0;
            jumpstate = false;
        }
        void KeyboardInput()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
            {
                if (!isJumping)
                {
                    isJumping = true;
                    MovingDirection = new Vector2f(MovingDirection.X, -1);
                    jumppos = sprite.Position.Y;
                }
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.A))
                MovingDirection = new Vector2f(-1, MovingDirection.Y);
            else if (Keyboard.IsKeyPressed(Keyboard.Key.D))
                MovingDirection = new Vector2f(1, MovingDirection.Y);
            else
                MovingDirection = new Vector2f(0, MovingDirection.Y);
            if (!isAttacking)
            {
                if (Mouse.IsButtonPressed(Mouse.Button.Left))
                {
                    //attack();
                    isAttacking = true;
                    attwatch.Restart();
                }
            }
        }

        public void disable()
        {
            while (true)
            {
                if (Keyboard.IsKeyPressed(Keyboard.Key.N))
                {
                    Game.resetComponents();
                    break;
                }
                if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                {
                    System.Environment.Exit(1);
                }
            }
        }

        protected void Animate(GameTime gTime)
        {
            if (attwatch.Elapsed.Seconds >= 1)
            {
                if (MovingDirection.X > 1)
                {
                    if (isJumping)
                        sprite.Texture = texturJump;
                    else
                        sprite.Texture = textur;
                }
                else
                {
                    if (isJumping)
                        sprite.Texture = texturJump2;
                    else
                        sprite.Texture = textur2;
                }
            }
            else if (MovingDirection.Y > 0)
                sprite.Texture = texturAtt;
            else
                sprite.Texture = texturAtt2;
        }

        public void Jump(GameTime gTime)
        {
            if (jumpheight < 350 && jumpstate == false)
            {
                MovingDirection = new Vector2f(MovingDirection.X, -1);
                jumpheight = jumppos-sprite.Position.Y;
                if(jumpheight>345)jumpstate = true;
            }
            else if (jumpheight>0)
            {
                MovingDirection = new Vector2f(MovingDirection.X, 1);
                jumpheight = jumppos - sprite.Position.Y;
                movementSpeedY = baseMovementSpeed * gTime.Ellapsed.Milliseconds * 1.5f; ;
            }
            else
            {
                MovingDirection = new Vector2f(MovingDirection.X, 0);
                isJumping = false;
                jumpheight = 0;
                jumppos = 0;
                jumpstate = false;
                movementSpeedY = 0;
            }
        }
        public override void Update(GameTime gTime){
            movementSpeedX = baseMovementSpeed * gTime.Ellapsed.Milliseconds;
            movementSpeedY = baseMovementSpeed * gTime.Ellapsed.Milliseconds;
            KeyboardInput();
            if (isJumping)Jump(gTime);
            Move();
            if (isMoving)
                Animate(gTime);
        }
    }
}