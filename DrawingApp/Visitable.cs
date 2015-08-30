namespace DrawingApp
{
    interface Visitable
    {
        //The only function the Visitable class has is accept. It's just used to parse the Visitor to the right operation class. (Move Resize WriteToFile)
        void accept(Visitor visitor);
    }
}
