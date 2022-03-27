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

        public void RaiseLevyArmy(Province[,] MapArray, Country[] Countries, int Player)
        {
            int LevyArmySize = 0;
            foreach (Province Hex in MapArray)
            {
                if (Hex.OwnedBy == Countries[Player])
                {
                    if (Hex.Structure == "Empty")
                    {
                        LevyArmySize += 50;
                    }
                    else if (Hex.Structure == "Settlement")
                    {
                        LevyArmySize += 200 * Hex.StructureLevel;
                    }
                    else if (Hex.Structure == "Farm" || Hex.Structure == "Forester" || Hex.Structure == "Mine")
                    {
                        LevyArmySize += 100 * Hex.StructureLevel;
                    }
                }
            }
            int ArmyX = 0;
            int ArmyY = 0;
            if (MapArray[Countries[Player].CapitalX, Countries[Player].CapitalY].ArmyInside == null)
            {
                ArmyX = Countries[Player].CapitalX;
                ArmyY = Countries[Player].CapitalY;
            }
            else
            {
                for (int i = 5; i >= 0; i--)
                {
                    if (MapArray[MapArray[Countries[Player].CapitalX, Countries[Player].CapitalY].AdjacentTo[i, 0], MapArray[Countries[Player].CapitalX, Countries[Player].CapitalY].AdjacentTo[i, 1]].ArmyInside == null)
                    {
                        ArmyX = MapArray[Countries[Player].CapitalX, Countries[Player].CapitalY].AdjacentTo[i, 0];
                        ArmyY = MapArray[Countries[Player].CapitalX, Countries[Player].CapitalY].AdjacentTo[i, 1];
                    }
                }
            }
            if (ArmyX != 0 || ArmyY != 0)
            {
                Countries[Player].Levy = new LevyArmy(ArmyX, ArmyY, LevyArmySize, Countries[Player].Name, true, false);
                MapArray[Countries[Player].Levy.X, Countries[Player].Levy.Y].ArmyInside = Countries[Player].Levy;
            }
        }
    }
}
