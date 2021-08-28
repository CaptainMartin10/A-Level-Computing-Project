using System;
using System.Collections.Generic;
using System.Text;

namespace A_Level_Computing_Project
{
    public class Province
    {
        public int X, Y, StructureLevel, StructureGarrison, StructureProduction;
        public string Terrain, Structure;

        public Province(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
