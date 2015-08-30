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
        Shape outline;
        List<Shape> outlines = new List<Shape>();
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
            public void ToggleSelect(Shape shape)
            {
                controller.ToggleSelect(shape);
            }

            public Stack<UndoableCommand> GetCommandStack()
            {
                return commandstack;
            }
            public List<Shape> GetShapes()
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
            public void AddShape(Shape shape)
            {
                // Create command operation and execute it
                UndoableCommand command = new AddShapeCommand(controller, shape);
                command.Execute();
                // Add command to command stack
                commandstack.Push(command);
            }
            public void MoveShape(Shape shape, int new_x_pos, int new_y_pos)
            {
                // Create command operation and execute it
                UndoableCommand command = new MoveShapeCommand(controller, shapeVisitor, shape, new_x_pos, new_y_pos);
                command.Execute();
                // Add command to command stack
                commandstack.Push(command);
            }
            public void ResizeShape(Shape shape, int new_x_size, int new_y_size)
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
                            mainWindow.AddShape(new Shape("rectangle", randomColor, mouse_pos.X, initialMousePos.Y, size_x * -1, size_y, false));
                        }
                        else if (mode == "Create Ellipse")
                        {
                            //This will execute the AddShape command
                            mainWindow.AddShape(new Shape("ellipse", randomColor, mouse_pos.X, initialMousePos.Y, size_x * -1, size_y, false));
                        }
                    }
                    else if (size_x > 0 && size_y < 0)
                    {
                        if (mode == "Create Rectangle")
                        {
                            mainWindow.AddShape(new Shape("rectangle", randomColor, initialMousePos.X, mouse_pos.Y, size_x, size_y * -1, false));
                        }
                        else if (mode == "Create Ellipse")
                        {
                            mainWindow.AddShape(new Shape("ellipse", randomColor, initialMousePos.X, mouse_pos.Y, size_x, size_y * -1, false));
                        }
                    }
                    else if (size_x < 0 && size_y < 0)
                    {
                        if (mode == "Create Rectangle")
                        {
                            mainWindow.AddShape(new Shape("rectangle", randomColor, mouse_pos.X, mouse_pos.Y, size_x * -1, size_y * -1, false));
                        }
                        else if (mode == "Create Ellipse")
                        {
                            mainWindow.AddShape(new Shape("ellipse", randomColor, mouse_pos.X, mouse_pos.Y, size_x * -1, size_y * -1, false));
                        }
                    }
                    else
                    {
                        if (mode == "Create Rectangle")
                        {
                            mainWindow.AddShape(new Shape("rectangle", randomColor, initialMousePos.X, initialMousePos.Y, size_x, size_y, false));
                        }
                        else if (mode == "Create Ellipse")
                        {
                            mainWindow.AddShape(new Shape("ellipse", randomColor, initialMousePos.X, initialMousePos.Y, size_x, size_y, false));
                        }
                    }
                    outline = null;
                }
            }
            //Once the shape is added the whole window needs to be refreshed.
            this.Refresh();
        }

        private void DrawingApp_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            //While painting the app needs to redraw every shape in the shapequeue.
            List<Shape> allShapes = this.mainWindow.GetShapes();
            foreach (Shape currentShape in allShapes)
            {
                SolidBrush brush = new SolidBrush(currentShape.back_color);
                if (currentShape.type == "rectangle")
                {
                    g.FillRectangle(brush, currentShape.pos_x, currentShape.pos_y, currentShape.size_x, currentShape.size_y);
                    if (currentShape.is_selected)
                    {
                        //g.DrawRectangle(selected_pen, currentShape.pos_x, currentShape.pos_y, currentShape.size_x, currentShape.size_y);
                    }
                }
                else if (currentShape.type == "ellipse")
                {
                    g.FillEllipse(brush, currentShape.pos_x, currentShape.pos_y, currentShape.size_x, currentShape.size_y);
                    if (currentShape.is_selected)
                    {
                        //g.DrawEllipse(selected_pen, currentShape.pos_x, currentShape.pos_y, currentShape.size_x, currentShape.size_y);
                    }
                }
            }

            foreach (GroupComponent currentComponent in mainWindow.GetGroups())
            {
                if (currentComponent.isSelected())
                {
                    g.DrawRectangle(selected_pen, currentComponent.GetMinX(), currentComponent.GetMinY(), currentComponent.GetMaxX() - currentComponent.GetMinX(), currentComponent.GetMaxY() - currentComponent.GetMinY());
                }
            }

            //And an outline needs to be drawn as well to show a new shape is being created.
            foreach (Shape currentOutline in outlines)
            {
                if (currentOutline.type == "Outline rectangle")
                {
                    g.DrawRectangle(blackPen, currentOutline.pos_x, currentOutline.pos_y, currentOutline.size_x, currentOutline.size_y);
                }
                else if (outline.type == "Outline ellipse")
                {
                    g.DrawEllipse(blackPen, currentOutline.pos_x, currentOutline.pos_y, currentOutline.size_x, currentOutline.size_y);
                }
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
                    foreach (Shape current_shape in this.mainWindow.GetShapes())
                    {
                        if (current_shape.is_selected)
                        {
                            //Add an outline to show where the shape will be moved to.
                            Shape newOutline = new Shape("Outline " + current_shape.type, Color.Black, current_shape.pos_x + (mouse_pos.X - initialMousePos.X), current_shape.pos_y + (mouse_pos.Y - initialMousePos.Y), current_shape.size_x, current_shape.size_y, false);
                            outlines.Add(newOutline);
                        }
                    }
                }
                if (mode == "Resize")
                {
                    foreach (Shape current_shape in this.mainWindow.GetShapes())
                    {
                        if (current_shape.is_selected)
                        {
                            //Change the size of every shape that has the is_selected boolean active.
                            Shape newOutline = new Shape("Outline " + current_shape.type, Color.Black, current_shape.pos_x, current_shape.pos_y, mouse_pos.X - current_shape.pos_x, mouse_pos.Y - current_shape.pos_y, false);
                            outlines.Add(newOutline);
                            this.Refresh();
                        }
                    }
                }
                else
                {
                    label1.Text = "Coordinates: " + this.PointToClient(Cursor.Position).X + "x" + this.PointToClient(Cursor.Position).Y;
                    //Every posible direction to draw a new shape is handled here. 
                    Shape newOutline = null;
                    if (size_x < 0 && size_y > 0)
                    {
                        if (mode == "Create Rectangle")
                        {
                            //Just the outline rectangle is drawn first
                            newOutline = new Shape("Outline rectangle", Color.Black, mouse_pos.X, initialMousePos.Y, size_x * -1, size_y, false);
                        }
                        else if (mode == "Create Ellipse")
                        {
                            newOutline = new Shape("Outline ellipse", Color.Black, mouse_pos.X, initialMousePos.Y, size_x * -1, size_y, false);
                        }
                    }
                    else if (size_x > 0 && size_y < 0)
                    {
                        if (mode == "Create Rectangle")
                        {
                            newOutline = new Shape("Outline rectangle", Color.Black, initialMousePos.X, mouse_pos.Y, size_x, size_y * -1, false);
                        }
                        else if (mode == "Create Ellipse")
                        {
                            newOutline = new Shape("Outline ellipse", Color.Black, initialMousePos.X, mouse_pos.Y, size_x, size_y * -1, false);
                        }
                    }
                    else if (size_x < 0 && size_y < 0)
                    {
                        if (mode == "Create Rectangle")
                        {
                            newOutline = new Shape("Outline rectangle", Color.Black, mouse_pos.X, mouse_pos.Y, size_x * -1, size_y * -1, false);
                        }
                        else if (mode == "Create Ellipse")
                        {
                            newOutline = new Shape("Outline ellipse", Color.Black, mouse_pos.X, mouse_pos.Y, size_x * -1, size_y * -1, false);
                        }
                    }
                    else
                    {
                        if (mode == "Create Rectangle")
                        {
                            newOutline = new Shape("Outline rectangle", Color.Black, initialMousePos.X, initialMousePos.Y, size_x, size_y, false);
                        }
                        else if (mode == "Create Ellipse")
                        {
                            newOutline = new Shape("Outline ellipse", Color.Black, initialMousePos.X, initialMousePos.Y, size_x, size_y, false);
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
                foreach (Shape currentshape in this.mainWindow.GetShapes())
                {
                    currentshape.is_selected = false;
                }
                this.Refresh();
            }
            mode = (string)comboBox.SelectedItem;
        }

        private void DrawingApp_MouseClick(object sender, MouseEventArgs e)
        {
            //This function is used to finalize the move or resize.
            Point finalMousePos = this.PointToClient(Cursor.Position);
            if (initialMousePos != finalMousePos)
            {
                if (mode == "Move")
                {
                    foreach (Shape currentShape in this.mainWindow.GetShapes())
                    {
                        if (currentShape.is_selected)
                        {
                            mainWindow.MoveShape(currentShape, currentShape.pos_x + (finalMousePos.X - initialMousePos.X), currentShape.pos_y + (finalMousePos.Y - initialMousePos.Y));
                        }
                    }
                }
                else if (mode == "Resize")
                {
                    foreach (Shape currentShape in this.mainWindow.GetShapes())
                    {
                        if (currentShape.is_selected)
                        {
                            mainWindow.ResizeShape(currentShape, finalMousePos.X - currentShape.pos_x, finalMousePos.Y - currentShape.pos_y);
                        }
                    }
                }
                //Once a move or reszie is done the outline needs to be removed.
                outline = null;
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
    }
}
