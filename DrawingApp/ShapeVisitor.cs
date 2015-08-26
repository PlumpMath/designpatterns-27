using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp
{
    interface ShapeVisitor : Visitor
    {
        public ShapeVisitor()
        {

        }
        public void visit(MoveObject shapeOrGroup)
        {

        }
        public void visit(ResizeObject shapeOrGroup)
        {

        }
        public void visit(WriteToFile shapeOrGroup)
        {

        }
    }
}
