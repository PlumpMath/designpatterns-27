using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp
{
    public abstract class OrnamentDecorator : OrnamentBase
    {
        protected OrnamentBase tempOrnamentBase;
        public OrnamentDecorator(OrnamentBase newOrnamentBase)
        {
            this.tempOrnamentBase = newOrnamentBase;
        }
        public override string getText()
        {
            return (this.tempOrnamentBase.getText() + this.text);
        }
        public override string getSide()
        {
            return (this.tempOrnamentBase.getSide() + this.side);
        }
    }
}
