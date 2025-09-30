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
    internal class Ennemi
    {
        Random _random = new Random();
        Vector2 _position;

        private int _health;
        private string _name;

        private Texture2D _texture;

        private GraphicsDevice _graphicsDevice;
        private ContentManager _content;

        private double _ennemyTimer;
        private double _interval = 1.0;


        public Ennemi(int health, string name, GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            _health = health;
            _name = name;
            _graphicsDevice = graphicsDevice;
            _content = contentManager;

            _position = new Vector2(
                _random.Next(80, 900),
                -10
                );
            _texture = _content.Load<Texture2D>("Ennemie");
        }
        public void Update(GameTime gameTime)
        {
            _ennemyTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (_ennemyTimer >= _interval)
            {
                _position.Y += 5;

                _ennemyTimer = 0;
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        { 
            Rectangle ennemi = new Rectangle((int)_position.X, (int)_position.Y, 150, 200);
         
            spriteBatch.Draw(_texture, ennemi, Color.White);
            
        }
      
    }
}
