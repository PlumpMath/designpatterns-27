namespace DrawingApp
{
    interface Visitor
    {
        //The three possible object types are parsed as a variable.
        void visit(MoveObject shapeOrGroup);
        void visit(ResizeObject shapeOrGroup);
        void visit(WriteToFile shapeOrGroup);
    }
}
