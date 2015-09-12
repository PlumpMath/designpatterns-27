using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawingApp
{
    class Controller
    {
        private List<BasisFiguur> shapeList = new List<BasisFiguur>();
        private List<GroupComponent> groupList = new List<GroupComponent>();
        Random Random = new Random();
        int groupCounter = 0;

        public void Display()
        {
            foreach (GroupComponent currentShape in groupList)
            {
                currentShape.Display(0);
            }
            Console.WriteLine("---------------------------------");
        }

        public void GroupShapes(List<GroupComponent> shapesToGroup)
        {
            GroupComposite newGroup = new GroupComposite();
            newGroup.setName("group " + groupCounter);
            groupCounter++;

            foreach (GroupComponent currentShape in shapesToGroup)
            {
                //First remove the shape or group from the list or else there are duplicates in the list.
                groupList.Remove(currentShape);
                newGroup.Add(currentShape);
            }
            groupList.Add(newGroup);
            //Print the groups to show what's changed.
            Display();
        }

        public void UnGroupShapes(List<GroupComponent> shapesToUnGroup)
        {
            foreach (GroupComponent group in shapesToUnGroup)
            {
                //First get all the member of the group
                List<GroupComponent> groupMembers = group.UnGroup();
                //Next remove the old group from the list
                //But check first if there isn't a leaf that's being ungrouped.
                if (groupMembers != null)
                {
                    groupList.Remove(group);
                    foreach (GroupComponent groupMember in groupMembers)
                    {
                        //Now put the member back in the list.
                        groupList.Add(groupMember);
                    }
                }
            }
            Display();
        }

        public void ToggleSelect(BasisFiguur shape)
        {
            GroupComponent groupToSelect = null;
            foreach (GroupComponent composite in groupList)
            {
                if (composite.ContainsMember(shape))
                {
                    groupToSelect = composite;
                }
            }
            if (groupToSelect != null)
            {
                groupToSelect.ToggleSelected();
            }
            else
            {
                shape.ToggleSelected();
            }
        }

        public List<BasisFiguur> GetShapes()
        {
            return shapeList;
        }

        public List<GroupComponent> GetGroups()
        {
            return groupList;
        }

        public void AddShape(BasisFiguur shape)
        {
            groupList.Add(shape);
            shapeList.Add(shape);
            Display();
        }
        public void RemoveShape(BasisFiguur shape)
        {
            shapeList.Remove(shape);
        }
        public void LoadFile()
        {
            OpenFileDialog openFielDialog = new OpenFileDialog();
            openFielDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFielDialog.FilterIndex = 2;
            openFielDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFielDialog.RestoreDirectory = true;
            List<GroupComponent> elements = new List<GroupComponent>();
            List<Tuple<int, GroupComposite>> proceedingSpaces = new List<Tuple<int, GroupComposite>>();
            if (openFielDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader reader = new StreamReader(openFielDialog.OpenFile()))
                {
                    while (!reader.EndOfStream)
                    {
                        Color randomColor = Color.FromArgb(Random.Next(255), Random.Next(255), Random.Next(255));
                        string[] newline = reader.ReadLine().Split(' ');

                        int counter = 0;
                        while (newline[counter] == "")
                        {
                            counter++;
                        }
                        if (newline[counter] == "group")
                        {
                            GroupComposite newGroup = new GroupComposite();
                            newGroup.setName("group " + newline[counter + 1]);
                            //Increase the groupCounter or else there will be multiple groups called "group 0" for example.
                            groupCounter++;
                            proceedingSpaces.Insert(0, new Tuple<int, GroupComposite>(counter, newGroup));
                            if (counter > 0)
                            {
                                foreach (Tuple<int, GroupComposite> currentGroup in proceedingSpaces)
                                {
                                    if (currentGroup.Item1 < counter)
                                    {
                                        currentGroup.Item2.Add(newGroup);
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                elements.Add(newGroup);
                            }
                        }
                        else if (newline[counter] == "rectangle" || newline[counter] == "ellipse")
                        {
                            BasisFiguur newShape = null;
                            if (newline[counter] == "rectangle") {
                                newShape = new BasisFiguur(BasisFiguur.Shapes.RECTANGLE);

                            }
                            else if(newline[counter] == "ellipse")
                            {
                                newShape = new BasisFiguur(BasisFiguur.Shapes.ELLIPSE);
                            }
                            if (newShape != null)
                            {
                                newShape.setPosSiz(Convert.ToInt32(newline[counter + 1]), Convert.ToInt32(newline[counter + 2]), Convert.ToInt32(newline[counter + 3]), Convert.ToInt32(newline[counter + 4]));
                                newShape.setBackColor(randomColor);
                                newShape.setSelected(false);
                                //Shape newShape = new Shape(newline[counter], randomColor, , false);
                                shapeList.Add(newShape);
                                //If there are spaces before the shape it means the shape in one or more groups.
                                if (counter > 0)
                                {
                                    //Find the group that has been last added to the list, and less spaces in front of it.
                                    //Which means it it higher in the hierarchy.
                                    foreach (Tuple<int, GroupComposite> currentGroup in proceedingSpaces)
                                    {
                                        if (currentGroup.Item1 < counter)
                                        {
                                            currentGroup.Item2.Add(newShape);
                                            //Once the group is found the search stops. The shape only needs to be added to one group.
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    elements.Add(newShape);
                                }
                            }
                        }
                    }
                }
                groupList = elements;
                Display();
            }
        }
    }
}
