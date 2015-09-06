using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp
{
    class Rechthoek : BasisFiguur
    {
        private static Rechthoek _instance = new Rechthoek();
        private Rechthoek() { }
        public static Rechthoek Instance
        {
            get
            {
                _instance.figuurType = new IsRectangle();
                return _instance;
            }
        }
    }
}
