using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp
{
    class Redo : Command
    {
        DrawingInterface theApp;
        public Redo(DrawingInterface newApp)
        {
            theApp = newApp;
        }
        public void execute()
        {
            theApp.redo();
        }
    }
}
