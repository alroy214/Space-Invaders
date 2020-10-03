using C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities.Ships;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;

namespace C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities
{
    public class BulletMagazine
    {
        protected readonly Bullet[] r_Bullets;

        protected BulletMagazine(int i_MaxCapacity)
        {
            r_Bullets = new Bullet[i_MaxCapacity];
        }

        public bool ShootBullet(Vector2 i_ShootPosition)
        {
            bool bulletShot = false;

            foreach (Bullet bullet in r_Bullets)
            {
                if (!bullet.Visible)
                {
                    i_ShootPosition.X -= (float)bullet.Texture.Width / 2;
                    bullet.Position = i_ShootPosition;
                    bullet.Visible = true;
                    bulletShot = true;
                    break;
                }
            }

            return bulletShot;
        }

        public Bullet[] Bullets
        {
            get
            {
                return r_Bullets;
            }
        }
    }
    public class PlayerBulletMagazine : BulletMagazine
    {
        public PlayerBulletMagazine(GameScreen i_GameScreen, int i_MaxCapacity, PlayerShip.ePlayer i_Player)
            : base(i_MaxCapacity)
        {
            initBullets(i_GameScreen, i_Player);
        }

        private void initBullets(GameScreen i_GameScreen, PlayerShip.ePlayer i_Player)
        {
            for (int i = 0; i < r_Bullets.Length; i++)
            {
                r_Bullets[i] = new PlayerBullet(i_GameScreen, i_Player);
            }
        }
    }

    public class EnemyBulletMagazine : BulletMagazine
    {
        public EnemyBulletMagazine(GameScreen i_GameScreen, int i_MaxCapacity)
            : base(i_MaxCapacity)
        {
            initBullets(i_GameScreen);
        }

        private void initBullets(GameScreen i_GameScreen)
        {
            for (int i = 0; i < r_Bullets.Length; i++)
            {
                r_Bullets[i] = new EnemyBullet(i_GameScreen);
            }
        }
    }
}