using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp
{
    class DrawingApplication : DrawingInterface
    {

        public void draw()
        {
            Console.WriteLine("Drawing a shape.");
        }

        public void undo()
        {
            Console.WriteLine("Undoíng");
        }

        public void redo()
        {
            Console.WriteLine("Redoing");
        }

        public void save()
        {
            Console.WriteLine("Saving");
        }

        public void load()
        {
            Console.WriteLine("Loading");
        }
    }
}
