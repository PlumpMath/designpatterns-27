using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace DrawingApp
{
    public partial class DrawingApp : Form
    {
        Pen blackPen = new Pen(Color.Black, 3);
        Pen selected_pen = new Pen(Color.Black, 2);
        DrawingWindow mainWindow;
        List<BasisFiguur> outlines = new List<BasisFiguur>();
        static Random Random = new Random();
        Point initialMousePos;
        bool mouseDown = false;
        string mode = "Create Rectangle";

        public DrawingApp()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            modebox.SelectedIndex = 0;
            //Create a new window
            mainWindow = new DrawingWindow();

        }
        
        class DrawingWindow
        {
            // Initializers
            private Controller controller = new Controller();
            private Stack<UndoableCommand> commandstack = new Stack<UndoableCommand>();
            private Stack<UndoableCommand> redocommandstack = new Stack<UndoableCommand>();
            private ShapeVisitor shapeVisitor = new ShapeVisitor();

            public void GroupShapes(List<GroupComponent> shapesToGroup)
            {
                controller.GroupShapes(shapesToGroup);
            }
            public void UnGroupShapes(List<GroupComponent> shapesToUnGroup)
            {
                controller.UnGroupShapes(shapesToUnGroup);
            }
            public void AddOrnament(GroupComponent ornament, GroupComponent shapecomponent)
            {
                controller.AddOrnament(ornament, shapecomponent);
            }
            public void ToggleSelect(BasisFiguur shape)
            {
                controller.ToggleSelect(shape);
            }

            public Stack<UndoableCommand> GetCommandStack()
            {
                return commandstack;
            }
            public List<BasisFiguur> GetShapes()
            {
                return controller.GetShapes();
            }
            public List<GroupComponent> GetGroups()
            {
                return controller.GetGroups();
            }

            public void Redo()
            {
                // Perform redo operations
                if (redocommandstack.Count > 0)
                {
                    UndoableCommand command = redocommandstack.Pop();
                    command.Execute();
                    commandstack.Push(command);
                }
            }
            public void Undo()
            {
                // Perform undo operations
                if (commandstack.Count > 0)
                {
                    UndoableCommand command = commandstack.Pop();
                    command.UnExecute();
                    redocommandstack.Push(command);
                }
            }
            public void AddShape(BasisFiguur shape)
            {
                // Create command operation and execute it
                UndoableCommand command = new AddShapeCommand(controller, shape);
                command.Execute();
                // Add command to command stack
                commandstack.Push(command);
            }
            public void MoveShape(BasisFiguur shape, int new_x_pos, int new_y_pos)
            {
                // Create command operation and execute it
                UndoableCommand command = new MoveShapeCommand(controller, shapeVisitor, shape, new_x_pos, new_y_pos);
                command.Execute();
                // Add command to command stack
                commandstack.Push(command);
            }
            public void ResizeShape(BasisFiguur shape, int new_x_size, int new_y_size)
            {
                // Create command operation and execute it
                UndoableCommand command = new ResizeShapeCommand(controller, shapeVisitor, shape, new_x_size, new_y_size);
                command.Execute();
                // Add command to command stack
                commandstack.Push(command);
            }
            public void Save()
            {
                SaveCommand command = new SaveCommand(controller, shapeVisitor);
                command.Execute();
            }
            public void Load()
            {
                LoadCommand command = new LoadCommand(controller);
                command.Execute();
            }

        }
        private void undo_button_Click(object sender, EventArgs e)
        {
            mainWindow.Undo();
            this.Refresh();
        }

        private void redo_button_Click(object sender, EventArgs e)
        {
            mainWindow.Redo();
            this.Refresh();
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            mainWindow.Save();
            this.Refresh();
        }

        private void load_button_Click(object sender, EventArgs e)
        {
            mainWindow.Load();
            this.Refresh();
        }

        private void DrawingApp_MouseDown(object sender, MouseEventArgs e)
        {
            initialMousePos = this.PointToClient(Cursor.Position);
            mouseDown = true;
        }

        private void DrawingApp_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
            //This is the code to select a shape while Move or Resize is selected.
            if (mode == "Move" | mode == "Resize")
            {
                foreach (GroupComponent currentShape in this.mainWindow.GetGroups())
                {
                    Rectangle tempShape = new Rectangle(currentShape.GetMinX(), currentShape.GetMinY(), currentShape.GetMaxX() - currentShape.GetMinX(), currentShape.GetMaxY() - currentShape.GetMinY());
                    if (tempShape.Contains(initialMousePos))
                    {
                        currentShape.ToggleSelected();
                        break;
                    }
                }
            }
            else
            {
                var mouse_pos = this.PointToClient(Cursor.Position);
                //A random color is chosen to give to the newly created shape.
                Color randomColor = Color.FromArgb(Random.Next(255), Random.Next(255), Random.Next(255));
                //The size of the new shape can already be calculated.
                int size_x = mouse_pos.X - initialMousePos.X;
                int size_y = mouse_pos.Y - initialMousePos.Y;
                if (size_x != 0 || size_y != 0)
                {
                    if (size_x < 0 && size_y > 0)
                    {
                        if (mode == "Create Rectangle")
                        {
                            //To not have any negative numbers some of the variables need to be multiplied by -1.
                            BasisFiguur newShape = new BasisFiguur(BasisFiguur.Shapes.RECTANGLE);
                            newShape.setPosSiz(mouse_pos.X, initialMousePos.Y, size_x * -1, size_y);
                            newShape.setBackColor(randomColor);
                            newShape.setSelected(false);
                            newShape.setName("rectangle");
                            mainWindow.AddShape(newShape);
                        }
                        else if (mode == "Create Ellipse")
                        {
                            //This will execute the AddShape command
                            BasisFiguur newShape = new BasisFiguur(BasisFiguur.Shapes.ELLIPSE);
                            newShape.setPosSiz(mouse_pos.X, initialMousePos.Y, size_x * -1, size_y);
                            newShape.setBackColor(randomColor);
                            newShape.setSelected(false);
                            newShape.setName("ellipse");
                            mainWindow.AddShape(newShape);
                        }
                    }
                    else if (size_x > 0 && size_y < 0)
                    {
                        if (mode == "Create Rectangle")
                        {
                            BasisFiguur newShape = new BasisFiguur(BasisFiguur.Shapes.RECTANGLE);
                            newShape.setPosSiz(initialMousePos.X, mouse_pos.Y, size_x, size_y * -1);
                            newShape.setBackColor(randomColor);
                            newShape.setSelected(false);
                            newShape.setName("rectangle");
                            mainWindow.AddShape(newShape);
                        }
                        else if (mode == "Create Ellipse")
                        {
                            BasisFiguur newShape = new BasisFiguur(BasisFiguur.Shapes.ELLIPSE);
                            newShape.setPosSiz(initialMousePos.X, mouse_pos.Y, size_x, size_y * -1);
                            newShape.setBackColor(randomColor);
                            newShape.setSelected(false);
                            newShape.setName("ellipse");
                            mainWindow.AddShape(newShape);
                        }
                    }
                    else if (size_x < 0 && size_y < 0)
                    {
                        if (mode == "Create Rectangle")
                        {
                            BasisFiguur newShape = new BasisFiguur(BasisFiguur.Shapes.RECTANGLE);
                            newShape.setPosSiz(mouse_pos.X, mouse_pos.Y, size_x * -1, size_y * -1);
                            newShape.setBackColor(randomColor);
                            newShape.setSelected(false);
                            newShape.setName("rectangle");
                            mainWindow.AddShape(newShape);
                        }
                        else if (mode == "Create Ellipse")
                        {
                            BasisFiguur newShape = new BasisFiguur(BasisFiguur.Shapes.ELLIPSE);
                            newShape.setPosSiz(mouse_pos.X, mouse_pos.Y, size_x * -1, size_y * -1);
                            newShape.setBackColor(randomColor);
                            newShape.setSelected(false);
                            newShape.setName("ellipse");
                            mainWindow.AddShape(newShape);
                        }
                    }
                    else
                    {
                        if (mode == "Create Rectangle")
                        {
                            BasisFiguur newShape = new BasisFiguur(BasisFiguur.Shapes.RECTANGLE);
                            newShape.setPosSiz(initialMousePos.X, initialMousePos.Y, size_x, size_y);
                            newShape.setBackColor(randomColor);
                            newShape.setSelected(false);
                            newShape.setName("rectangle");
                            mainWindow.AddShape(newShape);
                        }
                        else if (mode == "Create Ellipse")
                        {
                            BasisFiguur newShape = new BasisFiguur(BasisFiguur.Shapes.ELLIPSE);
                            newShape.setPosSiz(initialMousePos.X, initialMousePos.Y, size_x, size_y);
                            newShape.setBackColor(randomColor);
                            newShape.setSelected(false);
                            newShape.setName("ellipse");
                            mainWindow.AddShape(newShape);
                        }
                    }
                }
            }
            //Once the shape is added the whole window needs to be refreshed.
            this.Refresh();
        }

        private void DrawingApp_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            foreach (GroupComponent currentComponent in mainWindow.GetGroups())
            {
                currentComponent.Draw(e);
            }

            //And an outline needs to be drawn as well to show a new shape is being created.
            foreach (BasisFiguur currentOutline in outlines)
            {
                currentOutline.strategy.Draw(e, new SolidBrush(Color.Black), new Rectangle(currentOutline.GetPosX(), currentOutline.GetPosY(), currentOutline.GetSizX(), currentOutline.GetSizY()), false);
            }
            //Empty the outlines once they've all been drawn.
            outlines.Clear();
        }

        private void DrawingApp_MouseMove(object sender, MouseEventArgs e)
        {
            //Outlines while creating/moving or resizing.
            if (mouseDown)
            {
                var mouse_pos = this.PointToClient(Cursor.Position);
                int size_x = mouse_pos.X - initialMousePos.X;
                int size_y = mouse_pos.Y - initialMousePos.Y;
                if (mode == "Move")
                {
                    foreach (BasisFiguur current_shape in this.mainWindow.GetShapes())
                    {
                        if (current_shape.isSelected())
                        {
                            //Add an outline to show where the shape will be moved to.
                            BasisFiguur newOutline = null;
                            
                            if (current_shape.strategy.toString() == "rectangle")
                            {
                                newOutline = new BasisFiguur(BasisFiguur.Shapes.RECTANGLE);
                                newOutline.setPosSiz(current_shape.GetMinX() + (mouse_pos.X - initialMousePos.X), current_shape.GetMinY() + (mouse_pos.Y - initialMousePos.Y), current_shape.GetMaxX() - current_shape.GetMinX(), current_shape.GetMaxY() - current_shape.GetMinY());
                                newOutline.setSelected(false);
                                newOutline.setBackColor(Color.Black);
                            }else if(current_shape.strategy.toString() == "ellipse")
                            {
                                newOutline = new BasisFiguur(BasisFiguur.Shapes.ELLIPSE);
                                newOutline.setPosSiz(current_shape.GetMinX() + (mouse_pos.X - initialMousePos.X), current_shape.GetMinY() + (mouse_pos.Y - initialMousePos.Y), current_shape.GetMaxX() - current_shape.GetMinX(), current_shape.GetMaxY() - current_shape.GetMinY());
                                newOutline.setSelected(false);
                                newOutline.setBackColor(Color.Black);
                            }
                            if (newOutline != null)
                            {
                                outlines.Add(newOutline);
                            }
                        }
                    }
                }
                if (mode == "Resize")
                {
                    foreach (BasisFiguur current_shape in this.mainWindow.GetShapes())
                    {
                        if (current_shape.isSelected())
                        {
                            //Change the size of every shape that has the is_selected boolean active.
                            BasisFiguur newOutline = null;
                            if(current_shape.strategy.toString() == "rectangle")
                            {
                                newOutline = new BasisFiguur(BasisFiguur.Shapes.RECTANGLE);
                                newOutline.setPosSiz(current_shape.GetPosX(), current_shape.GetPosY(), mouse_pos.X - current_shape.GetPosX(), mouse_pos.Y - current_shape.GetPosY());
                                newOutline.setSelected(false);
                                newOutline.setBackColor(Color.Black);

                            }else if(current_shape.strategy.toString() == "ellipse")
                            {
                                newOutline = new BasisFiguur(BasisFiguur.Shapes.ELLIPSE);
                                newOutline.setPosSiz(current_shape.GetPosX(), current_shape.GetPosY(), mouse_pos.X - current_shape.GetPosX(), mouse_pos.Y - current_shape.GetPosY());
                                newOutline.setSelected(false);
                                newOutline.setBackColor(Color.Black);
                            }
                            if (newOutline != null)
                            {
                                outlines.Add(newOutline);
                            }                            
                            this.Refresh();
                        }
                    }
                }
                else
                {
                    label1.Text = "Coordinates: " + this.PointToClient(Cursor.Position).X + "x" + this.PointToClient(Cursor.Position).Y;
                    //Every posible direction to draw a new shape is handled here. 
                    BasisFiguur newOutline = null;
                    if (size_x < 0 && size_y > 0)
                    {
                        if (mode == "Create Rectangle")
                        {
                            //Just the outline rectangle is drawn first
                            newOutline = new BasisFiguur(BasisFiguur.Shapes.RECTANGLE);
                            newOutline.setPosSiz(mouse_pos.X, initialMousePos.Y, size_x * -1, size_y);
                            newOutline.setBackColor(Color.Black);
                            newOutline.setSelected(false);
                        }
                        else if (mode == "Create Ellipse")
                        {
                            newOutline = new BasisFiguur(BasisFiguur.Shapes.ELLIPSE);
                            newOutline.setPosSiz(mouse_pos.X, initialMousePos.Y, size_x * -1, size_y);
                            newOutline.setBackColor(Color.Black);
                            newOutline.setSelected(false);
                        }
                    }
                    else if (size_x > 0 && size_y < 0)
                    {
                        if (mode == "Create Rectangle")
                        {
                            newOutline = new BasisFiguur(BasisFiguur.Shapes.RECTANGLE);
                            newOutline.setPosSiz(initialMousePos.X, mouse_pos.Y, size_x, size_y * -1);
                            newOutline.setBackColor(Color.Black);
                            newOutline.setSelected(false);
                        }
                        else if (mode == "Create Ellipse")
                        {
                            newOutline = new BasisFiguur(BasisFiguur.Shapes.ELLIPSE);
                            newOutline.setPosSiz(initialMousePos.X, mouse_pos.Y, size_x, size_y * -1);
                            newOutline.setBackColor(Color.Black);
                            newOutline.setSelected(false);
                        }
                    }
                    else if (size_x < 0 && size_y < 0)
                    {
                        if (mode == "Create Rectangle")
                        {
                            newOutline = new BasisFiguur(BasisFiguur.Shapes.RECTANGLE);
                            newOutline.setPosSiz(mouse_pos.X, mouse_pos.Y, size_x * -1, size_y * -1);
                            newOutline.setBackColor(Color.Black);
                            newOutline.setSelected(false);
                        }
                        else if (mode == "Create Ellipse")
                        {
                            newOutline = new BasisFiguur(BasisFiguur.Shapes.ELLIPSE);
                            newOutline.setPosSiz(mouse_pos.X, mouse_pos.Y, size_x * -1, size_y * -1);
                            newOutline.setBackColor(Color.Black);
                            newOutline.setSelected(false);
                        }
                    }
                    else
                    {
                        if (mode == "Create Rectangle")
                        {
                            newOutline = new BasisFiguur(BasisFiguur.Shapes.RECTANGLE);
                            newOutline.setPosSiz(initialMousePos.X, initialMousePos.Y, size_x, size_y);
                            newOutline.setBackColor(Color.Black);
                            newOutline.setSelected(false);
                        }
                        else if (mode == "Create Ellipse")
                        {
                            newOutline = new BasisFiguur(BasisFiguur.Shapes.ELLIPSE);
                            newOutline.setPosSiz(initialMousePos.X, initialMousePos.Y, size_x, size_y);
                            newOutline.setBackColor(Color.Black);
                            newOutline.setSelected(false);
                        }
                    }
                    if (newOutline != null)
                    {
                        outlines.Add(newOutline);
                    }
                    this.Refresh();
                }
            }
        }
        private void modebox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //This method will notice if a different mode is selected in the combobox and change the "mode".
            ComboBox comboBox = (ComboBox)sender;
            if (mode != (string)comboBox.SelectedItem)
            {
                foreach (BasisFiguur currentshape in this.mainWindow.GetShapes())
                {
                    currentshape.setSelected (false);
                }
                this.Refresh();
            }
            mode = (string)comboBox.SelectedItem;
            if (mode == "Move" || mode == "Resize")
            {
                AddOrnamentButton.Enabled = true;
                OrnamentSideCombo.Enabled = true;
                OrnamentTextbox.Enabled = true;
            }
            else
            {
                AddOrnamentButton.Enabled = false;
                OrnamentSideCombo.Enabled = false;
                OrnamentTextbox.Enabled = false;
            }
        }

        private void DrawingApp_MouseClick(object sender, MouseEventArgs e)
        {
            //This function is used to finalize the move or resize.
            Point finalMousePos = this.PointToClient(Cursor.Position);
            if (initialMousePos != finalMousePos)
            {
                if (mode == "Move")
                {
                    foreach (BasisFiguur currentShape in this.mainWindow.GetShapes())
                    {
                        if (currentShape.isSelected())
                        {
                            mainWindow.MoveShape(currentShape, currentShape.GetMinX() + (finalMousePos.X - initialMousePos.X), currentShape.GetMinY() + (finalMousePos.Y - initialMousePos.Y));
                        }
                    }
                }
                else if (mode == "Resize")
                {
                    foreach (BasisFiguur currentShape in this.mainWindow.GetShapes())
                    {
                        if (currentShape.isSelected())
                        {
                            mainWindow.ResizeShape(currentShape, finalMousePos.X - currentShape.GetMinX(), finalMousePos.Y - currentShape.GetMinY());
                        }
                    }
                }
            }
        }

        private void GroupButton_Click(object sender, EventArgs e)
        {
            List<GroupComponent> shapesToGroup = new List<GroupComponent>();
            foreach (GroupComponent currentShape in this.mainWindow.GetGroups())
            {
                if (currentShape.isSelected())
                {
                    shapesToGroup.Add(currentShape);
                }
            }
            mainWindow.GroupShapes(shapesToGroup);
            this.Refresh();
        }

        private void UngroupButton_Click(object sender, EventArgs e)
        {
            List<GroupComponent> shapesToUnGroup = new List<GroupComponent>();
            foreach (GroupComponent currentShape in this.mainWindow.GetGroups())
            {
                if (currentShape.isSelected())
                {
                    shapesToUnGroup.Add(currentShape);
                }
            }
            mainWindow.UnGroupShapes(shapesToUnGroup);
            this.Refresh();
        }

        private void AddOrnamentButton_Click(object sender, EventArgs e)
        {
            List<GroupComponent> newList = this.mainWindow.GetGroups();
            Console.WriteLine(newList.Count);
            //foreach (var currentShape1 in newList)
                for (int i = 0; i < newList.Count;i++ )
                {
                    GroupComponent currentShape = newList[i];
                        if (currentShape.isSelected())
                        {
                            string textInput = OrnamentTextbox.Text;
                            Font mainFont = new Font("Arial", 16);
                            SizeF stringSize = new SizeF();
                            Graphics g = this.CreateGraphics();
                            stringSize = g.MeasureString(textInput, mainFont, 200);
                            switch (OrnamentSideCombo.SelectedItem.ToString())
                            {
                                case "Top":
                                    //Create a new ornament and set the text.
                                    Ornament newOr = new Ornament(textInput);
                                    //This ornament will be a top ornament.
                                    TopOrnament newTopOrnament = new TopOrnament(newOr);
                                    //Add the new ornament to the list. After which it can be drawn or saved.
                                    //currentShape.AddOrnament(newTopOrnament);

                                    newTopOrnament.SetPosX((int)(currentShape.GetPosX() + (currentShape.GetSizX() / 2) - (stringSize.Width / 2)));
                                    newTopOrnament.SetPosY((int)(currentShape.GetPosY() - (stringSize.Height / 2)));
                                    mainWindow.AddOrnament(newTopOrnament, currentShape);
                                    break;
                                case "Bottom":
                                    Ornament newOrna = new Ornament(textInput);
                                    ButtomOrnament newBottomOrnament = new ButtomOrnament(newOrna);

                                    newBottomOrnament.SetPosX((int)(currentShape.GetPosX() + (currentShape.GetSizX() / 2) - (stringSize.Width / 2)));
                                    newBottomOrnament.SetPosY((int)((currentShape.GetPosY() + currentShape.GetSizY()) - (stringSize.Height / 2)));

                                    mainWindow.AddOrnament(newBottomOrnament, currentShape);
                                    break;
                                case "Left":
                                    Ornament newOrnam = new Ornament(textInput);
                                    LeftOrnament newLeftOrnament = new LeftOrnament(newOrnam);

                                    newLeftOrnament.SetPosX((int)(currentShape.GetPosX() - (stringSize.Width / 2)));
                                    newLeftOrnament.SetPosY((int)(currentShape.GetPosY() + (currentShape.GetSizY() / 2) - (stringSize.Height / 2)));

                                    mainWindow.AddOrnament(newLeftOrnament, currentShape);
                                    break;
                                case "Right":
                                    Ornament newOrname = new Ornament(textInput);
                                    RightOrnament newRightOrnament = new RightOrnament(newOrname);

                                    newRightOrnament.SetPosX((int)((currentShape.GetPosX() + currentShape.GetSizX()) - (stringSize.Width / 2)));
                                    newRightOrnament.SetPosY((int)(currentShape.GetPosY() + (currentShape.GetSizY() / 2) - (stringSize.Height / 2)));

                                    mainWindow.AddOrnament(newRightOrnament, currentShape);
                                    break;
                                default:
                                    break;
                            }
                    }
                }
            this.Refresh();
        }
    }
}
