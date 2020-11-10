using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Eridanus
{
    class InputHandler
    {
        DrawTest game;
        private float currentMouseWheelValue, previousMouseWheelValue;
        MouseState prevState, mouseState;
        KeyboardState state;

        public InputHandler(DrawTest g) { game = g; }

        public void run()
        {
            while (true)
            {
                Thread.Sleep(10);
                mouseState = Mouse.GetState();
                state = Keyboard.GetState();
                Vector2 cameraMovement = Vector2.Zero;
                if (state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
                {
                    //pause button
                    //game.end();
                }


                int moveSpeed;

                moveSpeed = ((int)((int)(50f / game.camera.Zoom) + 1f));

                if (moveSpeed > 1000) { moveSpeed = 1000; }

                if (mouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                {
                    Vector2 click = new Vector2(mouseState.X, mouseState.Y);
                    if (prevState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                    {
                        //held down
                    }

                    //check where the click was
                }
                //additonal options/command on unit
                if (mouseState.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                {
                    Vector2 click = new Vector2(mouseState.X, mouseState.Y);
                    //check where the click was
                }

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
                    game.camera.Position = Vector2.Zero;
                }

                previousMouseWheelValue = currentMouseWheelValue;
                currentMouseWheelValue = Mouse.GetState().ScrollWheelValue;

                if (currentMouseWheelValue > previousMouseWheelValue)
                {
                    game.camera.AdjustZoom(.005f);

                }

                if (currentMouseWheelValue < previousMouseWheelValue)
                {
                    game.camera.AdjustZoom(-.005f);
                }
                game.camera.MoveCamera(cameraMovement);

                game.camera.UpdateCamera(game);
            }
        }
    }
}
