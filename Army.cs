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
        public bool Moved, Retreating;
        public PhantomArmy[] MoveSelection = new PhantomArmy[6];
        public List<Province> BeenThrough = new List<Province>();

        public void Move(Province[,] MapArray, Country[] Countries, string Selected, Dictionary<string, int> ArmyCosts, int[] TempPArray, Dictionary<string, int> CountryIndexes)
        {
            PhantomArmy p = new PhantomArmy(TempPArray[0], TempPArray[1]);
            if (!(p.X == 0 && p.Y == 0) && !Moved)
            {
                if (!Retreating)
                {
                    Countries[CountryIndexes[OwnedBy]].Gold -= ((Infantry + Archers + Cavalry) / 10);
                    Countries[CountryIndexes[OwnedBy]].Food -= (((Infantry * ArmyCosts[MapArray[p.X, p.Y].Terrain]) + (Archers * ArmyCosts[MapArray[p.X, p.Y].Terrain]) + (Cavalry * ArmyCosts[MapArray[p.X, p.Y].Terrain] * 2)) / 10);
                    if (MapArray[p.X, p.Y].Terrain == "Shallow Sea")
                    {
                        Countries[CountryIndexes[OwnedBy]].Wood -= ((Infantry + Archers + Cavalry) / 10);
                    }
                }

                MapArray[p.X, p.Y].ArmyInside = this;
                int TempX = X;
                int TempY = Y;
                X = p.X;
                Y = p.Y;
                MapArray[TempX, TempY].ArmyInside = null;
                Moved = true;

            }
        }

        public int[] PickMoveLocation(int SelectedX, int SelectedY, Point mousePoint, Province[,] MapArray)
        {
            int[] MoveLocation = new int[2];

            for (int i = 0; i < 6; i++)
            {
                MoveSelection[i] = new PhantomArmy(MapArray[SelectedX, SelectedY].AdjacentTo[i, 0], MapArray[SelectedX, SelectedY].AdjacentTo[i, 1]);
            }

            foreach (PhantomArmy p in MoveSelection)
            {
                if (p.X >= 0 && p.X <= 23 && p.Y >= 0 && p.Y <= 17 && p.ContainsMousePointer(mousePoint) && MapArray[p.X, p.Y].Terrain != "Deep Ocean")
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

        public void Attack(RealArmy Defender, Province[,] MapArray, Country[] Countries, string Selected, Dictionary<string, int> ArmyCosts, Dictionary<string, int> CountryIndexes)
        {
            if (!(Defender.X == 0 && Defender.Y == 0))
            {
                int AttackerScore = GetArmyScore();
                int DefenderScore = Defender.GetArmyScore();
                bool Won = false;
                while (AttackerScore > 0 && DefenderScore > 0)
                {
                    if (AttackerScore > DefenderScore)
                    {
                        Defender.Infantry *= (DefenderScore / AttackerScore);
                        Defender.Archers *= (DefenderScore / AttackerScore);
                        Defender.Cavalry *= (DefenderScore / AttackerScore);
                        Infantry -= (Infantry * ((DefenderScore / AttackerScore) / 10));
                        Archers -= (Archers * ((DefenderScore / AttackerScore) / 10));
                        Cavalry -= (Cavalry * ((DefenderScore / AttackerScore) / 10));
                        DefenderScore = Defender.GetArmyScore();
                        AttackerScore = GetArmyScore();
                    }
                    else if (AttackerScore < DefenderScore)
                    {
                        Infantry *= (AttackerScore / DefenderScore);
                        Archers *= (AttackerScore / DefenderScore);
                        Cavalry *= (AttackerScore / DefenderScore);
                        Defender.Infantry -= (Defender.Infantry * ((AttackerScore / DefenderScore) / 10));
                        Defender.Archers -= (Defender.Archers * ((AttackerScore / DefenderScore) / 10));
                        Defender.Cavalry -= (Defender.Cavalry * ((AttackerScore / DefenderScore) / 10));
                        DefenderScore = Defender.GetArmyScore();
                        AttackerScore = GetArmyScore();
                    }
                    else if (AttackerScore == DefenderScore)
                    {
                        DefenderScore++;
                        Infantry *= (AttackerScore / DefenderScore);
                        Archers *= (AttackerScore / DefenderScore);
                        Cavalry *= (AttackerScore / DefenderScore);
                        Defender.Infantry -= (Defender.Infantry * ((AttackerScore / DefenderScore) / 10));
                        Defender.Archers -= (Defender.Archers * ((AttackerScore / DefenderScore) / 10));
                        Defender.Cavalry -= (Defender.Cavalry * ((AttackerScore / DefenderScore) / 10));
                        DefenderScore = Defender.GetArmyScore();
                        AttackerScore = GetArmyScore();
                    }
                }

                if (DefenderScore == 0)
                {
                    Won = true;
                }

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
                    Defender.Move(MapArray, Countries, Selected, ArmyCosts, MoveLocation, CountryIndexes);
                }
                else
                {
                    Moved = true;
                    Retreating = true;
                }
            }
        }

        public void Retreat(Province[,] MapArray, Country[] Countries, string Selected, Dictionary<string, int> ArmyCosts, Dictionary<string, int> CountryIndexes)
        {
            int DX = Countries[CountryIndexes[OwnedBy]].CapitalX;
            int DY = Countries[CountryIndexes[OwnedBy]].CapitalY;
            int[] BestDArray = new int[2];
            BestDArray[0] = X;
            BestDArray[1] = Y;

            for (int i = 0; i < 6; i++)
            {
                if (MapArray[MapArray[X, Y].AdjacentTo[i, 0], MapArray[X, Y].AdjacentTo[i, 1]].ArmyInside == null)
                {
                    if (MapArray[MapArray[X, Y].AdjacentTo[i, 0], MapArray[X, Y].AdjacentTo[i, 1]].IsCloserThan(MapArray[BestDArray[0], BestDArray[1]], MapArray[DX, DY]))
                    {
                        BestDArray[0] = MapArray[X, Y].AdjacentTo[i, 0];
                        BestDArray[1] = MapArray[X, Y].AdjacentTo[i, 1];
                    }
                }
            }

            if (DX == BestDArray[0] && DY == BestDArray[1])
            {
                Retreating = false;
            }

            if (!(BestDArray[0] == 0 && BestDArray[1] == 0))
            {
                Move(MapArray, Countries, Selected, ArmyCosts, BestDArray, CountryIndexes);
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
