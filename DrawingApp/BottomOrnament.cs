using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp
{
    class ButtomOrnament : OrnamentDecorator
    {
        public ButtomOrnament(OrnamentBase baseOrnament)
            : base(baseOrnament)
        {
            this.side = "bottom";
        }
    }
}
