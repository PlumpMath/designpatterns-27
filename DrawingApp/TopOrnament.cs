using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp
{
    class TopOrnament: OrnamentDecorator
    {
        public TopOrnament(OrnamentBase baseOrnament)
            :base(baseOrnament)
        {
            this.side = "top";
        }
    }
}
