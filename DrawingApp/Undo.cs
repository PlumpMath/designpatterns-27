using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp
{
    class Undo : Command
    {
        DrawingInterface theApp;
        public Undo(DrawingInterface newApp)
        {
            theApp = newApp;
        }
        public void execute()
        {
            theApp.undo();
        }
    }
}
