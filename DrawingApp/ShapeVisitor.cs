using System;
using System.IO;
using System.Windows.Forms;

namespace DrawingApp
{
    class ShapeVisitor : Visitor
    {
        //The ShapeVisitor holds the actual code for the operations.
        public ShapeVisitor(){
        }

        public void visit(MoveObject shapeOrGroup)
        {
            //Finnally actually move the shape to the new position.
            Shape currentShape = shapeOrGroup.getShape();
            currentShape.pos_x = shapeOrGroup.getX();
            currentShape.pos_y = shapeOrGroup.getY();
        }

        public void visit(ResizeObject shapeOrGroup)
        {
            //Resize the shape to the new dimentions.
            Shape currentShape = shapeOrGroup.getShape();
            currentShape.size_x = shapeOrGroup.getXSize();
            currentShape.size_y = shapeOrGroup.getYSize();
        }

        public void visit(WriteToFile shapeOrGroup)
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

                    foreach (GroupComponent component in shapeOrGroup.GetController().GetGroups())
                    {
                        component.WriteToFile(writer, 0);
                        //Only the important info is saved. Not the color. Unnessesary.
                    }
                }
            }
        }
    }
}
