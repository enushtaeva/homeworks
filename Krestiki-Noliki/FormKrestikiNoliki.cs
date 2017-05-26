using ClassLibrary1;
using Krestiki_Noliki.Classes;
using Krestiki_Noliki.Classes.Exceptins;
using Krestiki_Noliki.Classes.Statistics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Krestiki_Noliki
{
    public partial class FormKrestikiNoliki : Form
    {
        public WorkerBox worker { get; private set; } = new WorkerBox();
        //DataGridView со статистикой по заданию:
        //Результат: 0-победа пользователя, 1-победа компьютера, 2-ничья
        //Фигура пользователя: 1-крестик, 0-нолик
        public FormKrestikiNoliki()
        {
            InitializeComponent();
        }

        private void FormKrestikiNoliki_Load(object sender, EventArgs e)
        {
            try {
                worker.FormWorker.BuildPlayingFuild(this, 80, 100, 120, 120, "gameButton", Color.White, buttonGame_Click, FlatStyle.Popup, ImageLayout.Zoom);
                comboBox1.SelectedItem = comboBox1.Items[0];
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void buttonGame_Click(object sender, EventArgs e)
        {
            try {
                
                worker.FormWorker.Click(this, (sender as Button).Name, worker.FormWorker.Krestik);
                if (!worker.FormWorker.Start) comboBox1.Enabled = true;
            }
            catch(NotStartException ex)
            {
                MessageBox.Show(ex.Message+" Start= "+ex.Start, "Error");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            try {
                worker.FormWorker.Start = true;
                comboBox1.Enabled = false;
                worker.FormWorker.Krestik = radioButtonKrest.Checked;
                switch (worker.FormWorker.Size)
                {
                    case 3:
                        worker.FormWorker.BuildPlayingFuild(this, 80, 100, 120, 120, "gameButton", Color.White, buttonGame_Click, FlatStyle.Popup, ImageLayout.Zoom);
                        break;
                    case 4:
                        worker.FormWorker.BuildPlayingFuild(this, 50, 80, 120, 120, "gameButton", Color.White, buttonGame_Click, FlatStyle.Popup, ImageLayout.Zoom);
                        break;
                    case 5:
                        worker.FormWorker.BuildPlayingFuild(this, 10, 10, 120, 120, "gameButton", Color.White, buttonGame_Click, FlatStyle.Popup, ImageLayout.Zoom);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try {
                switch ((sender as ComboBox).SelectedItem.ToString())
                {
                    case "3x3":
                        worker.FormWorker.Size = 3;
                        worker.FormWorker.BuildPlayingFuild(this, 80, 100, 120, 120, "gameButton", Color.White, buttonGame_Click, FlatStyle.Popup, ImageLayout.Zoom);
                        break;
                    case "4x4":
                        worker.FormWorker.Size = 4;
                        worker.FormWorker.BuildPlayingFuild(this, 50, 80, 120, 120, "gameButton", Color.White, buttonGame_Click, FlatStyle.Popup, ImageLayout.Zoom);
                        break;
                    case "5x5":
                        worker.FormWorker.Size = 5;
                        worker.FormWorker.BuildPlayingFuild(this, 10, 10, 120, 120, "gameButton", Color.White, buttonGame_Click, FlatStyle.Popup, ImageLayout.Zoom);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            throw new Exception();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Thread mythread = new Thread(GetData);
            mythread.Start();
        }

        private void GetData()
        {
            try
            {
                worker.FormWorker.GetDataFromServer(this);
            }
            catch (Exception ex)
            {
                Invoke((MethodInvoker)(()=> MessageBox.Show(ex.Message, "Error")));
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Thread mythread = new Thread(GetDataTask);
            mythread.Start();

        }

        private void GetDataTask()
        {
            try
            {
                worker.FormWorker.GetDataFromServerTask(this);
            }
            catch (Exception ex)
            {
                Invoke((MethodInvoker)(() => MessageBox.Show(ex.Message, "Error")));
            }
        }
    }
}
