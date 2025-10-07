using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Shoot_em_UP
{
    internal class Player
    {
        Vector2 _position = new Vector2(890, 800);
        private int _health;
        private string _name;

        List<Bullet> bullets = new List<Bullet>();

        private Texture2D _texture;

        private MouseState _mouseState;

        private GraphicsDevice _graphicsDevice;
        private ContentManager _content;

        private Bullet ammo;
        private double _playerTimer;
        private double _interval = 0.5;

        public Player(int health, string name, GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            _health = health;
            _name = name;
            _graphicsDevice = graphicsDevice;
            _content = contentManager;
            _texture = _content.Load<Texture2D>("MainCara");

            ammo = new Bullet(graphicsDevice, contentManager, _position);
        }
        public void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.D))
                _position.X += 7;
            if (state.IsKeyDown(Keys.A))
                _position.X -= 7;

            Shoot(gameTime);
            _playerTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if(_playerTimer >= _interval)
            {
                ammo.Update();
            }
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle player = new Rectangle((int)_position.X, (int)_position.Y, 150, 200);
            spriteBatch.Draw(_texture, player, Color.White);

            foreach (var bullet in bullets)
                bullet.Draw(spriteBatch);

        }
        public void Shoot(GameTime gameTime)
        {
            _playerTimer += gameTime.ElapsedGameTime.TotalSeconds;
            _mouseState = Mouse.GetState();
            if (_mouseState.LeftButton == ButtonState.Pressed && _playerTimer >= _interval)
            {
                Vector2 bulletPosition = new Vector2(_position.X+55, _position.Y-200);
                ammo = new Bullet(_graphicsDevice, _content, bulletPosition);
                bullets.Add(ammo);
            }


        }
    }
}
