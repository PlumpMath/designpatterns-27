using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp
{
    class Load : Command
    {
        DrawingInterface theApp;
        public Load(DrawingInterface newApp)
        {
            theApp = newApp;
        }
        public void execute()
        {
            theApp.load();
        }
    }
}
