using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.Components;
using MonoGame.Forms.Controls;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using Eridanus.SpaceSystems;
using Microsoft.Xna.Framework.Input;
using EridanusA;
using System;

namespace Eridanus
{
    public class DrawTest : MonoGameControl
    {
        public static GraphicsDevice graphicsDevice;
        private Texture2D background;
        public Camera camera;
        public static int curSystem=0;
        public static Form1 gameForm;

        private float currentMouseWheelValue, previousMouseWheelValue;
        Vector2 prevClick;
        MouseState prevState, mouseState;
        KeyboardState state;
        protected override void Initialize()
        {
            graphicsDevice = GraphicsDevice;
            base.Initialize();
            FileStream fileStream = new FileStream("Resources/spaceback.png", FileMode.Open);
            background = Texture2D.FromStream(this.GraphicsDevice, fileStream);
            fileStream.Dispose();
            camera = new Camera(GraphicsDevice.Viewport);
            Galaxy.load();

            Thread g = new Thread(GameRun.run);
            g.Start();
        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            state = Keyboard.GetState();
            Vector2 cameraMovement = Vector2.Zero;
            if (state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
            {
                //pause button
                GameRun.paused = true;
            }


            int moveSpeed;

            moveSpeed = ((int)((int)(50f / camera.Zoom) + 1f));

            if (moveSpeed > 1000) { moveSpeed = 1000; }

            if (mouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                Vector2 click = new Vector2(mouseState.X, mouseState.Y);
                if (click.Y > 60)   //ignore Top of screen
                {
                    if (prevState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                    {
                        //held down
                        cameraMovement = (prevClick - click) * (float)(moveSpeed * .05);

                    }
                    else
                    {
                        //check where the click was, for units, planets
                        Vector2 point = camera.ScreenToPoint(click); //use zoom as +/- margin when searching
                        if (curSystem > -1)
                        {
                            //check current system

                            

                        }
                        else
                        {
                            //check galactic map

                        }
                    }
                    prevClick = click;
                }
            }
            //additonal options/command on unit
            if (mouseState.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                Vector2 click = new Vector2(mouseState.X, mouseState.Y);
                //check where the click was
            }

            prevState = mouseState;

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                cameraMovement.Y = -moveSpeed;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                cameraMovement.Y = moveSpeed;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                cameraMovement.X = -moveSpeed;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                cameraMovement.X = moveSpeed;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.C))
            {
                camera.Position = Vector2.Zero;
            }

            previousMouseWheelValue = currentMouseWheelValue;
            currentMouseWheelValue = Mouse.GetState().ScrollWheelValue;

            if (currentMouseWheelValue > previousMouseWheelValue)
            {
                camera.AdjustZoom(.005f);

            }

            if (currentMouseWheelValue < previousMouseWheelValue)
            {
                camera.AdjustZoom(-.005f);
            }
            camera.MoveCamera(cameraMovement);

            camera.UpdateCamera(this);
        }

        protected override void Draw()
        {
            base.Draw();
            Editor.graphics.Clear(Color.Black);

            Editor.spriteBatch.Begin();
            Editor.spriteBatch.Draw(background, Vector2.Zero);
            Editor.spriteBatch.End();

            Editor.spriteBatch.Begin(SpriteSortMode.Texture, BlendState.NonPremultiplied, null, null, null, null, camera.Transform);

            if (curSystem > -1)
            {
                Body temp;
                Craft temp2;

                if (Settings.drawOrbits){
                    /*
                   for (int j = 0; j < Galaxy.solSystems[curSystem].bodies.Count; j++) //draw all planets in current system
                    {
                        temp = Galaxy.solSystems[curSystem].bodies[j];
                        Editor.spriteBatch.Draw(circleSprite, Vector2.Zero, origin: new Vector2(circleSprite.Width / 2, circleSprite.Height / 2), scale: temp.radius);
                    }
                    */
                }
               

                for (int j = 0; j < Galaxy.solSystems[curSystem].bodies.Count; j++) //draw all planets in current system
                {
                    temp = Galaxy.solSystems[curSystem].bodies[j];
                    Editor.spriteBatch.Draw(temp.sprite, temp.loc, origin: new Vector2(temp.sprite.Width / 2, temp.sprite.Height / 2), scale: temp.scale);
                }
                for (int j = 0; j < Galaxy.solSystems[curSystem].crafts.Count; j++) //draw all crafts in current system
                {
                    temp2 = Galaxy.crafts[Galaxy.solSystems[curSystem].crafts[j]];
                    Editor.spriteBatch.Draw(temp2.type.sprite, temp2.loc, rotation: temp2.orientation, origin: new Vector2(temp2.type.sprite.Width / 2, temp2.type.sprite.Height / 2), scale: temp2.type.scale);
                }

            }
            else
            {

                for (int i = 0; i < Galaxy.solSystems.Count; i++)
                {
                    //draws just the sun from each system
                    Editor.spriteBatch.Draw(Galaxy.solSystems[i].bodies[0].sprite, Galaxy.solSystems[i].loc, origin: new Vector2(Galaxy.solSystems[i].bodies[0].sprite.Width / 2, Galaxy.solSystems[i].bodies[0].sprite.Height / 2), scale: Galaxy.solSystems[i].bodies[0].scale);
                }
                /*
                for (int i = 0; i < galaxy.crafts.Count; i++)
                {

                    spriteBatch.Draw(galaxy.crafts[i].type.sprite, galaxy.crafts[i].loc, rotation: galaxy.crafts[i].orientation, origin: new Vector2(galaxy.crafts[i].type.sprite.Width / 2, galaxy.crafts[i].type.sprite.Height / 2), scale: galaxy.crafts[i].type.scale);
                }
                */

            }

            Editor.spriteBatch.End();
            gameForm.updateTimeLabel(GameRun.worldTime);
        }

        public Viewport getView() { return GraphicsDevice.Viewport;  }
    }
}