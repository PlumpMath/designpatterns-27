using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp
{
    abstract class UndoableCommand
    {
        public abstract void Execute();
        public abstract void UnExecute();
    }
}
