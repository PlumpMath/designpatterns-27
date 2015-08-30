using System.Collections.Generic;
using System.IO;

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
    }
}
