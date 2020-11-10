using Eridanus.SpaceSystems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Threading;

namespace Eridanus
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class EridGame : Game
    {
        public static Settings settings;
        private static InputHandler inputHandler;
        private Thread input;
        //private static Screen currentScreen;
        private GraphicsDeviceManager graphics;
        private RenderTarget2D drawBuffer;
        private SpriteBatch spriteBatch;
        public  Camera camera;
        public static int curSystem=0; //index of currently viewed system
        public static GraphicsDevice graphicsDevice;
        private Texture2D background;
        
        public EridGame()
        {
            graphics = new GraphicsDeviceManager(this); 
            inputHandler = new InputHandler(this);
            settings = new Settings();
            Window.AllowUserResizing = true;
            this.IsFixedTimeStep = false;
            this.graphics.SynchronizeWithVerticalRetrace = true; 

            //threading
            input = new Thread(inputHandler.run);   //new thread for inputHandler
   
        }


        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.IsMouseVisible = true;
            graphicsDevice = GraphicsDevice;
            camera = new Camera(GraphicsDevice.Viewport);
            input.Start();
            spriteBatch = new SpriteBatch(GraphicsDevice);  // Create a new SpriteBatch, which can be used to draw textures.
            this.LoadContent();
        }

        protected override void OnDeactivated(object sender, EventArgs args) { }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            
            FileStream fileStream = new FileStream("Resources/spaceback.png", FileMode.Open);
            background = Texture2D.FromStream(this.GraphicsDevice, fileStream);
            fileStream.Dispose();

            SolSystem test = new SolSystem();
            test.bodies.Add(new Body("sol.png", new Vector2(10f, 10f), new Vector2(0, 0)));
            test.bodies.Add(new Planet("mercury.png", new Vector2(.2f, .2f), new Vector2(5790, 00)));
            test.bodies.Add(new Planet("venus.png", new Vector2(1.8f, 1.8f), new Vector2(10820, 00)));
            test.bodies.Add(new Planet("Earth.png", new Vector2(3f, 3f), new Vector2(14960, 0)));
            test.bodies.Add(new Planet("Mars.png", new Vector2(1.6f, 1.6f), new Vector2(22790, 000)));
            test.bodies.Add(new Planet("Jupiter.png", new Vector2(5f, 5f), new Vector2(77860, 00)));
            test.bodies.Add(new Planet("saturn.png", new Vector2(5f, 5f), new Vector2(143350, 00)));
            test.bodies.Add(new Planet("uranus.png", new Vector2(2f, 2f), new Vector2(287250, 00)));
            test.bodies.Add(new Planet("neptune.png", new Vector2(2f, 2f), new Vector2(449510, 00)));
            test.bodies.Add(new Planet("pluto.png", new Vector2(.8f, .8f), new Vector2(590640, 00)));
            test.bodies.Add(new Moon("Luna.png", new Vector2(.5f, .5f), new Vector2(38.44f, 00), test.bodies[3]));

            test.bodies[1].radians = .000049583f * 60;
            test.bodies[2].radians = .000019393f * 60;
            test.bodies[3].radians = .000011954f * 60;
            test.bodies[4].radians = .000006351f * 60;
            test.bodies[5].radians = .000001008f * 60;
            test.bodies[6].radians = .000024733f;
            test.bodies[7].radians = .000008539f;
            test.bodies[8].radians = .000004347f;
            test.bodies[9].radians = .000002892f;
            test.bodies[10].radians = .009696274f;

            //Program.galaxy.solSystems.Add(test);
        }

        public Viewport getView() { return GraphicsDevice.Viewport;  }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            base.UnloadContent();

            if (drawBuffer != null)
            {
                drawBuffer.Dispose();
                drawBuffer = null;
            }

            if (spriteBatch != null)
            {
                spriteBatch.Dispose();
                spriteBatch = null;
            }
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime){}


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Black);

            //draws background, orbits
            spriteBatch.Begin();
            spriteBatch.Draw(background, Vector2.Zero);
            

            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Texture, BlendState.NonPremultiplied, null, null, null, null, camera.Transform);

            if (curSystem > -1)
            {
                //if(settings.drawOrbits()){}
 
                
                Body temp;
                Craft temp2;

                for (int j = 0; j < Program.galaxy.solSystems[curSystem].bodies.Count; j++) //draw all planets in current system
                {
                    temp = Program.galaxy.solSystems[curSystem].bodies[j];
                    spriteBatch.Draw(temp.sprite, temp.loc, origin: new Vector2(temp.sprite.Width / 2, temp.sprite.Height / 2), scale: temp.scale);
                }
                for (int j = 0; j < Program.galaxy.solSystems[curSystem].crafts.Count; j++) //draw all crafts in current system
                {
                    temp2 = Program.galaxy.solSystems[curSystem].crafts[j];
                    spriteBatch.Draw(temp2.type.sprite, temp2.loc, rotation: temp2.orientation, origin: new Vector2(temp2.type.sprite.Width / 2, temp2.type.sprite.Height / 2), scale: temp2.type.scale);
                }
            }
            else
            {
                //draw galactic map
                /*
                for (int i = 0; i < galaxy.solSystems.Count; i++)
                {
                    //draws just the sun from each system
                    spriteBatch.Draw(galaxy.solSystems[i].bodies[0].sprite, galaxy.solSystems[i].loc, origin: new Vector2(galaxy.solSystems[i].bodies[0].sprite.Width / 2, galaxy.solSystems[i].bodies[0].sprite.Height / 2), scale: galaxy.solSystems[i].bodies[0].scale);
                }
                for (int i = 0; i < galaxy.crafts.Count; i++)
                {

                    spriteBatch.Draw(galaxy.crafts[i].type.sprite, galaxy.crafts[i].loc, rotation: galaxy.crafts[i].orientation, origin: new Vector2(galaxy.crafts[i].type.sprite.Width / 2, galaxy.crafts[i].type.sprite.Height / 2), scale: galaxy.crafts[i].type.scale);
                }
                */

            }

            spriteBatch.End();

            //draw UI
            /*
             spriteBatch.Begin();
            //spriteBatch.DrawString(arial, worldTime.toString(), Vector2.Zero, Color.White);
             spriteBatch.End();
            */
            //base.Draw(gameTime);

            //currentScreen.Render();
        }


        public void end()
        {
           this.Exit();
            try
            {
                input.Abort();
            }
            catch (System.Threading.ThreadAbortException) { }
        }

        protected override void OnExiting(object sender, EventArgs args) { this.end(); }
    }
}
