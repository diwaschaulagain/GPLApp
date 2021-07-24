namespace GPLApp
{
    /// <summary>
    /// Abstract class declared to get shapetype
    /// </summary>
    abstract class Creator
    {
        /// <summary>
        /// Method used to pass the shape of object to be drawn
        /// </summary>
        /// <param name="ShapeType">Parameter of shape object</param>
        /// <returns></returns>
        public abstract ShapesInterface getShape(string ShapeType);
    }
}
