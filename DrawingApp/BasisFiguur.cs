using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp
{
    class BasisFiguur : GroupComponent
    {
        //A shape class can store all the information. And it will be added to the shapequeue once it is created.
        private Color backColor;
        private int posX;
        private int posY;
        private int sizX;
        private int sizY;
        private bool selected;
        public Strategy strategy;

        public enum Shapes
        {
            RECTANGLE,
            ELLIPSE
        }

        public BasisFiguur(Shapes type)
        {
            switch (type)
            {
                case Shapes.RECTANGLE:
                    strategy = Rechthoek.Instance;
                    break;
                case Shapes.ELLIPSE:
                    strategy = Ellips.Instance;
                    break;
                default:
                    break;
            }
        }

        public override void setName(string name) 
        {
            this.name = name;
        }

        public void setPosSiz(int posX, int posY, int sizX, int sizY)
        {
            this.posX = posX;
            this.posY = posY;
            this.sizX = sizX;
            this.sizY = sizY;
        }


        public override void Add(GroupComponent c)
        {
            Console.WriteLine("Cannot add to a leaf");
        }

        public override void Remove(GroupComponent c)
        {
            Console.WriteLine("Cannot remove from a leaf");
        }
        public override List<GroupComponent> UnGroup()
        {
            Console.WriteLine("Cannot ungroup a leaf");
            return null;
        }

        public override void Display(int depth)
        {
            Console.WriteLine(new String(' ', depth) + this.name + ' ' + posX + ' ' + posY + ' ' + sizX + ' ' + sizY);
        }
        public override void WriteToFile(System.IO.StreamWriter writer, int depth)
        {
            writer.WriteLine(new String(' ', depth) + this.name + ' ' + posX + ' ' + posY+ ' ' + sizX+ ' ' + sizY);
        }
        public override bool ContainsMember(GroupComponent shape)
        {
            return false;
        }
        public override void ToggleSelected()
        {
            selected = !selected;
        }
        public override bool isSelected()
        {
            return selected;
        }
        public override void setSelected(bool selected)
        {
            this.selected = selected;
        }
        public override int Size()
        {
            Console.WriteLine("Cannot get the size of a leaf");
            return 0;
        }
        public override int GetMaxX()
        {
            return posX + sizX;
        }
        public override int GetMaxY()
        {
            return posY + sizY;
        }
        public override int GetMinX()
        {
            return posX;
        }
        public override int GetMinY()
        {
            return posY;
        }
        public override void setBackColor(Color g)
        {
            this.backColor = g;
        }
        public override Color getBackColor()
        {
            return backColor;
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
    }
}
