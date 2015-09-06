namespace DrawingApp
{
    class ResizeObject : Visitable
    {
        private BasisFiguur shape;
        private int newXSize;
        private int newYSize;

        public ResizeObject(BasisFiguur shape, int newXSize, int newYSize)
        {
            this.shape = shape;
            this.newXSize = newXSize;
            this.newYSize = newYSize;
        }

        public void accept(Visitor visitor)
        {
            visitor.visit(this);
        }
        public BasisFiguur getShape()
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
