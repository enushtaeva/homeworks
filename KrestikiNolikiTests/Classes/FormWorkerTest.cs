using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KrestikiNolikiTests.Classes
{
    public class FormWorkerTest
    {
        public void BuildPlayingFuildTest(Form form, int leftfirstbutton, int topfirstbutton, int widthbutton, int heightbutton, string buttonname, Color color, EventHandler functionToExec, FlatStyle flatstyle, ImageLayout layout)
        {
            TabControl TabCont = new TabControl();
            int[][] ArrayFigures;
            Button tempbutton;
            int number = 0;
            ArrayFigures = new int[3][];
            foreach (Control c in form.Controls)
            {
                if (c is TabControl)
                    TabCont = c as TabControl;

            }
            for (int i = 0; i < 3; i++)
            {
                ArrayFigures[i] = new int[3];
                for (int j = 0; j < 3; j++)
                {
                    ArrayFigures[i][j] = 0;
                    tempbutton = new Button();
                    tempbutton.Top = topfirstbutton + heightbutton * i;
                    tempbutton.Left = leftfirstbutton + widthbutton * j;
                    tempbutton.Width = widthbutton;
                    tempbutton.Height = heightbutton;
                    tempbutton.Name = buttonname + number.ToString();
                    tempbutton.BackColor = color;
                    tempbutton.FlatStyle = flatstyle;
                    tempbutton.Tag = (i + 1).ToString() + (j + 1).ToString();
                    tempbutton.BackgroundImageLayout = layout;
                    tempbutton.Click += functionToExec;
                    TabCont.TabPages[0].Controls.Add(tempbutton);
                    number++;
                }
            }

        }
    }
}
