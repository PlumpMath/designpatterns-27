using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DrawingApp
{
    public partial class DrawingApp : Form
    {
        Pen blackPen = new Pen(Color.Black, 3);
        Pen selected_pen = new Pen(Color.Black, 2);
        DrawingWindow mainWindow;
        static Stack<Shape> redostack;
        static Stack<Shape> shapestack;
        Shape outline;
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
            redostack = new Stack<Shape>();
            shapestack = new Stack<Shape>();
            
        }
        abstract class UndoableCommand
        {
            public abstract void Execute();
            public abstract void UnExecute();
        }
        abstract class NormalCommand
        {
            public abstract void Execute();
        }
        class MoveShapeCommand : UndoableCommand
        {
            private Shape shape;
            private Controller controller;
            private int old_pos_x;
            private int old_pos_y;
            private int new_pos_x;
            private int new_pos_y;

            public MoveShapeCommand(Controller controller, Shape shape, int new_x_pos, int new_y_pos)
            {
                this.controller = controller;
                this.shape = shape;
                this.new_pos_x = new_x_pos;
                this.new_pos_y = new_y_pos;
            }

            public override void Execute()
            {
                old_pos_x = shape.pos_x;
                old_pos_y = shape.pos_y;
                shape.pos_x = new_pos_x;
                shape.pos_y = new_pos_y;
            }

            public override void UnExecute()
            {
                shape.pos_x = old_pos_x;
                shape.pos_y = old_pos_y;
            }
        }
        class AddShapeCommand : UndoableCommand
        {
            private Shape shape;
            private Controller controller;

            // Constructor
            public AddShapeCommand(Controller controller, Shape shape)
            {
                this.shape = shape;
                this.controller = controller;
            }

            // Execute new command
            public override void Execute()
            {
                controller.AddShape(shape);
            }

            // Unexecute last command
            public override void UnExecute()
            {
                controller.RemoveShape();
                
            }
        }
        class SaveCommand : NormalCommand
        {
            private Controller controller;
            public SaveCommand(Controller controller)
            {
                this.controller = controller;
            }
            public override void Execute()
            {
                controller.SaveToFile();
            }
        }
        class LoadCommand : NormalCommand
        {
            private Controller controller;
            public LoadCommand(Controller controller)
            {
                this.controller = controller;
            }
            public override void Execute()
            {
                controller.LoadFile();
            }
        }
        class Controller
        {

            public void AddShape(Shape shape)
            {
                shapestack.Push(shape);
            }
            public void RemoveShape()
            {
                redostack.Push(shapestack.Pop());
            }
            public void SaveToFile()
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                saveFileDialog1.RestoreDirectory = true;


                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter writer = new StreamWriter(saveFileDialog1.OpenFile()))
                    {
                        foreach (Shape currentshape in shapestack)
                        {
                            writer.WriteLine(currentshape.type + " " + currentshape.pos_x + " " + currentshape.pos_y + " " + currentshape.size_x + " " + currentshape.size_y);
                        }
                    }
                }
            }
            public void LoadFile()
            {
                OpenFileDialog openFielDialog = new OpenFileDialog();
                openFielDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFielDialog.FilterIndex = 2;
                openFielDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                openFielDialog.RestoreDirectory = true;
                if (openFielDialog.ShowDialog() == DialogResult.OK)
                {
                    shapestack.Clear();
                    using (StreamReader reader = new StreamReader(openFielDialog.OpenFile()))
                    {
                        while (!reader.EndOfStream)
                        {
                            Color randomColor = Color.FromArgb(Random.Next(255), Random.Next(255), Random.Next(255));
                            string[] newline = reader.ReadLine().Split(' ');
                            shapestack.Push(new Shape(newline[0], randomColor, Convert.ToInt32(newline[1]), Convert.ToInt32(newline[2]), Convert.ToInt32(newline[3]), Convert.ToInt32(newline[4]), false));
                        }
                    }
                }
            }
        }
        class DrawingWindow
        {
            // Initializers
            private Controller controller = new Controller();
            private List<UndoableCommand> commandList = new List<UndoableCommand>();
            private int _current = 0;

            public void Redo()
            {
                // Perform redo operations
                if (_current < commandList.Count)
                {
                    UndoableCommand command = commandList[_current++] as UndoableCommand;
                    command.Execute();
                }
            }
            public void Undo()
            {
                // Perform undo operations
                if (_current > 0)
                {
                    UndoableCommand command = commandList[--_current] as UndoableCommand;
                    command.UnExecute();
                }
            }
            public void AddShape(Shape shape)
            {
                // Create command operation and execute it
                UndoableCommand command = new AddShapeCommand(controller,shape);
                command.Execute();

                // Add command to undo list
                commandList.Add(command);
                _current++;
            }
            public void MoveShape(Shape shape, int new_x_pos, int new_y_pos)
            {
                // Create command operation and execute it
                UndoableCommand command = new MoveShapeCommand(controller, shape, new_x_pos, new_y_pos);
                command.Execute();

                // Add command to undo list
                commandList.Add(command);
                _current++;
            }
            public void Save()
            {
                NormalCommand command = new SaveCommand(controller);
                command.Execute();
            }
            public void Load()
            {
                NormalCommand command = new LoadCommand(controller);
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
                foreach (Shape current_shape in shapestack)
                {
                    if (new Rectangle(current_shape.pos_x, current_shape.pos_y, current_shape.size_x, current_shape.size_y).Contains(initialMousePos))
                    {
                        current_shape.is_selected = !current_shape.is_selected;
                        //Make sure to break or else all the underlaying shapes will also be selected.
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
                if (size_x < 0 && size_y > 0)
                {
                    if (mode == "Create Rectangle")
                    {
                        //To not have any negative numbers some of the variables need to be multiplied by -1.
                        mainWindow.AddShape(new Shape("Rectangle", randomColor, mouse_pos.X, initialMousePos.Y, size_x * -1, size_y, false));
                    }
                    else if (mode == "Create Ellipse")
                    {
                        //This will execute the AddShape command
                        mainWindow.AddShape(new Shape("Ellipse", randomColor, mouse_pos.X, initialMousePos.Y, size_x * -1, size_y, false));
                    }
                }
                else if (size_x > 0 && size_y < 0)
                {
                    if (mode == "Create Rectangle")
                    {
                        mainWindow.AddShape(new Shape("Rectangle", randomColor, initialMousePos.X, mouse_pos.Y, size_x, size_y * -1, false));
                    }
                    else if (mode == "Create Ellipse")
                    {
                        mainWindow.AddShape(new Shape("Ellipse", randomColor, initialMousePos.X, mouse_pos.Y, size_x, size_y * -1, false));
                    }
                }
                else if (size_x < 0 && size_y < 0)
                {
                    if (mode == "Create Rectangle")
                    {
                        mainWindow.AddShape(new Shape("Rectangle", randomColor, mouse_pos.X, mouse_pos.Y, size_x * -1, size_y * -1, false));
                    }
                    else if (mode == "Create Ellipse")
                    {
                        mainWindow.AddShape(new Shape("Ellipse", randomColor, mouse_pos.X, mouse_pos.Y, size_x * -1, size_y * -1, false));
                    }
                }
                else
                {
                    if (mode == "Create Rectangle")
                    {
                        mainWindow.AddShape(new Shape("Rectangle", randomColor, initialMousePos.X, initialMousePos.Y, size_x, size_y, false));
                    }
                    else if (mode == "Create Ellipse")
                    {
                        mainWindow.AddShape(new Shape("Ellipse", randomColor, initialMousePos.X, initialMousePos.Y, size_x, size_y, false));
                    }
                }
                outline = null;
            }
            //Once the shape is added the whole window needs to be refreshed.
            this.Refresh();
        }

        private void DrawingApp_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            //While painting the app needs to redraw every shape in the shapequeue.
                foreach (Shape current_shape in shapestack.Reverse())
                {
                    SolidBrush brush = new SolidBrush(current_shape.back_color);
                    if (current_shape.type == "Rectangle")
                    {
                        g.FillRectangle(brush, current_shape.pos_x, current_shape.pos_y, current_shape.size_x, current_shape.size_y);
                        if (current_shape.is_selected)
                        {
                            g.DrawRectangle(selected_pen, current_shape.pos_x, current_shape.pos_y, current_shape.size_x, current_shape.size_y);
                        }
                    }
                    else if (current_shape.type == "Ellipse")
                    {
                        g.FillEllipse(brush, current_shape.pos_x, current_shape.pos_y, current_shape.size_x, current_shape.size_y);
                        if (current_shape.is_selected)
                        {
                            g.DrawEllipse(selected_pen, current_shape.pos_x, current_shape.pos_y, current_shape.size_x, current_shape.size_y);
                        }
                    }
                }
            //And an outline needs to be drawn as well to show a new shape is being created.
            if (outline != null)
            {
                if (outline.type == "Outline Rectangle")
                {
                    g.DrawRectangle(blackPen, outline.pos_x, outline.pos_y, outline.size_x, outline.size_y);
                }
                else if (outline.type == "Outline Ellipse")
                {
                    g.DrawEllipse(blackPen, outline.pos_x, outline.pos_y, outline.size_x, outline.size_y);
                }
            }
        }

        private void DrawingApp_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                var mouse_pos = this.PointToClient(Cursor.Position);
                int size_x = mouse_pos.X - initialMousePos.X;
                int size_y = mouse_pos.Y - initialMousePos.Y;
                if (mode == "Move")
                {
                    foreach (Shape current_shape in shapestack)
                    {
                        if (current_shape.is_selected)
                        {
                            //Change the position of every shape that has the is_selected boolean active.
                            current_shape.pos_x = mouse_pos.X - current_shape.size_x / 2;
                            current_shape.pos_y = mouse_pos.Y - current_shape.size_y / 2;
                            this.Refresh();
                        }
                    }
                }
                if (mode == "Resize")
                {
                    foreach (Shape current_shape in shapestack)
                    {
                        if (current_shape.is_selected)
                        {
                            //Change the size of every shape that has the is_selected boolean active.
                            current_shape.size_x = mouse_pos.X - current_shape.pos_x;
                            current_shape.size_y = mouse_pos.Y - current_shape.pos_y;
                            this.Refresh();
                        }
                    }
                }
                else
                {
                    label1.Text = "Coordinates: " + this.PointToClient(Cursor.Position).X + "x" + this.PointToClient(Cursor.Position).Y;
                    //Every posible direction to draw a new shape is handled here. 
                    if (size_x < 0 && size_y > 0)
                    {
                        if (mode == "Create Rectangle")
                        {
                            //Just the outline rectangle is drawn first
                            outline = new Shape("Outline Rectangle", Color.Black, mouse_pos.X, initialMousePos.Y, size_x * -1, size_y, false);
                        }
                        else if (mode == "Create Ellipse")
                        {
                            outline = new Shape("Outline Ellipse", Color.Black, mouse_pos.X, initialMousePos.Y, size_x * -1, size_y, false);
                        }
                    }
                    else if (size_x > 0 && size_y < 0)
                    {
                        if (mode == "Create Rectangle")
                        {
                            outline = new Shape("Outline Rectangle", Color.Black, initialMousePos.X, mouse_pos.Y, size_x, size_y * -1, false);
                        }
                        else if (mode == "Create Ellipse")
                        {
                            outline = new Shape("Outline Ellipse", Color.Black, initialMousePos.X, mouse_pos.Y, size_x, size_y * -1, false);
                        }
                    }
                    else if (size_x < 0 && size_y < 0)
                    {
                        if (mode == "Create Rectangle")
                        {
                            outline = new Shape("Outline Rectangle", Color.Black, mouse_pos.X, mouse_pos.Y, size_x * -1, size_y * -1, false);
                        }
                        else if (mode == "Create Ellipse")
                        {
                            outline = new Shape("Outline Ellipse", Color.Black, mouse_pos.X, mouse_pos.Y, size_x * -1, size_y * -1, false);
                        }
                    }
                    else
                    {
                        if (mode == "Create Rectangle")
                        {
                            outline = new Shape("Outline Rectangle", Color.Black, initialMousePos.X, initialMousePos.Y, size_x, size_y, false);
                        }
                        else if (mode == "Create Ellipse")
                        {
                            outline = new Shape("Outline Ellipse", Color.Black, initialMousePos.X, initialMousePos.Y, size_x, size_y, false);
                        }
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
                foreach (Shape currentshape in shapestack)
                {
                    currentshape.is_selected = false;
                }
                this.Refresh();
            }
            mode = (string)comboBox.SelectedItem;
        }
    }
}
