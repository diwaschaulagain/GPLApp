using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace GPLApp
{
    /// <summary>
    /// Classs which validates the user input and gives proper error response
    /// </summary>
    public class ProgramValidation
    {
        private TextBox txtCommand;
        public Boolean isValidCommand = true;
        public Boolean isSomethingInvalid = false;
        public int Radius = 0;
        public int Width = 0;
        public int Height = 0;
        public int counter = 0;
        public int LoopCounter = 0;
        public int lineNumber = 0;

        public Boolean hasLoop = false;
        public Boolean hasEndLoop = false;
        public Boolean hasIf = false;
        public Boolean hasEndif = false;

        public int loopLineNo = 0, endLoopLineNo = 0, ifLineNo = 0, endIfLineNo = 0;
        public ProgramValidation(TextBox txtCommand)
        {
            this.txtCommand = txtCommand;

            int numberOfLines = txtCommand.Lines.Length;
            for (int i = 0; i < numberOfLines; i++)
            {
                String oneLineCommand = txtCommand.Lines[i];
                oneLineCommand = oneLineCommand.Trim();
                if (!oneLineCommand.Equals(""))
                {
                    checkLineValidation(oneLineCommand);
                    lineNumber = (i + 1);
                    if (!isValidCommand)
                    {
                        MessageBox.Show("Error in line " + lineNumber);
                        isValidCommand = true;
                    }
                }

            }
            checkLoopAndIfValidation();
            if (!isValidCommand)
            {
                isSomethingInvalid = true;
            }
        }
        /// <summary>
        /// This method validates loop and if endif statement in program
        /// </summary>
        public void checkLoopAndIfValidation()
        {
            int numberOfLines = txtCommand.Lines.Length;


            for (int i = 0; i < numberOfLines; i++)
            {
                String oneLineCommand = txtCommand.Lines[i];
                oneLineCommand = oneLineCommand.Trim();
                if (!oneLineCommand.Equals(""))
                {
                    hasLoop = Regex.IsMatch(oneLineCommand.ToLower(), @"\bloop\b");
                    if (hasLoop)
                    {
                        loopLineNo = (i + 1);
                    }
                    hasEndLoop = oneLineCommand.ToLower().Contains("endloop");
                    if (hasEndLoop)
                    {
                        endLoopLineNo = (i + 1);
                    }
                    hasIf = Regex.IsMatch(oneLineCommand.ToLower(), @"\bif\b");
                    if (hasIf)
                    {
                        ifLineNo = (i + 1);
                    }
                    hasEndif = oneLineCommand.ToLower().Contains("endif");
                    if (hasEndif)
                    {
                        endIfLineNo = (i + 1);
                    }
                }
            }
            if (loopLineNo > 0)
            {
                hasLoop = true;
            }
            if (endLoopLineNo > 0)
            {
                hasEndLoop = true;
            }
            if (ifLineNo > 0)
            {
                hasIf = true;
            }
            if (endIfLineNo > 0)
            {
                hasEndif = true;
            }
            if (hasLoop)
            {
                if (hasEndLoop)
                {
                    if (loopLineNo < endLoopLineNo)
                    {

                    }
                    else
                    {
                        isValidCommand = false;
                        MessageBox.Show("'ENDLOOP' must be after loop start");
                    }
                }
                else
                {
                    isValidCommand = false;
                    MessageBox.Show("Loop Not Ended with 'ENDLOOP'");
                }
            }
            if (hasIf)
            {
                if (hasEndif)
                {
                    if (ifLineNo < endIfLineNo)
                    {

                    }
                    else
                    {
                        isValidCommand = false;
                        MessageBox.Show("'ENDIF' must be after IF");
                    }
                }
                else
                {
                    isValidCommand = false;
                    MessageBox.Show("IF Not Ended with 'ENDIF'");
                }
            }
        }


        /// <summary>
        /// This method checks the line command in program like moveto,drawto etc.
        /// </summary>
        /// <param name="lineOfCommand"></param>
        public void checkLineValidation(string lineOfCommand)
        {
            String[] keyword = { "circle", "rectangle", "triangle", "drawto", "moveto", "repeat", "if", "endif", "loop", "endloop" };
            String[] shapes = { "circle", "rectangle", "triangle"};
            String[] variable = { "radius", "width", "height", "counter", "size" };
            lineOfCommand = Regex.Replace(lineOfCommand, @"\s+", " ");
            string[] cmd = lineOfCommand.Split(' ');
            // for removing the white spaces in between cmd
            for (int i = 0; i < cmd.Length; i++)
            {
                cmd[i] = cmd[i].Trim();
            }
            String firstWord = cmd[0].ToLower();
            Boolean firstWordIsKeyword = keyword.Contains(firstWord);
            if (firstWordIsKeyword)
            {
                Boolean firstWordIsShape = shapes.Contains(cmd[0].ToLower());
                if (firstWordIsShape)
                {
                    if (cmd[0].ToLower().Equals("circle"))
                    {
                        if (cmd.Length == 2)
                        {
                            Boolean isInt = cmd[1].All(char.IsDigit);
                            if (!isInt)
                            {
                                //if it isnot variable then invalid
                                Boolean isVariable = variable.Contains(cmd[1].ToLower());
                                if (isVariable)
                                {
                                    checkIfVariableDefined(cmd[1]);
                                }
                                else
                                {
                                    isValidCommand = false;
                                }
                                

                            }
                        }
                        else
                        {
                            isValidCommand = false;
                        }
                    }
                    else if (cmd[0].ToLower().Equals("rectangle"))
                    {
                        String args = lineOfCommand.Substring(9, (lineOfCommand.Length - 9));
                        String[] parms = args.Split(',');

                        if (parms.Length == 2)
                        {
                            Boolean isInt = false;
                            for (int i = 0; i < parms.Length; i++)
                            {
                                parms[i] = parms[i].Trim();
                                isInt = parms[i].All(char.IsDigit);
                                if (!isInt)
                                {
                                    //if it isnot variable then invalid
                                    Boolean isVariable = variable.Contains(parms[i].ToLower());
                                    if (!isVariable)
                                    {
                                        isValidCommand = false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            isValidCommand = false;
                        }
                    }
                    else if (cmd[0].ToLower().Equals("triangle"))
                    {
                        String args = lineOfCommand.Substring(8, (lineOfCommand.Length - 8));
                        String[] parms = args.Split(',');

                        if (parms.Length == 3)
                        {
                            Boolean isInt = false;
                            for (int i = 0; i < parms.Length; i++)
                            {
                                parms[i] = parms[i].Trim();
                                isInt = parms[i].All(char.IsDigit);
                                if (!isInt)
                                {
                                    isValidCommand = false;
                                }
                            }
                        }
                        else
                        {
                            isValidCommand = false;
                        }
                    }
                    
                   
                }
                else if (firstWord.Equals("loop"))
                {
                    if (cmd.Length == 2)
                    {
                        Boolean isInt = cmd[1].All(char.IsDigit);
                        if (!isInt)
                        {
                            isValidCommand = false;
                        }
                    }
                    else
                    {
                        isValidCommand = false;
                    }
                }
                else if (firstWord.Equals("end"))
                {
                    if (cmd.Length == 2)
                    {
                        if (!cmd[1].Equals("loop"))
                        {
                            isValidCommand = false;
                        }
                    }
                    else
                    {
                        isValidCommand = false;
                    }
                }
                else if (firstWord.Equals("repeat"))
                {
                    if (cmd.Length >= 4 && cmd.Length <= 6)
                    {
                        Boolean isInt = cmd[1].All(char.IsDigit);
                        if (isInt)
                        {
                            if (shapes.Contains(cmd[2].ToLower()))
                            {

                                Boolean hasPlus = cmd[3].Contains('+');
                                if (hasPlus)
                                {
                                    string[] cmd2 = cmd[3].Split('+');
                                    for (int i = 0; i < cmd2.Length; i++)
                                    {
                                        cmd2[i] = cmd2[i].Trim();
                                    }
                                    Boolean firstWordIsVariable = variable.Contains(cmd2[0].ToLower());
                                    if (!firstWordIsVariable)
                                    {
                                        isValidCommand = false;
                                    }
                                    else
                                    {
                                        if (cmd2.Length != 2)
                                        {
                                            isValidCommand = false;
                                        }
                                        else
                                        {
                                            //third char should be int to be valid
                                            Boolean isInt2 = cmd2[1].All(char.IsDigit);
                                            if (!isInt2)
                                            {
                                                isValidCommand = false;
                                            }
                                        }
                                    }

                                }
                                else
                                {
                                    if (variable.Contains(cmd[3].ToLower()))
                                    {
                                        if (cmd[4].Trim().Equals("+"))
                                        {
                                            Boolean isInt3 = cmd[5].All(char.IsDigit);
                                            if (!isInt3)
                                            {
                                                isValidCommand = false;
                                            }
                                        }
                                        else
                                        {
                                            Boolean hasPlus2 = cmd[4].Contains('+');
                                            if (hasPlus2)
                                            {
                                                string[] cmd2 = cmd[4].Split('+');
                                                for (int i = 0; i < cmd2.Length; i++)
                                                {
                                                    cmd2[i] = cmd2[i].Trim();
                                                }
                                                if (cmd2.Length == 2)
                                                {
                                                    Boolean isInt2 = cmd2[1].All(char.IsDigit);
                                                    if (!isInt2)
                                                    {
                                                        isValidCommand = false;
                                                    }
                                                }
                                                else
                                                {
                                                    isValidCommand = false;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                    }
                    else
                    {
                        isValidCommand = false;
                    }
                }
                else if (firstWord.Equals("if"))
                {
                    if (cmd.Length == 5)
                    {
                        if (variable.Contains(cmd[1].ToLower()))
                        {
                            if (cmd[2].Equals("="))
                            {
                                Boolean isInt = cmd[3].All(char.IsDigit);
                                if (isInt)
                                {
                                    if (cmd[4].ToLower().Equals("then"))
                                    {

                                    }
                                    else { isValidCommand = false; }
                                }
                                else { isValidCommand = false; }

                            }
                            else { isValidCommand = false; }
                        }
                        else { isValidCommand = false; }

                    }
                    else
                    {
                        isValidCommand = false;
                    }

                }
                else if (firstWord.Equals("endif"))
                {
                    if (cmd.Length != 1)
                    {
                        isValidCommand = false;
                    }
                }
                else if (firstWord.Equals("drawto") || firstWord.Equals("moveto"))
                {
                    String args = lineOfCommand.Substring(6, (lineOfCommand.Length - 6));
                    String[] parms = args.Split(',');

                    if (parms.Length == 2)
                    {
                        Boolean isInt = false;
                        for (int i = 0; i < parms.Length; i++)
                        {
                            parms[i] = parms[i].Trim();
                            isInt = parms[i].All(char.IsDigit);
                            if (!isInt)
                            {
                                isValidCommand = false;
                            }
                        }
                    }
                    else
                    {
                        isValidCommand = false;
                    }
                }
            }
            else
            {
                Boolean hasPlus = lineOfCommand.Contains('+');
                Boolean hasEquals = lineOfCommand.Contains("=");
                if (!hasEquals && !hasPlus)
                {
                    isValidCommand = false;
                }
                else
                {
                    if (hasEquals)
                    {
                        string[] cmd2 = lineOfCommand.Split('=');
                        for (int i = 0; i < cmd2.Length; i++)
                        {
                            cmd2[i] = cmd2[i].Trim();
                        }
                        Boolean firstWordIsVariable = variable.Contains(cmd2[0].ToLower());
                        if (!firstWordIsVariable)
                        {
                            isValidCommand = false;
                        }
                        else
                        {
                            if (cmd2.Length != 2)
                            {
                                isValidCommand = false;
                            }
                            else
                            {
                                //third char should be int to be valid                        
                                Boolean isInt = cmd2[1].All(char.IsDigit);
                                if (!isInt)
                                {
                                    isValidCommand = false;
                                }
                            }
                        }

                    }
                    if (hasPlus)
                    {
                        string[] cmd2 = lineOfCommand.Split('+');
                        for (int i = 0; i < cmd2.Length; i++)
                        {
                            cmd2[i] = cmd2[i].Trim();
                        }
                        Boolean firstWordIsVariable = variable.Contains(cmd2[0].ToLower());
                        if (!firstWordIsVariable)
                        {
                            isValidCommand = false;
                        }
                        else
                        {
                            if (cmd2.Length != 2)
                            {
                                isValidCommand = false;
                            }
                            else
                            {
                                //third char should be int to be valid
                                Boolean isInt = cmd2[1].All(char.IsDigit);
                                if (!isInt)
                                {
                                    isValidCommand = false;
                                }
                            }
                        }

                    }
                }
            }
            if (!isValidCommand)
            {
                isSomethingInvalid = true;
            }

        }
        /// <summary>
        /// Check whether the variable has been defined in the program or not
        /// </summary>
        /// <param name="variable"></param>
        public void checkIfVariableDefined(string variable)
        {
            Boolean isVaraibleFound = false;
            if (txtCommand.Lines.Length > 1)
            {
                if (lineNumber > 0)
                {
                    for (int i = 0; i < lineNumber; i++)
                    {
                        String oneLineCommand = txtCommand.Lines[i];
                        oneLineCommand = oneLineCommand.Trim();
                        if (!oneLineCommand.Equals(""))
                        {
                            Boolean isVariableDefined = oneLineCommand.ToLower().Contains(variable.ToLower());
                            if (isVariableDefined)
                            {
                                isVaraibleFound = true;
                            }
                        }

                    }
                    if (!isVaraibleFound)
                    {
                        MessageBox.Show("Variable not defined");
                        isValidCommand = false;
                    }
                }
                else
                {
                    MessageBox.Show("Variable not defined");
                    isValidCommand = false;
                }

            }
            else
            {
                MessageBox.Show("Variable not defined");
                isValidCommand = false;
            }
        }

    }

}
