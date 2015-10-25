using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp
{
    public class OrnamentBase : GroupComponent
    {
        protected string text;
        protected string side;
        private List<GroupComponent> components = new List<GroupComponent>();
        public virtual string getText()
        {
            return this.text;
        }
        public virtual string getSide()
        {
            return this.side;
        }

        public override void Add(GroupComponent c)
        {
            components.Add(c);
        }

        public override void Remove(GroupComponent c)
        {
            throw new NotImplementedException();
        }

        public override void Display(int depth)
        {
            Console.WriteLine(new String(' ', depth) + "ornament " + getSide() + " \"" + getText() + "\"");

            // Recursively display child nodes
            foreach (GroupComponent component in components)
            {
                component.Display(depth);
            }
        }

        public override bool ContainsMember(GroupComponent shape)
        {
            throw new NotImplementedException();
        }

        public override void WriteToFile(System.IO.StreamWriter writer, int depth)
        {
            writer.WriteLine(new String(' ', depth) + "ornament " + getSide() + " \"" + getText() + "\"");
            foreach (GroupComponent component in components)
            {
                component.WriteToFile(writer, depth);
            }
        }

        public override List<GroupComponent> UnGroup()
        {
            return components;
        }

        public override void ToggleSelected()
        {
            foreach (GroupComponent component in components)
            {
                component.ToggleSelected();
            }
        }

        public override bool isSelected()
        {
            bool selected = false;
            foreach (GroupComponent component in components)
            {
                if (component.isSelected())
                {
                    selected = true;
                }
            }
            return selected;
        }

        public override void setSelected(bool selected)
        {
            foreach (GroupComponent component in components)
            {
                component.setSelected(selected);
            }
        }

        public override int Size()
        {
            throw new NotImplementedException();
        }

        public override int GetMaxX()
        {
            int maxX = 0;
            foreach (GroupComponent component in components)
            {
                if (component.GetMaxX() > maxX)
                {
                    maxX = component.GetMaxX();
                }
            }
            return maxX;
        }

        public override int GetMaxY()
        {
            int maxY = 0;
            foreach (GroupComponent component in components)
            {
                if (component.GetMaxY() > maxY)
                {
                    maxY = component.GetMaxY();
                }
            }
            return maxY;
        }

        public override int GetMinX()
        {
            int minX = 10000;
            foreach (GroupComponent component in components)
            {
                if (component.GetMinX() < minX)
                {
                    minX = component.GetMinX();
                }
            }
            return minX;
        }

        public override int GetMinY()
        {
            int minY = 10000;
            foreach (GroupComponent component in components)
            {
                if (component.GetMinY() < minY)
                {
                    minY = component.GetMinY();
                }
            }
            return minY;
        }

        public override int GetPosX()
        {
            return GetMinX();
        }

        public override int GetPosY()
        {
            return GetMinY();
        }

        public override int GetSizX()
        {
            return GetMaxX();
        }

        public override int GetSizY()
        {
            return GetMaxY();
        }

        public override void SetPosX(int posX)
        {
            //An ornament doesn't need to be moved. It's drawn relative to it's components.
        }

        public override void SetPosY(int posY)
        {
            
        }

        public override void SetSizX(int sizX)
        {
            //Resizing is also not nessesary for ornaments. The text size stays the same.
        }

        public override void SetSizY(int sizY)
        {
            
        }

        public override Color getBackColor()
        {
            throw new NotImplementedException();
        }

        public override void setBackColor(Color c)
        {
            throw new NotImplementedException();
        }

        public override void setName(string name)
        {
            throw new NotImplementedException();
        }

        public override void Draw(System.Windows.Forms.PaintEventArgs e)
        {
            Font mainFont = new Font("Arial", 16);
            Brush fontColor = new SolidBrush(Color.Black);
            SizeF stringSize = new SizeF();
            stringSize = e.Graphics.MeasureString(getText(), mainFont);

            foreach (GroupComponent component in components)
            {
                component.Draw(e);
            }
            int x = 0;
            int y = 0;
            switch (getSide())
            {
                case "top":

                    x = this.GetMaxX();
                    y = (int)(GetMinY() - (stringSize.Height / 2));
                    break;
                case "bottom":

                    x = (int)(GetMinX() + ((GetMaxX() - GetMinX()) / 2) - (stringSize.Width / 2));
                    y = (int)((GetMinY() + (GetMaxY() - GetMinY())) - (stringSize.Height / 2));

                    break;
                case "left":

                    x = (int)(GetMinX() - (stringSize.Width / 2));
                    y = (int)(GetMinY() + ((GetMaxY() - GetMinY()) / 2) - (stringSize.Height / 2));

                    break;
                case "right":

                    x = this.GetMaxX();
                    y = (int)(this.GetMinY() + ((this.GetMaxY() - this.GetMinY()) / 2) - (stringSize.Height / 2));

                    break;
                default:
                    break;
            }

            e.Graphics.DrawString(getText(), mainFont, fontColor, x, y);

        }

        public override void SetPosXOffset(int posXOffset)
        {
            foreach (GroupComponent component in components)
            {
                component.SetPosXOffset(posXOffset);
            }
            SetPosX(GetPosX() + posXOffset);
        }

        public override void SetPosYOffset(int posYOffset)
        {
            foreach (GroupComponent component in components)
            {
                component.SetPosYOffset(posYOffset);
            }
            SetPosY(GetPosY() + posYOffset);
        }

        public override void SetSizXOffset(int sizXOffset)
        {
            foreach (GroupComponent component in components)
            {
                component.SetSizXOffset(sizXOffset);
            }
            SetSizX(GetSizX() + sizXOffset);
        }

        public override void SetSizYOffset(int sizYOffset)
        {
            foreach (GroupComponent component in components)
            {
                component.SetSizYOffset(sizYOffset);
            }
            SetSizY(GetSizY() + sizYOffset);
        }
    }
}
