using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eridanus
{
    class ClickMenu
    {
        public BaseObj obj; //top left corner of box, location of planet or craft
        public Rectangle hitbox;
        public List<String> text;   //caption for each line
        public static Color color = new Color(117, 226, 247, 180), ordColor = new Color(242, 216, 87), boxColor = new Color(143, 252, 226, 180);
        public static int width = 100, height = 25;

        public ClickMenu(BaseObj b)
        {
            obj = b;
            text = new List<string>();
            text.Add("Enter Orbit");
            text.Add("Land on Surface");
            text.Add("Survey");
            text.Add("Load Cargo");
            text.Add("Unload Cargo");
            hitbox = new Rectangle((int)obj.loc.X, (int)obj.loc.Y, width + 10, ((height + 5) * (text.Count+1)));
        }

        public void draw(SpriteBatch s, Camera c)
        {
            Vector2 loc = c.PointToScreen(obj.loc);
            hitbox = new Rectangle((int)loc.X, (int)loc.Y, width + 10, ((height+5) * (text.Count+1)));
            Primitives2D.FillRectangle(s, hitbox, color);
            //s.DrawString(default, "Orders", position: new Vector2((int)hitbox.X, (int)hitbox.Y), ordColor);
            float x = hitbox.X+5;
            float y = hitbox.Y+5+height;

            for (int i = 0; i < text.Count; i++) { 

           
                Primitives2D.FillRectangle(s, new Rectangle((int)x, (int)y, width, height), boxColor);

                //s.DrawString(default, text[i], position: new Vector2((int)x,(int)y), Color.White);
                y += height + 5;
            }
        }

        public Boolean checkClick(Vector2 click)
        {
            float x = hitbox.X + 5;
            float y = hitbox.Y;
            Rectangle box;

            for (int i = 0; i < text.Count; i++) { 
                y += height + 5;
                box =new Rectangle((int)x, (int)y, width, height);

                if (box.Contains(click))
                {
                    //do order
                    return true;
                }
                
            }
            return false;
        }
    }
}
