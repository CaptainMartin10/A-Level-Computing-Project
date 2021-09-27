using System;
using System.Collections.Generic;
using System.Text;

namespace A_Level_Computing_Project
{
    public class Army
    {
        public int X, Y, Infantry;
        public Country OwnedBy;

        public Army(int x, int y, int i, Country o)
        {
            X = x;
            Y = y;
            Infantry = i;
            OwnedBy = o;
        }
    }
}
