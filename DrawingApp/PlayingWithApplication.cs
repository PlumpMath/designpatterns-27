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
        public DrawingApp()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            modebox.SelectedIndex = 0;

            DrawingInterface newApp = ApplicationController.getApplication();
            DrawNewShape drawCommand = new DrawNewShape(newApp);
            ApplicationButton drawPressed = new ApplicationButton(drawCommand);
            drawPressed.press();

            Undo undoCommand = new Undo(newApp);
            ApplicationButton undoPressed = new ApplicationButton(undoCommand);
            undoPressed.press();

            Redo redoCommand = new Redo(newApp);
            ApplicationButton redoPressed = new ApplicationButton(redoCommand);
            redoPressed.press();

            Save saveCommand = new Save(newApp);
            ApplicationButton savePressed = new ApplicationButton(saveCommand);
            savePressed.press();

            Load loadCommand = new Load(newApp);
            ApplicationButton loadPressed = new ApplicationButton(loadCommand);
            loadPressed.press();

        }
    }
}
