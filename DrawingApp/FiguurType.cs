using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp
{
    public interface FiguurType
    {
        void DrawShape(Graphics g, Color backColor, int posX, int posY, int sizX, int sizY);
        string toString();
    }
    class IsRectangle : FiguurType
    {
        public void DrawShape(Graphics g, Color backColor, int posX, int posY, int sizX, int sizY)
        {
            g.FillRectangle(new SolidBrush(backColor), posX, posY, sizX, sizY);
        }

        public string toString()
        {
            return "rectangle";
        }
    }
    class IsEllipse : FiguurType
    {

        public void DrawShape(Graphics g, Color backColor, int posX, int posY, int sizX, int sizY)
        {
            g.FillEllipse(new SolidBrush(backColor), posX, posY, sizX, sizY);
        }

        public string toString()
        {
            return "ellipse";
        }
    }
}

