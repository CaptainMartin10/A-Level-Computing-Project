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
        public int SelectedX, SelectedY = 0;
        public SpriteFont MenuFont;
        public Texture2D Background, Fort, Settlement, Farm, Forester, Mine, BuildStructureMenu;
        public int Player = 8;
        public int Turn = 1;
        public MouseState CurrentMouseState, LastMouseState;
        public Dictionary<string, int> FarmProduction = new Dictionary<string, int>();
        public Dictionary<string, int> ForesterProduction = new Dictionary<string, int>();
        public Dictionary<string, int> MineProduction = new Dictionary<string, int>();
        public string Menu = "Game";

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

            if (CurrentMouseState.LeftButton != ButtonState.Pressed && LastMouseState.LeftButton == ButtonState.Pressed)
            {
                if (Menu == "Game")
                {
                    foreach (Province Hex in MapArray)
                    {
                        if (Hex.ContainsMousePointer(mousePoint))
                        {
                            SelectedX = Hex.X;
                            SelectedY = Hex.Y;
                        }
                    }

                    Rectangle NextTurnButton = new Rectangle(663, 624, 498, 36);
                    if (NextTurnButton.Contains(mousePoint))
                    {
                        Turn++;
                        foreach (Country C in Countries)
                        {
                            C.Gold += 50;
                            C.Metal += 50;
                            C.Stone += 50;
                            C.Wood += 50;
                            C.Food += 50;
                        }
                        foreach (Province P in MapArray)
                        {
                            if (P.Structure == "Settlement")
                            {
                                P.OwnedBy.Gold += 100 * P.StructureLevel;
                            }
                            if (P.Structure == "Mine")
                            {
                                P.OwnedBy.Stone += MineProduction[P.Terrain] * P.StructureLevel;
                                P.OwnedBy.Metal += MineProduction[P.Terrain] * P.StructureLevel;
                            }
                            if (P.Structure == "Farm")
                            {
                                P.OwnedBy.Food += FarmProduction[P.Terrain] * P.StructureLevel;
                            }
                            if (P.Structure == "Forester")
                            {
                                P.OwnedBy.Wood += ForesterProduction[P.Terrain] * P.StructureLevel;
                            }
                        }
                    }

                    Rectangle BuildStructureButton = new Rectangle(663, 498, 498, 36);
                    if (BuildStructureButton.Contains(mousePoint) && MapArray[SelectedX, SelectedY].OwnedBy == Countries[Player] && MapArray[SelectedX, SelectedY].Structure == "Empty")
                    {
                        Menu = "Build Structure";
                    }

                    Rectangle UpgradeStrucutreButton = new Rectangle(663, 578, 498, 36);
                    if (UpgradeStrucutreButton.Contains(mousePoint) && MapArray[SelectedX, SelectedY].OwnedBy == Countries[Player] && MapArray[SelectedX, SelectedY].Structure != "Empty" && MapArray[SelectedX, SelectedY].StructureLevel < 5 && Countries[Player].Gold > 100 * (MapArray[SelectedX, SelectedY].StructureLevel + 1) && Countries[Player].Food > 100 * (MapArray[SelectedX, SelectedY].StructureLevel + 1) && Countries[Player].Wood > 100 * (MapArray[SelectedX, SelectedY].StructureLevel + 1) && Countries[Player].Stone > 100 * (MapArray[SelectedX, SelectedY].StructureLevel + 1) && Countries[Player].Metal > 100 * (MapArray[SelectedX, SelectedY].StructureLevel + 1))
                    {
                        MapArray[SelectedX, SelectedY].StructureLevel += 1;
                        Countries[Player].Gold -= 100 * MapArray[SelectedX, SelectedY].StructureLevel;
                        Countries[Player].Food -= 100 * MapArray[SelectedX, SelectedY].StructureLevel;
                        Countries[Player].Wood -= 100 * MapArray[SelectedX, SelectedY].StructureLevel;
                        Countries[Player].Stone -= 100 * MapArray[SelectedX, SelectedY].StructureLevel;
                        Countries[Player].Metal -= 100 * MapArray[SelectedX, SelectedY].StructureLevel;
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

            LastMouseState = CurrentMouseState;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            _spriteBatch.Draw(Background, new Vector2(0, 0), Color.White);
            _spriteBatch.DrawString(MenuFont, "Mouse Coordinates: " + CurrentMouseState.X + " , " + CurrentMouseState.Y, new Vector2(666, 5), Color.White);

            _spriteBatch.DrawString(MenuFont, "Player: " + Countries[Player].Name, new Vector2(666, 51), Color.White);
            _spriteBatch.DrawString(MenuFont, "Gold: " + Countries[Player].Gold, new Vector2(666, 91), Color.White);
            _spriteBatch.DrawString(MenuFont, "Food: " + Countries[Player].Food, new Vector2(666, 131), Color.White);
            _spriteBatch.DrawString(MenuFont, "Wood: " + Countries[Player].Wood, new Vector2(666, 171), Color.White);
            _spriteBatch.DrawString(MenuFont, "Stone: " + Countries[Player].Stone, new Vector2(666, 211), Color.White);
            _spriteBatch.DrawString(MenuFont, "Metal: " + Countries[Player].Metal, new Vector2(666, 251), Color.White);
            _spriteBatch.DrawString(MenuFont, "Standing Army Location: " + Countries[Player].Standing.X + " , " + Countries[Player].Standing.Y, new Vector2(666, 291), Color.White);

            if(Countries[Player].Levy != null)
            {
                _spriteBatch.DrawString(MenuFont, "Levy Army Location: " + Countries[Player].Levy.X + " , " + Countries[Player].Levy.Y, new Vector2(666, 331), Color.White);
            }
            else
            {
                _spriteBatch.DrawString(MenuFont, "Raise Levy Army", new Vector2(666, 331), Color.White);
            }

            _spriteBatch.DrawString(MenuFont, "Province Coordinates: " + SelectedX + " , " + SelectedY, new Vector2(666, 377), Color.White);
            _spriteBatch.DrawString(MenuFont, "Owned By: " + (MapArray[SelectedX, SelectedY].OwnedBy).Name, new Vector2(666, 417), Color.White);
            _spriteBatch.DrawString(MenuFont, "Terrain: " + MapArray[SelectedX, SelectedY].Terrain, new Vector2(666, 457), Color.White);
            
            if (MapArray[SelectedX, SelectedY].Structure != "Empty" && MapArray[SelectedX, SelectedY].OwnedBy.IsAI == false)
            {
                _spriteBatch.DrawString(MenuFont, MapArray[SelectedX, SelectedY].Structure, new Vector2(666, 497), Color.White);
                _spriteBatch.DrawString(MenuFont, "Level: " + MapArray[SelectedX, SelectedY].StructureLevel, new Vector2(666, 537), Color.White);
                _spriteBatch.DrawString(MenuFont, "Upgrade", new Vector2(666, 577), Color.White);
            }
            else if (MapArray[SelectedX, SelectedY].Structure != "Empty" && MapArray[SelectedX, SelectedY].OwnedBy.IsAI == true)
            {
                _spriteBatch.DrawString(MenuFont, MapArray[SelectedX, SelectedY].Structure, new Vector2(666, 497), Color.White);
                _spriteBatch.DrawString(MenuFont, "Level: " + MapArray[SelectedX, SelectedY].StructureLevel, new Vector2(666, 537), Color.White);
                _spriteBatch.DrawString(MenuFont, "Cannot Upgrade", new Vector2(666, 577), Color.White);
            }
            else if (MapArray[SelectedX, SelectedY].Structure == "Empty" && MapArray[SelectedX, SelectedY].OwnedBy.IsAI == false)
            {
                _spriteBatch.DrawString(MenuFont, "Build Structure", new Vector2(666, 497), Color.White);
                _spriteBatch.DrawString(MenuFont, "Empty", new Vector2(666, 537), Color.White);
                _spriteBatch.DrawString(MenuFont, "Empty", new Vector2(666, 577), Color.White);
            }
            else if (MapArray[SelectedX, SelectedY].Structure == "Empty" && MapArray[SelectedX, SelectedY].OwnedBy.IsAI == true)
            {
                _spriteBatch.DrawString(MenuFont, "Empty", new Vector2(666, 497), Color.White);
                _spriteBatch.DrawString(MenuFont, "Empty", new Vector2(666, 537), Color.White);
                _spriteBatch.DrawString(MenuFont, "Empty", new Vector2(666, 577), Color.White);
            }

            _spriteBatch.DrawString(MenuFont, "Turn: " + Turn + ", Next Turn", new Vector2(666, 623 ), Color.White);

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

            if (Menu == "Build Structure")
            {
                _spriteBatch.Draw(BuildStructureMenu, new Vector2(215, 306), Color.White);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
