using System;
using System.Drawing;
using System.Windows.Forms;

namespace GPLApp
{
    public class Triangle : ShapesInterface
    {
        /// <summary>
        /// Triangle sides
        /// </summary>
        public int xi1, yi1, xi2, yi2, xii1, yii1, xii2, yii2, xiii1, yiii1, xiii2, yiii2;


        /// <summary>
        /// Method to draw triangle in panelbox
        /// </summary>
        /// <param name="g"></param>
        public void Draw(Graphics g)
        {
            try
            {
                Pen p = new Pen(Color.Black, 2);
                g.DrawLine(p, xi1, yi1, xi2, yi2);
                g.DrawLine(p, xii1, yii1, xii2, yii2);
                g.DrawLine(p, xiii1, yiii1, xiii2, yiii2);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error: " + ex.Message);
            }
        }


        /// <summary>
        /// Method which takes all the axis points and sets its value
        /// </summary>
        /// <param name="list"></param>
        public void Set(params int[] list)
        {
            this.xi1 = list[0];
            this.yi1 = list[1];
            this.xi2 = list[2];
            this.yi2 = list[3];

            this.xii1 = list[4];
            this.yii1 = list[5];
            this.xii2 = list[6];
            this.yii2 = list[7];

            this.xiii1 = list[8];
            this.yiii1 = list[9];
            this.xiii2 = list[10];
            this.yiii2 = list[11];
        }
    }
}
