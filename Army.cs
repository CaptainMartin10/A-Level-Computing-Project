using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace A_Level_Computing_Project
{
    public class Army
    {
        public int X, Y;

        public bool ContainsMousePointer(Point mousePoint)
        {
            if (X % 2 == 0)
            {
                Rectangle CircleBounds1 = new Rectangle((X * 27) + 15, (Y * 36) + 4, 6, 28);
                Rectangle CircleBounds2 = new Rectangle((X * 27) + 13, (Y * 36) + 5, 10, 26);
                Rectangle CircleBounds3 = new Rectangle((X * 27) + 11, (Y * 36) + 6, 14, 24);
                Rectangle CircleBounds4 = new Rectangle((X * 27) + 9, (Y * 36) + 7, 18, 22);
                Rectangle CircleBounds5 = new Rectangle((X * 27) + 8, (Y * 36) + 8, 20, 20);
                Rectangle CircleBounds6 = new Rectangle((X * 27) + 7, (Y * 36) + 9, 22, 18);
                Rectangle CircleBounds7 = new Rectangle((X * 27) + 6, (Y * 36) + 11, 24, 14);
                Rectangle CircleBounds8 = new Rectangle((X * 27) + 5, (Y * 36) + 13, 26, 10);
                Rectangle CircleBounds9 = new Rectangle((X * 27) + 4, (Y * 36) + 15, 28, 6);
                if (CircleBounds1.Contains(mousePoint) || CircleBounds2.Contains(mousePoint) || CircleBounds3.Contains(mousePoint) || CircleBounds4.Contains(mousePoint) || CircleBounds5.Contains(mousePoint) || CircleBounds6.Contains(mousePoint) || CircleBounds7.Contains(mousePoint) || CircleBounds8.Contains(mousePoint) || CircleBounds9.Contains(mousePoint))
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
                Rectangle CircleBounds1 = new Rectangle((X * 27) + 15, (Y * 36) + 22, 6, 28);
                Rectangle CircleBounds2 = new Rectangle((X * 27) + 13, (Y * 36) + 23, 10, 26);
                Rectangle CircleBounds3 = new Rectangle((X * 27) + 11, (Y * 36) + 24, 14, 24);
                Rectangle CircleBounds4 = new Rectangle((X * 27) + 9, (Y * 36) + 25, 18, 22);
                Rectangle CircleBounds5 = new Rectangle((X * 27) + 8, (Y * 36) + 26, 20, 20);
                Rectangle CircleBounds6 = new Rectangle((X * 27) + 7, (Y * 36) + 27, 22, 18);
                Rectangle CircleBounds7 = new Rectangle((X * 27) + 6, (Y * 36) + 29, 24, 14);
                Rectangle CircleBounds8 = new Rectangle((X * 27) + 5, (Y * 36) + 31, 26, 10);
                Rectangle CircleBounds9 = new Rectangle((X * 27) + 4, (Y * 36) + 33, 28, 6);
                if (CircleBounds1.Contains(mousePoint) || CircleBounds2.Contains(mousePoint) || CircleBounds3.Contains(mousePoint) || CircleBounds4.Contains(mousePoint) || CircleBounds5.Contains(mousePoint) || CircleBounds6.Contains(mousePoint) || CircleBounds7.Contains(mousePoint) || CircleBounds8.Contains(mousePoint) || CircleBounds9.Contains(mousePoint))
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
    }

    public class RealArmy : Army
    {
        public int Infantry, Cavalry, Archers;
        public string OwnedBy;
        public bool Moved;
        public PhantomArmy[] MoveSelection = new PhantomArmy[6];

        public void Move(int SelectedX, int SelectedY, Point mousePoint, Province[,] MapArray, Country[] Countries, int Player, string Selected, Dictionary<string, int> ArmyCosts)
        {
            if (SelectedX % 2 == 0)
            {
                MoveSelection[0] = new PhantomArmy(SelectedX, SelectedY - 1);
                MoveSelection[1] = new PhantomArmy(SelectedX + 1, SelectedY - 1);
                MoveSelection[2] = new PhantomArmy(SelectedX + 1, SelectedY);
                MoveSelection[3] = new PhantomArmy(SelectedX, SelectedY + 1);
                MoveSelection[4] = new PhantomArmy(SelectedX - 1, SelectedY);
                MoveSelection[5] = new PhantomArmy(SelectedX - 1, SelectedY - 1);
            }
            else if (SelectedX % 2 == 1)
            {
                MoveSelection[0] = new PhantomArmy(SelectedX, SelectedY - 1);
                MoveSelection[1] = new PhantomArmy(SelectedX + 1, SelectedY);
                MoveSelection[2] = new PhantomArmy(SelectedX + 1, SelectedY + 1);
                MoveSelection[3] = new PhantomArmy(SelectedX, SelectedY + 1);
                MoveSelection[4] = new PhantomArmy(SelectedX - 1, SelectedY + 1);
                MoveSelection[5] = new PhantomArmy(SelectedX - 1, SelectedY);
            }
            foreach (PhantomArmy p in MoveSelection)
            {
                if (p.X >= 0 && p.X <= 23 && p.Y >= 0 && p.Y <= 17 && p.ContainsMousePointer(mousePoint) && MapArray[SelectedX, SelectedY].Terrain != "Deep Ocean")
                {
                    if (Selected == "Standing" && !Countries[Player].Standing.Moved)
                    {
                        MapArray[p.X, p.Y].ArmyInside = Countries[Player].Standing;
                        Countries[Player].Standing.X = p.X;
                        Countries[Player].Standing.Y = p.Y;
                        MapArray[SelectedX, SelectedY].ArmyInside = null;
                        SelectedX = p.X;
                        SelectedY = p.Y;
                        Countries[Player].Standing.Moved = true;
                        Countries[Player].Gold -= (Countries[Player].Standing.Infantry + Countries[Player].Standing.Archers + Countries[Player].Standing.Cavalry);
                        Countries[Player].Food -= ((Countries[Player].Standing.Infantry * ArmyCosts[MapArray[p.X, p.Y].Terrain]) + (Countries[Player].Standing.Archers * ArmyCosts[MapArray[p.X, p.Y].Terrain]) + (Countries[Player].Standing.Cavalry * ArmyCosts[MapArray[p.X, p.Y].Terrain] * 2));
                        if (MapArray[p.X, p.Y].Terrain == "Shallow Sea")
                        {
                            Countries[Player].Wood -= (Countries[Player].Standing.Infantry + Countries[Player].Standing.Archers + Countries[Player].Standing.Cavalry);
                        }
                    }
                    else if (Selected == "Levy" && !Countries[Player].Levy.Moved)
                    {
                        MapArray[p.X, p.Y].ArmyInside = Countries[Player].Levy;
                        Countries[Player].Levy.X = p.X;
                        Countries[Player].Levy.Y = p.Y;
                        MapArray[SelectedX, SelectedY].ArmyInside = null;
                        SelectedX = p.X;
                        SelectedY = p.Y;
                        Countries[Player].Levy.Moved = true;
                        Countries[Player].Food -= (Countries[Player].Levy.Infantry * ArmyCosts[MapArray[p.X, p.Y].Terrain]);
                        if (MapArray[p.X, p.Y].Terrain == "Shallow Sea")
                        {
                            Countries[Player].Wood -= (Countries[Player].Levy.Infantry);
                        }
                    }
                }
            }
        }
    }

    public class StandingArmy : RealArmy
    {
        public StandingArmy(int x, int y, string o)
        {
            X = x;
            Y = y;
            OwnedBy = o;
            Moved = false;
        }
    }

    public class LevyArmy : RealArmy
    {
        public LevyArmy(int x, int y, int i, string o)
        {
            X = x;
            Y = y;
            Infantry = i;
            OwnedBy = o;
            Moved = true;
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
