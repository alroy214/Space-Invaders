using C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders.Managers;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace C20_Ex03_Lior_204326607_Eitan_316486497.SpaceInvaders
{
    class MusicUtils
    {
        public const string k_SoundEffectRoot = @"C:/temp/XNA_Assets/Ex03/Sounds/";
        public const string k_PlayerShipShootSound = "SSGunShot";
        public const string k_EnemyShootSound = "EnemyGunShot";
        public const string k_EnemyKillSound = "EnemyKill";
        public const string k_MotherShipKillSound = "MotherShipKill";
        public const string k_BarrierHitSound = "BarrierHit";
        public const string k_GameOverSound = "GameOver";
        public const string k_LevelWinSound = "LevelWin";
        public const string k_LifeDieSound = "LifeDie";
        public const string k_MenuMoveSound = "MenuMove";
        public const string k_BackgroundMusicSound = "BGMusic";

        public static void LoadSoundEffects(SoundManager i_SoundManager, ContentManager i_Content)
        {
            i_SoundManager.AddSoundEffect(i_Content.Load<SoundEffect>($"{k_SoundEffectRoot}{k_PlayerShipShootSound}"), k_PlayerShipShootSound);
            i_SoundManager.AddSoundEffect(i_Content.Load<SoundEffect>($"{k_SoundEffectRoot}{k_EnemyShootSound}"), k_EnemyShootSound);
            i_SoundManager.AddSoundEffect(i_Content.Load<SoundEffect>($"{k_SoundEffectRoot}{k_EnemyKillSound}"), k_EnemyKillSound);
            i_SoundManager.AddSoundEffect(i_Content.Load<SoundEffect>($"{k_SoundEffectRoot}{k_MotherShipKillSound}"), k_MotherShipKillSound);
            i_SoundManager.AddSoundEffect(i_Content.Load<SoundEffect>($"{k_SoundEffectRoot}{k_BarrierHitSound}"), k_BarrierHitSound);
            i_SoundManager.AddSoundEffect(i_Content.Load<SoundEffect>($"{k_SoundEffectRoot}{k_GameOverSound}"), k_GameOverSound);
            i_SoundManager.AddSoundEffect(i_Content.Load<SoundEffect>($"{k_SoundEffectRoot}{k_LevelWinSound}"), k_LevelWinSound);
            i_SoundManager.AddSoundEffect(i_Content.Load<SoundEffect>($"{k_SoundEffectRoot}{k_LifeDieSound}"), k_LifeDieSound);
            i_SoundManager.AddSoundEffect(i_Content.Load<SoundEffect>($"{k_SoundEffectRoot}{k_MenuMoveSound}"), k_MenuMoveSound);
            i_SoundManager.SetBackgroundMusic(i_Content.Load<SoundEffect>($"{k_SoundEffectRoot}{k_BackgroundMusicSound}"));
            i_SoundManager.SetHoverSoundEffect(k_BackgroundMusicSound);
            i_SoundManager.ChangeBackgroundMusicVolumeLevel(-90);
            i_SoundManager.ChangeSoundEffectsVolumeLevel(-90);
            i_SoundManager.SoundToggle = true;
        }
    }
}
