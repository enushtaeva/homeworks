using Krestiki_Noliki.Classes.Exceptins;
using ClassLibrary1;
using Krestiki_Noliki.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Krestiki_Noliki.Classes.Statistics;
using Newtonsoft.Json;
using System.Net;
using System.Collections.Specialized;
using Krestiki_Noliki.Classes.Server.Classes;
using Krestiki_Noliki.Classes.Server.Interfaces;

namespace Krestiki_Noliki.Classes
{
    public class FormWorker :Worker
    {
        public TabControl TabCont { get; set; }
        public FormWorker(List<Button> buttons, bool krestik, int size, IGameWorker worker)
        {
            this.Buttons = buttons;
            this.Krestik = krestik;
            this.Size = size;
            this.GameWorker = worker;
        }

        public FormWorker(IGameWorker worker,IXmlWorker<Statistic> xmlworker,IServerWorker<Statistic> serverWorker)
        {
            this.GameWorker = worker;
            this.XmlWorker = xmlworker;
            this.ServerWorker = serverWorker;
        }

        public override void BuildPlayingFuild(Form form, int leftfirstbutton, int topfirstbutton, int widthbutton, int heightbutton,string buttonname, Color color, EventHandler functionToExec, FlatStyle flatstyle, ImageLayout layout)
        {
            
            List<Statistic> statistics =XmlWorker.GetData(@"..\..\Classes\Statistics\StatisticFile\pom.xml");
            Button tempbutton;
            int number = 0;

            if (statistics.Count < 2)
            {
                if (statistics.Where(a => a.Login == "Пользователь").Count() == 0)
                    statistics.Add(new Statistic() { Login = "Пользователь", Win = 0, Won = 0, NF = 0 });
                if (statistics.Where(a => a.Login == "Компьютер").Count() == 0)
                    statistics.Add(new Statistic() { Login = "Компьютер", Win = 0, Won = 0, NF = 0 });
                XmlWorker.WriteData(@"..\..\Classes\Statistics\StatisticFile\pom.xml", statistics);
            }
             ChangeDataSource(form, statistics);
             foreach(Control c in form.Controls)
             {
                   if (c is TabControl) 
                   TabCont = c as TabControl;
                    
             }
            this.ArrayFigures = new int[this.Size][];

            foreach (Button b in this.Buttons)
            {
                TabCont.TabPages[0].Controls.Remove(b);
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
                    TabCont.TabPages[0].Controls.Add(tempbutton);
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
                    if(b.Tag.ToString()[0].ToString()==(i+1).ToString() && (b.Tag.ToString()[1].ToString() == (j + 1).ToString()))
                    {
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
            foreach(Control c in form.Controls)
            {
                   if (!(c is TabControl)) continue;
                   TabCont = c as TabControl;
            }
            int k = 0;
            foreach (Control c in TabCont.TabPages[0].Controls)
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
                    Valide(form,k);
                    if (this.Start)
                    {
                         StepOfComputer(form, c.Tag.ToString(), krestik);
                         k = this.GameWorker.ValideWinOrWon(form, this.ArrayFigures, krestik, this.Size, this.KrestikValue, this.NolikValue);
                         Valide(form,k);
                    }
                            
                  }
            }
 
            
        }

        public override void StepOfComputer(Form form, string buttontag, bool krestik)
        {
            string step = this.GameWorker.StepOfComputer(form, buttontag, this.ArrayFigures, this.Krestik, this.Size, this.KrestikValue, this.NolikValue);
            if (step != "")
            {
                foreach (Control c in form.Controls)
                {
                    if (!(c is TabControl)) continue;
                    TabCont = c as TabControl;
                }
                foreach (Control c in TabCont.TabPages[0].Controls)
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
        public override void GetDataFromServer(Form form)
        {
                List<Statistic> stats = ServerWorker.GetData("http://localhost:17736/Home/GetData");
                XmlWorker.WriteData(@"..\..\Classes\Statistics\StatisticFile\pom.xml", stats);
                form.Invoke((MethodInvoker)(() => ChangeDataSource(form, XmlWorker.GetData(@"..\..\Classes\Statistics\StatisticFile\pom.xml"))));
        }

        #region PrivateSection
        private void Valide(Form form,int k)
        {
            List<Statistic> statistics = XmlWorker.GetData(@"..\..\Classes\Statistics\StatisticFile\pom.xml");
            Statistic temp;
            switch (k)
            {
                case 1:
                    MessageBox.Show("Ура, победа!\nВы победили!", "Победа");
                    temp=statistics.Where(a => a.Login == "Пользователь").First();
                    temp.Win = temp.Win + 1;
                    temp = statistics.Where(a => a.Login == "Компьютер").First();
                    temp.Won = temp.Won + 1;
                    XmlWorker.WriteData(@"..\..\Classes\Statistics\StatisticFile\pom.xml", statistics);
                    ChangeDataSource(form, statistics);
                    ServerWorker.PostDataAboutFinish("http://localhost:17736/Home/GetData", new ServerObject() { Login1 = "Пользователь", Login2 = "Компьютер", Kod = 0 });
                    this.Start = false;
                    break;
                case 2:
                    MessageBox.Show("Проигрыш!\nВы проиграли!", "Проигрыш");
                    temp = statistics.Where(a => a.Login == "Компьютер").First();
                    temp.Win = temp.Win + 1;
                    temp = statistics.Where(a => a.Login == "Пользователь").First();
                    temp.Won = temp.Won + 1;
                    XmlWorker.WriteData(@"..\..\Classes\Statistics\StatisticFile\pom.xml", statistics);
                    ChangeDataSource(form, statistics);
                    ServerWorker.PostDataAboutFinish("http://localhost:17736/Home/GetData", new ServerObject() { Login1 = "Пользователь", Login2 = "Компьютер", Kod = 1 });
                    this.Start = false;
                    break;
                case 3:
                    MessageBox.Show("Ничья!", "Ничья");
                    temp = statistics.Where(a => a.Login == "Компьютер").First();
                    temp.NF = temp.NF + 1;
                    temp = statistics.Where(a => a.Login == "Пользователь").First();
                    temp.NF = temp.NF + 1;
                    XmlWorker.WriteData(@"..\..\Classes\Statistics\StatisticFile\pom.xml", statistics);
                    ChangeDataSource(form, statistics);
                    ServerWorker.PostDataAboutFinish("http://localhost:17736/Home/GetData", new ServerObject() { Login1 = "Пользователь", Login2 = "Компьютер", Kod = 2 });
                    this.Start = false;
                    break;
                default:
                    break;
            }

            
        }

        private void ChangeDataSource(Form form, List<Statistic> statistics)
        {
            DataGridView temp = new DataGridView();
            Dictionary<string, string> dict = new Dictionary<string, string>() { { "Login", "Логин" }, { "Win", "Победы" }, { "Won", "Поражения" }, { "NF", "Ничьи" } };
            foreach (Control c in form.Controls)
            {
                if (!(c is TabControl)) continue;
                TabCont = c as TabControl;
            }
            foreach (Control c in TabCont.TabPages[1].Controls)
            {
                if (c is DataGridView)
                    temp = c as DataGridView;
            }
            temp.DataSource = statistics;
            temp.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            temp.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            foreach (DataGridViewColumn col in temp.Columns)
            {
                col.Width = 173;
                try {
                    col.HeaderText = dict[col.HeaderText];
                }
                catch
                {

                }
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
