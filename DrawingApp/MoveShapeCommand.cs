using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp
{
    class MoveShapeCommand : UndoableCommand
    {
        private GroupComponent shape;
        private Controller controller;
        private ShapeVisitor shapeVisitor;
        private int new_x_offset;
        private int new_y_offset;

        public MoveShapeCommand(Controller controller, ShapeVisitor shapeVisitor, GroupComponent shape, int x_offset, int y_offset)
        {
            this.controller = controller;
            this.shapeVisitor = shapeVisitor;
            this.shape = shape;
            this.new_x_offset = x_offset;
            this.new_y_offset = y_offset;
        }

        public override void Execute()
        {
            MoveObject moveObject = new MoveObject( this.shape, this.new_x_offset, this.new_y_offset);
            moveObject.accept(shapeVisitor);
        }

        public override void UnExecute()
        {
            MoveObject moveObject = new MoveObject(this.shape, -this.new_x_offset, -this.new_y_offset);
            moveObject.accept(shapeVisitor);
        }
    }
}
