using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp
{
    public abstract class OrnamentBase
    {
        protected string text;
        protected string side;
        public virtual string getText()
        {
            return this.text;
        }
        public virtual string getSide()
        {
            return this.side;
        }
    }
}
