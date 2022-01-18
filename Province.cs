using Microsoft.Xna.Framework;

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

        public bool IsBetterDirection (Province Comparison, Province Destination)
        {
            return false;
        }
    }
}
