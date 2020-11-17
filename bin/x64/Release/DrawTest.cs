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

        //UI
        BaseObj leftSelected, rightSelected;

        protected override void Initialize()
        {
            graphicsDevice = GraphicsDevice;
            base.Initialize();
            FileStream fileStream = new FileStream("Resources/spaceback.png", FileMode.Open);
            background = Texture2D.FromStream(this.GraphicsDevice, fileStream);
            fileStream.Dispose();
            //orbit.SetData(new[] { Color.Green });
            
            camera = new Camera(GraphicsDevice.Viewport);
            Galaxy.load();
            leftSelected = null;
            rightSelected = null;
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
                leftSelected = null;
                if (click.Y > 60)   //ignore Top of screen
                {
                    if (prevState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                    {
                        //held down
                        cameraMovement = (prevClick - click) * (float)(moveSpeed * .05);

                    }

                    //check where the click was, for units, planets
                    Vector2 point = camera.ScreenToPoint(click); //use zoom as +/- margin when searching
                    if (curSystem > -1)
                    {
                        Body temp;
                        Craft temp2;
                        Boolean objSelected = false;

                        //check current system
                        for (int j = 0; j < Galaxy.solSystems[curSystem].crafts.Count; j++) //check all crafts in current system
                        {
                            temp2 = Galaxy.crafts[(int)Galaxy.solSystems[curSystem].crafts[j]];
                            if (temp2.getBox().Contains(point))
                            {
                                leftSelected = temp2;
                                objSelected = true;
                                break;
                            }

                        }
                        if (objSelected == false)
                        {
                            for (int j = 0; j < Galaxy.solSystems[curSystem].bodies.Count; j++) //check all planets in current system
                            {
                                temp = Galaxy.solSystems[curSystem].bodies[j];
                                if (temp.box.Contains(point))   //mouse click is within sprite
                                {
                                    leftSelected = temp;
                                    objSelected = true;
                                    break;
                                }
                            }
                        }
                        if (objSelected == false)
                        {
                            leftSelected = null;
                            rightSelected = null;
                        }

                    }
                    else
                    {
                        //check galactic map

                    }

                    prevClick = click;
                }
            }

            //additonal options/command on unit
            if (mouseState.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                Vector2 click = new Vector2(mouseState.X, mouseState.Y);
                Vector2 point = camera.ScreenToPoint(click); //use zoom as +/- margin when searching
                //check where the click was
                if (curSystem > -1)
                {
                    Body temp;
                    Craft temp2;
                    Boolean objSelected = false;

                    //check current system
                    for (int j = 0; j < Galaxy.solSystems[curSystem].crafts.Count; j++) //check all crafts in current system
                    {
                        temp2 = Galaxy.crafts[(int)Galaxy.solSystems[curSystem].crafts[j]];
                        if (temp2.getBox().Contains(point))
                        {
                            rightSelected = temp2;
                            objSelected = true;
                            break;
                        }

                    }
                    if (objSelected == false)
                    {
                        for (int j = 0; j < Galaxy.solSystems[curSystem].bodies.Count; j++) //check all planets in current system
                        {
                            temp = Galaxy.solSystems[curSystem].bodies[j];
                            if (temp.box.Contains(point))   //mouse click is within sprite
                            {
                                rightSelected = temp;
                                objSelected = true;
                                break;
                            }
                        }
                    }
                    if (objSelected == false)
                    {
                        rightSelected = null;
                    }

                }
                else
                {
                    //check galactic map

                }
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

            if (Keyboard.GetState().IsKeyDown(Keys.G))
            {
                camera.Zoom = .0005f;
                camera.AdjustZoom(-1);
            }

            previousMouseWheelValue = currentMouseWheelValue;
            currentMouseWheelValue = Mouse.GetState().ScrollWheelValue;

            if (currentMouseWheelValue > previousMouseWheelValue)
            {
                camera.AdjustZoom(+1);

            }

            if (currentMouseWheelValue < previousMouseWheelValue)
            {
                camera.AdjustZoom(-1);
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


                if (Settings.drawOrbits)
                {

                    for (int j = 0; j < Galaxy.solSystems[curSystem].bodies.Count; j++) //draw all planets in current system
                    {
                        Galaxy.solSystems[curSystem].bodies[j].drawOrbit(Editor.spriteBatch, camera.Zoom);
                        
                    }
                    Editor.spriteBatch.End();
                    Editor.spriteBatch.Begin(SpriteSortMode.Texture, BlendState.NonPremultiplied, null, null, null, null, camera.Transform);

                }

                if (leftSelected != null)
                {
                    //draw outline around selected object
                    Primitives2D.DrawRectangle(Editor.spriteBatch, leftSelected.hitbox(), Color.Green, 1f / camera.Zoom);
                }

                for (int j = 0; j < Galaxy.solSystems[curSystem].bodies.Count; j++) //draw all planets in current system
                {
                    temp = Galaxy.solSystems[curSystem].bodies[j];
                    Editor.spriteBatch.Draw(temp.sprite, destinationRectangle: temp.box);
                }
                for (int j = 0; j < Galaxy.solSystems[curSystem].crafts.Count; j++) //draw all crafts in current system
                {
                    temp2 = Galaxy.crafts[(int)Galaxy.solSystems[curSystem].crafts[j]];
                    Editor.spriteBatch.Draw(temp2.type.sprite, temp2.loc, rotation: temp2.orientation, origin: new Vector2(temp2.type.sprite.Width / 2, temp2.type.sprite.Height / 2), scale: temp2.type.scale);
                }

                

            }
            else
            {

                for (int i = 0; i < Galaxy.solSystems.Count; i++)
                {
                    //draws just the sun from each system
                    Editor.spriteBatch.Draw(Galaxy.solSystems[i].bodies[0].sprite, destinationRectangle: Galaxy.solSystems[i].box);
                }
                /*
                for (int i = 0; i < galaxy.crafts.Count; i++)
                {

                    spriteBatch.Draw(galaxy.crafts[i].type.sprite, galaxy.crafts[i].loc, rotation: galaxy.crafts[i].orientation, origin: new Vector2(galaxy.crafts[i].type.sprite.Width / 2, galaxy.crafts[i].type.sprite.Height / 2), scale: galaxy.crafts[i].type.scale);
                }
                */

            }

            
            if (rightSelected != null)
            {
                if (leftSelected != null)
                {
                    //draw orders menu
                    //rightSelected.loc
                }
                else
                {
                    //draw selected info/options menu
                    //Editor.spriteBatch.Draw(rightSelected.menu(), rightSelected.box());
                }
            }

            Editor.spriteBatch.End();
            gameForm.updateTimeLabel(GameRun.worldTime);
        }

        public Viewport getView() { return GraphicsDevice.Viewport;  }
    }
}