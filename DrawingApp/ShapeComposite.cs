using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp
{
    class ShapeComposite : ShapeComponent
    {
        private List<ShapeComponent> shapes;

        public ShapeComposite(string Type, Color BackgroundColor, int PositionX, int PositionY, int SizeX, int SizeY, bool isSelected)
            : base(Type, BackgroundColor, PositionX, PositionY, SizeX, SizeY, isSelected)
        {
            shapes = new List<ShapeComponent>();
        }

        public void AddShape(ShapeComponent e)
        {
            shapes.Add(e);
        }
        public void RemoveShape(ShapeComponent e)
        {
            shapes.Remove(e);
        }
        public override void GetGroupedShapes(int index)
        {
            base.GetGroupedShapes(index);
            foreach (ShapeComponent e in shapes)
            {
                e.GetGroupedShapes(index + 1);
            }
        }
    }
}
