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

        public void NextTurn(Province[,] MapArray, Country[] Countries, Dictionary<string, int> CountryIndexes, int Player, Dictionary<string, int> TerrainCosts, Dictionary<string, int> MineProduction, Dictionary<string, int> FarmProduction, Dictionary<string, int> ForesterProduction)
        {
            Standing.Moved = false;
            if (Standing.Retreating)
            {
                Standing.Retreat(MapArray, Countries, CountryIndexes);
            }

            if (Standing.Sieging)
            {
                Standing.Siege(MapArray, Countries, CountryIndexes, TerrainCosts);
            }

            if (Levy != null)
            {
                Levy.Moved = false;
                if (Levy.Retreating)
                {
                    Levy.Retreat(MapArray, Countries, CountryIndexes);
                }
            }
            else
            {
                Gold += 50;
                Metal += 50;
                Stone += 50;
                Wood += 50;
                Food += 50;
                if (!Standing.Retreating && !Standing.Sieging)
                {
                    Standing.Infantry += 50;
                }
            }

            if (IsAI)
            {
                int[] MoveLocation = MapArray[Standing.X, Standing.Y].FindBestDirection(MapArray[Countries[Player].Standing.X, Countries[Player].Standing.Y], MapArray);
                if (!(MoveLocation[0] == 0 && MoveLocation[1] == 0) && !(MoveLocation[0] == Standing.X && MoveLocation[1] == Standing.Y) && MapArray[MoveLocation[0], MoveLocation[1]].OwnedBy == this)
                {
                    Standing.Move(MapArray, Countries, TerrainCosts, MoveLocation, CountryIndexes);
                }
            }

            foreach (Province P in MapArray)
            {
                if (P.OwnedBy == this && Levy == null)
                {
                    if (P.Structure == "Settlement")
                    {
                        Gold += 100 * P.StructureLevel;
                    }
                    else if (P.Structure == "Mine")
                    {
                        Stone += MineProduction[P.Terrain] * P.StructureLevel;
                        Metal += MineProduction[P.Terrain] * P.StructureLevel;
                    }
                    else if (P.Structure == "Farm")
                    {
                        Food += FarmProduction[P.Terrain] * P.StructureLevel;
                    }
                    else if (P.Structure == "Forester")
                    {
                        Wood += ForesterProduction[P.Terrain] * P.StructureLevel;
                    }
                    else if (P.Structure == "Fort")
                    {
                        if (!Standing.Retreating && !Standing.Sieging)
                        {
                            Standing.Infantry += 50 * P.StructureLevel;
                            Standing.Archers += 25 * P.StructureLevel;
                            Standing.Cavalry += 25 * P.StructureLevel;
                        }
                    }
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
