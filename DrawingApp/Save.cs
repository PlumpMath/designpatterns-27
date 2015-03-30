using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp
{
    class Save : Command
    {
        DrawingInterface theApp;
        public Save(DrawingInterface newApp)
        {
            theApp = newApp;
        }
        public void execute()
        {
            theApp.save();
        }
    }
}
