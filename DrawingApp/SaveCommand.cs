using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp
{
    class SaveCommand
    {
        //The save and load command only have an execute function.
        //So it doesn't inherit from undoablecommand.
        private Controller controller;
        private ShapeVisitor shapeVisitor;
        public SaveCommand(Controller controller, ShapeVisitor shapeVisitor)
        {
            this.controller = controller;
            this.shapeVisitor = shapeVisitor;
        }
        public void Execute()
        {
            WriteToFile moveObject = new WriteToFile(this.controller);
            moveObject.accept(shapeVisitor);
        }
    }
}
