using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp
{
    class BasisFiguur : GroupComponent
    {
        //A shape class can store all the information. And it will be added to the shapequeue once it is created.
        private Color backColor;
        private int posX;
        private int posY;
        private int sizX;
        private int sizY;
        private bool selected;
        public Strategy strategy;
        public List<OrnamentBase> Ornaments = new List<OrnamentBase>();
        
        public enum Shapes
        {
            RECTANGLE,
            ELLIPSE
        }

        public BasisFiguur(Shapes type)
        {

            switch (type)
            {
                case Shapes.RECTANGLE:
                    strategy = Rechthoek.Instance;
                    break;
                case Shapes.ELLIPSE:
                    strategy = Ellips.Instance;
                    break;
                default:
                    break;
            }
        }

        public override void setName(string name) 
        {
            this.name = name;
        }

        public void setPosSiz(int posX, int posY, int sizX, int sizY)
        {
            this.posX = posX;
            this.posY = posY;
            this.sizX = sizX;
            this.sizY = sizY;
        }


        public override void Add(GroupComponent c)
        {
            Console.WriteLine("Cannot add to a leaf");
        }

        public override void Remove(GroupComponent c)
        {
            Console.WriteLine("Cannot remove from a leaf");
        }
        public override List<GroupComponent> UnGroup()
        {
            Console.WriteLine("Cannot ungroup a leaf");
            return null;
        }

        public override void Display(int depth)
        {
            Console.WriteLine(new String(' ', depth) + this.name + ' ' + posX + ' ' + posY + ' ' + sizX + ' ' + sizY);
        }
        public override void WriteToFile(System.IO.StreamWriter writer, int depth)
        {
            //Write every ornament to the file before the shape is written.
            foreach(OrnamentBase orn in Ornaments)
            {
                writer.WriteLine(new String(' ', depth) + "ornament " + orn.getSide() + " \"" + orn.getText() + "\"");
            }
            writer.WriteLine(new String(' ', depth) + this.name + ' ' + posX + ' ' + posY+ ' ' + sizX+ ' ' + sizY);
        }
        public override bool ContainsMember(GroupComponent shape)
        {
            return false;
        }
        public override void ToggleSelected()
        {
            selected = !selected;
        }
        public override bool isSelected()
        {
            return selected;
        }
        public override void setSelected(bool selected)
        {
            this.selected = selected;
        }
        public override int Size()
        {
            Console.WriteLine("Cannot get the size of a leaf");
            return 0;
        }
        public override int GetMaxX()
        {
            return posX + sizX;
        }
        public override int GetMaxY()
        {
            return posY + sizY;
        }
        public override int GetMinX()
        {
            return posX;
        }
        public override int GetMinY()
        {
            return posY;
        }
        public override void setBackColor(Color g)
        {
            this.backColor = g;
        }
        public override Color getBackColor()
        {
            return backColor;
        }

        public override int GetPosX()
        {
            return this.posX;
        }

        public override int GetPosY()
        {
            return this.posY;
        }

        public override int GetSizX()
        {
            return this.sizX;
        }

        public override int GetSizY()
        {
            return this.sizY;
        }

        public override void SetPosX(int posX)
        {
            this.posX = posX;
        }

        public override void SetPosY(int posY)
        {
            this.posY = posY;
        }

        public override void SetSizX(int sizX)
        {
            this.sizX = sizX;
        }

        public override void SetSizY(int sizY)
        {
            this.sizY = sizY;
        }

        public override void AddOrnament(string text, string side)
        {
            switch (side)
            {
                case "Top":
                    //Create a new ornament and set the text.
                    Ornament newOr = new Ornament(text);
                    //This ornament will be a top ornament.
                    TopOrnament newTopOrnament = new TopOrnament(newOr);
                    //Add the new ornament to the list. After which it can be drawn or saved.
                    Ornaments.Add(newTopOrnament);
                    break;
                case "Bottom":
                    Ornament newOrna = new Ornament(text);
                    ButtomOrnament newBottomOrnament = new ButtomOrnament(newOrna);
                    Ornaments.Add(newBottomOrnament);
                    break;
                case "Left":
                    Ornament newOrnam = new Ornament(text);
                    LeftOrnament newLeftOrnament = new LeftOrnament(newOrnam);
                    Ornaments.Add(newLeftOrnament);
                    break;
                case "Right":
                    Ornament newOrname = new Ornament(text);
                    RightOrnament newRightOrnament = new RightOrnament(newOrname);
                    Ornaments.Add(newRightOrnament);
                    break;
                default:
                    break;
            }

            foreach(OrnamentBase orn in Ornaments)
            {
                Console.WriteLine("ornament " + orn.getSide() + " " + orn.getText());
            }
        }
        public override void DrawOrnaments(Graphics g)
        {
            Font mainFont = new Font("Arial", 16);
            Brush blackBrush = new SolidBrush(Color.Black);
            SizeF stringSize = new SizeF();
            foreach (OrnamentBase orn in Ornaments)
            {
                switch (orn.getSide())
                {
                    case "top":
                        stringSize = g.MeasureString(orn.getText(), mainFont, 200);
                        g.DrawString(orn.getText(), mainFont, blackBrush, (GetMinX() + (GetMaxX() - GetMinX())/2)- (stringSize.Width / 2), GetMinY() - (stringSize.Height /2));
                        break;
                    case "bottom":
                        stringSize = g.MeasureString(orn.getText(), mainFont, 200);
                        g.DrawString(orn.getText(), mainFont, blackBrush, (GetMinX() + (GetMaxX() - GetMinX()) / 2) - (stringSize.Width / 2), (GetMaxY() - (stringSize.Height / 2)));
                        break;
                    case "left":
                        stringSize = g.MeasureString(orn.getText(), mainFont, 200);
                        g.DrawString(orn.getText(), mainFont, blackBrush, GetMinX() - (stringSize.Width / 2), GetMinY() + ((GetMaxY() - GetMinY()) / 2) - (stringSize.Height / 2));
                        break;
                    case "right":
                        stringSize = g.MeasureString(orn.getText(), mainFont, 200);
                        g.DrawString(orn.getText(), mainFont, blackBrush, GetMaxX() - (stringSize.Width / 2), GetMinY() + ((GetMaxY() - GetMinY()) / 2) - (stringSize.Height / 2));
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
