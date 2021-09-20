using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System;

namespace A_Level_Computing_Project
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public Province[,] MapArray = new Province[24, 18];
        public Country[] Countries = new Country[11];
        public int CurrentHexX, CurrentHexY = 0;
        public SpriteFont MenuFont;
        public Texture2D Background, Fort, Settlement, Farm, Forester, Mine;
        public int Player = 7;
        public int Turn = 1;
        public MouseState CurrentMouseState, LastMouseState;

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
                    if (line.Length == 43)
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

            CurrentMouseState = Mouse.GetState();
            Point mousePoint = new Point(CurrentMouseState.X, CurrentMouseState.Y);

            if (CurrentMouseState.LeftButton != ButtonState.Pressed && LastMouseState.LeftButton == ButtonState.Pressed)
            {
                foreach (Province Hex in MapArray)
                {
                    if (Hex.ContainsMousePointer(mousePoint))
                    {
                        CurrentHexX = Hex.X;
                        CurrentHexY = Hex.Y;
                    }
                }

                Rectangle NextTurnButton = new Rectangle(663, 624, 498, 36);
                if (NextTurnButton.Contains(mousePoint))
                {
                    Turn++;
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

            _spriteBatch.DrawString(MenuFont, "Province Coordinates: " + CurrentHexX + " , " + CurrentHexY, new Vector2(666, 297), Color.White);
            _spriteBatch.DrawString(MenuFont, "Owned By: " + (MapArray[CurrentHexX, CurrentHexY].OwnedBy).Name, new Vector2(666, 337), Color.White);
            _spriteBatch.DrawString(MenuFont, "Terrain: " + MapArray[CurrentHexX, CurrentHexY].Terrain, new Vector2(666, 377), Color.White);
            
            if (MapArray[CurrentHexX, CurrentHexY].Structure != "Empty" && MapArray[CurrentHexX, CurrentHexY].OwnedBy.IsAI == false)
            {
                _spriteBatch.DrawString(MenuFont, MapArray[CurrentHexX, CurrentHexY].Structure, new Vector2(666, 417), Color.White);
                _spriteBatch.DrawString(MenuFont, "Garrison: " + MapArray[CurrentHexX, CurrentHexY].StructureGarrison, new Vector2(666, 457), Color.White);
                _spriteBatch.DrawString(MenuFont, "Producing: " + MapArray[CurrentHexX, CurrentHexY].StructureProduction, new Vector2(666, 497), Color.White);
                _spriteBatch.DrawString(MenuFont, "Level: " + MapArray[CurrentHexX, CurrentHexY].StructureLevel, new Vector2(666, 537), Color.White);
                _spriteBatch.DrawString(MenuFont, "Upgrade", new Vector2(666, 577), Color.White);
            }
            else if (MapArray[CurrentHexX, CurrentHexY].Structure != "Empty" && MapArray[CurrentHexX, CurrentHexY].OwnedBy.IsAI == true)
            {
                _spriteBatch.DrawString(MenuFont, MapArray[CurrentHexX, CurrentHexY].Structure, new Vector2(666, 417), Color.White);
                _spriteBatch.DrawString(MenuFont, "Garrison: " + MapArray[CurrentHexX, CurrentHexY].StructureGarrison, new Vector2(666, 457), Color.White);
                _spriteBatch.DrawString(MenuFont, "Producing: " + MapArray[CurrentHexX, CurrentHexY].StructureProduction, new Vector2(666, 497), Color.White);
                _spriteBatch.DrawString(MenuFont, "Level: " + MapArray[CurrentHexX, CurrentHexY].StructureLevel, new Vector2(666, 537), Color.White);
                _spriteBatch.DrawString(MenuFont, "Cannot Upgrade", new Vector2(666, 577), Color.White);
            }
            else if (MapArray[CurrentHexX, CurrentHexY].Structure == "Empty" && MapArray[CurrentHexX, CurrentHexY].OwnedBy.IsAI == false)
            {
                _spriteBatch.DrawString(MenuFont, "Build Fort", new Vector2(666, 417), Color.White);
                _spriteBatch.DrawString(MenuFont, "Build Settlement", new Vector2(666, 457), Color.White);
                _spriteBatch.DrawString(MenuFont, "Build Farm", new Vector2(666, 497), Color.White);
                _spriteBatch.DrawString(MenuFont, "Build Forester", new Vector2(666, 537), Color.White);
                _spriteBatch.DrawString(MenuFont, "Build Mine", new Vector2(666, 577), Color.White);
            }
            else if (MapArray[CurrentHexX, CurrentHexY].Structure == "Empty" && MapArray[CurrentHexX, CurrentHexY].OwnedBy.IsAI == true)
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
