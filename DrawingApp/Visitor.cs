namespace DrawingApp
{
    interface Visitor
    {
        //The three possible object types are parsed as a variable.
        void visit(MoveObject moveObject);
        void visit(ResizeObject resizeObject);
        void visit(WriteToFile writeToFile);
    }
}
