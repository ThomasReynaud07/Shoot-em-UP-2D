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
        private Vector2 _position;      // Position actuelle de la balle
        private Texture2D _texture;     // Texture (sprite) de la balle
        private float _speed = 10f;     // Vitesse de déplacement verticale

        public Vector2 Position => _position; // Accès public à la position

        public Bullet(GraphicsDevice graphicsDevice, ContentManager content, Vector2 position)
        {
            _position = position;
            _texture = content.Load<Texture2D>("tung"); // Charge la texture de la balle
        }

        public void Update()
        {
            // Fait monter la balle vers le haut de l'écran
            _position.Y -= _speed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Dessine la balle à sa position actuelle
            Rectangle bulletRect = new Rectangle((int)_position.X, (int)_position.Y, 50, 100);
            spriteBatch.Draw(_texture, bulletRect, Color.White);
        }

        // Rectangle de collision (pour détecter les impacts avec les ennemis)
        public Rectangle Bounds
        {
            get
            {
                return new Rectangle((int)_position.X, (int)_position.Y, 100, 200);
            }
        }
    }
}
