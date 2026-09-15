using System;
using static assignment1.Program;
using static System.Net.Mime.MediaTypeNames;

namespace assignment1
{
    class ConsoleColorsClass
    {
        enum Colors
        {
            Red,
            Orange,
            Green,
            Blue,
            Black
        };
    }
        internal class Program
    {

        public enum STATE
        {
            End_Game,
            Dialogue,
            Setup_Combat,
            Enemy_Attack,
            Player_Choose_Combat,
            Player_Choose_Defend,
        }
        static void Main(string[] args)
        {
            Game game = new Game();
            
            game.setup();

            while (game.running) 
            {
                game.tick();
            }
            
        }
    }
}
