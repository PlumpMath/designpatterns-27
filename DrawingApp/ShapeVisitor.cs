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

        public void visit(MoveObject moveObject)
        {
            //Finnally actually move the shape to the new position.
            BasisFiguur currentShape = moveObject.getShape();
            currentShape.SetPosX(moveObject.getX());
            currentShape.SetPosY(moveObject.getY());
        }

        public void visit(ResizeObject resizeObject)
        {
            //Resize the shape to the new dimentions.
            BasisFiguur currentShape = resizeObject.getShape();
            currentShape.SetSizX(resizeObject.getXSize());
            currentShape.SetSizY(resizeObject.getYSize());
        }

        public void visit(WriteToFile writeToFile)
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

                    foreach (GroupComponent component in writeToFile.GetController().GetGroups())
                    {
                        component.WriteToFile(writer, 0);
                        //Only the important info is saved. Not the color. Unnessesary.
                    }
                }
            }
        }
    }
}
