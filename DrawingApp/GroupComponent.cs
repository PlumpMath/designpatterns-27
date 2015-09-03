using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace DrawingApp
{
    abstract class GroupComponent
    {
        public abstract void Add(GroupComponent c);
        public abstract void Remove(GroupComponent c);
        public abstract void Display(int depth);
        public abstract bool ContainsMember(GroupComponent shape);
        public abstract void WriteToFile(StreamWriter writer, int depth);
        public abstract List<GroupComponent> UnGroup();
        public abstract void ToggleSelected();
        public abstract bool isSelected();
        public abstract int Size();
        public abstract int GetMaxX();
        public abstract int GetMaxY();
        public abstract int GetMinX();
        public abstract int GetMinY();
        public abstract Color getBackColor();
    }
}
