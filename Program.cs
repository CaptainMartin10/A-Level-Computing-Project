using System;

namespace A_Level_Computing_Project
{
    public static class Program
    {
        [STAThread]
        static void Main()
       {
            using (var game = new Game1())
                game.Run();
        }
    }
}
