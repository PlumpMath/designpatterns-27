using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp
{
    class DrawNewShape : Command
    {
        DrawingInterface theApp;
        public DrawNewShape(DrawingInterface newApp)
        {
            theApp = newApp;
        }
        public void execute()
        {
            theApp.draw();
        }
    }
}
