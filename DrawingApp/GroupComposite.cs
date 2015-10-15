using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp
{
    class GroupComposite : GroupComponent
    {
        private List<GroupComponent> shapes = new List<GroupComponent>();

        public override void Add(GroupComponent e)
        {
            shapes.Add(e);
        }
        public override void Remove(GroupComponent e)
        {
            shapes.Remove(e);
        }
        public override void Display(int depth)
        {
            Console.WriteLine(new String(' ', depth) + name);

            // Recursively display child nodes
            foreach (GroupComponent component in shapes)
            {
                component.Display(depth + 2);
            }
        }
        public override void WriteToFile(System.IO.StreamWriter writer, int depth)
        {

            writer.WriteLine(new String(' ', depth) + name);
            foreach (GroupComponent component in shapes)
            {
                component.WriteToFile(writer, depth + 2);
            }
        }
        public override List<GroupComponent> UnGroup()
        {
            //Just return the member since the grouplist in handled in the PlayingWithApplication.cs
            return shapes;
        }
        public override bool ContainsMember(GroupComponent shape)
        {
            return shapes.Contains(shape);
        }
        public override void ToggleSelected()
        {
            foreach (GroupComponent component in shapes)
            {
                component.ToggleSelected();
            }
        }
        public override bool isSelected()
        {
            bool selected = false;
            foreach (GroupComponent currentShape in shapes)
            {
                if (currentShape.isSelected())
                {
                    selected = true;
                }
            }
            return selected;
        }
        public override int Size(){
            return shapes.Count();
        }
        public override int GetMaxX()
        {
            int maxX = 0;
            foreach (GroupComponent component in shapes)
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
            foreach (GroupComponent component in shapes)
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
            foreach (GroupComponent component in shapes)
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
            foreach (GroupComponent component in shapes)
            {
                if (component.GetMinY() < minY)
                {
                    minY = component.GetMinY();
                }
            }
            return minY;
        }

        public override void setSelected(bool selected)
        {
            foreach (GroupComponent component in shapes)
            {
                component.setSelected(selected);
            }
        }

        public override Color getBackColor()
        {
            throw new NotImplementedException();
        }

        public override void setBackColor(Color g)
        {
            throw new NotImplementedException();
        }

        public override int GetPosX()
        {
            return 0;
        }

        public override int GetPosY()
        {
            return 0;
        }

        public override int GetSizX()
        {
            return 0;
        }

        public override int GetSizY()
        {
            return 0;
        }

        public override void SetPosX(int posX)
        {
            foreach (GroupComponent component in shapes)
            {
                component.SetPosX(posX);
            }
        }

        public override void SetPosY(int posY)
        {
            foreach (GroupComponent component in shapes)
            {
                component.SetPosY(posY);
            }
        }

        public override void SetSizX(int sizX)
        {
            throw new NotImplementedException();
        }

        public override void SetSizY(int sizY)
        {
            throw new NotImplementedException();
        }

        public override void setName(string name)
        {
            this.name = name;
        }

        public override void Draw(System.Windows.Forms.PaintEventArgs e)
        {
            foreach (GroupComponent component in shapes)
            {
                component.Draw(e);
            }
            //Draw an outline if the group is selected.
            //After the componentdraw so that the outline will be on top of the shapes.
            if (isSelected())
            {
                Rectangle r = new Rectangle(GetMinX(), GetMinY(), GetMaxX() - GetMinX(), GetMaxY() - GetMinY());
                Pen selected_pen = new Pen(Color.Black, 2);
                e.Graphics.DrawRectangle(selected_pen, r);
            }
        }

        public override void SetPosXOffset(int posXOffset)
        {
            foreach (GroupComponent component in shapes)
            {
                component.SetPosXOffset(posXOffset);
            }
        }

        public override void SetPosYOffset(int posYOffset)
        {
            foreach (GroupComponent component in shapes)
            {
                component.SetPosYOffset(posYOffset);
            }
        }

        public override void SetSizXOffset(int sizXOffset)
        {
            foreach (GroupComponent component in shapes)
            {
                component.SetSizXOffset(sizXOffset);
            }
        }

        public override void SetSizYOffset(int sizYOffset)
        {
            foreach (GroupComponent component in shapes)
            {
                component.SetSizYOffset(sizYOffset);
            }
        }
    }
}
