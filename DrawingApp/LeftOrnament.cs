using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp
{
    class LeftOrnament : OrnamentDecorator
    {
        public LeftOrnament(OrnamentBase baseOrnament)
            :base(baseOrnament)
        {
            this.side = "left";
        }
    }
}
