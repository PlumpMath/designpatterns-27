using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp
{
    class MoveObject : Visitable
    {
        public void accept(Visitor visitor)
        {
            visitor.visit(this);
        }
    }
}
