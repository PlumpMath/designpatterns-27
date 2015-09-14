using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp
{
    class RightOrnament : OrnamentDecorator
    {
        public RightOrnament(OrnamentBase baseOrnament)
            :base(baseOrnament)
        {
            this.side = "right";
        }
    }
}
