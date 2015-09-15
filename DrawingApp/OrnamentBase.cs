using System;
using System.Collections.Generic;
using System.Drawing;
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
        public virtual void drawOrnament(int posX, int posY, int sizX, int sizY, Graphics g)
        {
            Font mainFont = new Font("Arial", 16);
            Brush fontColor = new SolidBrush(Color.Black);
            SizeF stringSize = new SizeF();
            stringSize = g.MeasureString(getText(), mainFont, 200);
            switch (this.getSide())
            {
                case "top":
                    g.DrawString(getText(), mainFont, fontColor, (posX + (sizX / 2) - (stringSize.Width / 2)), posY - (stringSize.Height / 2));
                    break;
                case "bottom":
                    g.DrawString(getText(), mainFont, fontColor, (posX + (sizX / 2) - (stringSize.Width / 2)), ((posY + sizY) - (stringSize.Height / 2)));
                    break;
                case "left":
                    g.DrawString(getText(), mainFont, fontColor, posX - (stringSize.Width / 2), posY + (sizY / 2) - (stringSize.Height / 2));
                    break;
                case "right":
                    g.DrawString(getText(), mainFont, fontColor, (posX + sizX) - (stringSize.Width / 2), posY + (sizY / 2) - (stringSize.Height / 2));
                    break;
                default:
                    break;
            }
        }
    }
}
