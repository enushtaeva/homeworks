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
using System.Threading;

namespace Krestiki_Noliki.Classes
{
    public class FormWorker :Worker
    {
        public TabControl TabCont { get; set; }
        public FormWorker(List<Button> buttons, bool krestik, int size, IGameWorker worker,IXmlWorker<Statistic> xmlworker,IServerWorker<Statistic> serverWorker, IXmlWorker<StatisticOnTask> xmlworkertask, IServerWorker<StatisticOnTask> serverWorkerTask)
        {
            this.Buttons = buttons;
            this.Krestik = krestik;
            this.Size = size;
            this.GameWorker = worker;
            this.XmlWorker = xmlworker;
            this.ServerWorker = serverWorker;
            this.XmlWorkerTask = xmlworkertask;
            this.ServerWorkerTask = serverWorkerTask;
        }

        public FormWorker(IGameWorker worker,IXmlWorker<Statistic> xmlworker,IServerWorker<Statistic> serverWorker, IXmlWorker<StatisticOnTask> xmlworkertask, IServerWorker<StatisticOnTask> serverWorkerTask)
        {
            this.GameWorker = worker;
            this.XmlWorker = xmlworker;
            this.ServerWorker = serverWorker;
            this.XmlWorkerTask = xmlworkertask;
            this.ServerWorkerTask = serverWorkerTask;
        }

