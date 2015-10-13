using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawingApp
{
    interface Strategy
    {
        void Draw(PaintEventArgs e, Brush b, Rectangle r, bool selected);
        string toString();
    }
}
