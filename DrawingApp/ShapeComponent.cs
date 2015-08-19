using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp
{
    abstract class ShapeComponent
    {
        public string type { get; private set; }
        public Color back_color { get; private set; }
        public int pos_x { get; private set; }
        public int pos_y { get; private set; }
        public int size_x { get; private set; }
        public int size_y { get; private set; }
        public bool is_selected { get; private set; }
        public ShapeComponent(string Type, Color BackgroundColor, int PositionX, int PositionY, int SizeX, int SizeY, bool isSelected)
        {
            this.type = Type;
            this.back_color = BackgroundColor;
            this.pos_x = PositionX;
            this.pos_y = PositionY;
            this.size_x = SizeX;
            this.size_y = SizeY;
            this.is_selected = isSelected;
        }
        public void SetPosition(int NewPositionX, int NewPositionY)
        {
            pos_x = NewPositionX;
            pos_y = NewPositionY;
        }
        public void SetSize(int NewSizeX, int NewSizeY)
        {
            size_x = NewSizeX;
            size_y = NewSizeY;
        }
        public void SetSelected(bool Selected)
        {
            is_selected = Selected;
        }
        public virtual void GetGroupedShapes(int index)
        {
            for (int counter = 0; counter < index; counter++)
            {
                Console.Write(" ");
            }

            Console.WriteLine(type);
        }
    }
}
