using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace DrawingApp
{
    abstract class GroupComponent
    {
        public string name;

        public abstract void Add(GroupComponent c);
        public abstract void Remove(GroupComponent c);
        public abstract void Display(int depth);
        public abstract bool ContainsMember(GroupComponent shape);
        public abstract void WriteToFile(StreamWriter writer, int depth);
        public abstract List<GroupComponent> UnGroup();
        public abstract void ToggleSelected();
        public abstract bool isSelected();
        public abstract void setSelected(bool selected);
        public abstract int Size();
        public abstract int GetMaxX();
        public abstract int GetMaxY();
        public abstract int GetMinX();
        public abstract int GetMinY();

        public abstract int GetPosX();
        public abstract int GetPosY();
        public abstract int GetSizX();
        public abstract int GetSizY();

        public abstract void SetPosX(int posX);
        public abstract void SetPosY(int posY);
        public abstract void SetSizX(int sizX);
        public abstract void SetSizY(int sizY);

        public abstract Color getBackColor();
        public abstract void setBackColor(Color g);
        public abstract void setName(string name);

        public abstract void AddOrnament(string text, string side);
        public abstract void DrawOrnaments(Graphics g);
    }
}
