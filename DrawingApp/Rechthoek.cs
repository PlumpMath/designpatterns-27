using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawingApp
{
    class Rechthoek : Strategy
    {
        protected internal static Rechthoek _instance = new Rechthoek();
        private Rechthoek() { }
        public static Rechthoek Instance
        {
            get
            {
                return _instance;
            }
        }

        public void Draw(PaintEventArgs e, Brush b, Rectangle r)
        {
            e.Graphics.FillRectangle(b, r);
        }

        public string toString()
        {
            return "rectangle";
        }
    }
}
