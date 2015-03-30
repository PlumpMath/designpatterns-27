using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp
{
    public interface DrawingInterface
    {
        void draw();
        void undo();
        void redo();
        void save();
        void load();
    }
}
