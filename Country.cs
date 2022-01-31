using System;
using System.Collections.Generic;
using System.Text;

namespace A_Level_Computing_Project
{
    public class Country
    {
        public int Gold, Wood, Stone, Food, Metal, CapitalX, CapitalY;
        public bool IsAI;
        public string Name;
        public StandingArmy Standing;
        public LevyArmy Levy;

        public Country (bool AI, string n, int x, int y, int ax, int ay)
        {
            IsAI = AI;
            Name = n;
            Gold = 500;
            Wood = 500;
            Stone = 500;
            Food = 500;
            Metal = 500;
            CapitalX = x;
            CapitalY = y;
            Standing = new StandingArmy(ax, ay, Name);
        }

        public bool CanAfford(int ReqGold, int ReqWood, int ReqStone, int ReqFood, int ReqMetal)
        {
            if (Gold >= ReqGold && Wood >= ReqWood && Stone >= ReqStone && Food >= ReqFood && Metal >=ReqMetal)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Pay(int g, int w, int s, int f, int m)
        {
            Gold -= g;
            Wood -= w;
            Stone -= s;
            Food -= f;
            Metal -= m;
        }
    }
}
