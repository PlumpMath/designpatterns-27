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
        public FiguurType figuurType;

        public void setPosSiz(int posX, int posY, int sizX, int sizY)
        {
            this.posX = posX;
            this.posY = posY;
            this.sizX = sizX;
            this.sizY = sizY;
        }

        public void DrawShape(Graphics g)
        {
            figuurType.DrawShape(g, backColor, posX, posY, sizX, sizY, selected);
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
            Console.WriteLine(new String(' ', depth) + figuurType.figuurType() + ' ' + posX + ' ' + posY + ' ' + sizX + ' ' + sizY);
        }
        public override void WriteToFile(System.IO.StreamWriter writer, int depth)
        {
            writer.WriteLine(new String(' ', depth) + figuurType.figuurType() + ' ' + posX + ' ' + posY+ ' ' + sizX+ ' ' + sizY);
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
        public override Color getBackColor()
        {
            return backColor;
        }
    }
}
