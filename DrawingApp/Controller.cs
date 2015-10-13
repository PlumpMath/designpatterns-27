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
            Display();
        }
        public void RemoveShape(BasisFiguur shape)
        {
            shapeList.Remove(shape);
        }
        public void AddOrnament(GroupComponent ornament, GroupComponent shapegroup)
        {
            groupList.Remove(shapegroup);
            ornament.Add(shapegroup);
            groupList.Add(ornament);
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
            List<OrnamentBase> Ornaments = new List<OrnamentBase>();
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
                                newShape.setName("rectangle");
                            }
                            else if(newline[counter] == "ellipse")
                            {
                                newShape = new BasisFiguur(BasisFiguur.Shapes.ELLIPSE);
                                newShape.setName("ellipse");
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
                        else if (newline[counter] == "ornament")
                        {
                            string textInput = newline[counter + 2];
                            Console.WriteLine(textInput);
                            //Remove the extra " at the beginning and end of the string.
                            textInput.TrimEnd();
                            textInput = textInput.TrimEnd(new char[] { '\"'});
                            textInput = textInput.TrimStart(new char[] { '\"' });
                            switch (newline[counter + 1])
                            {
                                case "top":
                                    //Create a new ornament and set the text.
                                    Ornament newOr = new Ornament(textInput);
                                    //This ornament will be a top ornament.
                                    TopOrnament newTopOrnament = new TopOrnament(newOr);
                                    //Add the new ornament to the list. After which it can be added to the next shape or group.
                                    Ornaments.Add(newTopOrnament);
                                    break;
                                case "bottom":
                                    Ornament newOrna = new Ornament(textInput);
                                    ButtomOrnament newBottomOrnament = new ButtomOrnament(newOrna);
                                    Ornaments.Add(newBottomOrnament);
                                    break;
                                case "left":
                                    Ornament newOrnam = new Ornament(textInput);
                                    LeftOrnament newLeftOrnament = new LeftOrnament(newOrnam);
                                    Ornaments.Add(newLeftOrnament);
                                    break;
                                case "right":
                                    Ornament newOrname = new Ornament(textInput);
                                    RightOrnament newRightOrnament = new RightOrnament(newOrname);
                                    Ornaments.Add(newRightOrnament);
                                    break;
                                default:
                                    break;
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
