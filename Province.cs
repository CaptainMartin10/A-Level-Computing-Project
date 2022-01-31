using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace A_Level_Computing_Project
{
    public class Province
    {
        public int X, Y, StructureLevel;
        public string Terrain, Structure;
        public Country OwnedBy;
        public RealArmy ArmyInside;
        public int[,] AdjacentTo = new int[6, 2];

        public Province(int x, int y, int sl, string s, Country o, string t)
        {
            X = x;
            Y = y;
            StructureLevel = sl;
            Structure = s;
            OwnedBy = o;
            Terrain = t;

            if (X % 2 == 0)
            {
                AdjacentTo[0, 0] = X;
                AdjacentTo[0, 1] = Y - 1;
                AdjacentTo[1, 0] = X + 1;
                AdjacentTo[1, 1] = Y - 1;
                AdjacentTo[2, 0] = X + 1;
                AdjacentTo[2, 1] = Y;
                AdjacentTo[3, 0] = X;
                AdjacentTo[3, 1] = Y + 1;
                AdjacentTo[4, 0] = X - 1;
                AdjacentTo[4, 1] = Y;
                AdjacentTo[5, 0] = X - 1;
                AdjacentTo[5, 1] = Y - 1;
            }
            else if (X % 2 == 1)
            {
                AdjacentTo[0, 0] = X;
                AdjacentTo[0, 1] = Y - 1;
                AdjacentTo[1, 0] = X + 1;
                AdjacentTo[1, 1] = Y;
                AdjacentTo[2, 0] = X + 1;
                AdjacentTo[2, 1] = Y + 1;
                AdjacentTo[3, 0] = X;
                AdjacentTo[3, 1] = Y + 1;
                AdjacentTo[4, 0] = X - 1;
                AdjacentTo[4, 1] = Y + 1;
                AdjacentTo[5, 0] = X - 1;
                AdjacentTo[5, 1] = Y;
            }
        }

        public bool ContainsMousePointer(Point mousePoint)
        {
            if (X % 2 == 0)
            {
                Rectangle HexBounds1 = new Rectangle((X * 27) + 9, (Y * 36), 18, 36);
                Rectangle HexBounds2 = new Rectangle((X * 27) + 8, (Y * 36) + 1, 20, 34);
                Rectangle HexBounds3 = new Rectangle((X * 27) + 7, (Y * 36) + 3, 22, 30);
                Rectangle HexBounds4 = new Rectangle((X * 27) + 6, (Y * 36) + 5, 24, 26);
                Rectangle HexBounds5 = new Rectangle((X * 27) + 5, (Y * 36) + 7, 26, 22);
                Rectangle HexBounds6 = new Rectangle((X * 27) + 4, (Y * 36) + 9, 28, 18);
                Rectangle HexBounds7 = new Rectangle((X * 27) + 3, (Y * 36) + 11, 30, 14);
                Rectangle HexBounds8 = new Rectangle((X * 27) + 2, (Y * 36) + 13, 32, 10);
                Rectangle HexBounds9 = new Rectangle((X * 27) + 1, (Y * 36) + 15, 34, 6);
                Rectangle HexBounds10 = new Rectangle((X * 27), (Y * 36) + 17, 36, 2);
                if (HexBounds1.Contains(mousePoint) || HexBounds2.Contains(mousePoint) || HexBounds3.Contains(mousePoint) || HexBounds4.Contains(mousePoint) || HexBounds5.Contains(mousePoint) || HexBounds6.Contains(mousePoint) || HexBounds7.Contains(mousePoint) || HexBounds8.Contains(mousePoint) || HexBounds9.Contains(mousePoint) || HexBounds10.Contains(mousePoint))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (X % 2 == 1)
            {
                Rectangle HexBounds1 = new Rectangle((X * 27) + 9, (Y * 36) + 18, 18, 36);
                Rectangle HexBounds2 = new Rectangle((X * 27) + 8, (Y * 36) + 19, 20, 34);
                Rectangle HexBounds3 = new Rectangle((X * 27) + 7, (Y * 36) + 21, 22, 30);
                Rectangle HexBounds4 = new Rectangle((X * 27) + 6, (Y * 36) + 23, 24, 26);
                Rectangle HexBounds5 = new Rectangle((X * 27) + 5, (Y * 36) + 25, 26, 22);
                Rectangle HexBounds6 = new Rectangle((X * 27) + 4, (Y * 36) + 27, 28, 18);
                Rectangle HexBounds7 = new Rectangle((X * 27) + 3, (Y * 36) + 29, 30, 14);
                Rectangle HexBounds8 = new Rectangle((X * 27) + 2, (Y * 36) + 31, 32, 10);
                Rectangle HexBounds9 = new Rectangle((X * 27) + 1, (Y * 36) + 33, 34, 6);
                Rectangle HexBounds10 = new Rectangle((X * 27), (Y * 36) + 35, 36, 2);
                if (HexBounds1.Contains(mousePoint) || HexBounds2.Contains(mousePoint) || HexBounds3.Contains(mousePoint) || HexBounds4.Contains(mousePoint) || HexBounds5.Contains(mousePoint) || HexBounds6.Contains(mousePoint) || HexBounds7.Contains(mousePoint) || HexBounds8.Contains(mousePoint) || HexBounds9.Contains(mousePoint) || HexBounds10.Contains(mousePoint))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public int[] FindBestDirection(Province Destination, Province[,] MapArray)
        {
            List<int> Options = new List<int>();
            if (Destination.X == X)
            {
                if (Destination.Y > Y)
                {
                    Options.Add(3);
                    Options.Add(4);
                    Options.Add(2);
                    Options.Add(5);
                    Options.Add(1);
                    Options.Add(0);
                }
                else if (Destination.Y < Y)
                {
                    Options.Add(0);
                    Options.Add(1);
                    Options.Add(5);
                    Options.Add(2);
                    Options.Add(4);
                    Options.Add(3);
                }
            }
            else if (Destination.X > X)
            {
                if (Destination.Y > Y)
                {
                    Options.Add(2);
                    Options.Add(3);
                    Options.Add(1);
                    Options.Add(4);
                    Options.Add(0);
                    Options.Add(5);
                }
                else if (Destination.Y < Y)
                {
                    Options.Add(1);
                    Options.Add(2);
                    Options.Add(0);
                    Options.Add(3);
                    Options.Add(5);
                    Options.Add(4);
                }
                else if (Destination.Y == Y)
                {
                    if (X % 2 == 0)
                    {
                        Options.Add(2);
                        Options.Add(3);
                        Options.Add(1);
                        Options.Add(4);
                        Options.Add(0);
                        Options.Add(5);
                    }
                    else if (X % 2 == 1)
                    {
                        Options.Add(1);
                        Options.Add(2);
                        Options.Add(0);
                        Options.Add(3);
                        Options.Add(5);
                        Options.Add(4);
                    }
                }
            }
            else if (Destination.X < X)
            {
                if (Destination.Y > Y)
                {
                    Options.Add(4);
                    Options.Add(5);
                    Options.Add(3);
                    Options.Add(0);
                    Options.Add(2);
                    Options.Add(1);
                }
                else if (Destination.Y < Y)
                {
                    Options.Add(5);
                    Options.Add(0);
                    Options.Add(4);
                    Options.Add(1);
                    Options.Add(3);
                    Options.Add(2);
                }
                else if (Destination.Y == Y)
                {
                    if (X % 2 == 0)
                    {
                        Options.Add(4);
                        Options.Add(5);
                        Options.Add(3);
                        Options.Add(0);
                        Options.Add(2);
                        Options.Add(1);
                    }
                    else if (X % 2 == 1)
                    {
                        Options.Add(5);
                        Options.Add(0);
                        Options.Add(4);
                        Options.Add(1);
                        Options.Add(3);
                        Options.Add(2);
                    }
                }
            }

            List<int> OptionsToRemove = new List<int>();
            for (int i = 0; i < 6; i++)
            {
                if (AdjacentTo[i, 0] >= 0 && AdjacentTo[i, 0] <= 23 && AdjacentTo[i, 1] >= 0 && AdjacentTo[i, 1] <= 17 && (MapArray[AdjacentTo[i, 0], AdjacentTo[i, 1]].ArmyInside != null || MapArray[AdjacentTo[i, 0], AdjacentTo[i, 1]].Terrain == "Shallow Sea" || MapArray[AdjacentTo[i, 0], AdjacentTo[i, 1]].Terrain == "Deep Ocean"))
                {
                    OptionsToRemove.Add(i);
                }
            }
            foreach (int i in OptionsToRemove)
            {
                Options.Remove(i);
            }
            OptionsToRemove.Clear();

            int[] BestDirection = new int[2];
            BestDirection[0] = AdjacentTo[Options[0], 0];
            BestDirection[1] = AdjacentTo[Options[0], 1];

            return BestDirection;
        }

        public bool CanColonise(Province[,] MapArray, Country[] Countries, int Coloniser)
        {
            bool CanColonise = false;

            for (int i = 0; i < 6; i++)
            {
                if (AdjacentTo[i, 0] >= 0 && AdjacentTo[i, 0] <= 23 && AdjacentTo[i, 1] >= 0 && AdjacentTo[i, 1] <= 23 && MapArray[AdjacentTo[i, 0], AdjacentTo[i, 1]].OwnedBy == Countries[Coloniser] && OwnedBy == Countries[0])
                {
                    CanColonise = true;
                }
            }

            return CanColonise;
        }

        public bool CanAnnex(Province[,] MapArray, Country[] Countries, int Annexer)
        {
            bool CanAnnex = false;

            for (int i = 0; i < 6; i++)
            {
                if (ArmyInside != null && ArmyInside.OwnedBy == Countries[Annexer].Name && AdjacentTo[i, 0] >= 0 && AdjacentTo[i, 0] <= 23 && AdjacentTo[i, 1] >= 0 && AdjacentTo[i, 1] <= 23 && MapArray[AdjacentTo[i, 0], AdjacentTo[i, 1]].OwnedBy == Countries[Annexer] && OwnedBy != Countries[0] && OwnedBy != Countries[Annexer] && !ArmyInside.Moved && !ArmyInside.Sieging)
                {
                    CanAnnex = true;
                }
            }

            return CanAnnex;
        }
    }
}
