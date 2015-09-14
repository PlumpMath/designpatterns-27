using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp
{
    public class Ornament : OrnamentBase
    {
        public Ornament(string text)
        {
            //Every ornament has a text. The side is kept in the other class like TopOrnament for example.
            this.text = text;
        }
    }
}
