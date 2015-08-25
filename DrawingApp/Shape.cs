using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp
{
    class Shape : GroupComponent
    {
        //A shape class can store all the information. And it will be added to teh shapequeue once it is created.
        public String type { get; set; }
        public Color back_color { get; set; }
        public int pos_x { get; set; }
        public int pos_y { get; set; }
        public int size_x { get; set; }
        public int size_y { get; set; }
        public bool is_selected { get; set; }
        public Shape(String Type, Color BackgroundColor, int PositionX, int PositionY, int SizeX, int SizeY, bool isSelected) : base(Type)
        {
            type = Type;
            back_color = BackgroundColor;
            pos_x = PositionX;
            pos_y = PositionY;
            size_x = SizeX;
            size_y = SizeY;
            is_selected = isSelected;
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
            Console.WriteLine(new String(' ', depth) + name + ' ' + pos_x + ' ' + pos_y + ' ' + size_x + ' ' + size_y);
        }
        public override bool ContainsMember(GroupComponent shape)
        {
            return false;
        }
        public override void ToggleSelected()
        {
            is_selected = !is_selected;
        }
        public override bool isSelected()
        {
            return is_selected;
        }
        public override int Size()
        {
            Console.WriteLine("Cannot get the size of a leaf");
            return 0;
        }
        public override int GetMaxX()
        {
            return pos_x + size_x;
        }
        public override int GetMaxY()
        {
            return pos_y + size_y;
        }
        public override int GetMinX()
        {
            return pos_x;
        }
        public override int GetMinY()
        {
            return pos_y;
        }
    }
}