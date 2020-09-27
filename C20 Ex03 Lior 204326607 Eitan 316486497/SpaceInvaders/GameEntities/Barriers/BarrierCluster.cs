using Microsoft.Xna.Framework;

namespace C20_Ex03_Lior_204326607_Eitan_316486497.GameEntities
{
    public class BarrierCluster
    {
        private const int k_NumberOfBarriers = 4;
        private readonly Barrier[] r_Barriers;

        public BarrierCluster(Game i_Game, int i_NumOfBarriers = k_NumberOfBarriers)
        {
            r_Barriers = new Barrier[i_NumOfBarriers];
            initBarriers(i_Game);
        }

        private void initBarriers(Game i_Game)
        {
            for (int i = 0; i < r_Barriers.Length; i++)
            {
                r_Barriers[i] = new Barrier(i_Game, i, r_Barriers.Length);
            }
        }
    }
}
