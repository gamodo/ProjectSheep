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
        private float jumpHeight;
        private bool isJumping;
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
            jumpHeight = 0;
        }
        void KeyboardInput()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
            {
                if (!isJumping)
                {
                    isJumping = true;
                    MovingDirection = new Vector2f(MovingDirection.X, -1);
                    jumpwatch.Restart();
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

        public void Jump()
        {
            if (jumpwatch.ElapsedMilliseconds < 1000)
            {

                MovingDirection = new Vector2f(MovingDirection.X, -1);
            }
            else if (jumpwatch.ElapsedMilliseconds < 2000)
            {
                MovingDirection = new Vector2f(MovingDirection.X, 1);
            }
            else
            {
                MovingDirection = new Vector2f(MovingDirection.X, 0);
                jumpwatch.Reset();
                isJumping = false;
            }
        }
        public override void Update(GameTime gTime){
            movementSpeedX = baseMovementSpeed * gTime.Ellapsed.Milliseconds;
            movementSpeedY = baseMovementSpeed * gTime.Ellapsed.Milliseconds;
            KeyboardInput();
            if (isJumping) Jump();
            Move();
            if (isMoving)
                Animate(gTime);
        }
    }
}