        public override void BuildPlayingFuild(Form form, int leftfirstbutton, int topfirstbutton, int widthbutton, int heightbutton,string buttonname, Color color, EventHandler functionToExec, FlatStyle flatstyle, ImageLayout layout)
        {
            this.DateOfStart = DateTime.Now;
            this.CountOfStep = 0;
            List<Statistic> statistics =XmlWorker.GetData(@"..\..\Classes\Statistics\StatisticFile\pom.xml");
            List<StatisticOnTask> statisticstask = XmlWorkerTask.GetData(@"..\..\Classes\Statistics\StatisticFile\pom2.xml");
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
             ChangeDataSource2(form, statisticstask);
            foreach (Control c in form.Controls)
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
                    this.CountOfStep++;
                    k = this.GameWorker.ValideWinOrWon(form, this.ArrayFigures, krestik, this.Size, this.KrestikValue, this.NolikValue);
                    Valide(form,k,krestik);
                    if (this.Start)
                    {
                         StepOfComputer(form, c.Tag.ToString(), krestik);
                         k = this.GameWorker.ValideWinOrWon(form, this.ArrayFigures, krestik, this.Size, this.KrestikValue, this.NolikValue);
                         Valide(form,k,krestik);
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
            try {
                List<Statistic> stats = ServerWorker.GetData("http://localhost:17736/Home/GetData");
                XmlWorker.WriteData(@"..\..\Classes\Statistics\StatisticFile\pom.xml", stats);
                form.Invoke((MethodInvoker)(() => ChangeDataSource(form, XmlWorker.GetData(@"..\..\Classes\Statistics\StatisticFile\pom.xml"))));
            }
            catch(Exception ex)
            {
                try
                {
                    form.Invoke((MethodInvoker)(() => MessageBox.Show(ex.Message, "Error")));
                }
                catch
                {

                }
            }
        }
        public override void GetDataFromServerTask(Form form)
        {
            try { 
            List<StatisticOnTask> stats = ServerWorkerTask.GetData("http://localhost:17736/Home/GetDataTask");
            XmlWorkerTask.WriteData(@"..\..\Classes\Statistics\StatisticFile\pom2.xml", stats);
            form.Invoke((MethodInvoker)(() => ChangeDataSource2(form, XmlWorkerTask.GetData(@"..\..\Classes\Statistics\StatisticFile\pom2.xml"))));
             }
              catch(Exception ex)
            {
                try
                {
                    form.Invoke((MethodInvoker)(() => MessageBox.Show(ex.Message, "Error")));
                }
                catch
                {

                }
            }
        }
        #region PrivateSection
        private void Valide(Form form,int k,bool krestik)
        {
            List<Statistic> statistics = XmlWorker.GetData(@"..\..\Classes\Statistics\StatisticFile\pom.xml");
            Statistic temp;
            switch (k)
            {
                case 1:
                    Thread myThreadMD1 = new Thread(() => LoadStatistic(form, new StatisticOnTask() { DateOfStart = this.DateOfStart, TimeToPlay = DateTime.Now - this.DateOfStart, X = krestik ? 1 : 0, CountOfStep = this.CountOfStep, Result = 0 }));
                     myThreadMD1.Start();
                    MessageBox.Show("Ура, победа!\nВы победили!", "Победа");
                    temp=statistics.Where(a => a.Login == "Пользователь").First();
                    temp.Win = temp.Win + 1;
                    temp = statistics.Where(a => a.Login == "Компьютер").First();
                    temp.Won = temp.Won + 1;
                    XmlWorker.WriteData(@"..\..\Classes\Statistics\StatisticFile\pom.xml", statistics);
                    ChangeDataSource(form, statistics);
                    this.Start = false;
                    ServerWorker.PostDataAboutFinish("http://localhost:17736/Home/WriteData", new ServerObject() { Login1 = "Пользователь", Login2 = "Компьютер", Kod = 0 });
                    break;
                case 2:
                    Thread myThreadMD2 = new Thread(() => LoadStatistic(form, new StatisticOnTask() { DateOfStart = this.DateOfStart, TimeToPlay = DateTime.Now - this.DateOfStart, X = krestik ? 1 : 0, CountOfStep = this.CountOfStep, Result = 1 }));
                    myThreadMD2.Start();
                    MessageBox.Show("Проигрыш!\nВы проиграли!", "Проигрыш");
                    temp = statistics.Where(a => a.Login == "Компьютер").First();
                    temp.Win = temp.Win + 1;
                    temp = statistics.Where(a => a.Login == "Пользователь").First();
                    temp.Won = temp.Won + 1;
                    XmlWorker.WriteData(@"..\..\Classes\Statistics\StatisticFile\pom.xml", statistics);
                    ChangeDataSource(form, statistics);
                    this.Start = false;
                    ServerWorker.PostDataAboutFinish("http://localhost:17736/Home/WriteData", new ServerObject() { Login1 = "Пользователь", Login2 = "Компьютер", Kod = 1 });
                    break;
                case 3:
                    Thread myThreadMD3 = new Thread(() => LoadStatistic(form, new StatisticOnTask() { DateOfStart = this.DateOfStart, TimeToPlay = DateTime.Now - this.DateOfStart, X = krestik ? 1 : 0, CountOfStep = this.CountOfStep, Result = 2 }));
                    myThreadMD3.Start();
                    MessageBox.Show("Ничья!", "Ничья");
                    temp = statistics.Where(a => a.Login == "Компьютер").First();
                    temp.NF = temp.NF + 1;
                    temp = statistics.Where(a => a.Login == "Пользователь").First();
                    temp.NF = temp.NF + 1;
                    XmlWorker.WriteData(@"..\..\Classes\Statistics\StatisticFile\pom.xml", statistics);
                    ChangeDataSource(form, statistics);
                    this.Start = false;
                    ServerWorker.PostDataAboutFinish("http://localhost:17736/Home/WriteData", new ServerObject() { Login1 = "Пользователь", Login2 = "Компьютер", Kod = 2 });
                    break;
                default:
                    break;
            }

            
        }
        private void LoadStatistic(Form form,StatisticOnTask stat)
        {
            try {
                ServerWorker.PostStatistic("http://localhost:17736/Home/WriteDataTask", stat);
                List<StatisticOnTask> statisticontask = XmlWorkerTask.GetData(@"..\..\Classes\Statistics\StatisticFile\pom2.xml");
                statisticontask.Add(stat);
                XmlWorkerTask.WriteData(@"..\..\Classes\Statistics\StatisticFile\pom2.xml", statisticontask);
                form.Invoke(new MethodInvoker(() => ChangeDataSource2(form, statisticontask)));
            }
            catch (Exception ex)
            {
                try {
                    form.Invoke((MethodInvoker)(() => MessageBox.Show(ex.Message, "Error")));
                }
                catch
                {

                }
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

        private void ChangeDataSource2(Form form, List<StatisticOnTask> statistics)
        {
            DataGridView temp = new DataGridView();
            Dictionary<string, string> dict = new Dictionary<string, string>() { { "DateOfStart", "Дата игры" }, { "TimeToPlay", "Длительность игры" }, { "Result", "Результат" }, { "X", "Фигура пользователя" },{ "CountOfStep", "Количество ходов пользователя" } };
            foreach (Control c in form.Controls)
            {
                if (!(c is TabControl)) continue;
                TabCont = c as TabControl;
            }
            foreach (Control c in TabCont.TabPages[2].Controls)
            {
                if (c is DataGridView)
                    temp = c as DataGridView;
            }
            temp.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            temp.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            temp.Columns.Clear();
            temp.Rows.Clear();
            temp.AllowUserToAddRows = false;
            temp.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText="Дата игры",Width=168});
            temp.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Время игры", Width = 168 });
            temp.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Результат", Width = 168 });
            temp.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Фигура пользователя", Width = 168 });
            temp.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Количество ходов пользователя", Width = 168 });
            foreach (StatisticOnTask st in statistics)
            {
                DataGridViewRow c = new DataGridViewRow();
                c.Cells.Add(new DataGridViewTextBoxCell());
                c.Cells[0].Value = st.DateOfStart.ToString("d");
                c.Cells.Add(new DataGridViewTextBoxCell());
                c.Cells[1].Value = string.Format("{0:hh\\:mm\\:ss}", st.TimeToPlay);
                c.Cells.Add(new DataGridViewTextBoxCell());
                c.Cells[2].Value = ((st.Result == 0) ? "WinOfUser" : (st.Result == 1) ? "WinOfComputer" : "Draw");
                c.Cells.Add(new DataGridViewTextBoxCell());
                c.Cells[3].Value = ((st.X == 1) ? "X" : "O");
                c.Cells.Add(new DataGridViewTextBoxCell());
                c.Cells[4].Value = st.CountOfStep;
                temp.Rows.Add(c);
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
