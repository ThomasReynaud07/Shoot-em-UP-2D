using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Shoot_em_UP
{
    internal class Wall
    {
        private Vector2 _position;
        private Texture2D _texture;
        private const int WIDTH = 200;
        private const int HEIGHT = 60;

        public Rectangle Bounds => new Rectangle((int)_position.X, (int)_position.Y, WIDTH, HEIGHT);

        public Wall(ContentManager content, Vector2 position)
        {
            // Texture pour le mur
            _texture = content.Load<Texture2D>("Wall");
            _position = position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Bounds, Color.White);
        }
    }
}
