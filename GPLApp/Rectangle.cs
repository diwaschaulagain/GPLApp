using System;
using System.Drawing;
using System.Windows.Forms;

namespace GPLApp
{
    /// <summary>
    /// Class is defined as rectangle which inherits from ShapesInterface
    /// </summary>
    public class Rectangle : ShapesInterface
    {
        /// <summary>
        /// The value of x and y axis in panelbox with height and width of rectangle to be drawn
        /// </summary>
        public int x, y, width, height;
       public Color newcolor;


        /// <summary>
        /// provides width and height of rectangle
        /// </summary>
        public Rectangle() : base()
        {
            width = 0;
            height = 0;
        }

        public Rectangle(int x, int y, int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        /// <summary>
        /// method used to draw the shape in panelbox
        /// </summary>
        /// <param name="g"></param>
        public void Draw(Graphics g)
        {
            try
            {
                Pen p = new Pen(Color.Black, 2);
                g.DrawRectangle(p, x - (width / 2), y - (height / 2), width * 2, height * 2);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error: " + ex.Message);
            }
        }


        /// <summary>
        /// Method which sets the value of x, y axis along with height and width of rectangle
        /// </summary>
        /// <param name="list"></param>
       public void Set(params int[] list)
        {
            try
            {
                this.x = list[0];
                this.y = list[1];
                this.width = list[2];
                this.height = list[3];
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public void PenColor (Color color)
        {
            this.newcolor = color;
        }
    }
}
