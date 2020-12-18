using System;
using System.Threading.Tasks;
using Eridanus.SpaceSystems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Eridanus
{
    public class Camera
    {
        public float Zoom { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle Bounds { get; protected set; }
        public Rectangle VisibleArea { get; protected set; }
        public Matrix Transform { get; protected set; }

        private float currentMouseWheelValue, previousMouseWheelValue, zoom, previousZoom;

        public Camera(Viewport viewport)
        {
            Bounds = viewport.Bounds;
            Zoom = .05f; ;
            Position = Vector2.Zero;
        }


        private void UpdateVisibleArea()
        {
            var inverseViewMatrix = Matrix.Invert(Transform);

            var tl = Vector2.Transform(Vector2.Zero, inverseViewMatrix);
            var tr = Vector2.Transform(new Vector2(Bounds.X, 0), inverseViewMatrix);
            var bl = Vector2.Transform(new Vector2(0, Bounds.Y), inverseViewMatrix);
            var br = Vector2.Transform(new Vector2(Bounds.Width, Bounds.Height), inverseViewMatrix);

            var min = new Vector2(
                MathHelper.Min(tl.X, MathHelper.Min(tr.X, MathHelper.Min(bl.X, br.X))),
                MathHelper.Min(tl.Y, MathHelper.Min(tr.Y, MathHelper.Min(bl.Y, br.Y))));
            var max = new Vector2(
                MathHelper.Max(tl.X, MathHelper.Max(tr.X, MathHelper.Max(bl.X, br.X))),
                MathHelper.Max(tl.Y, MathHelper.Max(tr.Y, MathHelper.Max(bl.Y, br.Y))));
            VisibleArea = new Rectangle((int)min.X, (int)min.Y, (int)(max.X - min.X), (int)(max.Y - min.Y));
        }

        private void UpdateMatrix()
        {
            Transform = Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) *
                    Matrix.CreateScale(Zoom, Zoom, 1) *
                    Matrix.CreateTranslation(new Vector3(Bounds.Width * 0.5f, Bounds.Height * 0.5f, 0));
            UpdateVisibleArea();
        }

        public void MoveCamera(Vector2 movePosition)
        {
            Vector2 newPosition = Position + movePosition;
            Position = newPosition;
        }

        public void AdjustZoom(int num)
        {

            Zoom += (float)(num *(.05f*Zoom));
            if (Zoom <= .0005f)
            {
                if(DrawTest.curSystem != -1)
                {
                    Position = Galaxy.solSystems[DrawTest.curSystem].loc;
                    DrawTest.curSystem = -1;    //enter galactic view
                }
               
                Zoom = .0005f;
            }
            else if(Zoom > .0005f && DrawTest.curSystem ==-1)
            {
                DrawTest.curSystem = this.closestSys();
                Position = Vector2.Zero;
            }
            if (Zoom > 20f)
            {
                Zoom = 20f;
            }

            previousZoom = zoom;
            zoom = Zoom;
        }

        public void UpdateCamera(DrawTest game)
        {
            Viewport bounds = game.getView();
            Bounds = bounds.Bounds;
            UpdateMatrix();

        }

        public Vector2 ScreenToPoint(Vector2 point)
        {
            Matrix invertedMatrix = Matrix.Invert(Transform);
            return Vector2.Transform(point, invertedMatrix);
        }

        public Vector2 PointToScreen(Vector2 point)
        {
            return Vector2.Transform(point, this.Transform);
        }

        public int closestSys()
        {
            int sysID = 0;
            float minDist = 1000000000;

            for(int i=0; i< Galaxy.solSystems.Count; i++)
            {
                float dist = (float)Math.Sqrt(Math.Pow(Position.X - Galaxy.solSystems[i].loc.X, 2)+ Math.Pow(Position.Y - Galaxy.solSystems[i].loc.Y, 2));
                if(dist< minDist)
                {
                    minDist = dist;
                    sysID = i;
                }
            }

            return sysID;
        }



    }
}
