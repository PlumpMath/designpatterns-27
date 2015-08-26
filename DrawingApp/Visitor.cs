using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp
{
    interface Visitor
    {
        public void visit(MoveObject shapeOrGroup);
        public void visit(ResizeObject shapeOrGroup);
        public void visit(WriteToFile shapeOrGroup);
    }
}
