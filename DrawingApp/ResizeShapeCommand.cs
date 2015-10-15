using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp
{
    class ResizeShapeCommand : UndoableCommand
    {
        private GroupComponent shape;
        private Controller controller;
        private ShapeVisitor shapeVisitor;
        private int new_size_x;
        private int new_size_y;

        public ResizeShapeCommand(Controller controller, ShapeVisitor shapeVisitor, GroupComponent shape, int new_x_size, int new_y_size)
        {
            this.controller = controller;
            this.shapeVisitor = shapeVisitor;
            this.shape = shape;
            this.new_size_x = new_x_size;
            this.new_size_y = new_y_size;
        }

        public override void Execute()
        {
            ResizeObject moveObject = new ResizeObject(this.shape, this.new_size_x, this.new_size_y);
            moveObject.accept(shapeVisitor);
        }

        public override void UnExecute()
        {
            ResizeObject moveObject = new ResizeObject(this.shape, -this.new_size_x, -this.new_size_y);
            moveObject.accept(shapeVisitor);
        }
    }
}
