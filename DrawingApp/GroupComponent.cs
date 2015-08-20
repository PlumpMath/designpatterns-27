using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp
{
    abstract class GroupComponent
    {
        public string name;
        public GroupComponent(string name)
        {
            this.name = name;
        }
        public abstract void Add(GroupComponent c);
        public abstract void Remove(GroupComponent c);
        public abstract void Display(int depth);
    }
}
