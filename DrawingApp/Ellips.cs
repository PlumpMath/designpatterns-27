using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp
{
    class Ellips : BasisFiguur
    {
        private static Ellips _instance;
        private Ellips() { }
        public static Ellips Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new Ellips();
                    _instance.figuurType = new IsEllipse();
                }
                return _instance;
            }
        }
    }
}
