using System;
using System.Drawing;
using System.Windows.Forms;

namespace GPLApp
{
    /// <summary>
    /// Holds the program commands of circle
    /// </summary>
    public class Circle : ShapesInterface
    {
        /// <summary>
        /// Getting values of x, y position and radius of cirlce
        /// </summary>
        public int x, y, radius;

        public Circle() : base()
        {
        }

        /// <summary>
        /// Used to pass the value of position x,y and radius for circle
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="radius"></param>
        public Circle(int x, int y, int radius)
        {
            this.radius = radius;
        }

        /// <summary>
        /// Method to draw the circle in panelbox
        /// </summary>
        /// <param name="g"></param>
       public void Draw(Graphics g)
        {
            try
            {
                Pen p = new Pen(Color.Black, 2);
                g.DrawEllipse(p, x - radius, y - radius, radius * 2, radius * 2);
            }
            catch (Exception ex)
            {

                //throw ex;
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        /// <summary>
        /// Method for setting value of position x,y and radius of circle
        /// </summary>
        /// <param name="list"></param>
       public void Set(params int[] list)
        {
            try
            {
                this.x = list[0];
                this.y = list[1];
                this.radius = list[2];
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
