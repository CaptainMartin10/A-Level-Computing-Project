﻿using Microsoft.Xna.Framework;
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
        public int SelectedX, SelectedY, Player = 4, SavesFP, SavesEP;
        public string Menu = "Game", Mapmode = "Regular", Selected = "Province";
        public List<string> Saves = new List<string>();
        public SpriteFont MenuFont;
        public Texture2D Background, Fort, Settlement, Farm, Forester, Mine, BuildStructureMenu, Unowned, Lindon, BlueMountainsNorth, BlueMountainsSouth, Shire, RangersoftheNorth, Rivendell, Breeland, Dunland, Isengard, Gundabad, LindonArmy, BlueMountainsNorthArmy, BlueMountainsSouthArmy, ShireArmy, RangersoftheNorthArmy, RivendellArmy, BreelandArmy, DunlandArmy, IsengardArmy, GundabadArmy, ArmyMovement, PauseMenu, CountryMenu, LoadMenu, MarketMenu;
        public MouseState CurrentMouseState, LastMouseState;
        public KeyboardState CurrentKeyboardState, LastKeyboardState;
        public Dictionary<string, int> FarmProduction = new Dictionary<string, int>();
        public Dictionary<string, int> ForesterProduction = new Dictionary<string, int>();
        public Dictionary<string, int> MineProduction = new Dictionary<string, int>();
        public Dictionary<string, int> TerrainCosts = new Dictionary<string, int>();
        public Dictionary<string, Texture2D> OwnedMapMode = new Dictionary<string, Texture2D>();
        public Dictionary<string, Texture2D> ArmyTextures = new Dictionary<string, Texture2D>();
        public Dictionary<string, int> CountryIndexes = new Dictionary<string, int>();

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
            PauseMenu = Content.Load<Texture2D>("Pause Menu");
            CountryMenu = Content.Load<Texture2D>("Country Menu");
            LoadMenu = Content.Load<Texture2D>("Load Menu");
            MarketMenu = Content.Load<Texture2D>("Market Menu");

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

            TerrainCosts.Add("Grassland", 1);
            TerrainCosts.Add("Hills", 2);
            TerrainCosts.Add("Forest", 1);
            TerrainCosts.Add("Forest Hills", 2);
            TerrainCosts.Add("Dense Forest", 1);
            TerrainCosts.Add("Dense Forest Hills", 2);
            TerrainCosts.Add("Mountains", 3);
            TerrainCosts.Add("Wasteland", 2);
            TerrainCosts.Add("Marshland", 3);
            TerrainCosts.Add("Shallow Sea", 2);

            OwnedMapMode.Add("Unowned", Unowned);
            OwnedMapMode.Add("Lindon", Lindon);
            OwnedMapMode.Add("Blue Mountains North", BlueMountainsNorth);
            OwnedMapMode.Add("Blue Mountains South", BlueMountainsSouth);
            OwnedMapMode.Add("Shire", Shire);
            OwnedMapMode.Add("Rangers of the North", RangersoftheNorth);
            OwnedMapMode.Add("Rivendell", Rivendell);
            OwnedMapMode.Add("Breeland", Breeland);
            OwnedMapMode.Add("Dunland", Dunland);
            OwnedMapMode.Add("Isengard", Isengard);
            OwnedMapMode.Add("Gundabad", Gundabad);

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

            CountryIndexes.Add("Unowned", 0);
            CountryIndexes.Add("Lindon", 1);
            CountryIndexes.Add("Blue Mountains North", 2);
            CountryIndexes.Add("Blue Mountains South", 3);
            CountryIndexes.Add("Shire", 4);
            CountryIndexes.Add("Rangers of the North", 5);
            CountryIndexes.Add("Rivendell", 6);
            CountryIndexes.Add("Breeland", 7);
            CountryIndexes.Add("Dunland", 8);
            CountryIndexes.Add("Isengard", 9);
            CountryIndexes.Add("Gundabad", 10);

            string Save = Path.GetFullPath("Saves/NewSave.txt");
            Save = Save.Remove(Save.Length - 41, 24);
            LoadSave(Save);
        }

        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here

            CurrentMouseState = Mouse.GetState();
            Point MousePoint = new Point(CurrentMouseState.X, CurrentMouseState.Y);
            CurrentKeyboardState = Keyboard.GetState();

            if (Menu == "Game")
            {
                CheckGameMenu(MousePoint);
            }

            else if (Menu == "Build Structure")
            {
                CheckBuildStructureMenu(MousePoint);
            }

            else if (Menu == "Pause")
            {
                CheckPauseMenu(MousePoint);
            }

            else if (Menu == "Pick Country")
            {
                CheckPickCountryMenu(MousePoint);
            }

            else if (Menu == "Load Game")
            {
                CheckLoadGameMenu(MousePoint);
            }

            else if (Menu == "Market")
            {
                CheckMarketMenu(MousePoint);
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
                        _spriteBatch.Draw(OwnedMapMode[Hex.OwnedBy.Name], new Vector2(Hex.X * 27, Hex.Y * 36), Color.White);
                    }
                    else if (Hex.X % 2 == 1)
                    {
                        _spriteBatch.Draw(OwnedMapMode[Hex.OwnedBy.Name], new Vector2(Hex.X * 27, (Hex.Y * 36) + 18), Color.White);
                    }
                }
            }

            if (Selected == "Standing" || Selected == "Levy")
            {
                if (MapArray[SelectedX, SelectedY].ArmyInside != null && MapArray[SelectedX, SelectedY].ArmyInside.OwnedBy == Countries[Player].Name)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        if (MapArray[SelectedX, SelectedY].AdjacentTo[i, 0] >= 0 && MapArray[SelectedX, SelectedY].AdjacentTo[i, 0] <= 23 && MapArray[SelectedX, SelectedY].AdjacentTo[i, 1] >= 0 && MapArray[SelectedX, SelectedY].AdjacentTo[i, 1] <= 17)
                        {
                            if (MapArray[SelectedX, SelectedY].AdjacentTo[i, 0] % 2 == 0)
                            {
                                _spriteBatch.Draw(ArmyMovement, new Vector2(MapArray[SelectedX, SelectedY].AdjacentTo[i, 0] * 27, MapArray[SelectedX, SelectedY].AdjacentTo[i, 1] * 36), Color.White);
                            }
                            else if (MapArray[SelectedX, SelectedY].AdjacentTo[i, 0] % 2 == 1)
                            {
                                _spriteBatch.Draw(ArmyMovement, new Vector2(MapArray[SelectedX, SelectedY].AdjacentTo[i, 0] * 27, (MapArray[SelectedX, SelectedY].AdjacentTo[i, 1] * 36) + 18), Color.White);
                            }
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

            _spriteBatch.DrawString(MenuFont, "Next Turn", new Vector2(666, 331), Color.White);
            _spriteBatch.DrawString(MenuFont, "Market", new Vector2(666, 377), Color.White);

            if (Selected == "Province")
            {
                _spriteBatch.DrawString(MenuFont, "Province Coordinates: " + SelectedX + " , " + SelectedY, new Vector2(666, 423), Color.White);

                if (MapArray[SelectedX, SelectedY].CanColonise(MapArray, Countries, Player))
                {
                    _spriteBatch.DrawString(MenuFont, "Colonise", new Vector2(666, 463), Color.White);
                }
                else if (MapArray[SelectedX, SelectedY].CanAnnex(MapArray, Countries, Player))
                {
                    _spriteBatch.DrawString(MenuFont, "Siege", new Vector2(666, 463), Color.White);
                }
                else
                {
                    _spriteBatch.DrawString(MenuFont, "Owned By: " + MapArray[SelectedX, SelectedY].OwnedBy.Name, new Vector2(666, 463), Color.White);
                }

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
                if (MapArray[SelectedX, SelectedY].ArmyInside.Retreating)
                {
                    _spriteBatch.DrawString(MenuFont, "Army Location: " + SelectedX + " , " + SelectedY + "; Retreating", new Vector2(666, 423), Color.White);
                }
                else if (MapArray[SelectedX, SelectedY].ArmyInside.Sieging)
                {
                    _spriteBatch.DrawString(MenuFont, "Army Location: " + SelectedX + " , " + SelectedY + "; Siegeing", new Vector2(666, 423), Color.White);
                }
                else if (MapArray[SelectedX, SelectedY].ArmyInside.Moved)
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

            if (Menu == "Pause")
            {
                _spriteBatch.Draw(PauseMenu, new Vector2(237, 248), Color.White);

                _spriteBatch.DrawString(MenuFont, "New Game", new Vector2(246, 253), Color.White);
                _spriteBatch.DrawString(MenuFont, "Load Game", new Vector2(246, 293), Color.White);
                _spriteBatch.DrawString(MenuFont, "Save Game", new Vector2(246, 333), Color.White);
                _spriteBatch.DrawString(MenuFont, "Exit Game", new Vector2(246, 373), Color.White);
            }

            if (Menu == "Pick Country")
            {
                _spriteBatch.Draw(CountryMenu, new Vector2(257, 219), Color.White);
            }

            if (Menu == "Load Game")
            {
                _spriteBatch.Draw(LoadMenu, new Vector2(54, 235), Color.White);

                for (int i = SavesFP; i <= SavesEP; i++)
                {
                    _spriteBatch.DrawString(MenuFont, Saves[i].Substring(Saves[i].Length - 25, 21), new Vector2(89, ((i - SavesFP) * 36) + 242), Color.White);
                }

            }

            if (Menu == "Market")
            {
                _spriteBatch.Draw(MarketMenu, new Vector2(0, 117), Color.White);

                _spriteBatch.DrawString(MenuFont, "Purchase 75 Gold for:", new Vector2(134, 122), Color.White);
                _spriteBatch.DrawString(MenuFont, "Purchase 75 Food for:", new Vector2(134, 208), Color.White);
                _spriteBatch.DrawString(MenuFont, "Purchase 75 Wood for:", new Vector2(134, 294), Color.White);
                _spriteBatch.DrawString(MenuFont, "Purchase 75 Stone for:", new Vector2(134, 380), Color.White);
                _spriteBatch.DrawString(MenuFont, "Purchase 75 Metal for:", new Vector2(134, 466), Color.White);

                for (int i = 0; i < 5; i++)
                {
                    _spriteBatch.DrawString(MenuFont, "100 G", new Vector2(9, 162 + (i * 86)), Color.White);
                    _spriteBatch.DrawString(MenuFont, "100 F", new Vector2(134, 162 + (i * 86)), Color.White);
                    _spriteBatch.DrawString(MenuFont, "100 W", new Vector2(259, 162 + (i * 86)), Color.White);
                    _spriteBatch.DrawString(MenuFont, "100 S", new Vector2(384, 162 + (i * 86)), Color.White);
                    _spriteBatch.DrawString(MenuFont, "100 M", new Vector2(509, 162 + (i * 86)), Color.White);
                }
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        protected void CheckGameMenu(Point MousePoint)
        {
            if (CurrentMouseState.LeftButton != ButtonState.Pressed && LastMouseState.LeftButton == ButtonState.Pressed)
            {
                Rectangle NextTurnButton = new Rectangle(663, 332, 498, 36);
                Rectangle OpenMarketButton = new Rectangle(663, 378, 498, 36);
                Rectangle GainLandButton = new Rectangle(663, 464, 498, 36);
                Rectangle BuildStructureButton = new Rectangle(663, 544, 498, 36);
                Rectangle UpgradeStrucutreButton = new Rectangle(663, 624, 498, 36);
                Rectangle RaiseLevyArmyButton = new Rectangle(663, 286, 498, 36);

                if (NextTurnButton.Contains(MousePoint))
                {
                    NextTurn();
                }

                else if (OpenMarketButton.Contains(MousePoint))
                {
                    Menu = "Market";
                }

                else if (GainLandButton.Contains(MousePoint) && MapArray[SelectedX, SelectedY].CanColonise(MapArray, Countries, Player) && Countries[Player].CanAfford(200, 200, 200, 200, 200))
                {
                    Countries[Player].Pay(200, 200, 200, 200, 200);
                    MapArray[SelectedX, SelectedY].OwnedBy = Countries[Player];
                }

                else if (GainLandButton.Contains(MousePoint) && MapArray[SelectedX, SelectedY].CanAnnex(MapArray, Countries, Player))
                {
                    MapArray[SelectedX, SelectedY].ArmyInside.Sieging = true;
                }

                else if (BuildStructureButton.Contains(MousePoint) && MapArray[SelectedX, SelectedY].OwnedBy == Countries[Player] && MapArray[SelectedX, SelectedY].Structure == "Empty")
                {
                    Menu = "Build Structure";
                }

                else if (UpgradeStrucutreButton.Contains(MousePoint) && MapArray[SelectedX, SelectedY].OwnedBy == Countries[Player] && MapArray[SelectedX, SelectedY].Structure != "Empty" && MapArray[SelectedX, SelectedY].StructureLevel < 5 && Countries[Player].CanAfford(100 * (MapArray[SelectedX, SelectedY].StructureLevel + 1), 100 * (MapArray[SelectedX, SelectedY].StructureLevel + 1), 100 * (MapArray[SelectedX, SelectedY].StructureLevel + 1), 100 * (MapArray[SelectedX, SelectedY].StructureLevel + 1), 100 * (MapArray[SelectedX, SelectedY].StructureLevel + 1)))
                {
                    MapArray[SelectedX, SelectedY].StructureLevel += 1;
                    Countries[Player].Pay(100 * MapArray[SelectedX, SelectedY].StructureLevel, 100 * MapArray[SelectedX, SelectedY].StructureLevel, 100 * MapArray[SelectedX, SelectedY].StructureLevel, 100 * MapArray[SelectedX, SelectedY].StructureLevel, 100 * MapArray[SelectedX, SelectedY].StructureLevel);
                }

                else if (RaiseLevyArmyButton.Contains(MousePoint) && Countries[Player].Levy == null)
                {
                    Countries[Player].RaiseLevyArmy(MapArray, Countries, Player);
                }

                else if (RaiseLevyArmyButton.Contains(MousePoint) && Countries[Player].Levy != null)
                {
                    MapArray[Countries[Player].Levy.X, Countries[Player].Levy.Y].ArmyInside = null;
                    Countries[Player].Levy = null;
                    Selected = "Terrain";
                }

                else
                {
                    if ((Selected == "Standing" || Selected == "Levy") && MapArray[SelectedX, SelectedY].ArmyInside != null && MapArray[SelectedX, SelectedY].ArmyInside.OwnedBy == Countries[Player].Name && !MapArray[SelectedX, SelectedY].ArmyInside.Retreating && !MapArray[SelectedX, SelectedY].ArmyInside.Sieging)
                    {
                        int[] MoveLocation = MapArray[SelectedX, SelectedY].ArmyInside.PickMoveLocation(SelectedX, SelectedY, MousePoint, MapArray);
                        if (!(MoveLocation[0] == 0 && MoveLocation[1] == 0))
                        {
                            if (MapArray[MoveLocation[0], MoveLocation[1]].ArmyInside == null)
                            {
                                MapArray[SelectedX, SelectedY].ArmyInside.Move(MapArray, Countries, TerrainCosts, MoveLocation, CountryIndexes);
                            }
                            else if (MapArray[MoveLocation[0], MoveLocation[1]].ArmyInside != null && !MapArray[MoveLocation[0], MoveLocation[1]].ArmyInside.Retreating)
                            {
                                MapArray[SelectedX, SelectedY].ArmyInside.Attack(MapArray[MoveLocation[0], MoveLocation[1]].ArmyInside, MapArray, Countries, TerrainCosts, CountryIndexes);
                            }
                        }
                    }

                    foreach (Province Hex in MapArray)
                    {
                        if (Hex.ContainsMousePointer(MousePoint))
                        {
                            if (Hex.X == 0 && Hex.Y == 0)
                            {
                                Menu = "Pause";
                            }
                            else
                            {
                                SelectedX = Hex.X;
                                SelectedY = Hex.Y;
                                Selected = "Province";
                            }
                        }
                    }

                    foreach (Country C in Countries)
                    {
                        if (C.Name != "Unowned" && C.Standing.ContainsMousePointer(MousePoint))
                        {
                            SelectedX = C.Standing.X;
                            SelectedY = C.Standing.Y;
                            Selected = "Standing";
                        }
                        else if (C.Name != "Unowned" && C.Levy != null && C.Levy.ContainsMousePointer(MousePoint))
                        {
                            SelectedX = C.Levy.X;
                            SelectedY = C.Levy.Y;
                            Selected = "Levy";
                        }

                    }
                }
            }

            else if (!CurrentKeyboardState.IsKeyDown(Keys.M) && LastKeyboardState.IsKeyDown(Keys.M))
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

            else if (!CurrentKeyboardState.IsKeyDown(Keys.Enter) && LastKeyboardState.IsKeyDown(Keys.Enter))
            {
                NextTurn();
            }

            else if (!CurrentKeyboardState.IsKeyDown(Keys.Escape) && LastKeyboardState.IsKeyDown(Keys.Escape))
            {
                Menu = "Pause";
            }
        }

        protected void CheckBuildStructureMenu(Point MousePoint)
        {
            if (CurrentMouseState.LeftButton != ButtonState.Pressed && LastMouseState.LeftButton == ButtonState.Pressed)
            {
                Rectangle BuildSettlement = new Rectangle(221, 312, 40, 40);
                Rectangle BuildFort = new Rectangle(265, 312, 40, 40);
                Rectangle BuildFarm = new Rectangle(309, 312, 40, 40);
                Rectangle BuildMine = new Rectangle(353, 312, 40, 40);
                Rectangle BuildForester = new Rectangle(397, 312, 40, 40);
                Rectangle CloseBuildMenu = new Rectangle(447, 312, 14, 14);
                if (BuildSettlement.Contains(MousePoint) && Countries[Player].CanAfford(100, 100, 100, 100, 100))
                {
                    MapArray[SelectedX, SelectedY].Structure = "Settlement";
                    MapArray[SelectedX, SelectedY].StructureLevel = 1;
                    Countries[Player].Pay(100, 100, 100, 100, 100);
                    Menu = "Game";
                }
                else if (BuildFort.Contains(MousePoint) && Countries[Player].CanAfford(100, 100, 100, 100, 100))
                {
                    MapArray[SelectedX, SelectedY].Structure = "Fort";
                    MapArray[SelectedX, SelectedY].StructureLevel = 1;
                    Countries[Player].Pay(100, 100, 100, 100, 100);
                    Menu = "Game";
                }
                else if (BuildFarm.Contains(MousePoint) && Countries[Player].CanAfford(100, 100, 100, 100, 100))
                {
                    MapArray[SelectedX, SelectedY].Structure = "Farm";
                    MapArray[SelectedX, SelectedY].StructureLevel = 1;
                    Countries[Player].Pay(100, 100, 100, 100, 100);
                    Menu = "Game";
                }
                else if (BuildMine.Contains(MousePoint) && Countries[Player].CanAfford(100, 100, 100, 100, 100))
                {
                    MapArray[SelectedX, SelectedY].Structure = "Mine";
                    MapArray[SelectedX, SelectedY].StructureLevel = 1;
                    Countries[Player].Pay(100, 100, 100, 100, 100);
                    Menu = "Game";
                }
                else if (BuildForester.Contains(MousePoint) && Countries[Player].CanAfford(100, 100, 100, 100, 100))
                {
                    MapArray[SelectedX, SelectedY].Structure = "Forester";
                    MapArray[SelectedX, SelectedY].StructureLevel = 1;
                    Countries[Player].Pay(100, 100, 100, 100, 100);
                    Menu = "Game";
                }
                else if (CloseBuildMenu.Contains(MousePoint))
                {
                    Menu = "Game";
                }
            }
            else if (!CurrentKeyboardState.IsKeyDown(Keys.Escape) && LastKeyboardState.IsKeyDown(Keys.Escape))
            {
                Menu = "Game";
            }
        }

        protected void CheckPauseMenu(Point MousePoint)
        {
            if (CurrentMouseState.LeftButton != ButtonState.Pressed && LastMouseState.LeftButton == ButtonState.Pressed)
            {
                Rectangle NewGameButton = new Rectangle(243, 254, 168, 36);
                Rectangle LoadGameButton = new Rectangle(243, 294, 168, 36);
                Rectangle SaveGameButton = new Rectangle(243, 334, 168, 36);
                Rectangle ExitGameButton = new Rectangle(243, 374, 168, 36);
                Rectangle CloseMenuButton = new Rectangle(425, 254, 14, 14);
                if (NewGameButton.Contains(MousePoint))
                {
                    Menu = "Pick Country";
                }
                else if (LoadGameButton.Contains(MousePoint))
                {
                    RefreshSavesList();
                    Menu = "Load Game";
                }
                else if (SaveGameButton.Contains(MousePoint))
                {
                    SaveGame();
                    Menu = "Game";
                }
                else if (ExitGameButton.Contains(MousePoint))
                {
                    Exit();
                }
                else if (CloseMenuButton.Contains(MousePoint))
                {
                    Menu = "Game";
                }
            }
            else if (!CurrentKeyboardState.IsKeyDown(Keys.Escape) && LastKeyboardState.IsKeyDown(Keys.Escape))
            {
                Menu = "Game";
            }
        }

        protected void CheckPickCountryMenu(Point MousePoint)
        {
            if (CurrentMouseState.LeftButton != ButtonState.Pressed && LastMouseState.LeftButton == ButtonState.Pressed)
            {
                Rectangle BackButton = new Rectangle(263, 225, 14, 14);
                Rectangle CloseCountryMenu = new Rectangle(381, 225, 14, 14);
                if (BackButton.Contains(MousePoint))
                {
                    Menu = "Pause";
                }
                else if (CloseCountryMenu.Contains(MousePoint))
                {
                    Menu = "Game";
                }
                else
                {
                    Rectangle[] CountryOptions = new Rectangle[10];
                    for (int i = 0; i < 10; i++)
                    {
                        int X;
                        int Y;
                        if (i % 2 == 0)
                        {
                            X = 287;
                            Y = (((i / 2) % 5) * 44) + 225;
                        }
                        else
                        {
                            X = 331;
                            Y = ((((i - 1) / 2) % 5) * 44) + 225;
                        }
                        CountryOptions[i] = new Rectangle(X, Y, 40, 40);
                        if (CountryOptions[i].Contains(MousePoint))
                        {
                            Player = i + 1;
                            string Save = Path.GetFullPath("Saves/NewSave.txt");
                            Save = Save.Remove(Save.Length - 41, 24);
                            LoadSave(Save);
                            Menu = "Game";
                        }
                    }
                }
            }
            else if (!CurrentKeyboardState.IsKeyDown(Keys.Escape) && LastKeyboardState.IsKeyDown(Keys.Escape))
            {
                Menu = "Game";
            }
        }

        protected void CheckLoadGameMenu(Point MousePoint)
        {
            if (CurrentMouseState.LeftButton != ButtonState.Pressed && LastMouseState.LeftButton == ButtonState.Pressed)
            {
                Rectangle BackButton = new Rectangle(60, 241, 14, 14);
                Rectangle CloseLoadMenu = new Rectangle(582, 241, 14, 14);
                Rectangle UpButton = new Rectangle(558, 241, 14, 14);
                Rectangle DownButton = new Rectangle(558, 411, 14, 14);
                if (BackButton.Contains(MousePoint))
                {
                    Menu = "Pause";
                }
                else if (CloseLoadMenu.Contains(MousePoint))
                {
                    Menu = "Game";
                }
                else if (UpButton.Contains(MousePoint) && SavesFP > 0)
                {
                    SavesFP--;
                    SavesEP--;
                }
                else if (DownButton.Contains(MousePoint) && SavesEP < Saves.Count - 1)
                {
                    SavesFP++;
                    SavesEP++;
                }
                else
                {
                    int ListPosition = 0;
                    for (int i = SavesFP; i <= SavesEP; i++)
                    {
                        Rectangle Button = new Rectangle(86, 243 + (ListPosition * 36), 466, 36);
                        ListPosition++;
                        if (Button.Contains(MousePoint))
                        {
                            LoadSave(Saves[i]);
                            Menu = "Game";
                        }
                    }
                }
            }
            else if (!CurrentKeyboardState.IsKeyDown(Keys.Escape) && LastKeyboardState.IsKeyDown(Keys.Escape))
            {
                Menu = "Game";
            }
        }

        protected void CheckMarketMenu(Point MousePoint)
        {
            if (CurrentMouseState.LeftButton != ButtonState.Pressed && LastMouseState.LeftButton == ButtonState.Pressed)
            {
                Rectangle CloseMarketButton = new Rectangle(637, 123, 14, 14);
                if (CloseMarketButton.Contains(MousePoint))
                {
                    Menu = "Game";
                }
                else
                {
                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            bool Purchased = false;
                            Rectangle PurchaseButton = new Rectangle(6 + (i * 125), 163 + (j * 86), 121, 36);
                            if (PurchaseButton.Contains(MousePoint))
                            {
                                if (i == 0)
                                {
                                    if (Countries[Player].CanAfford(75, 0, 0, 0, 0))
                                    {
                                        Countries[Player].Pay(75, 0, 0, 0, 0);
                                        Purchased = true;
                                    }
                                }
                                else if (i == 1)
                                {
                                    if (Countries[Player].CanAfford(0, 0, 0, 75, 0))
                                    {
                                        Countries[Player].Pay(0, 0, 0, 75, 0);
                                        Purchased = true;
                                    }
                                }
                                else if (i == 2)
                                {
                                    if (Countries[Player].CanAfford(0, 75, 0, 0, 0))
                                    {
                                        Countries[Player].Pay(0, 75, 0, 0, 0);
                                        Purchased = true;
                                    }
                                }
                                else if (i == 3)
                                {
                                    if (Countries[Player].CanAfford(0, 0, 75, 0, 0))
                                    {
                                        Countries[Player].Pay(0, 0, 75, 0, 0);
                                        Purchased = true;
                                    }
                                }
                                else if (i == 4)
                                {
                                    if (Countries[Player].CanAfford(0, 0, 0, 0, 75))
                                    {
                                        Countries[Player].Pay(0, 0, 0, 0, 75);
                                        Purchased = true;
                                    }
                                }
                            }

                            if (Purchased)
                            {
                                if (j == 0)
                                {
                                    Countries[Player].Gold += 100;
                                }
                                else if (j == 1)
                                {
                                    Countries[Player].Food += 100;
                                }
                                else if (j == 2)
                                {
                                    Countries[Player].Wood += 100;
                                }
                                else if (j == 3)
                                {
                                    Countries[Player].Stone += 100;
                                }
                                else if (j == 4)
                                {
                                    Countries[Player].Metal += 100;
                                }
                            }

                        }
                    }
                }
            }
            else if (!CurrentKeyboardState.IsKeyDown(Keys.Escape) && LastKeyboardState.IsKeyDown(Keys.Escape))
            {
                Menu = "Game";
            }
        }

        protected void LoadSave(string Save)
        {
            using (StreamReader sr = new StreamReader(Save))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Length == 35)
                    {
                        int X = Convert.ToInt32(line.Substring(0, 2));
                        int Y = Convert.ToInt32(line.Substring(2, 2));
                        int StructureLevel = Convert.ToInt32(line.Substring(4, 1));
                        string Structure = (line.Substring(5, 10)).Trim();
                        Country OwnedBy = Countries[Convert.ToInt32(line.Substring(15, 2))];
                        string Terrain = (line.Substring(17, 18)).Trim();

                        MapArray[X, Y] = new Province(X, Y, StructureLevel, Structure, OwnedBy, Terrain);
                    }
                    else if (line.Length == 59)
                    {
                        int ID = Convert.ToInt32(line.Substring(0, 2));
                        string Name = (line.Substring(2, 20)).Trim();
                        int CX = Convert.ToInt32(line.Substring(22, 2));
                        int CY = Convert.ToInt32(line.Substring(24, 2));
                        int Gold = Convert.ToInt32(line.Substring(26, 6));
                        int Wood = Convert.ToInt32(line.Substring(32, 6));
                        int Stone = Convert.ToInt32(line.Substring(38, 6));
                        int Food = Convert.ToInt32(line.Substring(44, 6));
                        int Metal = Convert.ToInt32(line.Substring(50, 6));
                        string CountryCode = line.Substring(56, 3);

                        Countries[ID] = new Country(true, Name, CX, CY, Gold, Wood, Stone, Food, Metal, CountryCode);
                    }
                    else if (line.Length == 42)
                    {
                        int ID = Convert.ToInt32(line.Substring(0, 2));
                        int X = Convert.ToInt32(line.Substring(2, 2));
                        int Y = Convert.ToInt32(line.Substring(4, 2));
                        int Infantry = Convert.ToInt32(line.Substring(6, 6));
                        int Archers = Convert.ToInt32(line.Substring(12, 6));
                        int Cavalry = Convert.ToInt32(line.Substring(18, 6));
                        bool Moved = Convert.ToBoolean((line.Substring(24, 5)).Trim());
                        bool Sieging = Convert.ToBoolean((line.Substring(29, 5)).Trim());
                        int SiegeProgress = Convert.ToInt32((line.Substring(34, 3)));
                        bool Retreating = Convert.ToBoolean((line.Substring(37, 5)).Trim());

                        Countries[ID].Standing = new StandingArmy(X, Y, Infantry, Archers, Cavalry, Countries[ID].Name, Moved, Sieging, Retreating, SiegeProgress);
                    }
                    else if (line.Length == 22)
                    {
                        int ID = Convert.ToInt32(line.Substring(0, 2));
                        int X = Convert.ToInt32(line.Substring(2, 2));
                        int Y = Convert.ToInt32(line.Substring(4, 2));
                        int Infantry = Convert.ToInt32(line.Substring(6, 6));
                        bool Moved = bool.Parse((line.Substring(12, 5)).Trim());
                        bool Retreating = bool.Parse((line.Substring(17, 5)).Trim());

                        Countries[ID].Levy = new LevyArmy(X, Y, Infantry, Countries[ID].Name, Moved, Retreating);
                    }
                    else if (line.Length == 2)
                    {
                        Player = Convert.ToInt32(line.Substring(0, 2));
                    }
                }
            }

            Countries[Player].IsAI = false;
            SelectedX = Countries[Player].CapitalX;
            SelectedY = Countries[Player].CapitalY;
            Selected = "Province";

            foreach (Country C in Countries)
            {
                MapArray[C.Standing.X, C.Standing.Y].ArmyInside = C.Standing;

                if (C.Levy != null)
                {
                    MapArray[C.Levy.X, C.Levy.Y].ArmyInside = C.Levy;
                }
            }
        }

        protected void SaveGame()
        {
            string Save = Path.GetFullPath("Saves/NewSave.txt");
            Save = Save.Remove(Save.Length - 41, 24);
            Save = Save.Remove(Save.Length - 11, 11);
            string Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            Date = Date.Replace('/', '_');
            Date = Date.Replace(':', '.');
            Save += Countries[Player].CountryCode + "(" + Date + ").txt";
            using (StreamWriter sw = new StreamWriter(Save))
            {
                foreach (Country c in Countries)
                {
                    string line = "";

                    line += Convert.ToString(CountryIndexes[c.Name]);
                    if (line.Length == 1)
                    {
                        line = line.Insert(0, "0");
                    }

                    line += c.Name;
                    while (line.Length < 22)
                    {
                        line = line.Insert(2, " ");
                    }

                    line += c.CapitalX;
                    if (line.Length == 23)
                    {
                        line = line.Insert(22, "0");
                    }

                    line += c.CapitalY;
                    if (line.Length == 25)
                    {
                        line = line.Insert(24, "0");
                    }

                    if (c.Gold > 999999)
                    {
                        line += "999999";
                    }
                    else
                    {
                        line += Convert.ToString(c.Gold);
                        while (line.Length < 32)
                        {
                            line = line.Insert(26, "0");
                        }
                    }

                    if (c.Wood > 999999)
                    {
                        line += "999999";
                    }
                    else
                    {
                        line += Convert.ToString(c.Wood);
                        while (line.Length < 38)
                        {
                            line = line.Insert(32, "0");
                        }
                    }

                    if (c.Stone > 999999)
                    {
                        line += "999999";
                    }
                    else
                    {
                        line += Convert.ToString(c.Stone);
                        while (line.Length < 44)
                        {
                            line = line.Insert(38, "0");
                        }
                    }

                    if (c.Food > 999999)
                    {
                        line += "999999";
                    }
                    else
                    {
                        line += Convert.ToString(c.Food);
                        while (line.Length < 50)
                        {
                            line = line.Insert(44, "0");
                        }
                    }

                    if (c.Metal > 999999)
                    {
                        line += "999999";
                    }
                    else
                    {
                        line += Convert.ToString(c.Metal);
                        while (line.Length < 56)
                        {
                            line = line.Insert(50, "0");
                        }
                    }

                    line += c.CountryCode;

                    sw.WriteLine(line);
                }

                foreach (Country c in Countries)
                {
                    string line = "";

                    line += Convert.ToString(CountryIndexes[c.Name]);
                    if (line.Length == 1)
                    {
                        line = line.Insert(0, "0");
                    }

                    line += Convert.ToString(c.Standing.X);
                    if (line.Length == 3)
                    {
                        line = line.Insert(2, "0");
                    }

                    line += Convert.ToString(c.Standing.Y);
                    if (line.Length == 5)
                    {
                        line = line.Insert(4, "0");
                    }

                    line += Convert.ToString(c.Standing.Infantry);
                    while (line.Length < 12)
                    {
                        line = line.Insert(6, "0");
                    }

                    line += Convert.ToString(c.Standing.Archers);
                    while (line.Length < 18)
                    {
                        line = line.Insert(12, "0");
                    }

                    line += Convert.ToString(c.Standing.Cavalry);
                    while (line.Length < 24)
                    {
                        line = line.Insert(18, "0");
                    }

                    if (c.Standing.Moved)
                    {
                        line += " True";
                    }
                    else if (!c.Standing.Moved)
                    {
                        line += "False";
                    }

                    if (c.Standing.Sieging)
                    {
                        line += " True";
                    }
                    else if (!c.Standing.Sieging)
                    {
                        line += "False";
                    }

                    line += Convert.ToString(c.Standing.SiegeProgress);
                    while (line.Length < 37)
                    {
                        line = line.Insert(34, "0");
                    }

                    sw.WriteLine(line);
                }

                foreach (Country c in Countries)
                {
                    if (c.Levy != null)
                    {
                        string line = "";

                        line += Convert.ToString(CountryIndexes[c.Name]);
                        if (line.Length == 1)
                        {
                            line = line.Insert(0, "0");
                        }

                        line += Convert.ToString(c.Levy.X);
                        if (line.Length == 3)
                        {
                            line = line.Insert(2, "0");
                        }

                        line += Convert.ToString(c.Levy.Y);
                        if (line.Length == 5)
                        {
                            line = line.Insert(4, "0");
                        }

                        line += Convert.ToString(c.Levy.Infantry);
                        while (line.Length < 12)
                        {
                            line = line.Insert(6, "0");
                        }

                        if (c.Levy.Moved)
                        {
                            line += " True";
                        }
                        else if (!c.Levy.Moved)
                        {
                            line += "False";
                        }

                        if (c.Levy.Retreating)
                        {
                            line += " True";
                        }
                        else if (!c.Levy.Retreating)
                        {
                            line += "False";
                        }

                        sw.WriteLine(line);
                    }
                }

                foreach (Province p in MapArray)
                {
                    string line = "";

                    line += Convert.ToString(p.X);
                    if (line.Length == 1)
                    {
                        line = line.Insert(0, "0");
                    }

                    line += Convert.ToString(p.Y);
                    if (line.Length == 3)
                    {
                        line = line.Insert(2, "0");
                    }

                    line += Convert.ToString(p.StructureLevel);

                    line += p.Structure;
                    while (line.Length < 15)
                    {
                        line = line.Insert(5, " ");
                    }

                    line += Convert.ToString(CountryIndexes[p.OwnedBy.Name]);
                    if (line.Length == 16)
                    {
                        line = line.Insert(15, "0");
                    }

                    line += p.Terrain;
                    while (line.Length < 35)
                    {
                        line = line.Insert(17, " ");
                    }

                    sw.WriteLine(line);
                }

                if (Player < 10)
                {
                    sw.WriteLine("0" + Convert.ToString(Player));
                }
                else
                {
                    sw.WriteLine(Convert.ToString(Player));
                }
            }
        }

        protected void NextTurn()
        {
            foreach (Country C in Countries)
            {
                if (C.Levy == null)
                {
                    C.Gold += 50;
                    C.Metal += 50;
                    C.Stone += 50;
                    C.Wood += 50;
                    C.Food += 50;
                    if (!C.Standing.Retreating && !C.Standing.Sieging)
                    {
                        C.Standing.Infantry += 50;
                    }
                }
            }
            foreach (Province P in MapArray)
            {
                if (P.OwnedBy.Levy == null)
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
                        if (!P.OwnedBy.Standing.Retreating && !P.OwnedBy.Standing.Sieging)
                        {
                            P.OwnedBy.Standing.Infantry += 50 * P.StructureLevel;
                            P.OwnedBy.Standing.Archers += 25 * P.StructureLevel;
                            P.OwnedBy.Standing.Cavalry += 25 * P.StructureLevel;
                        }
                    }
                }
            }
            foreach (Country c in Countries)
            {
                c.NextTurn(MapArray, Countries, CountryIndexes, Player, TerrainCosts);
            }
        }

        protected void RefreshSavesList()
        {
            string SavesPath = Path.GetFullPath("Saves/NewSave.txt");
            SavesPath = SavesPath.Remove(SavesPath.Length - 41, 24);
            SavesPath = SavesPath.Remove(SavesPath.Length - 11, 11);

            Saves.Clear();
            string[] TempSaves = Directory.GetFiles(SavesPath);
            foreach (string s in TempSaves)
            {
                if (!s.Contains("NewSave"))
                {
                    Saves.Add(s);
                }
            }

            SavesFP = 0;
            if (Saves.Count > 5)
            {
                SavesEP = 4;
            }
            else
            {
                SavesEP = Saves.Count - 1;
            }
        }
    }
}
