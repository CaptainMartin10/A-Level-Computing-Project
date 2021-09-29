using System;
using System.Collections.Generic;
using System.Text;

namespace A_Level_Computing_Project
{
    public class Army
    {
        public int X, Y, Infantry;
    }

    public class StandingArmy : Army
    {
        public int Cavalry, Archers;
        public StandingArmy(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public class LevyArmy : Army
    {
        public LevyArmy(int x, int y, int i)
        {
            X = x;
            Y = y;
            Infantry = i;
        }
    }
}
