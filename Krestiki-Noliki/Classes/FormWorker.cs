using Krestiki_Noliki.Classes.Exceptins;
using Krestiki_Noliki.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Krestiki_Noliki.Classes
{
    public class FormWorker :Worker
    {
       
        public FormWorker(List<Button> buttons, bool krestik, int size, IGameWorker worker)
        {
            this.Buttons = buttons;
            this.Krestik = krestik;
            this.Size = size;
            this.GameWorker = worker;
        }

        public FormWorker(IGameWorker worker)
        {
            this.GameWorker = worker;
        }

        public override void BuildPlayingFuild(Form form, int leftfirstbutton, int topfirstbutton, int widthbutton, int heightbutton,string buttonname, Color color, EventHandler functionToExec, FlatStyle flatstyle, ImageLayout layout)
        {

            Button tempbutton;
           
            int number = 0;
            this.ArrayFigures = new int[this.Size][];

            foreach (Button b in this.Buttons)
            {
                form.Controls.Remove(b);
            }
            this.Buttons.Clear();
            for (int i = 0; i < this.Size; i++)
            {
                this.ArrayFigures[i] = new int[this.Size];
                for(int j = 0; j < this.Size; j++)
                {
                    this.ArrayFigures[i][j] = 0;
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
                    form.Controls.Add(tempbutton);
                    this.Buttons.Add(tempbutton);
                    number++;
                }
            }
            if (!this.Krestik && this.Start)
            {
                Random rand = new Random();
                int i = rand.Next(0, this.Size - 1),j= rand.Next(0, this.Size - 1);
                this.ArrayFigures[i][j] = this.KrestikValue;
                foreach(Button b in this.Buttons)
                {
                    if(b.Tag.ToString()[0].ToString()==(i+1).ToString() && (b.Tag.ToString()[1].ToString() == (j + 1).ToString())){
                        b.BackgroundImage = (Image)Properties.Resources.krestik;
                    }
                }
            }

        }


        public override void ChangeImage(Button button,bool krestik)
        {
            if (krestik)
            {
                button.BackgroundImage = (Image)Properties.Resources.krestik;

            }
            else
            {
                button.BackgroundImage = (Image)Properties.Resources.nolik;
            }
        }


        public override void Click(Form form,string name,bool krestik) 
        {
           if (!this.Start)
            {
                throw new NotStartException(this.Start);
            }

                   int k = 0;
                
                    foreach (Control c in form.Controls)
                    {
                        if (!(c is Button)) continue;
                        if (c.Name == name)
                        {
                            if ((c as Button).BackgroundImage != null)
                                {
                                    throw new ButtonOccupiedException((c as Button).BackgroundImage);
                                }
                                ChangeImage((c as Button), krestik);
                                ChangeFigure(this.Krestik, Convert.ToInt32(c.Tag.ToString()[0].ToString()) - 1, Convert.ToInt32(c.Tag.ToString()[1].ToString()) - 1);
                                k = this.GameWorker.ValideWinOrWon(form, this.ArrayFigures, krestik, this.Size, this.KrestikValue, this.NolikValue);
                                Valide(k);
                                if (this.Start)
                                {
                                    StepOfComputer(form, c.Tag.ToString(), krestik);
                                    k = this.GameWorker.ValideWinOrWon(form, this.ArrayFigures, krestik, this.Size, this.KrestikValue, this.NolikValue);
                                    Valide(k);
                                }
                            
                        }
                    }
 
            
        }

        public override void StepOfComputer(Form form, string buttontag, bool krestik)
        {
            string step = this.GameWorker.StepOfComputer(form, buttontag, this.ArrayFigures, this.Krestik, this.Size, this.KrestikValue, this.NolikValue);
            if (step != "")
            {
                foreach(Control c in form.Controls)
                {
                    if (!(c is Button)) continue;

                    if (c.Tag!=null && c.Tag.ToString() == step)
                    {
                        if (krestik)
                        {
                            (c as Button).BackgroundImage = (Image)Properties.Resources.nolik;
                        }
                        else
                        {
                            (c as Button).BackgroundImage = (Image)Properties.Resources.krestik;
                        }
                    }
                }
            }
        }

        #region PrivateSection
        private void Valide(int k)
        {
            switch (k)
            {
                case 1:
                    MessageBox.Show("Ура, победа!\nВы победили!", "Победа");
                    this.Start = false;
                    break;
                case 2:
                    MessageBox.Show("Проигрыш!\nВы проиграли!", "Проигрыш");
                    this.Start = false;
                    break;
                case 3:
                    MessageBox.Show("Ничья!", "Ничья");
                    this.Start = false;
                    break;
                default:
                    break;
            }
        }


        private void ChangeFigure(bool krestik, int i, int j)
        {
            if (krestik)
            {
                this.ArrayFigures[i][j] = this.KrestikValue;
            }
            else
            {
                this.ArrayFigures[i][j] = this.NolikValue;
            }
            
        }
        #endregion



    }
}
