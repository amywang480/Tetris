//Author:               Amy Wang
//File Name:            MaleficentFire.cs
//Project Name:         ISU
//Creation Date:        May 23, 2018
//Modification Date:    June 17, 2018
/*Description:          Run the game, Maleficent's Fire, a sleeping beauty themed Tetris. In this game,
                        the player is able to control blocks and use them to form full horizontal lines.
                        The goal of the game is to create and clear as many full horizontal lines as possible
                        and achieve a score high enough for the scoreboard*/

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.IO;

namespace ISU
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MaleficentFire : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Store screen displayed
        string switchScreen = "MENU";

        //Retrieve mouse state and previous state
        MouseState mouse;
        MouseState prevMouse;

        //Retrieve keyboard state and previous state
        KeyboardState kb;
        KeyboardState prevKb;

        //Store screen width and height
        int screenWidth;
        int screenHeight;

        //MENU SCREEN:
        //Store button sound effect
        SoundEffect menuSnd;
        SoundEffectInstance menuInstance;

        //Store button sound effect
        SoundEffect buttonSnd;
        SoundEffectInstance buttonInstance;

        //Store fonts
        SpriteFont titleFont;
        SpriteFont buttonFont;

        //Store text locations
        Vector2 titleLoc = new Vector2(275, 55);
        
        //Store background image
        Texture2D menuBgImg;
        Rectangle menuBgRec;

        //Store dragon image
        Texture2D dragonImg;
        Rectangle dragonRec;

        //Store button images
        Texture2D swordBtnImg;
        Rectangle playBtnRec;
        Rectangle instructBtnRec;
        Rectangle scoresBtnRec;
        Rectangle exitBtnRec;
 
        //Store button name locations
        Vector2 playBtnLoc = new Vector2(850, 265);
        Vector2 instructBtnLoc = new Vector2(810, 395);
        Vector2 scoresBtnLoc = new Vector2(815, 525);
        Vector2 exitBtnLoc = new Vector2(855, 655);
      
        //USERNAME SCREEN:
        //Store fonts
        SpriteFont lengthFont;
        SpriteFont usernameFont;

        //Store locations
        Vector2 lengthLoc = new Vector2(290, 200);
        Vector2 enterUsernameLoc = new Vector2(290, 300);

        //Store background image
        Texture2D usernameBgImg;
        Rectangle usernameBgRec;

        //Store submit button image and name location
        Rectangle submitBtnRec;
        Vector2 submitBtnLoc = new Vector2(555, 615);

        //PLAY SCREEN:
        //Store background music
        SoundEffect playSnd;
        SoundEffectInstance playInstance;

        //Store line clear sound effect
        SoundEffect linesSnd;
        SoundEffectInstance linesInstance;

        //Store fonts
        SpriteFont playFont;
        SpriteFont lineClearFont;

        //Store text locations
        Vector2 timeLoc = new Vector2(900, 40);         
        Vector2 lineClearTotalLoc = new Vector2(900, 200);   
        Vector2 pointsLoc = new Vector2(900, 360);
        Vector2 usernameLoc = new Vector2(900, 670);
        Vector2 lineClearLoc = new Vector2(410, 250); 

        //Store background image
        Texture2D playBgImg;
        Rectangle playBgRec;

        //Store Maleficent image
        Texture2D malImg;
        Rectangle malRec;

        //Store game area image
        Texture2D gameAreaImg;
        Rectangle gameAreaRec;

        //Store game area corner images
        Texture2D upCornerImg;
        Rectangle upCornerRec;
        Texture2D lowCornerImg;
        Rectangle lowCornerRec;

        //Store fire block image
        Texture2D fireImg;

        //Store rectangles for fire
        Rectangle curRec;
        Rectangle oldRec;

        //Store water balloon image
        Texture2D waterImg;
        Rectangle waterRec;

        //Store powerful water balloon image
        Texture2D pwrWaterImg;
        Rectangle pwrWaterRec;
      
        //Store steel box image
        Texture2D steelImg;
        Rectangle steelRec;

        //Store username
        string username = "";

        //Store points earned
        int points = 0;

        //Store game area array
        int[,] gameArea = new int[21, 10];

        //Store blocks and rotations
        //Square Block:
        int[,] squareBlock = new int[4, 4];

        //Z Block:
        int[,] zBlock = new int[4, 4];
        int[,] zBlockR1 = new int[4, 4];
        int[,] zBlockR2 = new int[4, 4];
        int[,] zBlockR3 = new int[4, 4];

        //S Block:
        int[,] sBlock = new int[4, 4];
        int[,] sBlockR1 = new int[4, 4];
        int[,] sBlockR2 = new int[4, 4];
        int[,] sBlockR3 = new int[4, 4];

        //T Block:
        int[,] tBlock = new int[4, 4];
        int[,] tBlockR1 = new int[4, 4];
        int[,] tBlockR2 = new int[4, 4];
        int[,] tBlockR3 = new int[4, 4];

        //I Block:
        int[,] iBlock = new int[4, 4];
        int[,] iBlockR1 = new int[4, 4];
        int[,] iBlockR2 = new int[4, 4];
        int[,] iBlockR3 = new int[4, 4];

        //L Block:
        int[,] lBlock = new int[4, 4];
        int[,] lBlockR1 = new int[4, 4];
        int[,] lBlockR2 = new int[4, 4];
        int[,] lBlockR3 = new int[4, 4];

        //J Block:
        int[,] jBlock = new int[4, 4];
        int[,] jBlockR1 = new int[4, 4];
        int[,] jBlockR2 = new int[4, 4];
        int[,] jBlockR3 = new int[4, 4];

        //Water Balloon Block:
        int[,] waterBlock = new int[4, 4];

        //Powerful Water Balloon Block:
        int[,] pwrWaterBlock = new int[4, 4];

        //Steel Blocks:
        //Two Squares:
        int[,] steelBlock1 = new int[4, 4];
        int[,] steelBlock1R1 = new int[4, 4];
        int[,] steelBlock1R2 = new int[4, 4];
        int[,] steelBlock1R3 = new int[4, 4];

        //Three Squares:
        int[,] steelBlock2 = new int[4, 4];
        int[,] steelBlock2R1 = new int[4, 4];
        int[,] steelBlock2R2 = new int[4, 4];
        int[,] steelBlock2R3 = new int[4, 4];

        //Control when rotated blocks are displayed
        int rotate = 0;

        //Store number of blocks in a row
        int rowBlocks = 0;
        int columnBlocks = 0;

        //Store current block
        int[,] curBlock = new int[4, 4];
        int[,] currentBlock = new int[4, 4];
        int[,] oldBlock = new int[4, 4];

        //Store initial coordinates of blocks
        int blockX = 542;
        int blockY = -57;

        //Store previous coordinates of blocks
        int oldBlockX;
        int oldBlockY;

        //Store X and Y values when block remains in place
        int oldX;
        int oldY;

        //Store X and Y values while block is falling
        int fallX;
        int fallY;

        //Store x and y coordinates of blocks
        int x;
        int y;

        //Store new X and Y coordinates when block remains in place
        int newX;
        int newY;

        //Store bottom Y coordinate of current block
        int bottomY;

        //Set new position for falling
        int newPos = -57;

        //Store new rows and columns for game area 
        int newRow;
        int newCol;

        //Store row and col in game area array
        int gameAreaRow;
        int gameAreaCol;

        //Store adjustments for arrays
        int adjustmentLeft = 0;
        int adjustmentUp = 0;

        //Control when block is imprinted on game area
        bool isBlockImprinted = false;
        bool isBlockFinishImprinting = false;

        //Control block movement
        bool canRotate = false;
        bool goDown = false;
        bool goLeft = false;
        bool goRight = false;

        //Store numbers to determine block colour
        Color[] colors = new Color[13];

        //Store random numbers
        Random rng = new Random();
        int randomNum;
        int powerObs;
        int prevRandomNum;

        //Store times
        int time = 0;           //Track regular game time
        int stay = 1000;        //Set time for block to stay in its position
        int lineTime = 2000;    //Set time for showing line clear message
       
        //Track number of lines cleared and total
        int lineClear = 0;
        int lineClearTotal = 0;

        //Store values to display line clear message
        string linesMessage = "";
        bool showMessage = false;

        //GAME OVER SCREEN:
        //Store game over sound effect
        SoundEffect gameOverSnd;
        SoundEffectInstance gameOverInstance;

        //Store background image
        Texture2D gameOverBgImg;
        Rectangle gameOverBgRec;

        //Store title location
        Vector2 gameOverTitleLoc = new Vector2(290, 55);

        //Store saved values
        int savedTime;
        int savedLineClear;
        int savedPoints;
        string savedUsername;

        //TOP 10 SCREEN:
        //Store background music
        SoundEffect top10Snd;
        SoundEffectInstance top10Instance;

        //Store background image
        Texture2D top10BgImg;
        Rectangle top10BgRec;

        //Store title location
        Vector2 top10TitleLoc = new Vector2(320, 55);

        //Store info location
        Vector2[] top10Locs = new Vector2[4];

        //INSTRUCTIONS:
        //Store title location
        Vector2 instructTitleLoc = new Vector2(360, 55);

        //Store text locations
        Vector2[] instructLocs = new Vector2[6];

        //SCOREBOARD:
        //Store font
        SpriteFont scoreboardFont;

        //Store title location
        Vector2 scoreboardLoc = new Vector2(390, 55);

        //Store menu button image and location
        Rectangle menuBtnRec;
        Vector2 menuBtnLoc = new Vector2(600, 655);

        //Store names and scores in scoreboard
        string[,] scoreboard = new string[10, 2];

        //Store high scores and names
        Vector2[] namesLoc = new Vector2[10];
        Vector2[] scoresLoc = new Vector2[10];

        //Allow for reading and writing of TopScores file
        StreamWriter outFile;
        StreamReader inFile;

        //Store scoreboard file path
        string filePath;

        //Store data in scoreboard file
        string line;
        string[] data = new string[20];

        public MaleficentFire()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //Show mouse on screen
            IsMouseVisible = true;

            //Set width and height of game window
            this.graphics.PreferredBackBufferWidth = 1300;
            this.graphics.PreferredBackBufferHeight = 750;

            //Apply changes made
            graphics.ApplyChanges();

            //Set screen width and height
            screenWidth = graphics.GraphicsDevice.Viewport.Width;
            screenHeight = graphics.GraphicsDevice.Viewport.Height;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            //MENU SCREEN:
            //Audio:
            //Load menu background music
            menuSnd = Content.Load<SoundEffect>(@"Audio\Music\MenuSound");
            menuInstance = menuSnd.CreateInstance();
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 30f;
            menuInstance.IsLooped = true;

            //Load button sound effect
            buttonSnd = Content.Load<SoundEffect>(@"Audio\SoundEffects\ButtonSndEffect");
            buttonInstance = buttonSnd.CreateInstance();
            MediaPlayer.IsRepeating = false;
            MediaPlayer.Volume = 30f;
            buttonInstance.IsLooped = false;

            //Fonts:
            titleFont = Content.Load<SpriteFont>("Fonts/TitleFont");
            buttonFont = Content.Load<SpriteFont>("Fonts/ButtonFont");

            //Images:
            //Load background image
            menuBgImg = Content.Load<Texture2D>("Backgrounds/MenuBg");
            menuBgRec = new Rectangle(0, 0, screenWidth, screenHeight);

            //Load dragon image
            dragonImg = Content.Load<Texture2D>("Images/Dragon");
            dragonRec = new Rectangle(80, 180, (int)(dragonImg.Width * 0.38), (int)(dragonImg.Height * 0.38));

            //Load button images
            swordBtnImg = Content.Load<Texture2D>("Images/SwordBtn");
            playBtnRec = new Rectangle(750, 250, (int)(swordBtnImg.Width * 0.7), (int)(swordBtnImg.Height * 0.6));
            instructBtnRec = new Rectangle(750, 380, (int)(swordBtnImg.Width * 0.7), (int)(swordBtnImg.Height * 0.6));
            scoresBtnRec = new Rectangle(750, 510, (int)(swordBtnImg.Width * 0.7), (int)(swordBtnImg.Height * 0.6));
            exitBtnRec = new Rectangle(750, 640, (int)(swordBtnImg.Width * 0.7), (int)(swordBtnImg.Height * 0.6));

            //USERNAME SCREEN:
            //Fonts:
            lengthFont = Content.Load<SpriteFont>("Fonts/LengthFont");
            usernameFont = Content.Load<SpriteFont>("Fonts/UsernameFont");

            //Images:
            //Load background image
            usernameBgImg = Content.Load<Texture2D>("Backgrounds/UsernameBg");
            usernameBgRec = new Rectangle(0, 0, screenWidth, screenHeight);

            //Load submit button
            submitBtnRec = new Rectangle(460, 600, (int)(swordBtnImg.Width * 0.7), (int)(swordBtnImg.Height * 0.6));

            //PLAY SCREEN:
            //Audio:
            //Load play background music
            playSnd = Content.Load<SoundEffect>(@"Audio\Music\PlaySound");
            playInstance = playSnd.CreateInstance();
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 30f;
            playInstance.IsLooped = true;

            //Load line clear sound effect
            linesSnd = Content.Load<SoundEffect>(@"Audio\SoundEffects\LineClearSndEffect");
            linesInstance = linesSnd.CreateInstance();
            MediaPlayer.IsRepeating = false;
            MediaPlayer.Volume = 30f;
            linesInstance.IsLooped = false;

            //Fonts:
            playFont = Content.Load<SpriteFont>("Fonts/PlayFont");
            lineClearFont = Content.Load<SpriteFont>("Fonts/LineClearFont");

            //Images:
            //Load background image
            playBgImg = Content.Load<Texture2D>("Backgrounds/PlayBg");
            playBgRec = new Rectangle(0, 0, screenWidth, screenHeight);

            //Load Maleficent image
            malImg = Content.Load<Texture2D>("Images/Maleficent");
            malRec = new Rectangle(-450, 70, (int)(malImg.Width * 0.65), (int)(malImg.Height * 0.65));
         
            //Load game area image
            gameAreaImg = Content.Load<Texture2D>("Images/GameArea");
            gameAreaRec = new Rectangle(470, 15, 360, 720);

            //Load corner images
            upCornerImg = Content.Load<Texture2D>("Images/UpperCorner");
            upCornerRec = new Rectangle(705, 8, (int)(upCornerImg.Width * 0.15), (int)(upCornerImg.Height * 0.15));
            lowCornerImg = Content.Load<Texture2D>("Images/LowerCorner");
            lowCornerRec = new Rectangle(460, 610, (int)(lowCornerImg.Width * 0.15), (int)(lowCornerImg.Height * 0.15));
            
            //Load fire block image
            fireImg = Content.Load<Texture2D>("Images/FireBlock");

            //Load power-up images
            waterImg = Content.Load<Texture2D>("Images/WaterBalloon");
            pwrWaterImg = Content.Load<Texture2D>("Images/PowerWaterBalloon");

            //Load steel box image
            steelImg = Content.Load<Texture2D>("Images/SteelBox");

            //Set colours
            colors[1] = Color.Yellow; 
            colors[2] = Color.Red;      
            colors[3] = Color.LightGreen;   
            colors[4] = Color.Magenta;  
            colors[5] = Color.Cyan; 
            colors[6] = Color.Orange;   
            colors[7] = Color.LightCoral;
            colors[8] = Color.White;
            colors[9] = Color.White;
            colors[10] = Color.White;
            colors[11] = Color.White;
            colors[12] = Color.White;

            //Set values for fire blocks
            Blocks();

            //Set values for last row of game array
            for (int col = 0; col < 10; col++)
            {
                gameArea[20, col] = -1;
            }

            //Determine random number for blocks
            randomNum = rng.Next(1, 8);

            //GAME OVER SCREEN:
            //Audio:
            //Load game over sound effect
            gameOverSnd = Content.Load<SoundEffect>(@"Audio\SoundEffects\GameOverSndEffect");
            gameOverInstance = gameOverSnd.CreateInstance();
            MediaPlayer.IsRepeating = false;
            MediaPlayer.Volume = 30f;
            gameOverInstance.IsLooped = false;

            //Images:
            //Load background image
            gameOverBgImg = Content.Load<Texture2D>("Backgrounds/GameOverBg");
            gameOverBgRec = new Rectangle(0, 0, screenWidth, screenHeight);

            //TOP 10 SCREEN:
            //Audio:
            //Load top 10 sound effect
            top10Snd = Content.Load<SoundEffect>(@"Audio\SoundEffects\Top10SndEffect");
            top10Instance = top10Snd.CreateInstance();
            MediaPlayer.IsRepeating = false;
            MediaPlayer.Volume = 30f;
            top10Instance.IsLooped = false;

            //Set text locations
            top10Locs[0] = new Vector2(180, 200);
            top10Locs[1] = new Vector2(180, 270);
            top10Locs[2] = new Vector2(180, 320);
            top10Locs[3] = new Vector2(180, 370);

            //Images:
            //Load background image
            top10BgImg = Content.Load<Texture2D>("Backgrounds/Top10Bg");
            top10BgRec = new Rectangle(0, 0, screenWidth, screenHeight);

            //INSTRUCTIONS:
            //Set text locations:
            instructLocs[0] = new Vector2(200, 210);
            instructLocs[1] = new Vector2(200, 280);
            instructLocs[2] = new Vector2(200, 330);
            instructLocs[3] = new Vector2(200, 390);
            instructLocs[4] = new Vector2(200, 480);
            instructLocs[5] = new Vector2(200, 550);

            //SCOREBOARD:
            //Fonts:
            scoreboardFont = Content.Load<SpriteFont>("Fonts/ScoreboardFont");

            //Images:
            //Load menu button
            menuBtnRec = new Rectangle(470, 640, (int)(swordBtnImg.Width * 0.7), (int)(swordBtnImg.Height * 0.6));

            //Determine name and score locations
            NamesScores();

            //Set intial scores
            for (int i = 0; i <= 9; i++)
            {
                scoreboard[i, 1] = "0";
            }

            //Set initial names
            scoreboard[0, 0] = "aurora";
            scoreboard[1, 0] = "philip";
            scoreboard[2, 0] = "fauna";
            scoreboard[3, 0] = "mal";
            scoreboard[4, 0] = "umberto";
            scoreboard[5, 0] = "flora";
            scoreboard[6, 0] = "theking";
            scoreboard[7, 0] = "sleeping";
            scoreboard[8, 0] = "beauty";
            scoreboard[9, 0] = "fairies";
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //Store previous mouse state and retrieve new state
            prevMouse = mouse;
            mouse = Mouse.GetState();

            //Store previous keyboard state and retrieve new state
            prevKb = kb;
            kb = Keyboard.GetState();

            //Determine and retrieve file path for scoreboard file
            filePath = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            filePath = Path.GetDirectoryName(filePath);
            filePath = filePath.Substring(6);
            filePath = filePath + "/Scoreboard.txt";

            //Control displaying of screens
            if (switchScreen == "MENU")
            {
                //Stop game over sound effect
                if (gameOverInstance.State == SoundState.Playing)
                {
                    gameOverInstance.Stop();
                }

                //Stop top 10 sound effect
                if (top10Instance.State == SoundState.Playing)
                {
                    top10Instance.Stop();
                }

                //Play background music
                if (menuInstance.State == SoundState.Stopped)
                {
                    menuInstance.Play();
                }
                
                //Stretch buttons and allow for clicking
                Buttons();
            }

            if (switchScreen == "USERNAME")
            {
                //Stop menu background music
                if (menuInstance.State == SoundState.Playing)
                {
                    menuInstance.Stop();
                }

                //Display letters
                DetermineUserInput();

                //Remove last letter when backspace is pressed
                Backspace();

                //Stretch and click submit button
                ButtonStretch(submitBtnRec);
                ButtonClick(submitBtnRec.X, submitBtnRec.Right, submitBtnRec.Y, submitBtnRec.Bottom, submitBtnRec);
            }

            if (switchScreen == "PLAY")
            {
                //Play background music
                if (playInstance.State == SoundState.Stopped)
                {
                    playInstance.Play();
                }

                //Regular game time is tracked
                time += gameTime.ElapsedGameTime.Milliseconds;

                //Blocks fall downwards (simulated gravity)
                BlockFall(gameTime);

                //Allow for block rotation and movement
                BlockControl();

                //Display fire blocks and rotations
                DetermineRotations();

                //Detect line clears
                for (int row = 0; row < 20; row++)
                {
                    DetectLineClear(row, gameTime);
                }

                //Display line clear message
                LineClearMessage(gameTime);

                DetermineGameOver();
            }

            if (switchScreen == "GAMEOVER")
            {
                //Stop play background sound
                if (playInstance.State == SoundState.Playing)
                {
                    playInstance.Stop();
                }

                //Play game over sound
                if (gameOverInstance.State == SoundState.Stopped)
                {
                    gameOverInstance.Play();
                }
                
                //Increase the volume by 0.05 to a maximum of 1.0f
                gameOverInstance.Volume = Math.Min(1.0f, gameOverInstance.Volume + 0.1f);

                //Reset values for new game
                GameReset();
                
                //Stretch and click menu button
                ButtonStretch(menuBtnRec);
                ButtonClick(menuBtnRec.X, menuBtnRec.Right, menuBtnRec.Y, menuBtnRec.Bottom, menuBtnRec);
            }

            if (switchScreen == "TOP10")
            {
                //Stop play background sound
                if (playInstance.State == SoundState.Playing)
                {
                    playInstance.Stop();
                }

                //Play top 10 sound
                if (top10Instance.State == SoundState.Stopped)
                {
                    top10Instance.Play();
                }

                //Increase the volume by 0.05 to a maximum of 1.0f
                top10Instance.Volume = Math.Min(1.0f, top10Instance.Volume + 0.1f);

                //Reset values for new game
                GameReset();

                //Stretch and click menu button
                ButtonStretch(menuBtnRec);
                ButtonClick(menuBtnRec.X, menuBtnRec.Right, menuBtnRec.Y, menuBtnRec.Bottom, menuBtnRec);
            }

            if (switchScreen == "INSTRUCTIONS")
            {
                //Stretch and click menu button
                ButtonStretch(menuBtnRec);
                ButtonClick(menuBtnRec.X, menuBtnRec.Right, menuBtnRec.Y, menuBtnRec.Bottom, menuBtnRec);
            }

            if (switchScreen == "SCOREBOARD")
            {
                //Stretch and click menu button
                ButtonStretch(menuBtnRec);
                ButtonClick(menuBtnRec.X, menuBtnRec.Right, menuBtnRec.Y, menuBtnRec.Bottom, menuBtnRec);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //Set default background colour
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //Display screens
            if (switchScreen == "MENU")
            {
                DisplayMenu();
            }
            else if (switchScreen == "USERNAME")
            {
                DisplayUsername();
            }
            else if (switchScreen == "PLAY")
            {
                DisplayPlay();
            }
            else if (switchScreen == "GAMEOVER")
            {
                DisplayGameOver();
            }
            else if (switchScreen == "TOP10")
            {
                DisplayTop10();
            }
            else if (switchScreen == "INSTRUCTIONS")
            {
                DisplayInstructions();
            }
            else if (switchScreen == "SCOREBOARD")
            {
                DisplayScoreboard();
            }

            base.Draw(gameTime);
        }

        //Pre: None
        //Post: The title, buttons, and all other images are outputted on screen
        //Description: Display the menu screen
        private void DisplayMenu()
        {
            //Begin displaying images
            spriteBatch.Begin();

            //Display background image
            spriteBatch.Draw(menuBgImg, menuBgRec, Color.White);

            //Display title
            spriteBatch.DrawString(titleFont, "Maleficent's Fire", titleLoc, Color.White * 0.7f);

            //Display dragon image
            spriteBatch.Draw(dragonImg, dragonRec, Color.White * 0.8f);

            //Display button images
            spriteBatch.Draw(swordBtnImg, playBtnRec, Color.White);
            spriteBatch.Draw(swordBtnImg, instructBtnRec, Color.White);
            spriteBatch.Draw(swordBtnImg, scoresBtnRec, Color.White);
            spriteBatch.Draw(swordBtnImg, exitBtnRec, Color.White);

            //Display button names
            spriteBatch.DrawString(buttonFont, "Play", playBtnLoc, Color.Black);
            spriteBatch.DrawString(buttonFont, "Instructions", instructBtnLoc, Color.Black);
            spriteBatch.DrawString(buttonFont, "Scoreboard", scoresBtnLoc, Color.Black);
            spriteBatch.DrawString(buttonFont, "Exit", exitBtnLoc, Color.Black);

            //Finish displaying images
            spriteBatch.End();
        }

        //Pre: None
        //Post: Letters are displayed
        //Description: Allow user to see their typed username
        private void DisplayUsername()
        {
            //Begin displaying images
            spriteBatch.Begin();

            //Display background image
            spriteBatch.Draw(usernameBgImg, usernameBgRec, Color.White);

            //Display title
            spriteBatch.DrawString(titleFont, "Enter Username:", titleLoc, Color.White * 0.7f);
            spriteBatch.DrawString(lengthFont, "Maximum Length is 8 Letters!", lengthLoc, Color.White * 0.7f);

            //Display username
            spriteBatch.DrawString(usernameFont, "" + username, enterUsernameLoc, Color.White * 0.7f);

            //Display submit button image and name
            spriteBatch.Draw(swordBtnImg, submitBtnRec, Color.White);
            spriteBatch.DrawString(buttonFont, "Submit", submitBtnLoc, Color.Black);

            //Finish displaying images
            spriteBatch.End();
        }

        //Pre: None
        //Post: The blocks, power-ups, obstacle, images, and other game elements are outputted on screen
        //Description: Display the game screen
        private void DisplayPlay()
        {
            //Begin displaying images
            spriteBatch.Begin();

            //Display background image
            spriteBatch.Draw(playBgImg, playBgRec, Color.White);

            //Display Maleficent image
            spriteBatch.Draw(malImg, malRec, Color.White);

            //Display game area image
            spriteBatch.Draw(gameAreaImg, gameAreaRec, Color.White * 0.5f);

            //Imprint blocks
            BlocksImprinting();
            
            //Display blocks
            DisplayBlocks();

            //Display corner images
            spriteBatch.Draw(upCornerImg, upCornerRec, Color.White);
            spriteBatch.Draw(lowCornerImg, lowCornerRec, Color.White);

            //Display time, lines cleared, and points
            spriteBatch.DrawString(playFont, "Time: " + "\n" + (int)(time / 1000) + " s", timeLoc, Color.White);
            spriteBatch.DrawString(playFont, "Lines Cleared: " + "\n" + lineClearTotal, lineClearTotalLoc, Color.White);
            spriteBatch.DrawString(playFont, "Points: " + "\n" + points, pointsLoc, Color.White);

            //Display username
            spriteBatch.DrawString(playFont, "" + username, usernameLoc, Color.White);

            //Display line clear message
            if (showMessage)
            {
                spriteBatch.DrawString(lineClearFont, linesMessage, lineClearLoc, Color.White);
            }

            //Finish displaying images
            spriteBatch.End();
        }

        //Pre: None
        //Post: Time, lines cleared, and points are displayed on screen
        //Description: Display game over screen to inform the user their score did not place on the scoreboard
        private void DisplayGameOver()
        {
            //Begin displaying images
            spriteBatch.Begin();

            //Display background image
            spriteBatch.Draw(gameOverBgImg, gameOverBgRec, Color.White);

            //Display title
            spriteBatch.DrawString(titleFont, "Maleficent Wins!", gameOverTitleLoc, Color.White);

            //Display message
            spriteBatch.DrawString(playFont, "Unfortunately, your attacks weren't fast enough", top10Locs[0], Color.White);

            //Display info
            spriteBatch.DrawString(playFont, "Time: " + (int)(savedTime / 1000), top10Locs[1], Color.White);
            spriteBatch.DrawString(playFont, "Lines Cleared: " + savedLineClear, top10Locs[2], Color.White);
            spriteBatch.DrawString(playFont, "Points: " + savedPoints, top10Locs[3], Color.White);

            //Display menu button and name
            spriteBatch.Draw(swordBtnImg, menuBtnRec, Color.White);
            spriteBatch.DrawString(buttonFont, "Menu", menuBtnLoc, Color.Black);

            //Finish displaying images
            spriteBatch.End();
        }

        //Pre: None
        //Post: Time, lines cleared, and points are displayed on screen
        //Description: Display top 10 screen to inform the user their score placed on the scoreboard
        private void DisplayTop10()
        {
            //Begin displaying images
            spriteBatch.Begin();

            //Display background image
            spriteBatch.Draw(top10BgImg, top10BgRec, Color.White);

            //Display title
            spriteBatch.DrawString(titleFont, "Top 10 Score!", top10TitleLoc, Color.White);

            //Display message
            spriteBatch.DrawString(playFont, "Congratulations, your score is on the scoreboard!", top10Locs[0], Color.White);

            //Display info
            spriteBatch.DrawString(playFont, "Time: " + (int)(savedTime / 1000), top10Locs[1], Color.Black);
            spriteBatch.DrawString(playFont, "Lines Cleared: " + savedLineClear, top10Locs[2], Color.Black);
            spriteBatch.DrawString(playFont, "Points: " + savedPoints, top10Locs[3], Color.Black);

            //Display menu button and name
            spriteBatch.Draw(swordBtnImg, menuBtnRec, Color.White);
            spriteBatch.DrawString(buttonFont, "Menu", menuBtnLoc, Color.Black);

            //Finish displaying images
            spriteBatch.End();
        }

        //Pre: None
        //Post: Instructions are outputted on screen
        //Description: Display instructions for the game
        private void DisplayInstructions()
        {
            //Begin displaying images
            spriteBatch.Begin();

            //Display background image
            spriteBatch.Draw(usernameBgImg, usernameBgRec, Color.White);

            //Display title
            spriteBatch.DrawString(titleFont, "Instructions", instructTitleLoc, Color.White);

            //Display instructions
            spriteBatch.DrawString(scoreboardFont, "Goal: Fill in as many full horizontal lines as possible", instructLocs[0], Color.White);
            spriteBatch.DrawString(scoreboardFont, "1. Use the up arrow key to rotate blocks", instructLocs[1], Color.White);
            spriteBatch.DrawString(scoreboardFont, "2. Use the other arrow keys for block movement", instructLocs[2], Color.White);
            spriteBatch.DrawString(scoreboardFont, "3. Use the yellow water balloon to clear blocks within a one square" + "\n" + "radius", 
            instructLocs[3], Color.White);
            spriteBatch.DrawString(scoreboardFont, "4. Use the blue water balloon to clear a vertical line of blocks", instructLocs[4], Color.White);
            spriteBatch.DrawString(scoreboardFont, "*Steel boxes will prevent line clears! They can only be removed with" + "\n" + "power-ups*", 
            instructLocs[5], Color.White);

            //Display menu button and name
            spriteBatch.Draw(swordBtnImg, menuBtnRec, Color.White);
            spriteBatch.DrawString(buttonFont, "Menu", menuBtnLoc, Color.Black);

            //Finish displaying images
            spriteBatch.End();
        }

        //Pre: None
        //Post: Scores and usernames are outputted on screen
        //Description: Display the scoreboard with the game's top 10 high scores and players
        private void DisplayScoreboard()
        {
            //Begin displaying images
            spriteBatch.Begin();

            //Display background image
            spriteBatch.Draw(usernameBgImg, usernameBgRec, Color.White);

            //Display title
            spriteBatch.DrawString(titleFont, "Scoreboard", scoreboardLoc, Color.White * 0.7f);

            //Display menu button and name
            spriteBatch.Draw(swordBtnImg, menuBtnRec, Color.White);
            spriteBatch.DrawString(buttonFont, "Menu", menuBtnLoc, Color.Black);

            //Display names and scores
            for (int i = 9; i >= 0; i--)
            {
                spriteBatch.DrawString(scoreboardFont, scoreboard[i, 0], namesLoc[i], Color.White);
                spriteBatch.DrawString(scoreboardFont, scoreboard[i, 1], scoresLoc[i], Color.White);
            }

            //Finish displaying images
            spriteBatch.End();
        }

        //Pre: None
        //Post: Buttons can be clicked and are stretched
        //Description: Allow for buttons to be clicked and stretched 
        private void Buttons()
        {
            //Stretch buttons
            ButtonStretch(playBtnRec);
            ButtonStretch(instructBtnRec);
            ButtonStretch(scoresBtnRec);
            ButtonStretch(exitBtnRec);

            //Allow for buttons to be clicked
            ButtonClick(playBtnRec.X, playBtnRec.Right, playBtnRec.Y, playBtnRec.Bottom, playBtnRec);
            ButtonClick(instructBtnRec.X, instructBtnRec.Right, instructBtnRec.Y, instructBtnRec.Bottom, instructBtnRec);
            ButtonClick(scoresBtnRec.X, scoresBtnRec.Right, scoresBtnRec.Y, scoresBtnRec.Bottom, scoresBtnRec);
            ButtonClick(exitBtnRec.X, exitBtnRec.Right, exitBtnRec.Y, exitBtnRec.Bottom, exitBtnRec);
        }

        //Pre: Rectangle is drawn on screen
        //Post: Buttons expand horizontally
        //Description: Expand btn when mouse passes over the area of the btn
        private void ButtonStretch(Rectangle btn)
        {
            //Check for area of button
            if (mouse.X >= btn.X && mouse.X <= btn.Right && mouse.Y >= btn.Y && mouse.Y <= btn.Bottom)
            {
                //Play button sound effect
                if (buttonInstance.State == SoundState.Stopped)
                {
                    buttonInstance.Play();
                }

                //Increase the volume by 0.05 to a maximum of 1.0f
                buttonInstance.Volume = Math.Min(1.0f, buttonInstance.Volume + 0.1f);

                //Expand button
                if (btn == menuBtnRec)
                {
                    menuBtnRec.Width = 500;
                    menuBtnRec.X = 420;
                }
                else if (btn == playBtnRec)
                {
                    playBtnRec.Width = 500;
                    playBtnRec.X = 700;
                }
                else if (btn == submitBtnRec)
                {
                    submitBtnRec.Width = 500;
                    submitBtnRec.X = 410;
                }
                else if (btn == instructBtnRec)
                {
                    instructBtnRec.Width = 500;
                    instructBtnRec.X = 700;
                }
                else if (btn == scoresBtnRec)
                {
                    scoresBtnRec.Width = 500;
                    scoresBtnRec.X = 700;
                }
                else if (btn == exitBtnRec)
                {
                    exitBtnRec.Width = 500;
                    exitBtnRec.X = 700;
                }
            }
            else
            {
                if (btn == menuBtnRec)
                {
                    menuBtnRec.Width = (int)(swordBtnImg.Width * 0.7);
                    menuBtnRec.X = 470;
                }
                else if (btn == playBtnRec)
                {
                    playBtnRec.Width = (int)(swordBtnImg.Width * 0.7);
                    playBtnRec.X = 750;
                }
                else if (btn == submitBtnRec)
                {
                    submitBtnRec.Width = (int)(swordBtnImg.Width * 0.7);
                    submitBtnRec.X = 460;
                }
                else if (btn == instructBtnRec)
                {
                    instructBtnRec.Width = (int)(swordBtnImg.Width * 0.7);
                    instructBtnRec.X = 750;
                }
                else if (btn == scoresBtnRec)
                {
                    scoresBtnRec.Width = (int)(swordBtnImg.Width * 0.7);
                    scoresBtnRec.X = 750;
                }
                else if (btn == exitBtnRec)
                {
                    exitBtnRec.Width = (int)(swordBtnImg.Width * 0.7);
                    exitBtnRec.X = 750;
                }
            }
        }

        //Pre: x1, x2, y1, and y2 are integers all greater than zero and box is a rectangle drawn on screen
        //Post: A new screen is outputted based on which button is clicked
        //Description: Determine which screen to display after a click using the X and Y values of the box to determine the area of the click
        private void ButtonClick(int x1, int x2, int y1, int y2, Rectangle box)
        {
            //If the left mouse button is pressed while in a button area, display the matching screen
            if (mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton != ButtonState.Pressed)
            {
                if (x1 <= mouse.X && mouse.X <= x2 && y1 <= mouse.Y && mouse.Y <= y2)
                {
                    if (box == playBtnRec)
                    {
                        switchScreen = "USERNAME";
                    }
                    else if (box == submitBtnRec)
                    {
                        switchScreen = "PLAY";

                        //Determine random block to display
                        RandomBlocks();
                    }
                    else if (box == instructBtnRec)
                    {
                        switchScreen = "INSTRUCTIONS";
                    }
                    else if (box == scoresBtnRec)
                    {
                        switchScreen = "SCOREBOARD";
                    }
                    else if (box == menuBtnRec)
                    {
                        switchScreen = "MENU";
                    }
                    else if (box == exitBtnRec)
                    {
                        this.Exit();

                        //Reset scoreboard after exiting game
                        for (int i = 0; i <= 9; i++)
                        {
                            scoreboard[i, 1] = "0";
                        }

                        scoreboard[0, 0] = "aurora";
                        scoreboard[1, 0] = "philip";
                        scoreboard[2, 0] = "fauna";
                        scoreboard[3, 0] = "mal";
                        scoreboard[4, 0] = "umberto";
                        scoreboard[5, 0] = "flora";
                        scoreboard[6, 0] = "theking";
                        scoreboard[7, 0] = "sleeping";
                        scoreboard[8, 0] = "beauty";
                        scoreboard[9, 0] = "fairies";
                    }
                }
            }
        }

        //Pre: None
        //Post: Letters appear on screen
        //Description: Allow for letters and numbers to appear on screen based on keys pressed
        private void DetermineUserInput()
        {
            UserInputLetters(Keys.A, "a");
            UserInputLetters(Keys.B, "b");
            UserInputLetters(Keys.C, "c");
            UserInputLetters(Keys.D, "d");
            UserInputLetters(Keys.E, "e");
            UserInputLetters(Keys.F, "f");
            UserInputLetters(Keys.G, "g");
            UserInputLetters(Keys.H, "h");
            UserInputLetters(Keys.I, "i");
            UserInputLetters(Keys.J, "j");
            UserInputLetters(Keys.K, "k");
            UserInputLetters(Keys.L, "l");
            UserInputLetters(Keys.M, "m");
            UserInputLetters(Keys.N, "n");
            UserInputLetters(Keys.O, "o");
            UserInputLetters(Keys.P, "p");
            UserInputLetters(Keys.Q, "q");
            UserInputLetters(Keys.R, "r");
            UserInputLetters(Keys.S, "s");
            UserInputLetters(Keys.T, "t");
            UserInputLetters(Keys.U, "u");
            UserInputLetters(Keys.V, "v");
            UserInputLetters(Keys.W, "w");
            UserInputLetters(Keys.X, "x");
            UserInputLetters(Keys.Y, "y");
            UserInputLetters(Keys.Z, "z");
        }

        //Pre: None
        //Post: Letters are displayed on screen
        //Description: Display letters matching letter keys pressed
        private void UserInputLetters(Keys key, string text)
        {
            if (!prevKb.IsKeyDown(key) && kb.IsKeyDown(key))
            {
                if (username.Length < 8)
                {
                    username += text;
                }
            }
        }

        //Pre: None
        //Post: Last letter or number of text is removed
        //Description: Allow for backspacing to occur by removing the last letter or number when backspace is pressed
        private void Backspace()
        {
            if (!prevKb.IsKeyDown(Keys.Back) && kb.IsKeyDown(Keys.Back))
            {
                //Remove last letter/number
                if (username.Length > 0)
                {
                    username = username.Substring(0, username.Length - 1);
                }
                else
                {
                    username = "";
                }
            }
        }

        //Pre: None
        //Post: gameArea array is updated and blocks are drawn on screen
        //Description: Display blocks on screen and imprint blocks into gameArea array
        private void BlocksImprinting()
        {
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    //Display blocks
                    if (!isBlockImprinted)
                    {
                        if (curBlock[row, col] != 0)
                        {
                            x = blockX + (col * (int)(fireImg.Width * 0.15));
                            y = blockY + (row * (int)(fireImg.Height * 0.15));

                            curRec = new Rectangle(x, y, (int)(fireImg.Width * 0.15), (int)(fireImg.Height * 0.15));
                            waterRec = new Rectangle(x, y, 36, 36);
                            pwrWaterRec = new Rectangle(x, y, 36, 36);
                            steelRec = new Rectangle(x, y, 36, 36);

                            if (randomNum >= 1 && randomNum <= 7)
                            {
                                spriteBatch.Draw(fireImg, curRec, colors[curBlock[row, col]]);
                            }
                            else if (randomNum == 8)
                            {
                                spriteBatch.Draw(waterImg, waterRec, colors[curBlock[row, col]]);
                            }
                            else if (randomNum == 9 || randomNum == 10)
                            {
                                spriteBatch.Draw(steelImg, steelRec, colors[curBlock[row, col]]);
                            }
                            else if (randomNum == 11)
                            {
                                spriteBatch.Draw(pwrWaterImg, pwrWaterRec, colors[curBlock[row, col]]);
                            }
                        }
                    }

                    //Imprint blocks
                    if (isBlockImprinted)
                    {
                        if (oldBlock[row, col] != 0)
                        {
                            newX = oldBlockX + (col * (int)(fireImg.Width * 0.15));
                            newY = oldBlockY + (row * (int)(fireImg.Height * 0.15));

                            newCol = (newX - gameAreaRec.X) / 36;
                            newRow = (newY - gameAreaRec.Y) / 36;

                            gameArea[newRow, newCol] = prevRandomNum;
                        }
                    }
                }
            }
        }

        //Pre: None
        //Post: Display blocks in game area
        //Description: Display blocks that have been imprinted in the gameArea array
        private void DisplayBlocks()
        {
            for (int r = 0; r < 20; r++)
            {
                for (int c = 0; c < 10; c++)
                {
                    if (gameArea[r, c] != 0)
                    {
                        if (gameArea[newRow, newCol] == 8)
                        {
                            gameArea[newRow, newCol] = 0;
                        }

                        if (gameArea[newRow, newCol] == 11)
                        {
                            gameArea[newRow, newCol] = 0;
                        }

                        oldX = gameAreaRec.X + (c * (int)(fireImg.Width * 0.15));
                        oldY = gameAreaRec.Y + (r * (int)(fireImg.Width * 0.15));

                        oldRec = new Rectangle(oldX, oldY, (int)(fireImg.Width * 0.15), (int)(fireImg.Height * 0.15));
                        steelRec = new Rectangle(oldX, oldY, 36, 36);

                        if (gameArea[r, c] == 9 || gameArea[r, c] == 10)
                        {
                            spriteBatch.Draw(steelImg, steelRec, colors[gameArea[r, c]]);
                        }
                        else
                        {
                            spriteBatch.Draw(fireImg, oldRec, colors[gameArea[r, c]]);
                        }

                        //Track finishing of imprinting
                        isBlockImprinted = false;
                        isBlockFinishImprinting = true;
                    }
                }
            }
        }

        //Pre: None
        //Post: Block array values are set
        //Description: Set the values for each block and its rotation
        private void Blocks()
        {
            //Square:
            squareBlock[2, 0] = 1;
            squareBlock[2, 1] = 1;
            squareBlock[3, 0] = 1;
            squareBlock[3, 1] = 1;

            //Z Block:
            zBlock[1, 0] = 2;
            zBlock[1, 1] = 2;
            zBlock[2, 1] = 2;
            zBlock[2, 2] = 2;

            zBlockR1[1, 1] = 2;
            zBlockR1[2, 1] = 2;
            zBlockR1[1, 2] = 2;
            zBlockR1[0, 2] = 2;

            zBlockR2[0, 0] = 2;
            zBlockR2[0, 1] = 2;
            zBlockR2[1, 1] = 2;
            zBlockR2[1, 2] = 2;

            zBlockR3[0, 1] = 2;
            zBlockR3[1, 1] = 2;
            zBlockR3[1, 0] = 2;
            zBlockR3[2, 0] = 2;

            //S Block:
            sBlock[1, 1] = 3;
            sBlock[1, 2] = 3;
            sBlock[2, 0] = 3;
            sBlock[2, 1] = 3;

            sBlockR1[0, 1] = 3;
            sBlockR1[1, 1] = 3;
            sBlockR1[1, 2] = 3;
            sBlockR1[2, 2] = 3;

            sBlockR2[0, 1] = 3;
            sBlockR2[0, 2] = 3;
            sBlockR2[1, 0] = 3;
            sBlockR2[1, 1] = 3;

            sBlockR3[0, 0] = 3;
            sBlockR3[1, 0] = 3;
            sBlockR3[1, 1] = 3;
            sBlockR3[2, 1] = 3;

            //T Block:
            tBlock[2, 0] = 4;
            tBlock[1, 1] = 4;
            tBlock[2, 1] = 4;
            tBlock[2, 2] = 4;

            tBlockR1[1, 1] = 4;
            tBlockR1[2, 0] = 4;
            tBlockR1[2, 1] = 4;
            tBlockR1[3, 1] = 4;

            tBlockR2[2, 0] = 4;
            tBlockR2[2, 1] = 4;
            tBlockR2[2, 2] = 4;
            tBlockR2[3, 1] = 4;

            tBlockR3[1, 1] = 4;
            tBlockR3[2, 1] = 4;
            tBlockR3[2, 2] = 4;
            tBlockR3[3, 1] = 4;

            //I Block:
            iBlock[0, 1] = 5;
            iBlock[1, 1] = 5;
            iBlock[2, 1] = 5;
            iBlock[3, 1] = 5;

            iBlockR1[1, 0] = 5;
            iBlockR1[1, 1] = 5;
            iBlockR1[1, 2] = 5;
            iBlockR1[1, 3] = 5;

            iBlockR2[0, 1] = 5;
            iBlockR2[1, 1] = 5;
            iBlockR2[2, 1] = 5;
            iBlockR2[3, 1] = 5;

            iBlockR3[1, 0] = 5;
            iBlockR3[1, 1] = 5;
            iBlockR3[1, 2] = 5;
            iBlockR3[1, 3] = 5;

            //L Block:
            lBlock[0, 1] = 6;
            lBlock[1, 1] = 6;
            lBlock[2, 1] = 6;
            lBlock[2, 2] = 6;

            lBlockR1[1, 0] = 6;
            lBlockR1[1, 1] = 6;
            lBlockR1[1, 2] = 6;
            lBlockR1[0, 2] = 6;

            lBlockR2[0, 0] = 6;
            lBlockR2[0, 1] = 6;
            lBlockR2[1, 1] = 6;
            lBlockR2[2, 1] = 6;

            lBlockR3[2, 0] = 6;
            lBlockR3[1, 0] = 6;
            lBlockR3[1, 1] = 6;
            lBlockR3[1, 2] = 6;

            //J Block:
            jBlock[2, 0] = 7;
            jBlock[0, 1] = 7;
            jBlock[1, 1] = 7;
            jBlock[2, 1] = 7;

            jBlockR1[1, 0] = 7;
            jBlockR1[1, 1] = 7;
            jBlockR1[1, 2] = 7;
            jBlockR1[2, 2] = 7;

            jBlockR2[0, 1] = 7;
            jBlockR2[0, 2] = 7;
            jBlockR2[1, 1] = 7;
            jBlockR2[2, 1] = 7;

            jBlockR3[0, 0] = 7;
            jBlockR3[1, 0] = 7;
            jBlockR3[1, 1] = 7;
            jBlockR3[1, 2] = 7;

            //Water Balloon Block:
            waterBlock[0, 0] = 8;

            //Steel Obstacles:
            //2 Squares:
            steelBlock1[0, 1] = 9;
            steelBlock1[1, 1] = 9;

            steelBlock1R1[1, 0] = 9;
            steelBlock1R1[1, 1] = 9;

            steelBlock1R2[2, 1] = 9;
            steelBlock1R2[1, 1] = 9;

            steelBlock1R3[1, 2] = 9;
            steelBlock1R3[1, 1] = 9;

            //3 Squares:
            steelBlock2[0, 1] = 10;
            steelBlock2[1, 1] = 10;
            steelBlock2[2, 1] = 10;

            steelBlock2R1[1, 0] = 10;
            steelBlock2R1[1, 1] = 10;
            steelBlock2R1[1, 2] = 10;

            steelBlock2R2[0, 1] = 10;
            steelBlock2R2[1, 1] = 10;
            steelBlock2R2[2, 1] = 10;

            steelBlock2R3[1, 0] = 10;
            steelBlock2R3[1, 1] = 10;
            steelBlock2R3[1, 2] = 10;

            //Powerful Water Balloon Block:
            pwrWaterBlock[0, 0] = 11;
        }

        //Pre: None
        //Post: Current block used is determined
        //Description: Determine the current block based on a random number
        private void RandomBlocks()
        {
            switch (randomNum)
            {
                case 1:
                    CopyBlock(squareBlock);
                    break;
                case 2:
                    CopyBlock(zBlock);
                    break;
                case 3:
                    CopyBlock(sBlock);
                    break;
                case 4:
                    CopyBlock(tBlock);
                    break;
                case 5:
                    CopyBlock(iBlock);
                    break;
                case 6:
                    CopyBlock(lBlock);
                    break;
                case 7:
                    CopyBlock(jBlock);
                    break;
                case 8:
                    CopyBlock(waterBlock);
                    break;
                case 9:
                    CopyBlock(steelBlock1);
                    break;
                case 10:
                    CopyBlock(steelBlock2);
                    break;
                case 11:
                    CopyBlock(pwrWaterBlock);
                    break;
            }
        }

        //Pre: None
        //Post: Blocks fall downwards and remain in place upon collision with another block or ground
        //Description: Allow blocks to fall downwards, stay in place with gameTime, and continue falling. Blocks are imprinted upon collision
        private void BlockFall(GameTime gameTime)
        {
            //Allow blocks to move downwards
            goDown = true;

            //Determine when blocks can move downwards
            for (int row = 3; row >= 0; row--)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (curBlock[row, col] != 0)
                    {
                        if (blockY > 0)
                        {
                            fallX = blockX + col * 36;
                            fallY = blockY + row * 36;

                            gameAreaCol = (fallX - gameAreaRec.X) / 36;
                            gameAreaRow = (fallY - gameAreaRec.Y) / 36;

                            if (gameArea[gameAreaRow + 1, gameAreaCol] != 0)
                            {
                                goDown = false;
                            }
                        }
                    }
                }
            }    
         
            //Ensure blocks stay in place, then continue falling until collision
            if (goDown)
            {
                blockY = newPos;
                stay -= gameTime.ElapsedGameTime.Milliseconds;

                if (stay <= 0)
                {
                    stay = 1000;
                    newPos += curRec.Height;
                }
            }
            else
            {
                Imprinting();

                BlockReset();
            }
        }

        //Pre: None
        //Post: Block movement takes place
        //Description: Blocks move and change according to arrow keys pressed
        private void BlockControl()
        {
            //Allow for arrow keys to control blocks
            if (!prevKb.IsKeyDown(Keys.Up) && kb.IsKeyDown(Keys.Up))
            {
                UpKey();
            }
            else if (!prevKb.IsKeyDown(Keys.Left) && kb.IsKeyDown(Keys.Left))
            {
                LeftKey();
            }
            else if (!prevKb.IsKeyDown(Keys.Right) && kb.IsKeyDown(Keys.Right))
            {
                RightKey();
            }
            else if (!prevKb.IsKeyDown(Keys.Down) && kb.IsKeyDown(Keys.Down))
            {
                DownKey();
            }

            //Determine block adjustments
            BlockAdjustments();

            //Determine bottom Y value of current block
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (curBlock[row, col] != 0)
                    {
                        bottomY = blockY + (36 * columnBlocks) + 36;
                    }
                }
            }

            //Ensure blocks stay within game borders
            GameBorders();
        }

        //Pre: None
        //Post: Blocks move downwards
        //Description: Move blocks downwards when down key is pressed
        private void DownKey()
        {
            goDown = true;

            for (int row = 3; row >= 0; row--)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (curBlock[row, col] != 0)
                    {
                        if (blockY > 0)
                        {
                            fallX = blockX + col * 36;
                            fallY = blockY + row * 36;

                            gameAreaCol = (fallX - gameAreaRec.X) / 36;
                            gameAreaRow = (fallY - gameAreaRec.Y) / 36;

                            if (gameArea[gameAreaRow + 1, gameAreaCol] != 0)
                            {
                                goDown = false;
                            }
                        }
                    }
                }
            }

            if (goDown)
            {
                newPos += curRec.Height;
            }
            else
            {
                Imprinting();

                BlockReset();
            }
        }

        //Pre: None
        //Post: Blocks rotate
        //Description: Rotate blocks when up key is pressed
        private void UpKey()
        {
            if (randomNum != 1 || randomNum != 8 || randomNum != 11)
            {
                canRotate = true;
            }

            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (curBlock[row, col] != 0)
                    {
                        if (blockY > 0)
                        {
                            fallX = blockX + col * 36;
                            fallY = blockY + row * 36;

                            gameAreaCol = (fallX - gameAreaRec.X) / 36;
                            gameAreaRow = (fallY - gameAreaRec.Y) / 36;

                            if (gameAreaCol > 0 && gameAreaCol < 9 && gameArea[gameAreaRow, gameAreaCol - 1] != 0 && 
                                gameArea[gameAreaRow, gameAreaCol + 1] != 0)
                            {
                                canRotate = false;
                            }
                        }
                    }
                }
            }

            if (canRotate)
            {
                rotate++;
            }
        }

        //Pre: None
        //Post: Blocks move to the left
        //Description: Move blocks to the left when left key is pressed
        private void LeftKey()
        {
            goLeft = true;

            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (curBlock[row, col] != 0)
                    {
                        if (blockY > 0)
                        {
                            fallX = blockX + col * 36;
                            fallY = blockY + row * 36;

                            gameAreaCol = (fallX - gameAreaRec.X) / 36;
                            gameAreaRow = (fallY - gameAreaRec.Y) / 36;

                            if (gameAreaCol > 0 && gameArea[gameAreaRow, gameAreaCol - 1] != 0)
                            {
                                goLeft = false;
                            }
                        }
                    }
                }
            }

            if (goLeft)
            {
                blockX = blockX - (int)(fireImg.Width * 0.15);
            }
        }

        //Pre: None
        //Post: Blocks move to the right
        //Description: Move blocks to the right when right key is pressed
        private void RightKey()
        {
            goRight = true;

            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (curBlock[row, col] != 0)
                    {
                        if (blockY > 0)
                        {
                            fallX = blockX + col * 36;
                            fallY = blockY + row * 36;

                            gameAreaCol = (fallX - gameAreaRec.X) / 36;
                            gameAreaRow = (fallY - gameAreaRec.Y) / 36;

                            if (gameAreaCol <= 8 && gameArea[gameAreaRow, gameAreaCol + 1] != 0)
                            {
                                goRight = false;
                            }
                        }
                    }
                }
            }

            if (goRight)
            {
                blockX = blockX + (int)(fireImg.Width * 0.15);
            }
        }

        //Pre: None
        //Post: Block adjustments are set
        //Description: Set adjustments for arrays, due to extra space in 4 X 4 arrays
        private void BlockAdjustments()
        {
            //Determine row, column, and other adjustments
            //Z Block:
            RowCol(2, 3, 2, 3, 2, 2, 3, 2, 3);
            AdjustmentBlocks(2, 0, curRec.Width, 0, 0, curRec.Width, 0, 0, 0);

            //S Block:
            RowCol(3, 3, 2, 3, 2, 2, 3, 2, 3);
            AdjustmentBlocks(3, 0, curRec.Width, 0, 0, curRec.Width, 0, 0, 0);

            //T Block: 
            RowCol(4, 3, 2, 3, 2, 2, 3, 2, 3);
            AdjustmentBlocks(4, 0, 0, 0, curRec.Width, curRec.Width, curRec.Width, curRec.Width, curRec.Width);

            //I Block:
            RowCol(5, 1, 4, 1, 4, 4, 1, 4, 1);
            AdjustmentBlocks(5, curRec.Width, 0, curRec.Width, 0, 0, curRec.Width, 0, curRec.Width);

            //L Block:
            RowCol(6, 2, 3, 2, 3, 3, 2, 3, 2);
            AdjustmentBlocks(6, curRec.Width, 0, 0, 0, 0, 0, 0, curRec.Width);

            //J Block:
            RowCol(7, 2, 3, 2, 3, 3, 2, 3, 2);
            AdjustmentBlocks(7, 0, 0, curRec.Width, 0, 0, curRec.Width, 0, 0);

            //Steel Block 1:
            RowCol(9, 1, 2, 1, 2, 2, 1, 2, 1);
            AdjustmentBlocks(9, curRec.Width, 0, curRec.Width, curRec.Width, 0, curRec.Width, curRec.Width, curRec.Width);

            //Steel Block 2:
            RowCol(10, 1, 3, 1, 3, 3, 1, 3, 1);
            AdjustmentBlocks(10, curRec.Width, 0, curRec.Width, 0, 0, curRec.Width, 0, curRec.Width);

            //Square Block:
            if (randomNum == 1)
            {
                rowBlocks = 2;
                columnBlocks = 2;
                adjustmentUp = curRec.Width * 2;
                adjustmentLeft = 0;
            }

            //Water Balloon:
            if (randomNum == 8 || randomNum == 11)
            {
                rowBlocks = 1;
                columnBlocks = 1;
                adjustmentUp = 0;
                adjustmentLeft = 0;
            }
        }

        //Pre: None
        //Post: Row and column numbers are set
        //Description: Determine number of squares in one row and number of squares in one column for each block
        private void RowCol(int num, int rB0, int rB1, int rB2, int rB3, int cB0, int cB1, int cB2, int cB3)
        {
            if (randomNum == num)
            {
                switch (rotate)
                {
                    case 0:
                        rowBlocks = rB0;
                        columnBlocks = cB0;
                        break;
                    case 1:
                        rowBlocks = rB1;
                        columnBlocks = cB1;
                        break;
                    case 2:
                        rowBlocks = rB2;
                        columnBlocks = cB2;
                        break;
                    case 3:
                        rowBlocks = rB3;
                        columnBlocks = cB3;
                        break;
                    case 4:
                        rotate = 0;
                        break;
                }
            }
        }

        //Pre: num and all other adL (adjustment left) and adU (adjustment up) values are positive integers
        //Post: Adjustments are set
        //Description: Determine up and left adjustments for current block array, due to extra space
        private void AdjustmentBlocks(int num, int adL0, int adL1, int adL2, int adL3, int adU0, int adU1, int adU2, int adU3)
        {
            if (randomNum == num)
            {
                switch (rotate)
                {
                    case 0:
                        adjustmentLeft = adL0;
                        adjustmentUp = adU0;
                        break;
                    case 1:
                        adjustmentLeft = adL1;
                        adjustmentUp = adU1;
                        break;
                    case 2:
                        adjustmentLeft = adL2;
                        adjustmentUp = adU2;
                        break;
                    case 3:
                        adjustmentLeft = adL3;
                        adjustmentUp = adU3;
                        break;
                    case 4:
                        rotate = 0;
                        break;
                }
            }
        }

        //Pre: None
        //Post: Block rotations are determined
        //Description: Determine block rotations
        private void DetermineRotations()
        {
            Rotation(2, zBlock, zBlockR1, zBlockR2, zBlockR3);                         //Z Block
            Rotation(3, sBlock, sBlockR1, sBlockR2, sBlockR3);                         //S Block
            Rotation(4, tBlock, tBlockR1, tBlockR2, tBlockR3);                         //T Block
            Rotation(5, iBlock, iBlockR1, iBlockR2, iBlockR3);                         //I Block
            Rotation(6, lBlock, lBlockR1, lBlockR2, lBlockR3);                         //L Block
            Rotation(7, jBlock, jBlockR1, jBlockR2, jBlockR3);                         //J Block
            Rotation(9, steelBlock1, steelBlock1R1, steelBlock1R2, steelBlock1R3);     //Steel Block 1
            Rotation(10, steelBlock2, steelBlock2R1, steelBlock2R2, steelBlock2R3);    //Steel Block 2
        }

        //Pre: num is greater than 0 and block arrays all contain values greater than 0
        //Post: Rotations are displayed
        //Description: Display rotations by setting them as the current block
        private void Rotation(int num, int[,] block, int[,] block1, int[,] block2, int[,] block3)
        {
            if (randomNum == num)
            {
                switch (rotate)
                {
                    case 0:
                        CopyBlock(block);
                        break;
                    case 1:
                        CopyBlock(block1);
                        break;
                    case 2:
                        CopyBlock(block2);
                        break;
                    case 3:
                        CopyBlock(block3);
                        break;
                }
            }
        }

        //Pre: block is an array with values greater than 0
        //Post: curBlock is determined
        //Description: A block is stored as the current block
        private void CopyBlock(int[,] block)
        {
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    curBlock[row, col] = block[row, col];
                }
            }
        }

        //Pre: None
        //Post: Blocks stay within game area
        //Description: Stop blocks from leaving game area
        private void GameBorders()
        {
            //Ensure blocks do not leave left wall
            if ((blockX + adjustmentLeft) <= gameAreaRec.X)
            {
                //Set X value
                blockX = gameAreaRec.X - adjustmentLeft;
            }

            //Ensure blocks do not leave right wall
            if ((blockX + curRec.Width * rowBlocks + adjustmentLeft) >= gameAreaRec.Right)
            {
                //Set X value
                blockX = gameAreaRec.Right - (curRec.Width * rowBlocks + adjustmentLeft);
            }
        }

        //Pre: None
        //Post: Blocks are imprinted and power-ups are used after imprinting
        //Description: Determine when blocks are imprinted and allow for power-ups to be used after imprinting
        private void Imprinting()
        {
            //Store current values
            oldBlockX = blockX;
            oldBlockY = blockY;
            oldX = x;
            oldY = y;
            oldRec = curRec;
            prevRandomNum = randomNum;

            //Save current block into old block
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    oldBlock[row, col] = curBlock[row, col];
                }
            }

            //Begin imprinting
            isBlockImprinted = true;

            //Reset values once imprinting on larger array is complete
            if (isBlockFinishImprinting)
            {
                gameAreaCol = (blockX - gameAreaRec.X) / 36;
                gameAreaRow = (blockY - gameAreaRec.Y) / 36;

                //Allow water balloon power ups to be used
                if (randomNum == 8)
                {
                    WaterBalloon();
                }
                else if (randomNum == 11)
                {
                    PowerfulWaterBalloon();
                }
            }
        }

        //Pre: row is a positive integer
        //Post: Line clear sound effect is played, blocks move downwards, and line clear message is displayed
        //Description: Control what occurs once a line clear is detected
        private void DetectLineClear(int row, GameTime gameTime)
        {
            if (gameArea[row, 0] > 0 && gameArea[row, 0] < 8 &&
                gameArea[row, 1] > 0 && gameArea[row, 1] < 8 &&
                gameArea[row, 2] > 0 && gameArea[row, 2] < 8 &&
                gameArea[row, 3] > 0 && gameArea[row, 3] < 8 &&
                gameArea[row, 4] > 0 && gameArea[row, 4] < 8 &&
                gameArea[row, 5] > 0 && gameArea[row, 5] < 8 &&
                gameArea[row, 6] > 0 && gameArea[row, 6] < 8 &&
                gameArea[row, 7] > 0 && gameArea[row, 7] < 8 &&
                gameArea[row, 8] > 0 && gameArea[row, 8] < 8 &&
                gameArea[row, 9] > 0 && gameArea[row, 9] < 8)
            {
                //Add up lines cleared
                lineClear++;
                lineClearTotal += lineClear;

                //Shift blocks downwards by one square
                for (int r = row - 1; r >= 0; r--)
                {
                    for (int c = 0; c < 10; c++)
                    {
                        gameArea[r + 1, c] = gameArea[r, c];
                    }
                }

                //Determine line clear message
                if (lineClear == 1)
                {
                    linesMessage = "Single Line Clear!";
                    points += 100;
                }
                else if (lineClear == 2)
                {
                    linesMessage = "Double Line Clear!";
                    points += 300;
                }
                else if (lineClear >= 3)
                {
                    linesMessage = "Mega Line Clear!";
                    points += 500;
                }

                //Show line clear message
                showMessage = true;
            }
        }

        //Pre: gameTime 
        //Post: Line clear timing is set and reset
        //Description: Control for how long the line clear messages are displayed
        private void LineClearMessage(GameTime gameTime)
        {
            //Start timer for showing message
            if (showMessage)
            {
                //Count down for 2 seconds
                lineTime -= gameTime.ElapsedGameTime.Milliseconds;

                //Play line clear sound effect
                if (buttonInstance.State == SoundState.Stopped)
                {
                    linesInstance.Play();
                }

                //Increase the volume by 0.05 to a maximum of 1.0f
                linesInstance.Volume = Math.Min(1.0f, linesInstance.Volume + 0.1f);

            }
            else
            {
                //Reset values
                lineClear = 0;
                lineTime = 2000;
            }

            //Discontinue displaying line clear message
            if (lineTime <= 0)
            {
                showMessage = false;

                if (linesInstance.State == SoundState.Playing)
                {
                    linesInstance.Stop();
                }
            }
        }

        //Pre: None
        //Post: Blocks within one square radius of power-up disappear
        //Description: Determine radius around water balloon and make blocks within one square disappear
        private void WaterBalloon()
        {
            for (int i = gameAreaRow - 1; i <= gameAreaRow + 1; i++)
            {
                for (int j = gameAreaCol - 1; j <= gameAreaCol + 1; j++)
                {
                    if (i >= 0 && i <= 19 && j >= 0 && j <= 9 && gameArea[i, j] > 0)
                    {
                        //Add points
                        points += 10;

                        //Make blocks disappear
                        gameArea[i, j] = 0;
                    }
                }
            }
        }

        //Pre: None
        //Post: Blocks vertically underneath powerful water balloon disappear
        //Description: Determine the blocks that disappear when powerful water balloon is used
        private void PowerfulWaterBalloon()
        {
            for (int row = gameAreaRow + 1; row < 20; row++)
            {
                if (gameArea[row, gameAreaCol] != 0)
                {
                    points += 10;
                }

                gameArea[row, gameAreaCol] = 0;
            }
        }

        //Pre: None
        //Post: Values are reset when new block appears on screen
        //Description: Reset values for a new block
        private void BlockReset()
        {
            isBlockFinishImprinting = false;

            rotate = 0;

            powerObs = rng.Next(1, 26);

            if (powerObs == 1 || powerObs == 2)
            {
                randomNum = 8;
            }
            else if (powerObs == 3)
            {
                randomNum = 9;
            }
            else if (powerObs == 4)
            {
                randomNum = 10;
            }
            else if (powerObs == 5)
            {
                randomNum = 11;
            }
            else
            {
                randomNum = rng.Next(1, 8);
            }

            RandomBlocks();

            blockX = 542;
            blockY = -57;
            newPos = -57;
        }

        //Pre: None
        //Post: Locations of names and scores are determined
        //Description: Set locations of names and scores in the scoreboard
        private void NamesScores()
        {
            //Set locations of names
            namesLoc[9] = new Vector2(400, 200);
            namesLoc[8] = new Vector2(400, 240);
            namesLoc[7] = new Vector2(400, 280);
            namesLoc[6] = new Vector2(400, 320);
            namesLoc[5] = new Vector2(400, 360);
            namesLoc[4] = new Vector2(400, 400);
            namesLoc[3] = new Vector2(400, 440);
            namesLoc[2] = new Vector2(400, 480);
            namesLoc[1] = new Vector2(400, 520);
            namesLoc[0] = new Vector2(400, 560);

            //Set locations of top scores
            scoresLoc[9] = new Vector2(700, 200);
            scoresLoc[8] = new Vector2(700, 240);
            scoresLoc[7] = new Vector2(700, 280);
            scoresLoc[6] = new Vector2(700, 320);
            scoresLoc[5] = new Vector2(700, 360);
            scoresLoc[4] = new Vector2(700, 400);
            scoresLoc[3] = new Vector2(700, 440);
            scoresLoc[2] = new Vector2(700, 480);
            scoresLoc[1] = new Vector2(700, 520);
            scoresLoc[0] = new Vector2(700, 560);
        }

        //Pre: None
        //Post: The order of the names and scores in the scoreboard is modified
        /*Description: If the user's score is high enough for the leaderboard, reorganize the leader board to insert
        the user's score and username*/
        private void Scoring()
        {
            //Replace highest score and name
            if (savedPoints >= Convert.ToInt32(scoreboard[9, 1]))
            {
                for (int i = 0; i <= 8; i++)
                {
                    scoreboard[i, 1] = scoreboard[i + 1, 1];
                }
                scoreboard[9, 1] = Convert.ToString(savedPoints);

                for (int j = 0; j <= 8; j++)
                {
                    scoreboard[j, 0] = scoreboard[j + 1, 0];
                }
                scoreboard[9, 0] = savedUsername;
            }

            //Replace all other scores and names
            ScoreboardReplacement(9, 8, 7);
            ScoreboardReplacement(8, 7, 6);
            ScoreboardReplacement(7, 6, 5);
            ScoreboardReplacement(6, 5, 4);
            ScoreboardReplacement(5, 4, 3);
            ScoreboardReplacement(4, 3, 2);
            ScoreboardReplacement(3, 2, 1);

            //Replace last two scores and names
            if (savedPoints < Convert.ToInt32(scoreboard[2, 1]) && savedPoints >= Convert.ToInt32(scoreboard[1, 1]))
            {
                scoreboard[0, 1] = scoreboard[1, 1];
                scoreboard[1, 1] = Convert.ToString(savedPoints);

                scoreboard[0, 0] = scoreboard[1, 0];
                scoreboard[1, 0] = savedUsername;
            }
            else if (savedPoints < Convert.ToInt32(scoreboard[1, 1]) && savedPoints >= Convert.ToInt32(scoreboard[0, 1]))
            {
                scoreboard[0, 1] = Convert.ToString(savedPoints);
                scoreboard[0, 0] = savedUsername;
            }
        }

        /*Pre: score is an integer greater or equal to 0, highRow is an integer between 3 and 9, lowRow is an integer between 2 and 8, and replaceNum
        is an integer between 1 and 7*/
        //Post: Elements in scoreboard are reordered
        /*Description: Reorganize elements in center area of scoreboard by inserting score and name in the correct location. The score is compared against
        two rows, highRow and lowRow, to determine if the score should be inserted while replaceNum is used for counting the number of element positions to be
        shifted. After score position is changed, name position is adjusted accordingly*/
        private void ScoreboardReplacement(int highRow, int lowRow, int replaceNum)
        {
            if (savedPoints < Convert.ToInt32(scoreboard[highRow, 1]) && savedPoints >= Convert.ToInt32(scoreboard[lowRow, 1]))
            {
                for (int i = 0; i <= replaceNum; i++) 
                {
                    scoreboard[i, 1] = scoreboard[i + 1, 1];
                }
                scoreboard[lowRow, 1] = Convert.ToString(savedPoints);

                for (int j = 0; j <= replaceNum; j++)
                {
                    scoreboard[j, 0] = scoreboard[j + 1, 0];
                }
                scoreboard[lowRow, 0] = savedUsername;
            }
        }

        //Pre: None
        //Post: Scoreboard data is written in TopScores file 
        //Description: Using file IO, write new scoreboard data into TopScores file
        private void WriteFile()
        {
            //Write to the TopScores file
            outFile = File.CreateText(filePath);

            //Write scoreboard data
            for (int i = 0; i <= 9; i++)
            {
                outFile.WriteLine(scoreboard[i, 0] + "," + scoreboard[i, 1]);
            }

            //Close file
            outFile.Close();
        }

        //Pre: None
        //Post: Scoreboard data is read from TopScores file 
        //Description: Using file IO, read scoreboard data from TopScores file
        private void ReadFile()
        {
            //Read data from TopScores file
            inFile = File.OpenText(filePath);

            //Retrieve data and separate data from file
            for (int i = 0; i <= 9; i++)
            {
                line = inFile.ReadLine();
                data = line.Split(',');

                scoreboard[i, 0] = data[0];
                scoreboard[i, 1] = data[1];
            }

            //Close file
            inFile.Close();
        }

        //Pre: None
        //Post: Screen is switched to a game over screen depending on user's score and scoreboard is updated
        //Description: Update scoreboard and switch to new screen depending on user's score
        private void DetermineGameOver()
        {
            for (int row = 0; row < 20; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    if (gameArea[row, col] != 0)
                    {
                        y = gameAreaRec.Y + (row * 36);

                        if (bottomY == y && (y - gameAreaRec.Y) <= columnBlocks * 36)
                        {
                            //Save end of game values
                            savedTime = time;
                            savedLineClear = lineClearTotal;
                            savedPoints = points;
                            savedUsername = username;

                            //Update scoreboard
                            Scoring();

                            //Update scoreboard with file IO
                            WriteFile();
                            ReadFile();

                            //Determine screen to show
                            if (points < Convert.ToInt32(scoreboard[0, 1]))
                            {
                                //Display game over screen
                                switchScreen = "GAMEOVER";
                            }
                            else if (points >= Convert.ToInt32(scoreboard[0, 1]))
                            {
                                //Display top 10 game over screen
                                switchScreen = "TOP10";
                            }
                        }
                    }
                }
            }
        }

        //Pre: None
        //Post: Values for a new game are reset
        //Description: Reset values each game
        private void GameReset()
        {
            rotate = 0;

            blockX = 542;
            blockY = -57;
            newPos = -57;

            for (int row = 0; row < 20; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    gameArea[row, col] = 0;
                }
            }

            lineClear = 0;
            lineClearTotal = 0;
            time = 0;
            username = "";
            points = 0;
        }
    }
}
