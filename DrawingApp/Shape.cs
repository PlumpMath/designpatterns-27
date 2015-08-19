using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp
{
    class Shape : ShapeComponent
    {
        //A shape class can store all the information. And it will be added to teh shapequeue once it is created.
        public Shape(string Type, Color BackgroundColor, int PositionX, int PositionY, int SizeX, int SizeY, bool isSelected)
            : base(Type, BackgroundColor, PositionX, PositionY, SizeX, SizeY, isSelected)
        {

        }

        public override void GetGroupedShapes(int index)
        {
            base.GetGroupedShapes(index);
        }
    }
}