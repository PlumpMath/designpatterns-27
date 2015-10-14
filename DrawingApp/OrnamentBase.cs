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
        private int posX;
        private int posY;
        private int sizX;
        private int sizY;
        private List<GroupComponent> components = new List<GroupComponent>();
        public virtual string getText()
        {
            return this.text;
        }
        public virtual string getSide()
        {
            return this.side;
        }
        public virtual void drawOrnament(int posX, int posY, int sizX, int sizY, Graphics g)
        {
            Font mainFont = new Font("Arial", 16);
            Brush fontColor = new SolidBrush(Color.Black);
            SizeF stringSize = new SizeF();
            stringSize = g.MeasureString(getText(), mainFont, 200);
            switch (this.getSide())
            {
                case "top":
                    g.DrawString(getText(), mainFont, fontColor, (posX + (sizX / 2) - (stringSize.Width / 2)), posY - (stringSize.Height / 2));
                    break;
                case "bottom":
                    g.DrawString(getText(), mainFont, fontColor, (posX + (sizX / 2) - (stringSize.Width / 2)), ((posY + sizY) - (stringSize.Height / 2)));
                    break;
                case "left":
                    g.DrawString(getText(), mainFont, fontColor, posX - (stringSize.Width / 2), posY + (sizY / 2) - (stringSize.Height / 2));
                    break;
                case "right":
                    g.DrawString(getText(), mainFont, fontColor, (posX + sizX) - (stringSize.Width / 2), posY + (sizY / 2) - (stringSize.Height / 2));
                    break;
                default:
                    break;
            }
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
            throw new NotImplementedException();
        }

        public override bool ContainsMember(GroupComponent shape)
        {
            throw new NotImplementedException();
        }

        public override void WriteToFile(System.IO.StreamWriter writer, int depth)
        {
            throw new NotImplementedException();
        }

        public override List<GroupComponent> UnGroup()
        {
            throw new NotImplementedException();
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
            //throw new NotImplementedException();
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
            return this.posX;
        }

        public override int GetPosY()
        {
            return this.posY;
        }

        public override int GetSizX()
        {
            return this.sizX;
        }

        public override int GetSizY()
        {
            return this.sizY;
        }

        public override void SetPosX(int posX)
        {
            this.posX = posX;
        }

        public override void SetPosY(int posY)
        {
            this.posY = posY;
        }

        public override void SetSizX(int sizX)
        {
            this.sizX = sizX;
        }

        public override void SetSizY(int sizY)
        {
            this.sizY = sizY;
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

            foreach (GroupComponent component in components)
            {
                component.Draw(e);
            }

         e.Graphics.DrawString(getText(), mainFont, fontColor, GetPosX(), GetPosY());

        }
    }
}
