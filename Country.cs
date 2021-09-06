using System;
using System.Collections.Generic;
using System.Text;

namespace A_Level_Computing_Project
{
    public class Country
    {
        public int Gold, Wood, Stone, Food, Metal;
        public bool IsAI;
        public string Name;

        public Country (bool AI, string n)
        {
            IsAI = AI;
            Name = n;
            Gold = 500;
            Wood = 500;
            Stone = 500;
            Food = 500;
            Metal = 500;
        }
    }
}
