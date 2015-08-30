namespace DrawingApp
{
    class WriteToFile : Visitable
    {
        private Controller controller;
        public WriteToFile(Controller controller)
        {
            this.controller = controller;
        }
        public Controller GetController()
        {
            return this.controller;
        }
        public void accept(Visitor visitor)
        {
            visitor.visit(this);
        }
    }
}
