using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawingApp
{
    class Ellips : Strategy
    {
        protected internal static Ellips _instance = new Ellips();
        private Ellips() {}
        public static Ellips Instance
        {
            get
            {
                return _instance;
            }
        }

        public void Draw(PaintEventArgs e, Brush b, Rectangle r, bool selected)
        {
            e.Graphics.FillEllipse(b, r);
            if (selected)
            {
                Pen selected_pen = new Pen(Color.Black, 2);
                e.Graphics.DrawEllipse(selected_pen, r);
            }
        }

        public string toString()
        {
            return "ellipse";
        }
    }
}
