namespace DrawingApp
{
    class ResizeObject : Visitable
    {
        private Shape shape;
        private int newXSize;
        private int newYSize;

        public ResizeObject(Shape shape, int newXSize, int newYSize)
        {
            this.shape = shape;
            this.newXSize = newXSize;
            this.newYSize = newYSize;
        }

        public void accept(Visitor visitor)
        {
            visitor.visit(this);
        }
        public Shape getShape()
        {
            return shape;
        }
        public int getXSize()
        {
            return newXSize;
        }
        public int getYSize()
        {
            return newYSize;
        }
    }
}
