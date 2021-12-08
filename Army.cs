﻿using System;
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

        public void Move(Province[,] MapArray, Country[] Countries, string Selected, Dictionary<string, int> ArmyCosts, int[] TempPArray, Dictionary<string, int> CountryIndexes)
        {
            PhantomArmy p = new PhantomArmy(TempPArray[0], TempPArray[1]);
            if (!(p.X == 0 && p.Y == 0))
            {
                if (Selected == "Standing" && !Countries[CountryIndexes[OwnedBy]].Standing.Moved)
                {
                    if (!Retreating)
                    {
                        Countries[CountryIndexes[OwnedBy]].Gold -= ((Countries[CountryIndexes[OwnedBy]].Standing.Infantry + Countries[CountryIndexes[OwnedBy]].Standing.Archers + Countries[CountryIndexes[OwnedBy]].Standing.Cavalry) / 10);
                        Countries[CountryIndexes[OwnedBy]].Food -= (((Countries[CountryIndexes[OwnedBy]].Standing.Infantry * ArmyCosts[MapArray[p.X, p.Y].Terrain]) + (Countries[CountryIndexes[OwnedBy]].Standing.Archers * ArmyCosts[MapArray[p.X, p.Y].Terrain]) + (Countries[CountryIndexes[OwnedBy]].Standing.Cavalry * ArmyCosts[MapArray[p.X, p.Y].Terrain] * 2)) / 10);
                        if (MapArray[p.X, p.Y].Terrain == "Shallow Sea")
                        {
                            Countries[CountryIndexes[OwnedBy]].Wood -= ((Countries[CountryIndexes[OwnedBy]].Standing.Infantry + Countries[CountryIndexes[OwnedBy]].Standing.Archers + Countries[CountryIndexes[OwnedBy]].Standing.Cavalry) / 10);
                        }
                    }

                    if (MapArray[p.X, p.Y].ArmyInside != null && MapArray[p.X, p.Y].ArmyInside.OwnedBy != Countries[CountryIndexes[OwnedBy]].Name)
                    {
                        MapArray[X, Y].ArmyInside.Battle(MapArray[p.X, p.Y].ArmyInside);
                        if (MapArray[p.X, p.Y].ArmyInside.GetArmyScore() == 0)
                        {
                            MapArray[p.X, p.Y].ArmyInside.Retreat(MapArray, Countries, Selected, ArmyCosts, CountryIndexes);
                            MapArray[p.X, p.Y].ArmyInside = Countries[CountryIndexes[OwnedBy]].Standing;
                            int TempX = Countries[CountryIndexes[OwnedBy]].Standing.X;
                            int TempY = Countries[CountryIndexes[OwnedBy]].Standing.Y;
                            Countries[CountryIndexes[OwnedBy]].Standing.X = p.X;
                            Countries[CountryIndexes[OwnedBy]].Standing.Y = p.Y;
                            Selected = "Province";
                            MapArray[TempX, TempY].ArmyInside = null;
                            Countries[CountryIndexes[OwnedBy]].Standing.Moved = true;
                        }
                        else if (GetArmyScore() == 0)
                        {
                            Retreat(MapArray, Countries, Selected, ArmyCosts, CountryIndexes);
                        }
                    }
                    else
                    {
                        MapArray[p.X, p.Y].ArmyInside = Countries[CountryIndexes[OwnedBy]].Standing;
                        int TempX = Countries[CountryIndexes[OwnedBy]].Standing.X;
                        int TempY = Countries[CountryIndexes[OwnedBy]].Standing.Y;
                        Countries[CountryIndexes[OwnedBy]].Standing.X = p.X;
                        Countries[CountryIndexes[OwnedBy]].Standing.Y = p.Y;
                        Selected = "Province";
                        MapArray[TempX, TempY].ArmyInside = null;
                        Countries[CountryIndexes[OwnedBy]].Standing.Moved = true;
                    }
                }
                else if (Selected == "Levy" && !Countries[CountryIndexes[OwnedBy]].Levy.Moved)
                {
                    if (!Retreating)
                    {
                        Countries[CountryIndexes[OwnedBy]].Food -= ((Countries[CountryIndexes[OwnedBy]].Levy.Infantry * ArmyCosts[MapArray[p.X, p.Y].Terrain]) / 10);
                        if (MapArray[p.X, p.Y].Terrain == "Shallow Sea")
                        {
                            Countries[CountryIndexes[OwnedBy]].Wood -= ((Countries[CountryIndexes[OwnedBy]].Levy.Infantry) / 10);
                        }
                    }

                    if (MapArray[p.X, p.Y].ArmyInside != null && MapArray[p.X, p.Y].ArmyInside.OwnedBy != Countries[CountryIndexes[OwnedBy]].Name)
                    {
                        MapArray[X, Y].ArmyInside.Battle(MapArray[p.X, p.Y].ArmyInside);
                        if (MapArray[p.X, p.Y].ArmyInside.GetArmyScore() == 0)
                        {
                            MapArray[p.X, p.Y].ArmyInside.Retreat(MapArray, Countries, Selected, ArmyCosts, CountryIndexes);
                            MapArray[p.X, p.Y].ArmyInside = Countries[CountryIndexes[OwnedBy]].Levy;
                            int TempX = Countries[CountryIndexes[OwnedBy]].Levy.X;
                            int TempY = Countries[CountryIndexes[OwnedBy]].Levy.Y;
                            Countries[CountryIndexes[OwnedBy]].Levy.X = p.X;
                            Countries[CountryIndexes[OwnedBy]].Levy.Y = p.Y;
                            Selected = "Province";
                            MapArray[TempX, TempY].ArmyInside = null;
                            Countries[CountryIndexes[OwnedBy]].Levy.Moved = true;
                        }
                        else if (GetArmyScore() == 0)
                        {
                            Retreat(MapArray, Countries, Selected, ArmyCosts, CountryIndexes);
                        }
                    }
                    else
                    {
                        MapArray[p.X, p.Y].ArmyInside = Countries[CountryIndexes[OwnedBy]].Levy;
                        int TempX = Countries[CountryIndexes[OwnedBy]].Levy.X;
                        int TempY = Countries[CountryIndexes[OwnedBy]].Levy.Y;
                        Countries[CountryIndexes[OwnedBy]].Levy.X = p.X;
                        Countries[CountryIndexes[OwnedBy]].Levy.Y = p.Y;
                        Selected = "Province";
                        MapArray[TempX, TempY].ArmyInside = null;
                        Countries[CountryIndexes[OwnedBy]].Levy.Moved = true;
                    }
                }
            }
        }

        public int[] PickMoveLocation(int SelectedX, int SelectedY, Point mousePoint, Province[,] MapArray)
        {
            int[] MoveLocation = new int[2];
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

        public void Battle(RealArmy Defender)
        {
            int AttackerScore = GetArmyScore();
            int DefenderScore = Defender.GetArmyScore();
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
        }

        public void Retreat(Province[,] MapArray, Country[] Countries, string Selected, Dictionary<string, int> ArmyCosts, Dictionary<string, int> CountryIndexes)
        {
            int DX = Countries[CountryIndexes[OwnedBy]].CapitalX;
            int DY = Countries[CountryIndexes[OwnedBy]].CapitalY;
            int[] TempDArray = new int[2];

            if (DX > X && DY < Y)
            {
                TempDArray[0] = X + 1;
                TempDArray[1] = Y - 1;
            }
            else if (DX > X && DY > Y)
            {
                TempDArray[0] = X + 1;
                TempDArray[1] = Y + 1;
            }
            else if (DX < X && DY > Y)
            {
                TempDArray[0] = X - 1;
                TempDArray[1] = Y + 1;
            }
            else if (DX < X && DY < Y)
            {
                TempDArray[0] = X - 1;
                TempDArray[1] = Y - 1;
            }
            else if (DX == X && DY > Y)
            {
                TempDArray[0] = X;
                TempDArray[1] = Y + 1;
            }
            else if (DX == X && DY < Y)
            {
                TempDArray[0] = X;
                TempDArray[1] = Y - 1;
            }
            else if (DX > X && DY == Y)
            {
                TempDArray[0] = X + 1;
                TempDArray[1] = Y;
            }
            else if (DX < X && DY == Y)
            {
                TempDArray[0] = X - 1;
                TempDArray[1] = Y;
            }
            else if (DX == X && DY == Y)
            {
                if(Infantry == 0 && Archers == 0 && Cavalry == 0)
                {
                    TempDArray[0] = X + 1;
                    TempDArray[1] = Y + 1;
                }
                else
                {
                    Retreating = false;
                }
            }

            Move(MapArray, Countries, Selected, ArmyCosts, TempDArray, CountryIndexes);
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
