using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Shoot_em_UP
{
    internal class Player
    {
        // Position de départ du joueur
        private Vector2 _position = new Vector2(890, 800);

        private int _health;
        private string _name;

        private List<Bullet> _bullets = new List<Bullet>(); // Liste des balles tirées
        private Texture2D _texture; // Sprite du joueur

        private MouseState _mouseState;
        private GraphicsDevice _graphicsDevice;
        private ContentManager _content;

        // Délai entre deux tirs
        private double _playerTimer;
        private double _interval = 0.4; 

        public List<Bullet> Bullets => _bullets;
        public int Health => _health;

        private List<Wall> _walls = new List<Wall>(); // Liste des murs placés
        public List<Wall> Walls => _walls;

        public Player(int health, string name, GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            _health = health;
            _name = name;
            _graphicsDevice = graphicsDevice;
            _content = contentManager;

            // Chargement de la texture du joueur
            _texture = _content.Load<Texture2D>("MainCara");
        }

        private KeyboardState _previousState;

        public void Update(GameTime gameTime)
        {
            _mouseState = Mouse.GetState();
            KeyboardState state = Keyboard.GetState();

            // Déplacement gauche / droite
            if (state.IsKeyDown(Keys.D)) _position.X += 10;
            if (state.IsKeyDown(Keys.A)) _position.X -= 10;

            // Place un mur quand on appuie sur E (pas en continu)
            if (state.IsKeyDown(Keys.E) && !_previousState.IsKeyDown(Keys.E))
                PlaceWall();

            // Gestion des tirs
            Shoot(gameTime);

            // Mise à jour des balles + nettoyage si hors écran
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
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Affiche le joueur
            Rectangle player = new Rectangle((int)_position.X, (int)_position.Y, 150, 200);
            spriteBatch.Draw(_texture, player, Color.White);

            // Affiche les balles
            foreach (var bullet in _bullets)
                bullet.Draw(spriteBatch);

            // Affiche les murs
            foreach (var wall in _walls)
                wall.Draw(spriteBatch);
        }

        private void Shoot(GameTime gameTime)
        {
            _playerTimer += gameTime.ElapsedGameTime.TotalSeconds;

            // Tire quand le clic gauche est pressé et que le délai est écoulé
            if (_mouseState.LeftButton == ButtonState.Pressed && _playerTimer >= _interval)
            {
                Vector2 bulletPosition = new Vector2(_position.X + 55, _position.Y - 200);
                Bullet ammo = new Bullet(_graphicsDevice, _content, bulletPosition);
                _bullets.Add(ammo);
                _playerTimer = 0; // reset du timer
            }
        }

        // Zone de collision du joueur
        public Rectangle Bounds => new Rectangle((int)_position.X, (int)_position.Y, 150, 200);

        // Gère les dégâts et la mort
        public void TakeDamage(int amount) => _health -= amount;
        public bool IsDead() => _health <= 0;

        private void PlaceWall()
        {
            // Limite à 5 murs max
            if (_walls.Count >= 5) return;

            Vector2 wallPos = new Vector2(_position.X, _position.Y - 60);
            _walls.Add(new Wall(_content, wallPos));
        }
    }
}
