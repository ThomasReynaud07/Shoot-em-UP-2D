using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Shoot_em_UP
{
    internal class Player
    {
        private Vector2 _position = new Vector2(890, 800);
        private int _health;
        private string _name;

        private List<Bullet> _bullets = new List<Bullet>();
        private Texture2D _texture;

        private MouseState _mouseState;
        private GraphicsDevice _graphicsDevice;
        private ContentManager _content;

        private double _playerTimer;
        private double _interval = 0.4;

        public List<Bullet> Bullets => _bullets;
        public int Health => _health;

        private List<Wall> _walls = new List<Wall>();
        public List<Wall> Walls => _walls;


        public Player(int health, string name, GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            _health = health;
            _name = name;
            _graphicsDevice = graphicsDevice;
            _content = contentManager;
            _texture = _content.Load<Texture2D>("MainCara");
        }

        private KeyboardState _previousState;

        public void Update(GameTime gameTime)
        {
            _mouseState = Mouse.GetState();
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.D)) _position.X += 10;
            if (state.IsKeyDown(Keys.A)) _position.X -= 10;

            // Détecter "just pressed" pour la touche E
            if (state.IsKeyDown(Keys.E) && !_previousState.IsKeyDown(Keys.E))
            {
                PlaceWall();
            }

            Shoot(gameTime);

            for (int i = 0; i < _bullets.Count; i++)
            {
                _bullets[i].Update();
                if (_bullets[i].Position.Y < -200)
                {
                    _bullets.RemoveAt(i);
                    i--;
                }
            }

            _previousState = state;

            Shoot(gameTime);

            for (int i = 0; i < _bullets.Count; i++)
            {
                _bullets[i].Update();
                if (_bullets[i].Position.Y < -200)
                {
                    _bullets.RemoveAt(i);
                    i--;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle player = new Rectangle((int)_position.X, (int)_position.Y, 150, 200);
            spriteBatch.Draw(_texture, player, Color.White);

            foreach (var bullet in _bullets)
                bullet.Draw(spriteBatch);

            foreach (var wall in _walls)
                wall.Draw(spriteBatch);
        }

        private void Shoot(GameTime gameTime)
        {
            _playerTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (_mouseState.LeftButton == ButtonState.Pressed && _playerTimer >= _interval)
            {
                Vector2 bulletPosition = new Vector2(_position.X + 55, _position.Y - 200);
                Bullet ammo = new Bullet(_graphicsDevice, _content, bulletPosition);
                _bullets.Add(ammo);
                _playerTimer = 0;
            }
        }

        public Rectangle Bounds => new Rectangle((int)_position.X, (int)_position.Y, 150, 200);

        public void TakeDamage(int amount) => _health -= amount;
        public bool IsDead() => _health <= 0;

        private void PlaceWall()
        {
            // On limite à 3 murs max
            if (_walls.Count >= 5) return;

            Vector2 wallPos = new Vector2(_position.X, _position.Y - 60);
            _walls.Add(new Wall(_content, wallPos));
        }


    }
}
