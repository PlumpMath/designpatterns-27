using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp
{
    class ResizeShapeCommand : UndoableCommand
    {
        private Shape shape;
        private Controller controller;
        private ShapeVisitor shapeVisitor;
        private int old_size_x;
        private int old_size_y;
        private int new_size_x;
        private int new_size_y;

        public ResizeShapeCommand(Controller controller, ShapeVisitor shapeVisitor, Shape shape, int new_x_size, int new_y_size)
        {
            this.controller = controller;
            this.shapeVisitor = shapeVisitor;
            this.shape = shape;
            this.new_size_x = this.old_size_x = new_x_size;
            this.new_size_y = this.old_size_y = new_y_size;
        }

        public override void Execute()
        {
            shape.size_x = new_size_x;
            shape.size_y = new_size_y;

            ResizeObject moveObject = new ResizeObject(this.shape, this.new_size_x, this.new_size_y);
            moveObject.accept(shapeVisitor);
        }

        public override void UnExecute()
        {
            shape.size_x = old_size_x;
            shape.size_y = old_size_y;
        }
    }
}
