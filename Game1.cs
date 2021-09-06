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
        public Province[,] MapArray = new Province[52, 30];
        public Country[] Countries = new Country[20];
        public int CurrentHexX = 0;
        public int CurrentHexY = 0;
        public string MouseCoords = "Mouse Coordinates: 0 , 0";
        public SpriteFont MenuFont;
        public Texture2D Background, Fort, Settlement, Farm, Forester, Mine;

        
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();

            for (int i = 0; i < 20; i++)
            {
                Countries[i] = new Country(true, "Unowned");
            }
            Countries[1].Name = "Lindon";
            Countries[2].Name = "Blue Mountains North";
            Countries[3].Name = "Blue Mountains South";
            Countries[4].Name = "Shire";
            Countries[5].Name = "Rangers of the North";
            Countries[6].Name = "Rivendell";
            Countries[7].Name = "Rohan";
            Countries[8].Name = "Gondor";
            Countries[9].Name = "Lothlorien";
            Countries[10].Name = "Woodland Realm";
            Countries[11].Name = "Durin's Folk";
            Countries[12].Name = "Dale";
            Countries[13].Name = "Dorwinion";
            Countries[14].Name = "Mordor";
            Countries[15].Name = "Dunland";
            Countries[16].Name = "Isengard";
            Countries[17].Name = "Dol Guldur";
            Countries[18].Name = "Gundabad";
            Countries[19].Name = "Harondor";

            using (StreamReader sr = new StreamReader("C:/Users/ryanm/Documents/Ryan/School Work/A-Level Computing Project/Content/Provinces.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    int test = line.Length;
                    int i = Convert.ToInt32(line.Substring(0, 2));
                    int j = Convert.ToInt32(line.Substring(2, 4));
                    int sl = Convert.ToInt32(line.Substring(4, 5));
                    int sg = Convert.ToInt32(line.Substring(5, 9));
                    int sp = Convert.ToInt32(line.Substring(9, 13));
                    string s = (line.Substring(13, 23)).Trim();
                    string o = (line.Substring(23, 43)).Trim();
                    string t = (line.Substring(43, 61)).Trim();

                    MapArray[i, j] = new Province(i, j);
                    MapArray[i, j].Terrain = t;
                    MapArray[i, j].Structure = s;
                    MapArray[i, j].StructureLevel = sl;
                    MapArray[i, j].StructureGarrison = sg;
                    MapArray[i, j].StructureProduction = sp;
                    MapArray[i, j].OwnedBy = Countries[0];
                }
            }

                //            for (int i = 0; i < 52; i++)
                //            {
                //                for (int j = 0; j < 30; j++)
                //                {
                //                    MapArray[i, j] = new Province(i, j);
                //                    MapArray[i, j].Terrain = "Grassland";
                //                    MapArray[i, j].Structure = "Empty";
                //                    MapArray[i, j].StructureLevel = 0;
                //                    MapArray[i, j].StructureGarrison = 0;
                //                    MapArray[i, j].StructureProduction = 0;
                //                    MapArray[i, j].OwnedBy = Countries[0];
                //                }
                //            }

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
                        var HexBounds1 = new Rectangle((Hex.X * 27) + 9, (Hex.Y * 36) + 20, 18, 36);
                        var HexBounds2 = new Rectangle((Hex.X * 27) + 8, (Hex.Y * 36) + 21, 20, 34);
                        var HexBounds3 = new Rectangle((Hex.X * 27) + 7, (Hex.Y * 36) + 23, 22, 30);
                        var HexBounds4 = new Rectangle((Hex.X * 27) + 6, (Hex.Y * 36) + 25, 24, 26);
                        var HexBounds5 = new Rectangle((Hex.X * 27) + 5, (Hex.Y * 36) + 27, 26, 22);
                        var HexBounds6 = new Rectangle((Hex.X * 27) + 4, (Hex.Y * 36) + 29, 28, 18);
                        var HexBounds7 = new Rectangle((Hex.X * 27) + 3, (Hex.Y * 36) + 31, 30, 14);
                        var HexBounds8 = new Rectangle((Hex.X * 27) + 2, (Hex.Y * 36) + 33, 32, 10);
                        var HexBounds9 = new Rectangle((Hex.X * 27) + 1, (Hex.Y * 36) + 35, 34, 6);
                        var HexBounds10 = new Rectangle((Hex.X * 27), (Hex.Y * 36) + 37, 36, 2);
                        if (HexBounds1.Contains(mousePoint) || HexBounds2.Contains(mousePoint) || HexBounds3.Contains(mousePoint) || HexBounds4.Contains(mousePoint) || HexBounds5.Contains(mousePoint) || HexBounds6.Contains(mousePoint) || HexBounds7.Contains(mousePoint) || HexBounds8.Contains(mousePoint) || HexBounds9.Contains(mousePoint) || HexBounds10.Contains(mousePoint))
                        {
                            CurrentHexX = Hex.X;
                            CurrentHexY = Hex.Y;
                        }
                    }
                    else if (Hex.X % 2 == 1 && Hex.Y != 29)
                    {
                        var HexBounds1 = new Rectangle((Hex.X * 27) + 9, (Hex.Y * 36) + 38, 18, 36);
                        var HexBounds2 = new Rectangle((Hex.X * 27) + 8, (Hex.Y * 36) + 39, 20, 34);
                        var HexBounds3 = new Rectangle((Hex.X * 27) + 7, (Hex.Y * 36) + 41, 22, 30);
                        var HexBounds4 = new Rectangle((Hex.X * 27) + 6, (Hex.Y * 36) + 43, 24, 26);
                        var HexBounds5 = new Rectangle((Hex.X * 27) + 5, (Hex.Y * 36) + 45, 26, 22);
                        var HexBounds6 = new Rectangle((Hex.X * 27) + 4, (Hex.Y * 36) + 47, 28, 18);
                        var HexBounds7 = new Rectangle((Hex.X * 27) + 3, (Hex.Y * 36) + 49, 30, 14);
                        var HexBounds8 = new Rectangle((Hex.X * 27) + 2, (Hex.Y * 36) + 51, 32, 10);
                        var HexBounds9 = new Rectangle((Hex.X * 27) + 1, (Hex.Y * 36) + 53, 34, 6);
                        var HexBounds10 = new Rectangle((Hex.X * 27), (Hex.Y * 36) + 55, 36, 2);
                        if (HexBounds1.Contains(mousePoint) || HexBounds2.Contains(mousePoint) || HexBounds3.Contains(mousePoint) || HexBounds4.Contains(mousePoint) || HexBounds5.Contains(mousePoint) || HexBounds6.Contains(mousePoint) || HexBounds7.Contains(mousePoint) || HexBounds8.Contains(mousePoint) || HexBounds9.Contains(mousePoint) || HexBounds10.Contains(mousePoint))
                        {
                            CurrentHexX = Hex.X;
                            CurrentHexY = Hex.Y;
                        }
                    }     
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            _spriteBatch.Draw(Background, new Vector2(0, 0), Color.White);
            _spriteBatch.DrawString(MenuFont, MouseCoords, new Vector2(1420, 6), Color.White);
            foreach (Province Hex in MapArray)
            {
                if (Hex.Structure == "Fort")
                {
                    if (Hex.X % 2 == 0)
                    {
                        _spriteBatch.Draw(Fort, new Vector2(Hex.X * 27, Hex.Y * 36), Color.White);
                    }
                    else if (Hex.X % 2 == 1 && Hex.Y != 29)
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
                    else if (Hex.X % 2 == 1 && Hex.Y != 29)
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
                    else if (Hex.X % 2 == 1 && Hex.Y != 29)
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
                    else if (Hex.X % 2 == 1 && Hex.Y != 29)
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
                    else if (Hex.X % 2 == 1 && Hex.Y != 29)
                    {
                        _spriteBatch.Draw(Mine, new Vector2(Hex.X * 27, (Hex.Y * 36) + 18), Color.White);
                    }
                }
            }
            _spriteBatch.DrawString(MenuFont, "Province Coordinates: " + CurrentHexX + " , " + CurrentHexY, new Vector2(1420, 724), Color.White);
            _spriteBatch.DrawString(MenuFont, "Owned By: " + (MapArray[CurrentHexX, CurrentHexY].OwnedBy).Name, new Vector2(1420, 763), Color.White);
            _spriteBatch.DrawString(MenuFont, "Terrain: " + MapArray[CurrentHexX, CurrentHexY].Terrain, new Vector2(1420, 802), Color.White);
            if (MapArray[CurrentHexX, CurrentHexY].Structure != "Empty")
            {
                _spriteBatch.DrawString(MenuFont, MapArray[CurrentHexX, CurrentHexY].Structure, new Vector2(1420, 841), Color.White);
                _spriteBatch.DrawString(MenuFont, "Garrison: " + MapArray[CurrentHexX, CurrentHexY].StructureGarrison, new Vector2(1420, 880), Color.White);
                _spriteBatch.DrawString(MenuFont, "Producing: " + MapArray[CurrentHexX, CurrentHexY].StructureProduction, new Vector2(1420, 919), Color.White);
                _spriteBatch.DrawString(MenuFont, "Level: " + MapArray[CurrentHexX, CurrentHexY].StructureLevel, new Vector2(1420, 958), Color.White);
                _spriteBatch.DrawString(MenuFont, "Upgrade", new Vector2(1420, 997), Color.White);
            }
            else if (MapArray[CurrentHexX, CurrentHexY].Structure == "Empty" && (MapArray[CurrentHexX, CurrentHexY].OwnedBy).IsAI == false)
            {
                _spriteBatch.DrawString(MenuFont, "Build Fort", new Vector2(1420, 841), Color.White);
                _spriteBatch.DrawString(MenuFont, "Build Settlement", new Vector2(1420, 880), Color.White);
                _spriteBatch.DrawString(MenuFont, "Build Farm", new Vector2(1420, 919), Color.White);
                _spriteBatch.DrawString(MenuFont, "Build Forester", new Vector2(1420, 958), Color.White);
                _spriteBatch.DrawString(MenuFont, "Build Mine", new Vector2(1420, 997), Color.White);
            }
            else if ((MapArray[CurrentHexX, CurrentHexY].Structure == "Empty" && (MapArray[CurrentHexX, CurrentHexY].OwnedBy).IsAI != false) || MapArray[CurrentHexX, CurrentHexY].Terrain == "Deep Ocean")
            {
                _spriteBatch.DrawString(MenuFont, "Empty", new Vector2(1420, 841), Color.White);
                _spriteBatch.DrawString(MenuFont, "Empty", new Vector2(1420, 880), Color.White);
                _spriteBatch.DrawString(MenuFont, "Empty", new Vector2(1420, 919), Color.White);
                _spriteBatch.DrawString(MenuFont, "Empty", new Vector2(1420, 958), Color.White);
                _spriteBatch.DrawString(MenuFont, "Empty", new Vector2(1420, 997), Color.White);
            }
            _spriteBatch.DrawString(MenuFont, "Next Turn", new Vector2(1420, 1040 ), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
