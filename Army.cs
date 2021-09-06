using System;
using System.Collections.Generic;
using System.Text;

namespace A_Level_Computing_Project
{
    public class Army
    {
        public int X, Y, Infantry, Archers, Cavalry;
        public Country OwnedBy;

        public Army(int x, int y, int i, int a, int c, Country o)
        {
            X = x;
            Y = y;
            Infantry = i;
            Archers = a;
            Cavalry = c;
            OwnedBy = o;
        }
    }
}
