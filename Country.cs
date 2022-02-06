using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace A_Level_Computing_Project
{
    public class Country
    {
        public int Gold, Wood, Stone, Food, Metal, CapitalX, CapitalY, ID;
        public bool IsAI;
        public string Name, CountryCode;
        public StandingArmy Standing;
        public LevyArmy Levy;
        public Texture2D OwnedTileTexture, ArmyTexture;

        public Country (bool AI, string n, int x, int y, int g, int w, int s, int f, int m, string c)
        {
            IsAI = AI;
            Name = n;
            Gold = g;
            Wood = w;
            Stone = s;
            Food = f;
            Metal = m;
            CapitalX = x;
            CapitalY = y;
            CountryCode = c;
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
