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
    internal class Bullet
    {
        private Vector2 _position;
        private Texture2D _texture;
        private float _speed = 10f; 

        public Vector2 Position => _position; 

        public Bullet(GraphicsDevice graphicsDevice, ContentManager content, Vector2 position)
        {
            _position = position;
            _texture = content.Load<Texture2D>("tung"); 
        }

        public void Update()
        {
            
            _position.Y -= _speed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
            Rectangle bulletRect = new Rectangle((int)_position.X, (int)_position.Y, 100, 200);
            spriteBatch.Draw(_texture, bulletRect, Color.White);

        }
    }
}
