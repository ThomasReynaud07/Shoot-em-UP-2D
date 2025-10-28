using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Shoot_em_UP
{
    internal class Ennemi
    {
        private Vector2 _position;
        private int _health;
        private int _maxHealth;
        private Texture2D _texture;
        private Texture2D _pixel;
        private GraphicsDevice _graphics;
        private ContentManager _content;
        private static Random _random = new Random();

        private const int WIDTH = 150;
        private const int HEIGHT = 150;

        public Ennemi(int health, string name, GraphicsDevice graphics, ContentManager content)
        {
            _graphics = graphics;
            _content = content;
            _health = health;
            _maxHealth = health;

            _position = new Vector2(_random.Next(0, 1780 - WIDTH), -HEIGHT);
            _texture = _content.Load<Texture2D>("Ennemie");

            // texture blanche 1x1 pour les barres
            _pixel = new Texture2D(_graphics, 1, 1);
            _pixel.SetData(new[] { Color.White });
        }

        public void Update(GameTime gameTime)
        {
            _position.Y += 3; // vitesse ennemis
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Dessin de l'ennemi
            Rectangle rect = new Rectangle((int)_position.X, (int)_position.Y, WIDTH, HEIGHT);
            spriteBatch.Draw(_texture, rect, Color.White);

            // Barre de vie 
            int barWidth = WIDTH;
            int barHeight = 10;

            float healthPercent = (float)_health / _maxHealth;
            int greenWidth = (int)(barWidth * healthPercent);
            int redWidth = barWidth - greenWidth;

            // Position de base de la barre
            int barX = (int)_position.X;
            int barY = (int)_position.Y - 15;

            // Partie verte = vie restante
            if (greenWidth > 0)
                spriteBatch.Draw(_pixel, new Rectangle(barX, barY, greenWidth, barHeight), Color.Green);

            // Partie rouge = vie perdue
            if (redWidth > 0)
                spriteBatch.Draw(_pixel, new Rectangle(barX + greenWidth, barY, redWidth, barHeight), Color.Red);
        }

        public Rectangle Bounds => new Rectangle((int)_position.X, (int)_position.Y, WIDTH, HEIGHT);

        public void TakeDamage(int amount)
        {
            _health -= amount;
            if (_health < 0) _health = 0;
        }

        public bool IsDead() => _health <= 0;

        public bool IsOutOfScreen(int screenHeight) => _position.Y > screenHeight;
    }
}
