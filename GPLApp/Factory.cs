namespace GPLApp
{
    /// <summary>
    /// Class declared and inherit Creator class
    /// </summary>
    class Factory : Creator
    {

        /// <summary>
        /// Checking the shape type and returning it
        /// </summary>
        /// <param name="ShapeType">Shape parameter</param>
        /// <returns>Shape type of the object</returns>
        public override ShapesInterface getShape(string ShapeType)
        {
            ShapeType = ShapeType.ToLower().Trim();
            if (ShapeType.Equals("circle"))
            {
                return new Circle();
            }
            if (ShapeType.Equals("rectangle"))
            {
                return new Rectangle();
            }
            else if (ShapeType.Equals("triangle"))
            {
                return new Triangle();
            }
            else
            {
                //throw an appropriate exception.
                System.ArgumentException argEx = new System.ArgumentException("Factory error: " + ShapeType + " does not exist currently.");
                throw argEx;
            }
        }
    }
}
