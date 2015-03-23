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
        Queue<shape> shapequeue = new Queue<shape>();  
        shape outline;
        string mode = "Create Rectangle";
        Random Random = new Random();
        bool mouse_down = false;
        Point initial_mouse_pos;
        Pen blackPen = new Pen(Color.Black, 3);
        Pen selected_pen = new Pen(Color.Black, 2);

        public DrawingApp()
        {
            InitializeComponent();
            selected_pen.DashPattern = new float[] { 2.0F, 2.0F, 2.0F, 2.0F };
            this.DoubleBuffered = true;
            modebox.SelectedIndex = 0;
        }


        private void DrawingApp_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            foreach (shape current_shape in shapequeue)
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
            if (mouse_down)
            {
                var mouse_pos = this.PointToClient(Cursor.Position);
                int size_x = mouse_pos.X - initial_mouse_pos.X;
                int size_y = mouse_pos.Y - initial_mouse_pos.Y;
                if (mode == "Move")
                {
                    foreach (shape current_shape in shapequeue)
                    {
                        if (current_shape.is_selected)
                        {
                            current_shape.pos_x = mouse_pos.X - current_shape.size_x /2;
                            current_shape.pos_y = mouse_pos.Y - current_shape.size_y /2;
                            this.Refresh();
                        }
                    }
                }
                if (mode == "Resize")
                {
                    foreach (shape current_shape in shapequeue)
                    {
                        if (current_shape.is_selected)
                        {
                            current_shape.size_x = mouse_pos.X - current_shape.pos_x;
                            current_shape.size_y = mouse_pos.Y - current_shape.pos_y;
                            this.Refresh();
                        }
                    }
                }
                else { 
                label1.Text = "Coordinates: " + this.PointToClient(Cursor.Position).X + "x" + this.PointToClient(Cursor.Position).Y;
                if (size_x < 0 && size_y > 0)
                {
                    if (mode == "Create Rectangle")
                    {
                        outline = new shape("Outline Rectangle", Color.Black, mouse_pos.X, initial_mouse_pos.Y, size_x * -1, size_y, false);
                    }
                    else if (mode == "Create Ellipse")
                    {
                        outline = new shape("Outline Ellipse", Color.Black, mouse_pos.X, initial_mouse_pos.Y, size_x * -1, size_y, false);
                    }
                }
                else if (size_x > 0 && size_y < 0)
                {
                    if (mode == "Create Rectangle")
                    {
                        outline = new shape("Outline Rectangle", Color.Black, initial_mouse_pos.X, mouse_pos.Y, size_x, size_y * -1, false);
                    }
                    else if (mode == "Create Ellipse")
                    {
                        outline = new shape("Outline Ellipse", Color.Black, initial_mouse_pos.X, mouse_pos.Y, size_x, size_y * -1, false);
                    }
                }
                else if (size_x < 0 && size_y < 0)
                {
                    if (mode == "Create Rectangle")
                    {
                        outline = new shape("Outline Rectangle", Color.Black, mouse_pos.X, mouse_pos.Y, size_x * -1, size_y * -1, false);
                    }
                    else if (mode == "Create Ellipse")
                    {
                        outline = new shape("Outline Ellipse", Color.Black, mouse_pos.X, mouse_pos.Y, size_x * -1, size_y * -1, false);
                    }
                }
                else
                {
                    if (mode == "Create Rectangle")
                    {
                        outline = new shape("Outline Rectangle", Color.Black, initial_mouse_pos.X, initial_mouse_pos.Y, size_x, size_y, false);
                    }
                    else if (mode == "Create Ellipse")
                    {
                        outline = new shape("Outline Ellipse", Color.Black, initial_mouse_pos.X, initial_mouse_pos.Y, size_x, size_y, false);
                    }
                }
                this.Refresh();
                }
            }   
        }

        private void DrawingApp_MouseDown(object sender, MouseEventArgs e)
        {
            initial_mouse_pos = this.PointToClient(Cursor.Position);
            mouse_down = true;
        }

        private void DrawingApp_MouseUp(object sender, MouseEventArgs e)
        {
            mouse_down = false;
            if (mode == "Move" | mode == "Resize")
            {
                foreach (shape current_shape in shapequeue.Reverse())
                {
                    if (new Rectangle(current_shape.pos_x, current_shape.pos_y, current_shape.size_x, current_shape.size_y).Contains(initial_mouse_pos))
                    {
                        current_shape.is_selected = !current_shape.is_selected;
                        break;
                    }
                }
            }
            else
            {
                var mouse_pos = this.PointToClient(Cursor.Position);
                Color randomColor = Color.FromArgb(Random.Next(255), Random.Next(255), Random.Next(255));
                int size_x = mouse_pos.X - initial_mouse_pos.X;
                int size_y = mouse_pos.Y - initial_mouse_pos.Y;
                if (size_x < 0 && size_y > 0)
                {
                    if (mode == "Create Rectangle")
                    {
                        shapequeue.Enqueue(new shape("Rectangle", randomColor, mouse_pos.X, initial_mouse_pos.Y, size_x * -1, size_y, false));
                    }
                    else if (mode == "Create Ellipse")
                    {
                        shapequeue.Enqueue(new shape("Ellipse", randomColor, mouse_pos.X, initial_mouse_pos.Y, size_x * -1, size_y, false));
                    }
                }
                else if (size_x > 0 && size_y < 0)
                {
                    if (mode == "Create Rectangle")
                    {
                        shapequeue.Enqueue(new shape("Rectangle", randomColor, initial_mouse_pos.X, mouse_pos.Y, size_x, size_y * -1, false));
                    }
                    else if (mode == "Create Ellipse")
                    {
                        shapequeue.Enqueue(new shape("Ellipse", randomColor, initial_mouse_pos.X, mouse_pos.Y, size_x, size_y * -1, false));
                    }
                }
                else if (size_x < 0 && size_y < 0)
                {
                    if (mode == "Create Rectangle")
                    {
                        shapequeue.Enqueue(new shape("Rectangle", randomColor, mouse_pos.X, mouse_pos.Y, size_x * -1, size_y * -1, false));
                    }
                    else if (mode == "Create Ellipse")
                    {
                        shapequeue.Enqueue(new shape("Ellipse", randomColor, mouse_pos.X, mouse_pos.Y, size_x * -1, size_y * -1, false));
                    }
                }
                else
                {
                    if (mode == "Create Rectangle")
                    {
                        shapequeue.Enqueue(new shape("Rectangle", randomColor, initial_mouse_pos.X, initial_mouse_pos.Y, size_x, size_y, false));
                    }
                    else if (mode == "Create Ellipse")
                    {
                        shapequeue.Enqueue(new shape("Ellipse", randomColor, initial_mouse_pos.X, initial_mouse_pos.Y, size_x, size_y, false));
                    }
                }
                outline = null;
            }
            this.Refresh();
        }

        private void modebox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (mode != (string)comboBox.SelectedItem)
            {
                foreach (shape currentshape in shapequeue)
                {
                    currentshape.is_selected = false;
                }
                this.Refresh();
            }
            mode = (string)comboBox.SelectedItem;
            
        }

    }
    public class shape
    {
        public String type { get; set; }
        public Color back_color { get; set; }
        public int pos_x { get; set; }
        public int pos_y { get; set; }
        public int size_x { get; set; }
        public int size_y { get; set; }
        public bool is_selected { get; set; }
        public shape(String Type, Color BackgroundColor, int PositionX, int PositionY, int SizeX, int SizeY, bool isSelected)
        {
            type = Type;
            back_color = BackgroundColor;
            pos_x = PositionX;
            pos_y = PositionY;
            size_x = SizeX;
            size_y = SizeY;
            is_selected = isSelected;
        }
    }
}
