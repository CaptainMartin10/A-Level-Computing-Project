using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System;
using System.Windows;


namespace A_Level_Computing_Project
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public Province[,] MapArray = new Province[24, 18];
        public Country[] Countries = new Country[11];
        public int CurrentHexX = 0;
        public int CurrentHexY = 0;
        public string MouseCoords = "Mouse Coordinates: 0 , 0";
        public SpriteFont MenuFont;
        public Texture2D Background, Fort, Settlement, Farm, Forester, Mine;
        public int Player = 7;
        public int Turn = 1;
        public bool MouseUp = true;

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

            Countries[0] = new Country(true, "Unowned");
            Countries[1] = new Country(true, "Lindon");
            Countries[2] = new Country(true, "Blue Mountains North");
            Countries[3] = new Country(true, "Blue Mountains South");
            Countries[4] = new Country(true, "Shire");
            Countries[5] = new Country(true, "Rangers of the North");
            Countries[6] = new Country(true, "Rivendell");
            Countries[7] = new Country(true, "Breeland");
            Countries[8] = new Country(true, "Dunland");
            Countries[9] = new Country(true, "Isengard");
            Countries[10] = new Country(true, "Gundabad");

            Countries[Player].IsAI = false;

            using (StreamReader sr = new StreamReader("F:/Project/Content/Provinces.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    MapArray[Convert.ToInt32(line.Substring(0, 2)), Convert.ToInt32(line.Substring(2, 2))] = new Province(Convert.ToInt32(line.Substring(0, 2)), Convert.ToInt32(line.Substring(2, 2)));
                    MapArray[Convert.ToInt32(line.Substring(0, 2)), Convert.ToInt32(line.Substring(2, 2))].StructureLevel = Convert.ToInt32(line.Substring(4, 1));
                    MapArray[Convert.ToInt32(line.Substring(0, 2)), Convert.ToInt32(line.Substring(2, 2))].StructureGarrison = Convert.ToInt32(line.Substring(5, 4));
                    MapArray[Convert.ToInt32(line.Substring(0, 2)), Convert.ToInt32(line.Substring(2, 2))].StructureProduction = Convert.ToInt32(line.Substring(9, 4));
                    MapArray[Convert.ToInt32(line.Substring(0, 2)), Convert.ToInt32(line.Substring(2, 2))].Structure = (line.Substring(13, 10)).Trim();
                    MapArray[Convert.ToInt32(line.Substring(0, 2)), Convert.ToInt32(line.Substring(2, 2))].OwnedBy = Countries[Convert.ToInt32(line.Substring(23, 2))];
                    MapArray[Convert.ToInt32(line.Substring(0, 2)), Convert.ToInt32(line.Substring(2, 2))].Terrain = (line.Substring(25, 18)).Trim();
                }
            }

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
            MenuFont = Content.Load<SpriteFont>("MenuFont");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            var mouseState = Mouse.GetState();
            Point mousePoint = new Point(mouseState.X, mouseState.Y);
            MouseCoords = "Mouse Coordinates: " + mouseState.X + " , " + mouseState.Y;
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                foreach (Province Hex in MapArray)
                {
                    if (Hex.X % 2 == 0)
                    {
                        Rectangle HexBounds1 = new Rectangle((Hex.X * 27) + 9, (Hex.Y * 36), 18, 36);
                        Rectangle HexBounds2 = new Rectangle((Hex.X * 27) + 8, (Hex.Y * 36) + 1, 20, 34);
                        Rectangle HexBounds3 = new Rectangle((Hex.X * 27) + 7, (Hex.Y * 36) + 3, 22, 30);
                        Rectangle HexBounds4 = new Rectangle((Hex.X * 27) + 6, (Hex.Y * 36) + 5, 24, 26);
                        Rectangle HexBounds5 = new Rectangle((Hex.X * 27) + 5, (Hex.Y * 36) + 7, 26, 22);
                        Rectangle HexBounds6 = new Rectangle((Hex.X * 27) + 4, (Hex.Y * 36) + 9, 28, 18);
                        Rectangle HexBounds7 = new Rectangle((Hex.X * 27) + 3, (Hex.Y * 36) + 1, 30, 14);
                        Rectangle HexBounds8 = new Rectangle((Hex.X * 27) + 2, (Hex.Y * 36) + 3, 32, 10);
                        Rectangle HexBounds9 = new Rectangle((Hex.X * 27) + 1, (Hex.Y * 36) + 5, 34, 6);
                        Rectangle HexBounds10 = new Rectangle((Hex.X * 27), (Hex.Y * 36) + 7, 36, 2);
                        if (HexBounds1.Contains(mousePoint) || HexBounds2.Contains(mousePoint) || HexBounds3.Contains(mousePoint) || HexBounds4.Contains(mousePoint) || HexBounds5.Contains(mousePoint) || HexBounds6.Contains(mousePoint) || HexBounds7.Contains(mousePoint) || HexBounds8.Contains(mousePoint) || HexBounds9.Contains(mousePoint) || HexBounds10.Contains(mousePoint))
                        {
                            CurrentHexX = Hex.X;
                            CurrentHexY = Hex.Y;
                        }
                    }
                    else if (Hex.X % 2 == 1)
                    {
                        Rectangle HexBounds1 = new Rectangle((Hex.X * 27) + 9, (Hex.Y * 36) + 18, 18, 36);
                        Rectangle HexBounds2 = new Rectangle((Hex.X * 27) + 8, (Hex.Y * 36) + 19, 20, 34);
                        Rectangle HexBounds3 = new Rectangle((Hex.X * 27) + 7, (Hex.Y * 36) + 21, 22, 30);
                        Rectangle HexBounds4 = new Rectangle((Hex.X * 27) + 6, (Hex.Y * 36) + 23, 24, 26);
                        Rectangle HexBounds5 = new Rectangle((Hex.X * 27) + 5, (Hex.Y * 36) + 25, 26, 22);
                        Rectangle HexBounds6 = new Rectangle((Hex.X * 27) + 4, (Hex.Y * 36) + 27, 28, 18);
                        Rectangle HexBounds7 = new Rectangle((Hex.X * 27) + 3, (Hex.Y * 36) + 29, 30, 14);
                        Rectangle HexBounds8 = new Rectangle((Hex.X * 27) + 2, (Hex.Y * 36) + 31, 32, 10);
                        Rectangle HexBounds9 = new Rectangle((Hex.X * 27) + 1, (Hex.Y * 36) + 33, 34, 6);
                        Rectangle HexBounds10 = new Rectangle((Hex.X * 27), (Hex.Y * 36) + 35, 36, 2);
                        if (HexBounds1.Contains(mousePoint) || HexBounds2.Contains(mousePoint) || HexBounds3.Contains(mousePoint) || HexBounds4.Contains(mousePoint) || HexBounds5.Contains(mousePoint) || HexBounds6.Contains(mousePoint) || HexBounds7.Contains(mousePoint) || HexBounds8.Contains(mousePoint) || HexBounds9.Contains(mousePoint) || HexBounds10.Contains(mousePoint))
                        {
                            CurrentHexX = Hex.X;
                            CurrentHexY = Hex.Y;
                        }
                    }
                }

                Rectangle NextTurnButton = new Rectangle(663, 624, 498, 36);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            _spriteBatch.Draw(Background, new Vector2(0, 0), Color.White);
            _spriteBatch.DrawString(MenuFont, MouseCoords, new Vector2(666, 5), Color.White);

            _spriteBatch.DrawString(MenuFont, "Player: " + Countries[Player].Name, new Vector2(666, 51), Color.White);
            _spriteBatch.DrawString(MenuFont, "Gold: " + Countries[Player].Gold, new Vector2(666, 91), Color.White);
            _spriteBatch.DrawString(MenuFont, "Food: " + Countries[Player].Food, new Vector2(666, 131), Color.White);
            _spriteBatch.DrawString(MenuFont, "Wood: " + Countries[Player].Wood, new Vector2(666, 171), Color.White);
            _spriteBatch.DrawString(MenuFont, "Stone: " + Countries[Player].Stone, new Vector2(666, 211), Color.White);
            _spriteBatch.DrawString(MenuFont, "Metal: " + Countries[Player].Metal, new Vector2(666, 251), Color.White);

            _spriteBatch.DrawString(MenuFont, "Province Coordinates: " + CurrentHexX + " , " + CurrentHexY, new Vector2(666, 297), Color.White);
            _spriteBatch.DrawString(MenuFont, "Owned By: " + (MapArray[CurrentHexX, CurrentHexY].OwnedBy).Name, new Vector2(666, 337), Color.White);
            _spriteBatch.DrawString(MenuFont, "Terrain: " + MapArray[CurrentHexX, CurrentHexY].Terrain, new Vector2(666, 377), Color.White);
            
            if (MapArray[CurrentHexX, CurrentHexY].Structure != "Empty")
            {
                _spriteBatch.DrawString(MenuFont, MapArray[CurrentHexX, CurrentHexY].Structure, new Vector2(666, 417), Color.White);
                _spriteBatch.DrawString(MenuFont, "Garrison: " + MapArray[CurrentHexX, CurrentHexY].StructureGarrison, new Vector2(666, 457), Color.White);
                _spriteBatch.DrawString(MenuFont, "Producing: " + MapArray[CurrentHexX, CurrentHexY].StructureProduction, new Vector2(666, 497), Color.White);
                _spriteBatch.DrawString(MenuFont, "Level: " + MapArray[CurrentHexX, CurrentHexY].StructureLevel, new Vector2(666, 537), Color.White);
                _spriteBatch.DrawString(MenuFont, "Upgrade", new Vector2(666, 577), Color.White);
            }
            else if (MapArray[CurrentHexX, CurrentHexY].Structure == "Empty" && (MapArray[CurrentHexX, CurrentHexY].OwnedBy).IsAI == false)
            {
                _spriteBatch.DrawString(MenuFont, "Build Fort", new Vector2(666, 417), Color.White);
                _spriteBatch.DrawString(MenuFont, "Build Settlement", new Vector2(666, 457), Color.White);
                _spriteBatch.DrawString(MenuFont, "Build Farm", new Vector2(666, 497), Color.White);
                _spriteBatch.DrawString(MenuFont, "Build Forester", new Vector2(666, 537), Color.White);
                _spriteBatch.DrawString(MenuFont, "Build Mine", new Vector2(666, 577), Color.White);
            }
            else if ((MapArray[CurrentHexX, CurrentHexY].Structure == "Empty" && (MapArray[CurrentHexX, CurrentHexY].OwnedBy).IsAI != false) || MapArray[CurrentHexX, CurrentHexY].Terrain == "Deep Ocean")
            {
                _spriteBatch.DrawString(MenuFont, "Empty", new Vector2(666, 417), Color.White);
                _spriteBatch.DrawString(MenuFont, "Empty", new Vector2(666, 457), Color.White);
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

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
