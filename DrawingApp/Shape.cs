using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp
{
    class Shape
    {
        //A shape class can store all the information. And it will be added to teh shapequeue once it is created.
        public String type { get; set; }
        public Color back_color { get; set; }
        public int pos_x { get; set; }
        public int pos_y { get; set; }
        public int size_x { get; set; }
        public int size_y { get; set; }
        public bool is_selected { get; set; }
        public Shape(String Type, Color BackgroundColor, int PositionX, int PositionY, int SizeX, int SizeY, bool isSelected)
        {
            type = Type;
            back_color = BackgroundColor;
            pos_x = PositionX;
            pos_y = PositionY;
            size_x = SizeX;
            size_y = SizeY;
            is_selected = isSelected;
        }
    }
}