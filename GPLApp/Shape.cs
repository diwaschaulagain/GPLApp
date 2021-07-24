using System.Drawing;

namespace GPLApp
{
    abstract class Shape : ShapesInterface
    {
        protected Color color;
        protected int x, y;
       

        public Shape()
        {
           color = Color.Red;
        }
        public Shape(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Method which will be inherited by shapes classes to draw object 
        /// </summary>
        /// <param name="g"></param>
        public abstract void Draw(Graphics g);


        /// <summary>
        /// sets the value of x,y axis along with height and width
        /// </summary>
        /// <param name="list"></param>
        public virtual void Set(params int[] list)
        {
            this.x = list[0];
            this.y = list[1];
        }

    }
}
