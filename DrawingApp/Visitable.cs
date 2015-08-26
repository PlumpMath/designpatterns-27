using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp
{
    interface Visitable
    {
        public void accept(Visitor visitor);
    }
}
