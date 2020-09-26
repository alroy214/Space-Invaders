using System;

namespace C20_Ex03_Lior_204326607_Eitan_316486497
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using Invaders game = new Invaders();
            game.Run();
        }
    }
}
