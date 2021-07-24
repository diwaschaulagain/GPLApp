using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace GPLApp
{
    /// <summary>
    /// Main form class of the application
    /// </summary>
    public partial class MainForm : Form
    {
        Graphics g;
        ProgramValidation validate;
        public MainForm()
        {
            InitializeComponent();
            g = DisplayPnl.CreateGraphics();
        }

        Creator factory = new Factory();
        Pen myPen = new Pen(Color.Red);


        public Color newcolor;
        int x = 0, y = 0;
        int loopCounter;
        public int counter = 0;
        public int dSize = 0;
        public int radius = 0;
        public int width = 0;
        public int height = 0;

        /// <summary>
        /// This takes the execution command for application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_Exec_Cmd_TextChanged(object sender, EventArgs e)
        {

            if (txt_Exec_Cmd.Text.ToLower().Trim() == "run")
            {
                if (txt_Cmd_Box.Text != null && txt_Cmd_Box.Text != "")
                {
                    validate = new ProgramValidation(txt_Cmd_Box);

                    if (!validate.isSomethingInvalid)
                    {

                        loadCommand();
                    }
                }


            }
            else
            {
                if (txt_Exec_Cmd.Text.ToLower().Trim() == "clear")
                {
                    DisplayPnl.Invalidate();

                }
                else if (txt_Exec_Cmd.Text.ToLower().Trim() == "reset")
                {
                    txt_Cmd_Box.Clear();
                }
            }
        }

        /// <summary>
        /// This method loads the command and calls RunCommand further
        /// </summary>
        private void loadCommand()
        {
            Graphics g = DisplayPnl.CreateGraphics();
            string command = txt_Cmd_Box.Text.ToLower();
            string[] commandline = command.Split(new String[] { "\n" },

            StringSplitOptions.RemoveEmptyEntries);
            int numberOfLines = txt_Cmd_Box.Lines.Length;
            for (int k = 0; k < commandline.Length; k++)
            {
                string[] cmd = commandline[k].Split(' ');
                if (cmd[0].Equals("moveto") == true)
                {
                    DisplayPnl.Refresh();
                    string[] param = cmd[1].Split(',');
                    if (param.Length != 2)
                    { MessageBox.Show("Incorrect Parameter"); }
                    else
                    {
                        Int32.TryParse(param[0], out x);
                        Int32.TryParse(param[1], out y);
                        moveTo(x, y);
                    }

                }

                for (loopCounter = 0; loopCounter < numberOfLines; loopCounter++)
                {
                    String oneLineCommand = txt_Cmd_Box.Lines[loopCounter];
                    oneLineCommand = oneLineCommand.Trim();
                    if (!oneLineCommand.Equals(""))
                    {
                        RunCommand(oneLineCommand);
                    }

                }
            }
        }

        /// <summary>
        /// This method runs the program and further calls the draw command to finally draw object in panel
        /// </summary>
        /// <param name="oneLineCommand"></param>

        private void RunCommand(String oneLineCommand)
        {
            Boolean hasPlus = oneLineCommand.Contains("+");
            Boolean hasEquals = oneLineCommand.Contains("=");
            if (hasEquals)
            {
                oneLineCommand = Regex.Replace(oneLineCommand, @"\s+", " ");
                string[] cmd = oneLineCommand.Split(' ');
                //removing white spaces in between cmd
                for (int i = 0; i < cmd.Length; i++)
                {
                    cmd[i] = cmd[i].Trim();
                }
                String firstWord = cmd[0].ToLower();
                if (firstWord.Equals("if"))
                {
                    Boolean loop = false;
                    if (cmd[1].ToLower().Equals("radius"))
                    {
                        if (radius == int.Parse(cmd[3]))
                        {
                            loop = true;
                        }
                    }
                    else if (cmd[1].ToLower().Equals("width"))
                    {
                        if (width == int.Parse(cmd[3]))
                        {
                            loop = true;
                        }
                    }
                    else if (cmd[1].ToLower().Equals("height"))
                    {
                        if (height == int.Parse(cmd[3]))
                        {
                            loop = true;
                        }

                    }
                    else if (cmd[1].ToLower().Equals("counter"))
                    {
                        if (counter == int.Parse(cmd[3]))
                        {
                            loop = true;
                        }
                    }
                    int ifStartLine = (GetIfStartLineNumber());
                    int ifEndLine = (GetEndifEndLineNumber() - 1);
                    loopCounter = ifEndLine;
                    if (loop)
                    {
                        for (int j = ifStartLine; j <= ifEndLine; j++)
                        {
                            string oneLineCommand1 = txt_Cmd_Box.Lines[j];
                            oneLineCommand1 = oneLineCommand1.Trim();
                            if (!oneLineCommand1.Equals(""))
                            {
                                RunCommand(oneLineCommand1);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("If Statement is false");
                    }
                }
                else
                {
                    string[] cmd2 = oneLineCommand.Split('=');
                    for (int j = 0; j < cmd2.Length; j++)
                    {
                        cmd2[j] = cmd2[j].Trim();
                    }
                    if (cmd2[0].ToLower().Equals("radius"))
                    {
                        radius = int.Parse(cmd2[1]);
                    }
                    else if (cmd2[0].ToLower().Equals("width"))
                    {
                        width = int.Parse(cmd2[1]);
                    }
                    else if (cmd2[0].ToLower().Equals("height"))
                    {
                        height = int.Parse(cmd2[1]);
                    }
                    else if (cmd2[0].ToLower().Equals("counter"))
                    {
                        counter = int.Parse(cmd2[1]);
                    }
                }
            }
            else if (hasPlus)
            {
                oneLineCommand = System.Text.RegularExpressions.Regex.Replace(oneLineCommand, @"\s+", " ");
                string[] cmd = oneLineCommand.Split(' ');
                if (cmd[0].ToLower().Equals("repeat"))
                {
                    counter = int.Parse(cmd[1]);
                    if (cmd[2].ToLower().Equals("circle"))
                    {
                        int increaseValue = GetSize(oneLineCommand);
                        radius = increaseValue;
                        for (int j = 0; j < counter; j++)
                        {
                            DrawCircle(radius);
                            radius += increaseValue;
                        }
                    }
                    else if (cmd[2].ToLower().Equals("rectangle"))
                    {
                        int increaseValue = GetSize(oneLineCommand);
                        dSize = increaseValue;
                        for (int j = 0; j < counter; j++)
                        {
                            DrawRectangle(dSize, dSize);
                            dSize += increaseValue;
                        }
                    }
                    else if (cmd[2].ToLower().Equals("triangle"))
                    {
                        int increaseValue = GetSize(oneLineCommand);
                        dSize = increaseValue;
                        for (int j = 0; j < counter; j++)
                        {
                            DrawTriangle(dSize, dSize, dSize);
                            dSize += increaseValue;
                        }
                    }
                }
                else
                {
                    string[] cmd2 = oneLineCommand.Split('+');
                    for (int j = 0; j < cmd2.Length; j++)
                    {
                        cmd2[j] = cmd2[j].Trim();
                    }
                    if (cmd2[0].ToLower().Equals("radius"))
                    {
                        radius += int.Parse(cmd2[1]);
                    }
                    else if (cmd2[0].ToLower().Equals("width"))
                    {
                        width += int.Parse(cmd2[1]);
                    }
                    else if (cmd2[0].ToLower().Equals("height"))
                    {
                        height += int.Parse(cmd2[1]);
                    }
                }
            }
            else
            {
                sendDrawCommand(oneLineCommand);
            }


        }


        /// <summary>
        /// This method returns the size of structure
        /// </summary>
        /// <param name="lineCommand"></param>
        /// <returns>the value of the size</returns>
        private int GetSize(string lineCommand)
        {
            int value = 0;
            if (lineCommand.ToLower().Contains("radius"))
            {
                int pos = (lineCommand.IndexOf("radius") + 6);
                int size = lineCommand.Length;
                String tempLine = lineCommand.Substring(pos, (size - pos));
                tempLine = tempLine.Trim();
                String newTempLine = tempLine.Substring(1, (tempLine.Length - 1));
                newTempLine = newTempLine.Trim();
                value = int.Parse(newTempLine);

            }
            else if (lineCommand.ToLower().Contains("size"))
            {
                int pos = (lineCommand.IndexOf("size") + 4);
                int size = lineCommand.Length;
                String tempLine = lineCommand.Substring(pos, (size - pos));
                tempLine = tempLine.Trim();
                String newTempLine = tempLine.Substring(1, (tempLine.Length - 1));
                newTempLine = newTempLine.Trim();
                value = int.Parse(newTempLine);
            }
            return value;
        }


        /// <summary>
        /// Initiate shape and finally draw it in user passed command
        /// </summary>
        /// <param name="lineOfCommand"></param>
        private void sendDrawCommand(string lineOfCommand)
        {
            String[] shapes = { "circle", "rectangle", "triangle" };
            String[] variable = { "radius", "width", "height", "counter", "size" };

            lineOfCommand = System.Text.RegularExpressions.Regex.Replace(lineOfCommand, @"\s+", " ");
            string[] cmd = lineOfCommand.Split(' ');
            //removing white spaces in between cmd
            for (int i = 0; i < cmd.Length; i++)
            {
                cmd[i] = cmd[i].Trim();
            }
            String firstWord = cmd[0].ToLower();
            Boolean firstcmdhape = shapes.Contains(firstWord);
            if (firstcmdhape)
            {

                if (firstWord.Equals("circle"))
                {
                    Boolean secondWordIsVariable = variable.Contains(cmd[1].ToLower());
                    if (secondWordIsVariable)
                    {
                        if (cmd[1].ToLower().Equals("radius"))
                        {
                            DrawCircle(radius);
                        }
                    }
                    else
                    {
                        DrawCircle(Int32.Parse(cmd[1]));
                    }

                }
                else if (firstWord.Equals("rectangle"))
                {
                    String args = lineOfCommand.Substring(9, (lineOfCommand.Length - 9));
                    String[] parms = args.Split(',');
                    for (int i = 0; i < parms.Length; i++)
                    {
                        parms[i] = parms[i].Trim();
                    }
                    Boolean secondWordIsVariable = variable.Contains(parms[0].ToLower());
                    Boolean thirdWordIsVariable = variable.Contains(parms[1].ToLower());
                    if (secondWordIsVariable)
                    {
                        if (thirdWordIsVariable)
                        {
                            DrawRectangle(width, height);
                        }
                        else
                        {
                            DrawRectangle(width, Int32.Parse(parms[1]));
                        }

                    }
                    else
                    {
                        if (thirdWordIsVariable)
                        {
                            DrawRectangle(Int32.Parse(parms[0]), height);
                        }
                        else
                        {
                            DrawRectangle(Int32.Parse(parms[0]), Int32.Parse(parms[1]));
                        }
                    }
                }
                else if (firstWord.Equals("triangle"))
                {
                    String args = lineOfCommand.Substring(8, (lineOfCommand.Length - 8));
                    String[] parms = args.Split(',');
                    for (int i = 0; i < parms.Length; i++)
                    {
                        parms[i] = parms[i].Trim();
                    }
                    DrawTriangle(Int32.Parse(parms[0]), Int32.Parse(parms[1]), Int32.Parse(parms[2]));
                }

            }
            else
            {
                if (firstWord.Equals("loop"))
                {
                    counter = int.Parse(cmd[1]);
                    int loopStartLine = (GetLoopStartLineNumber());
                    int loopEndLine = (GetLoopEndLineNumber() - 1);
                    loopCounter = loopEndLine;
                    for (int i = 0; i < counter; i++)
                    {
                        for (int j = loopStartLine; j <= loopEndLine; j++)
                        {
                            String oneLineCommand = txt_Cmd_Box.Lines[j];
                            oneLineCommand = oneLineCommand.Trim();
                            if (!oneLineCommand.Equals(""))
                            {
                                RunCommand(oneLineCommand);
                            }
                        }
                    }
                }
                else if (firstWord.Equals("if"))
                {
                    Boolean loop = false;
                    if (cmd[1].ToLower().Equals("radius"))
                    {
                        if (radius == int.Parse(cmd[1]))
                        {
                            loop = true;
                        }
                    }
                    else if (cmd[1].ToLower().Equals("width"))
                    {
                        if (width == int.Parse(cmd[1]))
                        {
                            loop = true;
                        }
                    }
                    else if (cmd[1].ToLower().Equals("height"))
                    {
                        if (height == int.Parse(cmd[1]))
                        {
                            loop = true;
                        }

                    }
                    else if (cmd[1].ToLower().Equals("counter"))
                    {
                        if (counter == int.Parse(cmd[1]))
                        {
                            loop = true;
                        }
                    }
                    int ifStartLine = (GetIfStartLineNumber());
                    int ifEndLine = (GetEndifEndLineNumber() - 1);
                    loopCounter = ifEndLine;
                    if (loop)
                    {
                        for (int j = ifStartLine; j <= ifEndLine; j++)
                        {
                            String oneLineCommand = txt_Cmd_Box.Lines[j];
                            oneLineCommand = oneLineCommand.Trim();
                            if (!oneLineCommand.Equals(""))
                            {
                                RunCommand(oneLineCommand);
                            }
                        }
                    }
                }
            }
        }


        /// <summary>
        /// initiates the if statement
        /// </summary>
        /// <returns>lineNum</returns>
        private int GetIfStartLineNumber()
        {
            int numberOfLines = txt_Cmd_Box.Lines.Length;
            int lineNum = 0;

            for (int i = 0; i < numberOfLines; i++)
            {
                String oneLineCommand = txt_Cmd_Box.Lines[i];
                oneLineCommand = Regex.Replace(oneLineCommand, @"\s+", " ");
                string[] cmd = oneLineCommand.Split(' ');
                //removing white spaces in between cmd
                for (int j = 0; j < cmd.Length; j++)
                {
                    cmd[j] = cmd[j].Trim();
                }
                String firstWord = cmd[0].ToLower();
                oneLineCommand = oneLineCommand.Trim();
                if (firstWord.Equals("if"))
                {
                    lineNum = i + 1;

                }
            }
            return lineNum;
        }


        /// <summary>
        /// ends the endif statement in program
        /// </summary>
        /// <returns></returns>
        private int GetEndifEndLineNumber()
        {
            int numberOfLines = txt_Cmd_Box.Lines.Length;
            int lineNum = 0;

            for (int i = 0; i < numberOfLines; i++)
            {
                String oneLineCommand = txt_Cmd_Box.Lines[i];
                oneLineCommand = oneLineCommand.Trim();
                if (oneLineCommand.ToLower().Equals("endif"))
                {
                    lineNum = i + 1;

                }
            }
            return lineNum;
        }


        /// <summary>
        /// initiatess the for loop in program
        /// </summary>
        /// <returns>lineNum</returns>
        private int GetLoopStartLineNumber()
        {
            int numberOfLines = txt_Cmd_Box.Lines.Length;
            int lineNum = 0;

            for (int i = 0; i < numberOfLines; i++)
            {
                String oneLineCommand = txt_Cmd_Box.Lines[i];
                oneLineCommand = Regex.Replace(oneLineCommand, @"\s+", " ");
                string[] cmd = oneLineCommand.Split(' ');
                //removing white spaces in between cmd
                for (int j = 0; j < cmd.Length; j++)
                {
                    cmd[j] = cmd[j].Trim();
                }
                String firstWord = cmd[0].ToLower();
                oneLineCommand = oneLineCommand.Trim();
                if (firstWord.Equals("loop"))
                {
                    lineNum = i + 1;

                }
            }
            return lineNum;

        }


        /// <summary>
        /// ends the forloop in program
        /// </summary>
        /// <returns>lineNum</returns>
        private int GetLoopEndLineNumber()
        {
            try
            {
                int numberOfLines = txt_Cmd_Box.Lines.Length;
                int lineNum = 0;

                for (int i = 0; i < numberOfLines; i++)
                {
                    String oneLineCommand = txt_Cmd_Box.Lines[i];
                    oneLineCommand = oneLineCommand.Trim();
                    if (oneLineCommand.ToLower().Equals("endloop"))
                    {
                        lineNum = i + 1;

                    }
                }
                return lineNum;
            }
            catch (Exception e)
            {
                return 0;
            }
        }


        /// <summary>
        /// This method is to save the program in user local computer for future access
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "TXT files (*.txt)|*.txt|All files (*.*)|*.*";
            if (save.ShowDialog() == DialogResult.OK)
            {
                StreamWriter write = new StreamWriter(File.Create(save.FileName));
                write.WriteLine(txt_Cmd_Box.Text);
                write.Close();
                MessageBox.Show("File Successfully Save In Your Computer");
            }
        }

        /// <summary>
        /// This method is used to draw rectangle
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        private void DrawRectangle(int width, int height)
        {
            Pen p = new Pen(Color.Black, 2);
            g.DrawRectangle(p, x - (width / 2), y - (height / 2), width * 2, height * 2);
        }

        /// <summary>
        /// Method used while drawing circle
        /// </summary>
        /// <param name="radius"></param>
        private void DrawCircle(int radius)
        {
            Pen p = new Pen(Color.Black, 2);
            g.DrawEllipse(p, x - radius, y - radius, radius * 2, radius * 2);
        }

        /// <summary>
        /// Method to draw triangle in panelbox
        /// </summary>
        /// <param name="rBase"></param>
        /// <param name="adj"></param>
        /// <param name="hyp"></param>
        private void DrawTriangle(int rBase, int adj, int hyp)
        {
            Pen po = new Pen(Color.Black, 2);
            Point[] pnt = new Point[3];

            pnt[0].X = x;
            pnt[0].Y = y;

            pnt[1].X = x - rBase;
            pnt[1].Y = y;

            pnt[2].X = x;
            pnt[2].Y = y - adj;
            g.DrawPolygon(myPen, pnt);
        }

        /// <summary>
        /// Method to import the program from user local program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void browseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Title = "Browse file from specified folder";
            openFileDialog1.InitialDirectory = "E:\\";
            openFileDialog1.Filter = "TXT files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.Filter = "DOCX files (*.docx)|*.docx|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            //Browse .txt file from computer             
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            // Insert code to read the stream here.                        
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                //displays the text inside the file on TextBox named as txtInput                
                txt_Cmd_Box.Text = File.ReadAllText(openFileDialog1.FileName);
            }
        }


        /// <summary>
        /// Method to open the pdf file from user's computer for guideline in application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("file:///E:/GPLHelperFile.pdf");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }

        }

        /// <summary>
        /// Closes the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        /// <summary>
        /// Helps to position the shape in panelbox
        /// </summary>
        /// <param name="toX"></param>
        /// <param name="toY"></param>
        public void moveTo(int toX, int toY)
        {
            x = toX;
            y = toY;
        }


        /// <summary>
        /// Helps to draw the shape in panelbox
        /// </summary>
        /// <param name="toX"></param>
        /// <param name="toY"></param>
        public void drawTo(int toX, int toY)
        {
            x = toX;
            y = toY;
        }

    }
}

