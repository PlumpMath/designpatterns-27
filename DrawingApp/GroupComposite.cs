using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp
{
    class GroupComposite : GroupComponent
    {
        private List<GroupComponent> shapes = new List<GroupComponent>();

        public GroupComposite(string name)
            : base(name)
        {

        }

        public override void Add(GroupComponent e)
        {
            shapes.Add(e);
        }
        public override void Remove(GroupComponent e)
        {
            shapes.Remove(e);
        }
        public override void Display(int depth)
        {
            Console.WriteLine(new String(' ', depth) + name);

            // Recursively display child nodes
            foreach (GroupComponent component in shapes)
            {
                component.Display(depth + 2);
            }
        }
        public override bool ContainsMember(GroupComponent shape)
        {
            return shapes.Contains(shape);
        }
        public override void ToggleSelected()
        {
            foreach (GroupComponent component in shapes)
            {
                component.ToggleSelected();
            }
        }
    }
}
