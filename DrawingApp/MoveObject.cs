namespace DrawingApp
{
    class MoveObject : Visitable
    {
        private GroupComponent shape;
        private int new_x_offset;
        private int new_y_offset;

        public MoveObject(GroupComponent shape, int new_x_offset, int new_y_offset)
        {
            this.shape = shape;
            this.new_x_offset = new_x_offset;
            this.new_y_offset = new_y_offset;
        }

        public void accept(Visitor visitor)
        {
            visitor.visit(this);
        }
        public GroupComponent getShape()
        {
            return shape;
        }
        public int getX()
        {
            return new_x_offset;
        }
        public int getY()
        {
            return new_y_offset;
        }
    }
}
