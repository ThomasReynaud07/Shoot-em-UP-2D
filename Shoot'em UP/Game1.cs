using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Shoot_em_UP
{
    public class Game1 : Game
    {
        public static Game1 Instance; // Permet d'accéder à l'instance du jeu depuis d'autres classes

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Player Player;
        EnnemyManager EnnemiManager;
        Texture2D background;
        private SpriteFont gameFont;

        private bool isGameOver = false;
        private int score = 0;

        public Game1()
        {
            Instance = this;
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            // Taille de la fenêtre
            _graphics.PreferredBackBufferWidth = 1780;
            _graphics.PreferredBackBufferHeight = 980;
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            // Création du joueur et du gestionnaire d’ennemis
            Player = new Player(3, "Thomas", GraphicsDevice, Content);
            EnnemiManager = new EnnemyManager(GraphicsDevice, Content, Player);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            background = Content.Load<Texture2D>("Background");
            gameFont = Content.Load<SpriteFont>("GameFont");
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Escape)) Exit();

            if (!isGameOver)
            {
                Player.Update(gameTime);
                EnnemiManager.SpawnEnnemy(gameTime);

                // Mise à jour des ennemis + comptage des kills
                int kills = EnnemiManager.Update(gameTime, Player.Bullets);
                if (kills > 0) score += kills * 10;

                // Collisions joueur / ennemis
                for (int i = EnnemiManager.Ennemis.Count - 1; i >= 0; i--)
                {
                    var ennemi = EnnemiManager.Ennemis[i];

                    if (ennemi.Bounds.Intersects(Player.Bounds))
                    {
                        Player.TakeDamage(1);
                        EnnemiManager.Ennemis.RemoveAt(i);

                        if (Player.IsDead())
                            isGameOver = true;
                    }
                    else if (ennemi.IsOutOfScreen(GraphicsDevice.Viewport.Height))
                    {
                        isGameOver = true;
                    }
                }
            }
            else if (keyState.IsKeyDown(Keys.R))
            {
                // Redémarre la partie après un game over
                RestartGame();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();

            // Fond
            _spriteBatch.Draw(background, new Rectangle(0, 0, 1780, 980), Color.White);

            if (!isGameOver)
            {
                Player.Draw(_spriteBatch);
                EnnemiManager.Draw(_spriteBatch);

                // Ecriture pour le score et la vie
                _spriteBatch.DrawString(gameFont, $"Vies : {Player.Health}", new Vector2(20, 20), Color.White);
                _spriteBatch.DrawString(gameFont, $"Score : {score}", new Vector2(20, 60), Color.Yellow);
            }
            else
            {
                // Écran de fin
                string text = "GAME OVER\nAppuie sur R pour recommencer";
                Vector2 size = gameFont.MeasureString(text);
                Vector2 pos = new Vector2((1780 - size.X) / 2, (980 - size.Y) / 2);
                _spriteBatch.DrawString(gameFont, text, pos, Color.Red);
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }

        private void RestartGame()
        {
            // Réinitialise la partie
            Player = new Player(3, "Thomas", GraphicsDevice, Content);
            EnnemiManager = new EnnemyManager(GraphicsDevice, Content, Player);
            score = 0;
            isGameOver = false;
        }

        public void AddScore(int value) => score += value; // Méthode pour ajouter du score
    }
}
