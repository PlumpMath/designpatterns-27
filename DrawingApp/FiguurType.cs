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
        string figuurType();
        void DrawShape(Graphics g, Color backColor, int posX, int posY, int sizX, int sizY, bool isSelected);
        string toString();
    }
    class IsRectangle : FiguurType
    {
        public void DrawShape(Graphics g, Color backColor, int posX, int posY, int sizX, int sizY, bool isSelected)
        {
            g.FillRectangle(new SolidBrush(backColor), posX, posY, sizX, sizY);
            if (isSelected)
            {
                g.DrawRectangle(new Pen(Color.Black, 2), posX, posY, sizX, sizY);
            }
        }

        public string figuurType()
        {
            return "rectangle";
        }

        public string toString()
        {
            throw new NotImplementedException();
        }
    }
    class IsEllipse : FiguurType
    {

        public void DrawShape(Graphics g, Color backColor, int posX, int posY, int sizX, int sizY, bool isSelected)
        {
            g.FillEllipse(new SolidBrush(backColor), posX, posY, sizX, sizY);
            if (isSelected) {
                g.DrawRectangle(new Pen(Color.Black, 2), posX, posY, sizX, sizY);
            }
        }

        public string figuurType()
        {
            return "ellipse";
        }

        public string toString()
        {
            throw new NotImplementedException();
        }
    }
}

