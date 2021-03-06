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

            //generates 6 coords for 6 adjacent tiles according to coords
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

        public bool ContainsMousePointer(Point MousePoint)
        {
            //defines bounds
            Rectangle HexBounds1 = new Rectangle((X * 27) + 9, (Y * 36) + ((X % 2) * 18), 18, 36);
            Rectangle HexBounds2 = new Rectangle((X * 27) + 8, (Y * 36) + 1 + ((X % 2) * 18), 20, 34);
            Rectangle HexBounds3 = new Rectangle((X * 27) + 7, (Y * 36) + 3 + ((X % 2) * 18), 22, 30);
            Rectangle HexBounds4 = new Rectangle((X * 27) + 6, (Y * 36) + 5 + ((X % 2) * 18), 24, 26);
            Rectangle HexBounds5 = new Rectangle((X * 27) + 5, (Y * 36) + 7 + ((X % 2) * 18), 26, 22);
            Rectangle HexBounds6 = new Rectangle((X * 27) + 4, (Y * 36) + 9 + ((X % 2) * 18), 28, 18);
            Rectangle HexBounds7 = new Rectangle((X * 27) + 3, (Y * 36) + 11 + ((X % 2) * 18), 30, 14);
            Rectangle HexBounds8 = new Rectangle((X * 27) + 2, (Y * 36) + 13 + ((X % 2) * 18), 32, 10);
            Rectangle HexBounds9 = new Rectangle((X * 27) + 1, (Y * 36) + 15 + ((X % 2) * 18), 34, 6);
            Rectangle HexBounds10 = new Rectangle((X * 27), (Y * 36) + 17 + ((X % 2) * 18), 36, 2);
            
            //checks each bound
            if (HexBounds1.Contains(MousePoint) || HexBounds2.Contains(MousePoint) || HexBounds3.Contains(MousePoint) || HexBounds4.Contains(MousePoint) || HexBounds5.Contains(MousePoint) || HexBounds6.Contains(MousePoint) || HexBounds7.Contains(MousePoint) || HexBounds8.Contains(MousePoint) || HexBounds9.Contains(MousePoint) || HexBounds10.Contains(MousePoint))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int[] FindBestDirection(Province Destination, Province[,] MapArray)
        {
            List<int> Options = new List<int>();
            int DirectMovement = 0;
            
            //gets tile which is in direct straight line
            if (Destination.X == X && Destination.Y > Y)
            {
                DirectMovement = 3;
            }
            else if (Destination.X == X && Destination.Y < Y)
            {
                DirectMovement = 0;
            }
            else if ((Destination.X > X && Destination.Y > Y) || (Destination.X > X && Destination.Y == Y && X % 2 == 0))
            {
                DirectMovement = 2;
            }
            else if ((Destination.X > X && Destination.Y < Y) || (Destination.X > X && Destination.Y == Y && X % 2 == 1))
            {
                DirectMovement = 1;
            }
            else if ((Destination.X < X && Destination.Y > Y) || (Destination.X < X && Destination.Y == Y && X % 2 == 0))
            {
                DirectMovement = 4;
            }
            else if ((Destination.X < X && Destination.Y < Y) || (Destination.X < X && Destination.Y == Y && X % 2 == 1))
            {
                DirectMovement = 5;
            }

            //adds other 5 to list according to start direction
            for (int i = 0; i < 4; i++)
            {
                Random rnd = new Random();
                if (rnd.Next(2) == 0)
                {
                    Options.Add(DirectMovement + i);
                    Options.Add(DirectMovement - i);
                }
                else
                {
                    Options.Add(DirectMovement - i);
                    Options.Add(DirectMovement + i);
                }
            }
            for (int i = 0; i < Options.Count; i++)
            {
                if (Options[i] > 5)
                {
                    Options[i] -= 6;
                }
                else if (Options[i] < 0)
                {
                    Options[i] += 6;
                }
            }

            //removes any not viable options
            List<int> OptionsToRemove = new List<int>();
            foreach (int i in Options)
            {
                if (AdjacentTo[i, 0] < 0 || AdjacentTo[i, 0] > 23 || AdjacentTo[i, 1] < 0 || AdjacentTo[i, 1] > 17)
                {
                    OptionsToRemove.Add(i);
                }
                else if (MapArray[AdjacentTo[i, 0], AdjacentTo[i, 1]].ArmyInside != null || MapArray[AdjacentTo[i, 0], AdjacentTo[i, 1]].IsDangerous(MapArray) || MapArray[AdjacentTo[i, 0], AdjacentTo[i, 1]].Terrain == "Shallow Sea" || MapArray[AdjacentTo[i, 0], AdjacentTo[i, 1]].Terrain == "Deep Ocean")
                {
                    OptionsToRemove.Add(i);
                }
            }
            foreach (int i in OptionsToRemove)
            {
                Options.Remove(i);
            }
            OptionsToRemove.Clear();

            //sets return value to coords of best direction
            int[] BestDirection = new int[2];
            if (Options.Count == 0)
            {
                BestDirection[0] = X;
                BestDirection[1] = Y;
            }
            else
            {
                BestDirection[0] = AdjacentTo[Options[0], 0];
                BestDirection[1] = AdjacentTo[Options[0], 1];
            }

            return BestDirection;
        }

        public bool CanColonise(Province[,] MapArray, Country[] Countries, int Coloniser)
        {
            //checks if a tile owned by the coloniser is adjacent
            for (int i = 0; i < 6; i++)
            {
                if (AdjacentTo[i, 0] >= 0 && AdjacentTo[i, 0] <= 23 && AdjacentTo[i, 1] >= 0 && AdjacentTo[i, 1] <= 17 && MapArray[AdjacentTo[i, 0], AdjacentTo[i, 1]].OwnedBy == Countries[Coloniser] && OwnedBy == Countries[0] && Terrain != "Shallow Sea" && Terrain != "Deep Ocean")
                {
                    return true;
                }
            }

            return false;
        }

        public bool CanAnnex(Province[,] MapArray, Country[] Countries, int Annexer)
        {
            //checks it isnt the capital with other land still being owned by the country
            foreach (Country c in Countries)
            {
                if (X == c.CapitalX && Y == c.CapitalY && c.OwnsLand(MapArray))
                {
                    return false;
                }
            }

            //checks if a tile owned by the coloniser is adjacent and that the standing army of coloniser is inside
            for (int i = 0; i < 6; i++)
            {
                if (ArmyInside != null && ArmyInside == Countries[Annexer].Standing && AdjacentTo[i, 0] >= 0 && AdjacentTo[i, 0] <= 23 && AdjacentTo[i, 1] >= 0 && AdjacentTo[i, 1] <= 23 && MapArray[AdjacentTo[i, 0], AdjacentTo[i, 1]].OwnedBy == Countries[Annexer] && OwnedBy != Countries[0] && OwnedBy != Countries[Annexer] && !ArmyInside.Moved && !ArmyInside.Sieging)
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsDangerous(Province[,] MapArray)
        {
            //checks how many armies are nearby
            int TotalArmiesNearby = 0;
            for (int i = 0; i < 6; i++)
            {
                if (AdjacentTo[i, 0] >= 0 && AdjacentTo[i, 0] <= 23 && AdjacentTo[i, 1] >= 0 && AdjacentTo[i, 1] <= 17 && MapArray[AdjacentTo[i, 0], AdjacentTo[i, 1]].ArmyInside != null)
                {
                    TotalArmiesNearby++;
                }
            }

            if (TotalArmiesNearby > 1)
            {
                return true;
            }

            return false;
        }
    }
}
