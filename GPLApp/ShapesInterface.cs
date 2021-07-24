using System.Drawing;
namespace GPLApp
{
    public interface ShapesInterface
    {
        /// <summary>
        /// Interface method to set parameter value
        /// </summary>
        /// <param name="list"></param>
        void Set(params int[] list);

        /// <summary>
        /// Interface method to be inherited by Shapes class and draw the object in panelbox
        /// </summary>
        /// <param name="g"></param>
        void Draw(Graphics g);
    }
}
