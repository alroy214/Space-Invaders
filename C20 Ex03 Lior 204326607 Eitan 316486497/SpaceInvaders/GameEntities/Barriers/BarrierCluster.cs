using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;

namespace C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities
{
    public class BarrierCluster : GameComponent
    {
        private const int k_NumberOfBarriers = 4;
        private readonly Barrier[] r_Barriers;

        public BarrierCluster(GameScreen i_GameScreen, int i_NumOfBarriers = k_NumberOfBarriers) : base(i_GameScreen.Game)
        {
            r_Barriers = new Barrier[i_NumOfBarriers];
            initBarriers(i_GameScreen);
        }

        private void initBarriers(GameScreen i_GameScreen)
        {
            for (int i = 0; i < r_Barriers.Length; i++)
            {
                r_Barriers[i] = new Barrier(i_GameScreen, i, r_Barriers.Length);
            }
        }
    }
}
