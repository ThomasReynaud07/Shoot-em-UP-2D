using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Shoot_em_UP
{
    internal class EnnemyManager
    {
        private List<Ennemi> _ennemiList;
        private GraphicsDevice _graphicsDevice;
        private ContentManager _content;
        private Player _player;

        private double _interval = 2;
        private double _ennemySpawn;

        public List<Ennemi> Ennemis => _ennemiList;

        public EnnemyManager(GraphicsDevice graphicsDevice, ContentManager content, Player player)
        {
            _ennemiList = new List<Ennemi>();
            _graphicsDevice = graphicsDevice;
            _content = content;
            _player = player;
        }

        public void SpawnEnnemy(GameTime gameTime)
        {
            _ennemySpawn += gameTime.ElapsedGameTime.TotalSeconds;

            if (_ennemySpawn >= _interval)
            {
                var newEnnemi = new Ennemi(2, "ennemi", _graphicsDevice, _content);

                bool overlap = false;
                foreach (var ennemi in _ennemiList)
                    if (newEnnemi.Bounds.Intersects(ennemi.Bounds))
                        overlap = true;

                if (!overlap)
                    _ennemiList.Add(newEnnemi);

                _ennemySpawn = 0;
            }
        }

        public int Update(GameTime gameTime, List<Bullet> bullets)
        {
            int kills = 0;

            for (int i = _ennemiList.Count - 1; i >= 0; i--)
            {
                var ennemi = _ennemiList[i];
                ennemi.Update(gameTime);

                bool dead = false;

                // Collision balle / ennemi
                for (int j = bullets.Count - 1; j >= 0; j--)
                {
                    if (ennemi.Bounds.Intersects(bullets[j].Bounds))
                    {
                        bullets.RemoveAt(j);
                        ennemi.TakeDamage(1);

                        if (ennemi.IsDead())
                        {
                            dead = true;
                            kills++;
                        }
                        break; // on arrête de checker les balles
                    }
                }

                // Collision mur / ennemi
                if (!dead)
                {
                    for (int w = _player.Walls.Count - 1; w >= 0; w--)
                    {
                        if (ennemi.Bounds.Intersects(_player.Walls[w].Bounds))
                        {
                            _player.Walls.RemoveAt(w);
                            dead = true;
                            break; // on arrête de checker les murs
                        }
                    }
                }

                if (dead)
                    _ennemiList.RemoveAt(i);
            }

            return kills;
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var ennemi in _ennemiList)
                ennemi.Draw(spriteBatch);
        }
    }
}
