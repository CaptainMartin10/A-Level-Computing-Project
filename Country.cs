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

        public Country(bool AI, string n, int x, int y, int g, int w, int s, int f, int m, string c)
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
            if (Gold >= ReqGold && Wood >= ReqWood && Stone >= ReqStone && Food >= ReqFood && Metal >= ReqMetal)
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

        public bool OwnsLand(Province[,] MapArray)
        {
            foreach (Province p in MapArray)
            {
                if (p.OwnedBy == this && CapitalX != p.X && CapitalY != p.Y)
                {
                    return true;
                }
            }

            return false;
        }

        public void Collapse()
        {
            Standing.OwnedBy = "Unowned";
            if (Levy != null)
            {
                Levy.OwnedBy = "Unowned";
            }
            Name = "Unowned";
        }

        public void NextTurn(Province[,] MapArray, Country[] Countries, Dictionary<string, int> CountryIndexes, int Player, Dictionary<string, int> TerrainCosts)
        {
            Standing.Moved = false;
            if (Standing.Retreating)
            {
                Standing.Retreat(MapArray, Countries, CountryIndexes);
            }
            else if (Standing.Sieging)
            {
                Standing.Siege(MapArray, Countries, CountryIndexes);
            }

            if (Levy != null)
            {
                Levy.Moved = false;
                if (Levy.Retreating)
                {
                    Levy.Retreat(MapArray, Countries, CountryIndexes);
                }
            }

            if (IsAI)
            {
                int[] MoveLocation = MapArray[Standing.X, Standing.Y].FindBestDirection(MapArray[Countries[Player].Standing.X, Countries[Player].Standing.Y], MapArray);
                if (!(MoveLocation[0] == 0 && MoveLocation[1] == 0) && MapArray[MoveLocation[0], MoveLocation[1]].OwnedBy == this)
                {
                    Standing.Move(MapArray, Countries, TerrainCosts, MoveLocation, CountryIndexes);
                }
            }
        }
    }
}
