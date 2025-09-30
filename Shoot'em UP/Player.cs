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

        
        private Texture2D _texture;

        private GraphicsDevice _graphicsDevice;
        private ContentManager _content;

        public Player(int health, string name, GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            _health = health;
            _name = name;
            _graphicsDevice = graphicsDevice;
            _content = contentManager;
            _texture = _content.Load<Texture2D>("MainCara");

        }
        public void Update()
        {
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Right))
                _position.X += 7;
            if (state.IsKeyDown(Keys.Left))
                _position.X -= 7;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle player = new Rectangle((int)_position.X, (int)_position.Y, 150, 200);
            spriteBatch.Begin();
            spriteBatch.Draw(_texture, player, Color.White);
            spriteBatch.End();
        }
        
    }
}
