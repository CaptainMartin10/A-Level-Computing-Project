using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Collections.Generic;
using System;

namespace A_Level_Computing_Project
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public Province[,] MapArray = new Province[24, 18];
        public Country[] Countries = new Country[11];
        public int SelectedX, SelectedY, Player = 8, Turn = 1;
        public SpriteFont MenuFont;
        public Texture2D Background, Fort, Settlement, Farm, Forester, Mine, BuildStructureMenu, Unowned, Lindon, BlueMountainsNorth, BlueMountainsSouth, Shire, RangersoftheNorth, Rivendell, Breeland, Dunland, Isengard, Gundabad, LindonArmy, BlueMountainsNorthArmy, BlueMountainsSouthArmy, ShireArmy, RangersoftheNorthArmy, RivendellArmy, BreelandArmy, DunlandArmy, IsengardArmy, GundabadArmy, ArmyMovement;
        public MouseState CurrentMouseState, LastMouseState;
        public KeyboardState CurrentKeyboardState, LastKeyboardState;
        public Dictionary<string, int> FarmProduction = new Dictionary<string, int>();
        public Dictionary<string, int> ForesterProduction = new Dictionary<string, int>();
        public Dictionary<string, int> MineProduction = new Dictionary<string, int>();
        public Dictionary<string, int> ArmyCosts = new Dictionary<string, int>();
        public Dictionary<string, Texture2D> OwnedMapmode = new Dictionary<string, Texture2D>();
        public Dictionary<string, Texture2D> ArmyTextures = new Dictionary<string, Texture2D>();
        public string Menu = "Game", Mapmode = "Regular", Selected = "Terrain";

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _graphics.PreferredBackBufferWidth = 1167;
            _graphics.PreferredBackBufferHeight = 666;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            Background = Content.Load<Texture2D>("Background");
            Fort = Content.Load<Texture2D>("Fort");
            Settlement = Content.Load<Texture2D>("Settlement");
            Farm = Content.Load<Texture2D>("Farm");
            Forester = Content.Load<Texture2D>("Forester");
            Mine = Content.Load<Texture2D>("Mine");
            BuildStructureMenu = Content.Load<Texture2D>("Structure Menu");

            Unowned = Content.Load<Texture2D>("Blank Tile");
            Lindon = Content.Load<Texture2D>("Lindon Tile");
            BlueMountainsNorth = Content.Load<Texture2D>("Blue Mountains North Tile");
            BlueMountainsSouth = Content.Load<Texture2D>("Blue Mountains South Tile");
            Shire = Content.Load<Texture2D>("Shire Tile");
            RangersoftheNorth = Content.Load<Texture2D>("Rangers of the North Tile");
            Rivendell = Content.Load<Texture2D>("Rivendell Tile");
            Breeland = Content.Load<Texture2D>("Breeland Tile");
            Dunland = Content.Load<Texture2D>("Dunland Tile");
            Isengard = Content.Load<Texture2D>("Isengard Tile");
            Gundabad = Content.Load<Texture2D>("Gundabad Tile");

            ArmyMovement = Content.Load<Texture2D>("Army Movement");
            LindonArmy = Content.Load<Texture2D>("Lindon Army");
            BlueMountainsNorthArmy = Content.Load<Texture2D>("Blue Mountains North Army");
            BlueMountainsSouthArmy = Content.Load<Texture2D>("Blue Mountains South Army");
            ShireArmy = Content.Load<Texture2D>("Shire Army");
            RangersoftheNorthArmy = Content.Load<Texture2D>("Rangers of the North Army");
            RivendellArmy = Content.Load<Texture2D>("Rivendell Army");
            BreelandArmy = Content.Load<Texture2D>("Breeland Army");
            DunlandArmy = Content.Load<Texture2D>("Dunland Army");
            IsengardArmy = Content.Load<Texture2D>("Isengard Army");
            GundabadArmy = Content.Load<Texture2D>("Gundabad Army");

            MenuFont = Content.Load<SpriteFont>("MenuFont");

            Countries[0] = new Country(true, "Unowned", 0, 0);
            Countries[1] = new Country(true, "Lindon", 7, 8);
            Countries[2] = new Country(true, "Blue Mountains North", 7, 5);
            Countries[3] = new Country(true, "Blue Mountains South", 7, 10);
            Countries[4] = new Country(true, "Shire", 12, 8);
            Countries[5] = new Country(true, "Rangers of the North", 18, 6);
            Countries[6] = new Country(true, "Rivendell", 21, 5);
            Countries[7] = new Country(true, "Breeland", 16, 8);
            Countries[8] = new Country(true, "Dunland", 18, 14);
            Countries[9] = new Country(true, "Isengard", 20, 16);
            Countries[10] = new Country(true, "Gundabad", 22, 1);

            Countries[Player].IsAI = false;

            SelectedX = Countries[Player].CapitalX;
            SelectedY = Countries[Player].CapitalY;

            FarmProduction.Add("Grassland", 100);
            FarmProduction.Add("Hills", 75);
            FarmProduction.Add("Forest", 75);
            FarmProduction.Add("Forest Hills", 50);
            FarmProduction.Add("Dense Forest", 50);
            FarmProduction.Add("Dense Forest Hills", 25);
            FarmProduction.Add("Mountains", 25);
            FarmProduction.Add("Wasteland", 0);
            FarmProduction.Add("Marshland", 25);

            ForesterProduction.Add("Grassland", 50);
            ForesterProduction.Add("Hills", 25);
            ForesterProduction.Add("Forest", 100);
            ForesterProduction.Add("Forest Hills", 75);
            ForesterProduction.Add("Dense Forest", 150);
            ForesterProduction.Add("Dense Forest Hills", 125);
            ForesterProduction.Add("Mountains", 0);
            ForesterProduction.Add("Wasteland", 0);
            ForesterProduction.Add("Marshland", 0);

            MineProduction.Add("Grassland", 25);
            MineProduction.Add("Hills", 50);
            MineProduction.Add("Forest", 25);
            MineProduction.Add("Forest Hills", 50);
            MineProduction.Add("Dense Forest", 25);
            MineProduction.Add("Dense Forest Hills", 50);
            MineProduction.Add("Mountains", 100);
            MineProduction.Add("Wasteland", 50);
            MineProduction.Add("Marshland", 0);
            
            ArmyCosts.Add("Grassland", 1);
            ArmyCosts.Add("Hills", 2);
            ArmyCosts.Add("Forest", 1);
            ArmyCosts.Add("Forest Hills", 2);
            ArmyCosts.Add("Dense Forest", 1);
            ArmyCosts.Add("Dense Forest Hills", 2);
            ArmyCosts.Add("Mountains", 3);
            ArmyCosts.Add("Wasteland", 2);
            ArmyCosts.Add("Marshland", 3);
            ArmyCosts.Add("Shallow Sea", 2);

            OwnedMapmode.Add("Unowned", Unowned);
            OwnedMapmode.Add("Lindon", Lindon);
            OwnedMapmode.Add("Blue Mountains North", BlueMountainsNorth);
            OwnedMapmode.Add("Blue Mountains South", BlueMountainsSouth);
            OwnedMapmode.Add("Shire", Shire);
            OwnedMapmode.Add("Rangers of the North", RangersoftheNorth);
            OwnedMapmode.Add("Rivendell", Rivendell);
            OwnedMapmode.Add("Breeland", Breeland);
            OwnedMapmode.Add("Dunland", Dunland);
            OwnedMapmode.Add("Isengard", Isengard);
            OwnedMapmode.Add("Gundabad", Gundabad);

            ArmyTextures.Add("Lindon", LindonArmy);
            ArmyTextures.Add("Blue Mountains North", BlueMountainsNorthArmy);
            ArmyTextures.Add("Blue Mountains South", BlueMountainsSouthArmy);
            ArmyTextures.Add("Shire", ShireArmy);
            ArmyTextures.Add("Rangers of the North", RangersoftheNorthArmy);
            ArmyTextures.Add("Rivendell", RivendellArmy);
            ArmyTextures.Add("Breeland", BreelandArmy);
            ArmyTextures.Add("Dunland", DunlandArmy);
            ArmyTextures.Add("Isengard", IsengardArmy);
            ArmyTextures.Add("Gundabad", GundabadArmy);

            string NewSave = Path.GetFullPath("Saves/NewSave.txt");
            NewSave = NewSave.Remove(NewSave.Length - 41, 24);
            using (StreamReader sr = new StreamReader(Path.GetFullPath(NewSave)))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Length == 35)
                    {
                        MapArray[Convert.ToInt32(line.Substring(0, 2)), Convert.ToInt32(line.Substring(2, 2))] = new Province(Convert.ToInt32(line.Substring(0, 2)), Convert.ToInt32(line.Substring(2, 2)));
                        MapArray[Convert.ToInt32(line.Substring(0, 2)), Convert.ToInt32(line.Substring(2, 2))].StructureLevel = Convert.ToInt32(line.Substring(4, 1));
                        MapArray[Convert.ToInt32(line.Substring(0, 2)), Convert.ToInt32(line.Substring(2, 2))].Structure = (line.Substring(5, 10)).Trim();
                        MapArray[Convert.ToInt32(line.Substring(0, 2)), Convert.ToInt32(line.Substring(2, 2))].OwnedBy = Countries[Convert.ToInt32(line.Substring(15, 2))];
                        MapArray[Convert.ToInt32(line.Substring(0, 2)), Convert.ToInt32(line.Substring(2, 2))].Terrain = (line.Substring(17, 18)).Trim();
                    }
                }
            }

            foreach (Country C in Countries)
            {
                MapArray[C.Standing.X, C.Standing.Y].ArmyInside = C.Standing;
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            // TODO: Add your update logic here

            CurrentMouseState = Mouse.GetState();
            Point mousePoint = new Point(CurrentMouseState.X, CurrentMouseState.Y);
            CurrentKeyboardState = Keyboard.GetState();

            if (CurrentMouseState.LeftButton != ButtonState.Pressed && LastMouseState.LeftButton == ButtonState.Pressed)
            {
                if (Menu == "Game")
                {
                    if ((Selected == "Standing" || Selected == "Levy") && MapArray[SelectedX, SelectedY].ArmyInside.OwnedBy == Countries[Player].Name)
                    {
                        MapArray[SelectedX, SelectedY].ArmyInside.Move(SelectedX, SelectedY, mousePoint, MapArray, Countries, Player, Selected, ArmyCosts);
                    }

                    foreach (Province Hex in MapArray)
                    {
                        if (Hex.ContainsMousePointer(mousePoint))
                        {
                            SelectedX = Hex.X;
                            SelectedY = Hex.Y;
                            Selected = "Terrain";
                        }
                    }

                    foreach (Country C in Countries)
                    {
                        if (C.Name != "Unowned" && C.Standing.ContainsMousePointer(mousePoint))
                        {
                            SelectedX = C.Standing.X;
                            SelectedY = C.Standing.Y;
                            Selected = "Standing";
                        }
                        else if (C.Levy != null && C.Levy.ContainsMousePointer(mousePoint))
                        {
                            SelectedX = C.Levy.X;
                            SelectedY = C.Levy.Y;
                            Selected = "Levy";
                        }
                    }

                    Rectangle BuildStructureButton = new Rectangle(663, 544, 498, 36);
                    if (BuildStructureButton.Contains(mousePoint) && MapArray[SelectedX, SelectedY].OwnedBy == Countries[Player] && MapArray[SelectedX, SelectedY].Structure == "Empty")
                    {
                        Menu = "Build Structure";
                    }

                    Rectangle UpgradeStrucutreButton = new Rectangle(663, 624, 498, 36);
                    if (UpgradeStrucutreButton.Contains(mousePoint) && MapArray[SelectedX, SelectedY].OwnedBy == Countries[Player] && MapArray[SelectedX, SelectedY].Structure != "Empty" && MapArray[SelectedX, SelectedY].StructureLevel < 5 && Countries[Player].Gold > 100 * (MapArray[SelectedX, SelectedY].StructureLevel + 1) && Countries[Player].Food > 100 * (MapArray[SelectedX, SelectedY].StructureLevel + 1) && Countries[Player].Wood > 100 * (MapArray[SelectedX, SelectedY].StructureLevel + 1) && Countries[Player].Stone > 100 * (MapArray[SelectedX, SelectedY].StructureLevel + 1) && Countries[Player].Metal > 100 * (MapArray[SelectedX, SelectedY].StructureLevel + 1))
                    {
                        MapArray[SelectedX, SelectedY].StructureLevel += 1;
                        Countries[Player].Gold -= 100 * MapArray[SelectedX, SelectedY].StructureLevel;
                        Countries[Player].Food -= 100 * MapArray[SelectedX, SelectedY].StructureLevel;
                        Countries[Player].Wood -= 100 * MapArray[SelectedX, SelectedY].StructureLevel;
                        Countries[Player].Stone -= 100 * MapArray[SelectedX, SelectedY].StructureLevel;
                        Countries[Player].Metal -= 100 * MapArray[SelectedX, SelectedY].StructureLevel;
                    }

                    Rectangle RaiseLevyArmyButton = new Rectangle(663, 286, 498, 36);
                    if (RaiseLevyArmyButton.Contains(mousePoint) && Countries[Player].Levy == null)
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
                        else if (Countries[Player].CapitalX % 2 == 0)
                        {
                            if (MapArray[Countries[Player].CapitalX, Countries[Player].CapitalY - 1].ArmyInside == null && MapArray[Countries[Player].CapitalX, Countries[Player].CapitalY - 1].OwnedBy == Countries[Player])
                            {
                                ArmyX = Countries[Player].CapitalX;
                                ArmyY = Countries[Player].CapitalY - 1;
                            }
                            else if (MapArray[Countries[Player].CapitalX + 1, Countries[Player].CapitalY - 1].ArmyInside == null && MapArray[Countries[Player].CapitalX + 1, Countries[Player].CapitalY - 1].OwnedBy == Countries[Player])
                            {
                                ArmyX = Countries[Player].CapitalX + 1;
                                ArmyY = Countries[Player].CapitalY - 1;
                            }
                            else if (MapArray[Countries[Player].CapitalX + 1, Countries[Player].CapitalY].ArmyInside == null && MapArray[Countries[Player].CapitalX + 1, Countries[Player].CapitalY].OwnedBy == Countries[Player])
                            {
                                ArmyX = Countries[Player].CapitalX + 1;
                                ArmyY = Countries[Player].CapitalY;
                            }
                            else if (MapArray[Countries[Player].CapitalX, Countries[Player].CapitalY + 1].ArmyInside == null && MapArray[Countries[Player].CapitalX, Countries[Player].CapitalY + 1].OwnedBy == Countries[Player])
                            {
                                ArmyX = Countries[Player].CapitalX;
                                ArmyY = Countries[Player].CapitalY + 1;
                            }
                            else if (MapArray[Countries[Player].CapitalX - 1, Countries[Player].CapitalY].ArmyInside == null && MapArray[Countries[Player].CapitalX - 1, Countries[Player].CapitalY].OwnedBy == Countries[Player])
                            {
                                ArmyX = Countries[Player].CapitalX - 1;
                                ArmyY = Countries[Player].CapitalY;
                            }
                            else if (MapArray[Countries[Player].CapitalX - 1, Countries[Player].CapitalY - 1].ArmyInside == null && MapArray[Countries[Player].CapitalX - 1, Countries[Player].CapitalY - 1].OwnedBy == Countries[Player])
                            {
                                ArmyX = Countries[Player].CapitalX - 1;
                                ArmyY = Countries[Player].CapitalY - 1;
                            }
                        }
                        else if (Countries[Player].CapitalX % 2 == 1)
                        {
                            if (MapArray[Countries[Player].CapitalX, Countries[Player].CapitalY - 1].ArmyInside == null && MapArray[Countries[Player].CapitalX, Countries[Player].CapitalY - 1].OwnedBy == Countries[Player])
                            {
                                ArmyX = Countries[Player].CapitalX;
                                ArmyY = Countries[Player].CapitalY - 1;
                            }
                            else if (MapArray[Countries[Player].CapitalX + 1, Countries[Player].CapitalY].ArmyInside == null && MapArray[Countries[Player].CapitalX + 1, Countries[Player].CapitalY].OwnedBy == Countries[Player])
                            {
                                ArmyX = Countries[Player].CapitalX + 1;
                                ArmyY = Countries[Player].CapitalY;
                            }
                            else if (MapArray[Countries[Player].CapitalX + 1, Countries[Player].CapitalY + 1].ArmyInside == null && MapArray[Countries[Player].CapitalX + 1, Countries[Player].CapitalY + 1].OwnedBy == Countries[Player])
                            {
                                ArmyX = Countries[Player].CapitalX + 1;
                                ArmyY = Countries[Player].CapitalY + 1;
                            }
                            else if (MapArray[Countries[Player].CapitalX, Countries[Player].CapitalY + 1].ArmyInside == null && MapArray[Countries[Player].CapitalX, Countries[Player].CapitalY + 1].OwnedBy == Countries[Player])
                            {
                                ArmyX = Countries[Player].CapitalX;
                                ArmyY = Countries[Player].CapitalY + 1;
                            }
                            else if (MapArray[Countries[Player].CapitalX - 1, Countries[Player].CapitalY + 1].ArmyInside == null && MapArray[Countries[Player].CapitalX - 1, Countries[Player].CapitalY + 1].OwnedBy == Countries[Player])
                            {
                                ArmyX = Countries[Player].CapitalX - 1;
                                ArmyY = Countries[Player].CapitalY + 1;
                            }
                            else if (MapArray[Countries[Player].CapitalX - 1, Countries[Player].CapitalY].ArmyInside == null && MapArray[Countries[Player].CapitalX - 1, Countries[Player].CapitalY].OwnedBy == Countries[Player])
                            {
                                ArmyX = Countries[Player].CapitalX - 1;
                                ArmyY = Countries[Player].CapitalY;
                            }
                        }
                        Countries[Player].Levy = new LevyArmy(ArmyX, ArmyY, LevyArmySize, Countries[Player].Name);
                        MapArray[Countries[Player].Levy.X, Countries[Player].Levy.Y].ArmyInside = Countries[Player].Levy;
                    }
                    else if (RaiseLevyArmyButton.Contains(mousePoint) && Countries[Player].Levy != null)
                    {
                        MapArray[Countries[Player].Levy.X, Countries[Player].Levy.Y].ArmyInside = null;
                        Countries[Player].Levy = null;
                        Selected = "Terrain";
                    }
                }

                if (Menu == "Build Structure")
                {
                    Rectangle BuildSettlement = new Rectangle(221, 312, 40, 40);
                    Rectangle BuildFort = new Rectangle(265, 312, 40, 40);
                    Rectangle BuildFarm = new Rectangle(309, 312, 40, 40);
                    Rectangle BuildMine = new Rectangle(353, 312, 40, 40);
                    Rectangle BuildForester = new Rectangle(397, 312, 40, 40);
                    Rectangle CloseBuildMenu = new Rectangle(447, 312, 14, 14);
                    if (BuildSettlement.Contains(mousePoint) && Countries[Player].Gold > 100 && Countries[Player].Food > 100 && Countries[Player].Wood > 100 && Countries[Player].Stone > 100 && Countries[Player].Metal > 100)
                    {
                        MapArray[SelectedX, SelectedY].Structure = "Settlement";
                        MapArray[SelectedX, SelectedY].StructureLevel = 1;
                        Countries[Player].Gold -= 100;
                        Countries[Player].Food -= 100;
                        Countries[Player].Wood -= 100;
                        Countries[Player].Stone -= 100;
                        Countries[Player].Metal -= 100;
                        Menu = "Game";
                    }
                    else if (BuildFort.Contains(mousePoint) && Countries[Player].Gold > 100 && Countries[Player].Food > 100 && Countries[Player].Wood > 100 && Countries[Player].Stone > 100 && Countries[Player].Metal > 100)
                    {
                        MapArray[SelectedX, SelectedY].Structure = "Fort";
                        MapArray[SelectedX, SelectedY].StructureLevel = 1;
                        Countries[Player].Gold -= 100;
                        Countries[Player].Food -= 100;
                        Countries[Player].Wood -= 100;
                        Countries[Player].Stone -= 100;
                        Countries[Player].Metal -= 100;
                        Menu = "Game";
                    }
                    else if (BuildFarm.Contains(mousePoint) && Countries[Player].Gold > 100 && Countries[Player].Food > 100 && Countries[Player].Wood > 100 && Countries[Player].Stone > 100 && Countries[Player].Metal > 100)
                    {
                        MapArray[SelectedX, SelectedY].Structure = "Farm";
                        MapArray[SelectedX, SelectedY].StructureLevel = 1;
                        Countries[Player].Gold -= 100;
                        Countries[Player].Food -= 100;
                        Countries[Player].Wood -= 100;
                        Countries[Player].Stone -= 100;
                        Countries[Player].Metal -= 100;
                        Menu = "Game";
                    }
                    else if (BuildMine.Contains(mousePoint) && Countries[Player].Gold > 100 && Countries[Player].Food > 100 && Countries[Player].Wood > 100 && Countries[Player].Stone > 100 && Countries[Player].Metal > 100)
                    {
                        MapArray[SelectedX, SelectedY].Structure = "Mine";
                        MapArray[SelectedX, SelectedY].StructureLevel = 1;
                        Countries[Player].Gold -= 100;
                        Countries[Player].Food -= 100;
                        Countries[Player].Wood -= 100;
                        Countries[Player].Stone -= 100;
                        Countries[Player].Metal -= 100;
                        Menu = "Game";
                    }
                    else if (BuildForester.Contains(mousePoint) && Countries[Player].Gold > 100 && Countries[Player].Food > 100 && Countries[Player].Wood > 100 && Countries[Player].Stone > 100 && Countries[Player].Metal > 100)
                    {
                        MapArray[SelectedX, SelectedY].Structure = "Forester";
                        MapArray[SelectedX, SelectedY].StructureLevel = 1;
                        Countries[Player].Gold -= 100;
                        Countries[Player].Food -= 100;
                        Countries[Player].Wood -= 100;
                        Countries[Player].Stone -= 100;
                        Countries[Player].Metal -= 100;
                        Menu = "Game";
                    }
                    else if (CloseBuildMenu.Contains(mousePoint))
                    {
                        Menu = "Game";
                    }
                }
            }

            if (!CurrentKeyboardState.IsKeyDown(Keys.M) && LastKeyboardState.IsKeyDown(Keys.M))
            {
                if (Mapmode == "Regular")
                {
                    Mapmode = "ShowOwned";
                }
                else if (Mapmode == "ShowOwned")
                {
                    Mapmode = "Regular";
                }
            }

            if (!CurrentKeyboardState.IsKeyDown(Keys.Enter) && LastKeyboardState.IsKeyDown(Keys.Enter))
            {
                Turn++;
                if (Countries[Player].Levy == null)
                {
                    foreach (Country C in Countries)
                    {
                        C.Gold += 50;
                        C.Metal += 50;
                        C.Stone += 50;
                        C.Wood += 50;
                        C.Food += 50;
                        C.Standing.Infantry += 50;
                    }
                    foreach (Province P in MapArray)
                    {
                        if (P.Structure == "Settlement")
                        {
                            P.OwnedBy.Gold += 100 * P.StructureLevel;
                        }
                        else if (P.Structure == "Mine")
                        {
                            P.OwnedBy.Stone += MineProduction[P.Terrain] * P.StructureLevel;
                            P.OwnedBy.Metal += MineProduction[P.Terrain] * P.StructureLevel;
                        }
                        else if (P.Structure == "Farm")
                        {
                            P.OwnedBy.Food += FarmProduction[P.Terrain] * P.StructureLevel;
                        }
                        else if (P.Structure == "Forester")
                        {
                            P.OwnedBy.Wood += ForesterProduction[P.Terrain] * P.StructureLevel;
                        }
                        else if (P.Structure == "Fort")
                        {
                            P.OwnedBy.Standing.Infantry += 50 * P.StructureLevel;
                            P.OwnedBy.Standing.Archers += 25 * P.StructureLevel;
                            P.OwnedBy.Standing.Cavalry += 25 * P.StructureLevel;
                        }
                    }
                }
                foreach (Country c in Countries)
                {
                    c.Standing.Moved = false;
                    if (c.Levy != null)
                    {
                        c.Levy.Moved = false;
                    }
                }
            }

            LastMouseState = CurrentMouseState;
            LastKeyboardState = CurrentKeyboardState;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            _spriteBatch.Draw(Background, new Vector2(0, 0), Color.White);

            if (Mapmode == "ShowOwned")
            {
                foreach (Province Hex in MapArray)
                {
                    if (Hex.X % 2 == 0)
                    {
                        _spriteBatch.Draw(OwnedMapmode[Hex.OwnedBy.Name], new Vector2(Hex.X * 27, Hex.Y * 36), Color.White);
                    }
                    else if (Hex.X % 2 == 1)
                    {
                        _spriteBatch.Draw(OwnedMapmode[Hex.OwnedBy.Name], new Vector2(Hex.X * 27, (Hex.Y * 36) + 18), Color.White);
                    }
                }
            }

            if (Selected == "Standing" || Selected == "Levy")
            {
                if (MapArray[SelectedX, SelectedY].ArmyInside != null && MapArray[SelectedX, SelectedY].ArmyInside.OwnedBy == Countries[Player].Name)
                {
                    if (SelectedX % 2 == 0)
                    {
                        if ((SelectedX) >= 0 && (SelectedX) <= 23 && (SelectedY - 1) >= 0 && (SelectedY - 1) <= 17 && MapArray[SelectedX, SelectedY - 1].Terrain != "Deep Ocean")
                        {
                            _spriteBatch.Draw(ArmyMovement, new Vector2((SelectedX) * 27, (SelectedY - 1) * 36), Color.White);
                        }
                        if ((SelectedX + 1) >= 0 && (SelectedX + 1) <= 23 && (SelectedY - 1) >= 0 && (SelectedY - 1) <= 17 && MapArray[SelectedX + 1, SelectedY - 1].Terrain != "Deep Ocean")
                        {
                            _spriteBatch.Draw(ArmyMovement, new Vector2((SelectedX + 1) * 27, ((SelectedY - 1) * 36) + 18), Color.White);
                        }
                        if ((SelectedX + 1) >= 0 && (SelectedX + 1) <= 23 && (SelectedY) >= 0 && (SelectedY) <= 17 && MapArray[SelectedX+ 1, SelectedY].Terrain != "Deep Ocean")
                        {
                            _spriteBatch.Draw(ArmyMovement, new Vector2((SelectedX + 1) * 27, ((SelectedY) * 36) + 18), Color.White);
                        }
                        if ((SelectedX) >= 0 && (SelectedX) <= 23 && (SelectedY + 1) >= 0 && (SelectedY + 1) <= 17 && MapArray[SelectedX, SelectedY + 1].Terrain != "Deep Ocean")
                        {
                            _spriteBatch.Draw(ArmyMovement, new Vector2((SelectedX) * 27, ((SelectedY+ 1) * 36)), Color.White);
                        }
                        if ((SelectedX - 1) >= 0 && (SelectedX - 1) <= 23 && (SelectedY) >= 0 && (SelectedY) <= 17 && MapArray[SelectedX - 1, SelectedY].Terrain != "Deep Ocean")
                        {
                            _spriteBatch.Draw(ArmyMovement, new Vector2((SelectedX - 1) * 27, ((SelectedY) * 36) + 18), Color.White);
                        }
                        if ((SelectedX - 1) >= 0 && (SelectedX - 1) <= 23 && (SelectedY - 1) >= 0 && (SelectedY - 1) <= 17 && MapArray[SelectedX - 1, SelectedY - 1].Terrain != "Deep Ocean")
                        {
                            _spriteBatch.Draw(ArmyMovement, new Vector2((SelectedX - 1) * 27, ((SelectedY - 1) * 36) + 18), Color.White);
                        }
                    }
                    else if (SelectedX % 2 == 1)
                    {
                        if ((SelectedX) >= 0 && (SelectedX) <= 23 && (SelectedY - 1) >= 0 && (SelectedY - 1) <= 17 && MapArray[SelectedX, SelectedY - 1].Terrain != "Deep Ocean")
                        {
                            _spriteBatch.Draw(ArmyMovement, new Vector2((SelectedX) * 27, ((SelectedY - 1) * 36) + 18), Color.White);
                        }
                        if ((SelectedX + 1) >= 0 && (SelectedX + 1) <= 23 && (SelectedY) >= 0 && (SelectedY) <= 17 && MapArray[SelectedX + 1, SelectedY].Terrain != "Deep Ocean")
                        {
                            _spriteBatch.Draw(ArmyMovement, new Vector2((SelectedX + 1) * 27, (SelectedY) * 36), Color.White);
                        }
                        if ((SelectedX + 1) >= 0 && (SelectedX + 1) <= 23 && (SelectedY + 1) >= 0 && (SelectedY + 1) <= 17 && MapArray[SelectedX+ 1, SelectedY + 1].Terrain != "Deep Ocean")
                        {
                            _spriteBatch.Draw(ArmyMovement, new Vector2((SelectedX + 1) * 27, (SelectedY + 1) * 36), Color.White);
                        }
                        if ((SelectedX) >= 0 && (SelectedX) <= 23 && (SelectedY + 1) >= 0 && (SelectedY + 1) <= 17 && MapArray[SelectedX, SelectedY + 1].Terrain != "Deep Ocean")
                        {
                            _spriteBatch.Draw(ArmyMovement, new Vector2((SelectedX) * 27, ((SelectedY + 1) * 36) + 18), Color.White);
                        }
                        if ((SelectedX - 1) >= 0 && (SelectedX - 1) <= 23 && (SelectedY + 1) >= 0 && (SelectedY + 1) <= 17 && MapArray[SelectedX - 1, SelectedY + 1].Terrain != "Deep Ocean")
                        {
                            _spriteBatch.Draw(ArmyMovement, new Vector2((SelectedX - 1) * 27, (SelectedY + 1) * 36), Color.White);
                        }
                        if ((SelectedX - 1) >= 0 && (SelectedX - 1) <= 23 && (SelectedY) >= 0 && (SelectedY) <= 17 && MapArray[SelectedX - 1, SelectedY].Terrain != "Deep Ocean")
                        {
                            _spriteBatch.Draw(ArmyMovement, new Vector2((SelectedX - 1) * 27, (SelectedY) * 36), Color.White);
                        }
                    }
                }
            }

            foreach (Country C in Countries)
            {
                if (C.Name != "Unowned")
                {
                    if (C.Standing.X % 2 == 0)
                    {
                        _spriteBatch.Draw(ArmyTextures[C.Name], new Vector2(C.Standing.X * 27, C.Standing.Y * 36), Color.White);
                    }
                    else if (C.Standing.X % 2 == 1)
                    {
                        _spriteBatch.Draw(ArmyTextures[C.Name], new Vector2(C.Standing.X * 27, (C.Standing.Y * 36) + 18), Color.White);
                    }

                    if (C.Levy != null)
                    {
                        if (C.Levy.X % 2 == 0)
                        {
                            _spriteBatch.Draw(ArmyTextures[C.Name], new Vector2(C.Levy.X * 27, C.Levy.Y * 36), Color.White);
                        }
                        else if (C.Levy.X % 2 == 1)
                        {
                            _spriteBatch.Draw(ArmyTextures[C.Name], new Vector2(C.Levy.X * 27, (C.Levy.Y * 36) + 18), Color.White);
                        }
                    }
                }
            }

            foreach (Province Hex in MapArray)
            {
                if (Hex.Structure == "Fort")
                {
                    if (Hex.X % 2 == 0)
                    {
                        _spriteBatch.Draw(Fort, new Vector2(Hex.X * 27, Hex.Y * 36), Color.White);
                    }
                    else if (Hex.X % 2 == 1)
                    {
                        _spriteBatch.Draw(Fort, new Vector2(Hex.X * 27, (Hex.Y * 36) + 18), Color.White);
                    }
                }
                else if (Hex.Structure == "Settlement")
                {
                    if (Hex.X % 2 == 0)
                    {
                        _spriteBatch.Draw(Settlement, new Vector2(Hex.X * 27, Hex.Y * 36), Color.White);
                    }
                    else if (Hex.X % 2 == 1)
                    {
                        _spriteBatch.Draw(Settlement, new Vector2(Hex.X * 27, (Hex.Y * 36) + 18), Color.White);
                    }
                }
                else if (Hex.Structure == "Farm")
                {
                    if (Hex.X % 2 == 0)
                    {
                        _spriteBatch.Draw(Farm, new Vector2(Hex.X * 27, Hex.Y * 36), Color.White);
                    }
                    else if (Hex.X % 2 == 1)
                    {
                        _spriteBatch.Draw(Farm, new Vector2(Hex.X * 27, (Hex.Y * 36) + 18), Color.White);
                    }
                }
                else if (Hex.Structure == "Forester")
                {
                    if (Hex.X % 2 == 0)
                    {
                        _spriteBatch.Draw(Forester, new Vector2(Hex.X * 27, Hex.Y * 36), Color.White);
                    }
                    else if (Hex.X % 2 == 1)
                    {
                        _spriteBatch.Draw(Forester, new Vector2(Hex.X * 27, (Hex.Y * 36) + 18), Color.White);
                    }
                }
                else if (Hex.Structure == "Mine")
                {
                    if (Hex.X % 2 == 0)
                    {
                        _spriteBatch.Draw(Mine, new Vector2(Hex.X * 27, Hex.Y * 36), Color.White);
                    }
                    else if (Hex.X % 2 == 1)
                    {
                        _spriteBatch.Draw(Mine, new Vector2(Hex.X * 27, (Hex.Y * 36) + 18), Color.White);
                    }
                }
            }

            _spriteBatch.DrawString(MenuFont, "Player: " + Countries[Player].Name, new Vector2(666, 5), Color.White);
            _spriteBatch.DrawString(MenuFont, "Gold: " + Countries[Player].Gold, new Vector2(666, 45), Color.White);
            _spriteBatch.DrawString(MenuFont, "Food: " + Countries[Player].Food, new Vector2(666, 85), Color.White);
            _spriteBatch.DrawString(MenuFont, "Wood: " + Countries[Player].Wood, new Vector2(666, 125), Color.White);
            _spriteBatch.DrawString(MenuFont, "Stone: " + Countries[Player].Stone, new Vector2(666, 165), Color.White);
            _spriteBatch.DrawString(MenuFont, "Metal: " + Countries[Player].Metal, new Vector2(666, 205), Color.White);
            _spriteBatch.DrawString(MenuFont, "Standing Army Location: " + Countries[Player].Standing.X + " , " + Countries[Player].Standing.Y, new Vector2(666, 245), Color.White);

            if (Countries[Player].Levy != null)
            {
                _spriteBatch.DrawString(MenuFont, "Dismiss Levy Army at: " + Countries[Player].Levy.X + " , " + Countries[Player].Levy.Y, new Vector2(666, 285), Color.White);
            }
            else
            {
                _spriteBatch.DrawString(MenuFont, "Raise Levy Army", new Vector2(666, 285), Color.White);
            }

            _spriteBatch.DrawString(MenuFont, "Research", new Vector2(666, 331), Color.White);
            _spriteBatch.DrawString(MenuFont, "Market", new Vector2(666, 377), Color.White);

            if (Selected == "Terrain")
            {
                _spriteBatch.DrawString(MenuFont, "Province Coordinates: " + SelectedX + " , " + SelectedY, new Vector2(666, 423), Color.White);
                _spriteBatch.DrawString(MenuFont, "Owned By: " + (MapArray[SelectedX, SelectedY].OwnedBy).Name, new Vector2(666, 463), Color.White);
                _spriteBatch.DrawString(MenuFont, "Terrain: " + MapArray[SelectedX, SelectedY].Terrain, new Vector2(666, 503), Color.White);

                if (MapArray[SelectedX, SelectedY].Structure != "Empty" && MapArray[SelectedX, SelectedY].OwnedBy.IsAI == false)
                {
                    _spriteBatch.DrawString(MenuFont, MapArray[SelectedX, SelectedY].Structure, new Vector2(666, 543), Color.White);
                    _spriteBatch.DrawString(MenuFont, "Level: " + MapArray[SelectedX, SelectedY].StructureLevel, new Vector2(666, 583), Color.White);
                    _spriteBatch.DrawString(MenuFont, "Upgrade", new Vector2(666, 623), Color.White);
                }
                else if (MapArray[SelectedX, SelectedY].Structure != "Empty" && MapArray[SelectedX, SelectedY].OwnedBy.IsAI == true)
                {
                    _spriteBatch.DrawString(MenuFont, MapArray[SelectedX, SelectedY].Structure, new Vector2(666, 543), Color.White);
                    _spriteBatch.DrawString(MenuFont, "Level: " + MapArray[SelectedX, SelectedY].StructureLevel, new Vector2(666, 583), Color.White);
                    _spriteBatch.DrawString(MenuFont, "Cannot Upgrade", new Vector2(666, 623), Color.White);
                }
                else if (MapArray[SelectedX, SelectedY].Structure == "Empty" && MapArray[SelectedX, SelectedY].OwnedBy.IsAI == false)
                {
                    _spriteBatch.DrawString(MenuFont, "Build Structure", new Vector2(666, 543), Color.White);
                    _spriteBatch.DrawString(MenuFont, "Empty", new Vector2(666, 583), Color.White);
                    _spriteBatch.DrawString(MenuFont, "Empty", new Vector2(666, 623), Color.White);
                }
                else if (MapArray[SelectedX, SelectedY].Structure == "Empty" && MapArray[SelectedX, SelectedY].OwnedBy.IsAI == true)
                {
                    _spriteBatch.DrawString(MenuFont, "Empty", new Vector2(666, 543), Color.White);
                    _spriteBatch.DrawString(MenuFont, "Empty", new Vector2(666, 583), Color.White);
                    _spriteBatch.DrawString(MenuFont, "Empty", new Vector2(666, 623), Color.White);
                }
            }
            else if ((Selected == "Standing" || Selected == "Levy") && MapArray[SelectedX, SelectedY].ArmyInside != null)
            {
                if (MapArray[SelectedX, SelectedY].ArmyInside.Moved)
                {
                    _spriteBatch.DrawString(MenuFont, "Army Location: " + SelectedX + " , " + SelectedY + "; Moved", new Vector2(666, 423), Color.White);
                }
                else if (!MapArray[SelectedX, SelectedY].ArmyInside.Moved)
                {
                    _spriteBatch.DrawString(MenuFont, "Army Location: " + SelectedX + " , " + SelectedY + "; Not Moved", new Vector2(666, 423), Color.White);
                }
                _spriteBatch.DrawString(MenuFont, "Owned By: " + MapArray[SelectedX, SelectedY].ArmyInside.OwnedBy, new Vector2(666, 463), Color.White);
                _spriteBatch.DrawString(MenuFont, "Type: " + Selected, new Vector2(666, 503), Color.White);
                _spriteBatch.DrawString(MenuFont, "Infantry: " + MapArray[SelectedX, SelectedY].ArmyInside.Infantry, new Vector2(666, 543), Color.White);
                _spriteBatch.DrawString(MenuFont, "Archers: " + MapArray[SelectedX, SelectedY].ArmyInside.Archers, new Vector2(666, 583), Color.White);
                _spriteBatch.DrawString(MenuFont, "Cavalry: " + MapArray[SelectedX, SelectedY].ArmyInside.Cavalry, new Vector2(666, 623), Color.White);
            }

            if (Menu == "Build Structure")
            {
                _spriteBatch.Draw(BuildStructureMenu, new Vector2(215, 306), Color.White);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
