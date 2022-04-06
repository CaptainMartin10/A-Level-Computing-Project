using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace A_Level_Computing_Project
{
    public class Army
    {
        public int X, Y;

        public bool ContainsMousePointer(Point MousePoint)
        {
            //defines bounds
            Rectangle CircleBounds1 = new Rectangle((X * 27) + 15, (Y * 36) + 4 + ((X % 2) * 18), 6, 28);
            Rectangle CircleBounds2 = new Rectangle((X * 27) + 13, (Y * 36) + 5 + ((X % 2) * 18), 10, 26);
            Rectangle CircleBounds3 = new Rectangle((X * 27) + 11, (Y * 36) + 6 + ((X % 2) * 18), 14, 24);
            Rectangle CircleBounds4 = new Rectangle((X * 27) + 9, (Y * 36) + 7 + ((X % 2) * 18), 18, 22);
            Rectangle CircleBounds5 = new Rectangle((X * 27) + 8, (Y * 36) + 8 + ((X % 2) * 18), 20, 20);
            Rectangle CircleBounds6 = new Rectangle((X * 27) + 7, (Y * 36) + 9 + ((X % 2) * 18), 22, 18);
            Rectangle CircleBounds7 = new Rectangle((X * 27) + 6, (Y * 36) + 11 + ((X % 2) * 18), 24, 14);
            Rectangle CircleBounds8 = new Rectangle((X * 27) + 5, (Y * 36) + 13 + ((X % 2) * 18), 26, 10);
            Rectangle CircleBounds9 = new Rectangle((X * 27) + 4, (Y * 36) + 15 + ((X % 2) * 18), 28, 6);

            //checks each bound
            if (CircleBounds1.Contains(MousePoint) || CircleBounds2.Contains(MousePoint) || CircleBounds3.Contains(MousePoint) || CircleBounds4.Contains(MousePoint) || CircleBounds5.Contains(MousePoint) || CircleBounds6.Contains(MousePoint) || CircleBounds7.Contains(MousePoint) || CircleBounds8.Contains(MousePoint) || CircleBounds9.Contains(MousePoint))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class RealArmy : Army
    {
        public int Infantry, Cavalry, Archers, SiegeProgress;
        public string OwnedBy;
        public bool Moved, Retreating, Sieging;
        public PhantomArmy[] MoveSelection = new PhantomArmy[6];

        public void Move(Province[,] MapArray, Country[] Countries, Dictionary<string, int> TerrainCosts, int[] TempPArray, Dictionary<string, int> CountryIndexes)
        {
            PhantomArmy p = new PhantomArmy(TempPArray[0], TempPArray[1]);
            if (!Moved)
            {
                if (!Retreating)
                {
                    //checks for shallow sea terrain for extra wood cost, then pays for and moves army
                    if (MapArray[p.X, p.Y].Terrain == "Shallow Sea")
                    {
                        if (Countries[CountryIndexes[OwnedBy]].CanAfford((Infantry + Archers + Cavalry) / 10, (Infantry + Archers + Cavalry) / 10, 0, ((Infantry * TerrainCosts[MapArray[p.X, p.Y].Terrain]) + (Archers * TerrainCosts[MapArray[p.X, p.Y].Terrain]) + (Cavalry * TerrainCosts[MapArray[p.X, p.Y].Terrain] * 2)) / 10, 0))
                        {
                            Countries[CountryIndexes[OwnedBy]].Pay((Infantry + Archers + Cavalry) / 10, (Infantry + Archers + Cavalry) / 10, 0, ((Infantry * TerrainCosts[MapArray[p.X, p.Y].Terrain]) + (Archers * TerrainCosts[MapArray[p.X, p.Y].Terrain]) + (Cavalry * TerrainCosts[MapArray[p.X, p.Y].Terrain] * 2)) / 10, 0);

                            MapArray[p.X, p.Y].ArmyInside = this;
                            int TempX = X;
                            int TempY = Y;
                            X = p.X;
                            Y = p.Y;
                            MapArray[TempX, TempY].ArmyInside = null;
                            Moved = true;
                        }
                    }
                    else
                    {
                        if (Countries[CountryIndexes[OwnedBy]].CanAfford((Infantry + Archers + Cavalry) / 10, (Infantry + Archers + Cavalry) / 10, 0, ((Infantry * TerrainCosts[MapArray[p.X, p.Y].Terrain]) + (Archers * TerrainCosts[MapArray[p.X, p.Y].Terrain]) + (Cavalry * TerrainCosts[MapArray[p.X, p.Y].Terrain] * 2)) / 10, 0))
                        {
                            Countries[CountryIndexes[OwnedBy]].Pay((Infantry + Archers + Cavalry) / 10, 0, 0, ((Infantry * TerrainCosts[MapArray[p.X, p.Y].Terrain]) + (Archers * TerrainCosts[MapArray[p.X, p.Y].Terrain]) + (Cavalry * TerrainCosts[MapArray[p.X, p.Y].Terrain] * 2)) / 10, 0);

                            MapArray[p.X, p.Y].ArmyInside = this;
                            int TempX = X;
                            int TempY = Y;
                            X = p.X;
                            Y = p.Y;
                            MapArray[TempX, TempY].ArmyInside = null;
                            Moved = true;
                        }
                    }
                }

                //moves army without cost if not retreating
                else
                {
                    MapArray[p.X, p.Y].ArmyInside = this;
                    int TempX = X;
                    int TempY = Y;
                    X = p.X;
                    Y = p.Y;
                    MapArray[TempX, TempY].ArmyInside = null;
                    Moved = true;
                }
            }
        }

        public int[] PickMoveLocation(int SelectedX, int SelectedY, Point MousePoint, Province[,] MapArray)
        {
            //generates possible move locations
            int[] MoveLocation = new int[2];
            for (int i = 0; i < 6; i++)
            {
                MoveSelection[i] = new PhantomArmy(MapArray[SelectedX, SelectedY].AdjacentTo[i, 0], MapArray[SelectedX, SelectedY].AdjacentTo[i, 1]);
            }

            //checks each location for click
            foreach (PhantomArmy p in MoveSelection)
            {
                if (p.X >= 0 && p.X <= 23 && p.Y >= 0 && p.Y <= 17 && p.ContainsMousePointer(MousePoint) && MapArray[p.X, p.Y].Terrain != "Deep Ocean")
                {
                    MoveLocation[0] = p.X;
                    MoveLocation[1] = p.Y;
                }
            }
            return MoveLocation;
        }

        public int GetArmyScore()
        {
            int Score = 0;
            Score += (((Infantry * 2) + (Archers * 3) + (Cavalry * 4)) / 2);
            return Score;
        }

        public void Attack(RealArmy Defender, Province[,] MapArray, Country[] Countries, Dictionary<string, int> TerrainCosts, Dictionary<string, int> CountryIndexes)
        {
            int AttackerScore = GetArmyScore();
            int DefenderScore = Defender.GetArmyScore();
            bool Won = false;

            //performs the battle
            if (AttackerScore > DefenderScore)
            {
                int InfDeathTotal = Infantry - Defender.Infantry;
                int ArcDeathTotal = Archers - Defender.Archers;
                int CavDeathTotal = Cavalry - Defender.Cavalry;

                Infantry -= InfDeathTotal * TerrainCosts[MapArray[Defender.X, Defender.Y].Terrain];
                Archers -= ArcDeathTotal * TerrainCosts[MapArray[Defender.X, Defender.Y].Terrain];
                Cavalry -= CavDeathTotal * TerrainCosts[MapArray[Defender.X, Defender.Y].Terrain];

                Defender.Infantry -= InfDeathTotal;
                Defender.Archers -= ArcDeathTotal;
                Defender.Cavalry -= CavDeathTotal;

                Won = true;
            }
            else if (AttackerScore <= DefenderScore)
            {
                int InfDeathTotal = Defender.Infantry - Infantry;
                int ArcDeathTotal = Defender.Archers - Archers;
                int CavDeathTotal = Defender.Cavalry - Cavalry;

                Infantry -= InfDeathTotal * TerrainCosts[MapArray[Defender.X, Defender.Y].Terrain];
                Archers -= ArcDeathTotal * TerrainCosts[MapArray[Defender.X, Defender.Y].Terrain];
                Cavalry -= CavDeathTotal * TerrainCosts[MapArray[Defender.X, Defender.Y].Terrain];

                Defender.Infantry -= InfDeathTotal;
                Defender.Archers -= ArcDeathTotal;
                Defender.Cavalry -= CavDeathTotal;

                Won = false;
            }

            //makes sure no troop numbers are less than 0
            if (Defender.Infantry < 0)
            {
                Defender.Infantry = 0;
            }
            if (Defender.Archers < 0)
            {
                Defender.Archers = 0;
            }
            if (Defender.Cavalry < 0)
            {
                Defender.Cavalry = 0;
            }
            if (Infantry < 0)
            {
                Infantry = 0;
            }
            if (Archers < 0)
            {
                Archers = 0;
            }
            if (Cavalry < 0)
            {
                Cavalry = 0;
            }

            //moves defending army out of tile if won 
            if (Won)
            {
                int MoveLocationIndex = 0;
                int[] MoveLocation = new int[2];

                for (int i = 0; i < 6; i++)
                {
                    if (X == MapArray[Defender.X, Defender.Y].AdjacentTo[i, 0] && Y == MapArray[Defender.X, Defender.Y].AdjacentTo[i, 1])
                    {
                        MoveLocationIndex = i;
                    }
                }

                MoveLocationIndex -= 3;
                if (MoveLocationIndex < 0)
                {
                    MoveLocationIndex += 6;
                }

                MoveLocation[0] = MapArray[Defender.X, Defender.Y].AdjacentTo[MoveLocationIndex, 0];
                MoveLocation[1] = MapArray[Defender.X, Defender.Y].AdjacentTo[MoveLocationIndex, 1];

                Moved = true;
                Defender.Retreating = true;
                Defender.Sieging = false;
                Defender.Moved = false;
                Defender.Move(MapArray, Countries, TerrainCosts, MoveLocation, CountryIndexes);
            }

            //otherwise makes attacker retreat
            else
            {
                Moved = true;
                Retreating = true;
            }
        }

        public void Retreat(Province[,] MapArray, Country[] Countries, Dictionary<string, int> CountryIndexes, Dictionary<string, int> TerrainCosts)
        {
            //finds direction to move
            int DX = Countries[CountryIndexes[OwnedBy]].CapitalX;
            int DY = Countries[CountryIndexes[OwnedBy]].CapitalY;
            int[] BestDArray = MapArray[X, Y].FindBestDirection(MapArray[DX, DY], MapArray);

            //moves army
            if (!(BestDArray[0] == 0 && BestDArray[1] == 0) && !(BestDArray[0] == X && BestDArray[1] == Y))
            {
                Move(MapArray, Countries, TerrainCosts, BestDArray, CountryIndexes);
            }

            //stops retreating if at destination
            if (DX == X && DY == Y)
            {
                Retreating = false;
            }
        }
    }

    public class StandingArmy : RealArmy
    {
        public StandingArmy(int x, int y, int i, int a, int c, string o, bool m, bool s, bool r, int sp)
        {
            X = x;
            Y = y;
            Infantry = i;
            Archers = a;
            Cavalry = c;
            OwnedBy = o;
            Moved = m;
            Sieging = s;
            Retreating = r;
            SiegeProgress = sp;
        }

        public void Siege(Province[,] MapArray, Country[] Countries, Dictionary<string, int> CountryIndexes, Dictionary<string, int> TerrainCosts)
        {
            int AttackScore;

            //calculates division factor
            int AttackDivider = TerrainCosts[MapArray[X, Y].Terrain] * 10;
            if (MapArray[X, Y].Structure == "Fort")
            {
                AttackDivider *= 2;
            }
            if (MapArray[X, Y].StructureLevel > 0)
            {
                AttackDivider *= MapArray[X, Y].StructureLevel;
            }

            AttackScore = GetArmyScore() / AttackDivider;

            SiegeProgress += AttackScore;

            //checks if siege has been completed
            if (SiegeProgress >= 100)
            {
                Sieging = false;
                SiegeProgress = 0;
                if (X == MapArray[X, Y].OwnedBy.CapitalX && Y == MapArray[X, Y].OwnedBy.CapitalY)
                {
                    MapArray[X, Y].OwnedBy.Collapse();
                }
                MapArray[X, Y].OwnedBy = Countries[CountryIndexes[OwnedBy]];
            }
        }
    }

    public class LevyArmy : RealArmy
    {
        public LevyArmy(int x, int y, int i, string o, bool m, bool r)
        {
            X = x;
            Y = y;
            Infantry = i;
            OwnedBy = o;
            Moved = m;
            Retreating = r;
        }
    }

    public class PhantomArmy : Army
    {
        public PhantomArmy(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}