using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp
{
    class MoveShapeCommand : UndoableCommand
    {
        private Shape shape;
        private Controller controller;
        private ShapeVisitor shapeVisitor;
        private int old_pos_x;
        private int old_pos_y;
        private int new_pos_x;
        private int new_pos_y;

        public MoveShapeCommand(Controller controller, ShapeVisitor shapeVisitor, Shape shape, int new_x_pos, int new_y_pos)
        {
            this.controller = controller;
            this.shapeVisitor = shapeVisitor;
            this.shape = shape;
            this.new_pos_x = this.old_pos_x = new_x_pos;
            this.new_pos_y = this.old_pos_y = new_y_pos;
        }

        public override void Execute()
        {
            MoveObject moveObject = new MoveObject( this.shape, this.new_pos_x, this.new_pos_y);
            moveObject.accept(shapeVisitor);
        }

        public override void UnExecute()
        {
            MoveObject moveObject = new MoveObject(this.shape, this.old_pos_x, this.old_pos_y);
            moveObject.accept(shapeVisitor);
        }
    }
}
