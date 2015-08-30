using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp
{
    class LoadCommand
    {
        private Controller controller;
        public LoadCommand(Controller controller)
        {
            this.controller = controller;
        }
        public void Execute()
        {
            controller.LoadFile();
        }
    }
}
