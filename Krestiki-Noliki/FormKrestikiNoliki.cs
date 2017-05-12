﻿using Krestiki_Noliki.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Krestiki_Noliki
{
    public partial class FormKrestikiNoliki : Form
    {
        public WorkerBox worker { get; private set; } = new WorkerBox();

        public FormKrestikiNoliki()
        {
            InitializeComponent();
        }

        private void FormKrestikiNoliki_Load(object sender, EventArgs e)
        {
  
            worker.FormWorker.BuildPlayingFuild(this, 330, 150, 120, 120, "gameButton", Color.White, buttonGame_Click, FlatStyle.Popup,ImageLayout.Zoom);
            comboBox1.SelectedItem = comboBox1.Items[0];
        }

        private void buttonGame_Click(object sender, EventArgs e)
        {
            worker.FormWorker.Click(this, (sender as Button).Name, worker.FormWorker.Krestik);
            if (!worker.FormWorker.Start) comboBox1.Enabled = true;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            worker.FormWorker.Start = true;
            comboBox1.Enabled = false;
            worker.FormWorker.Krestik = radioButtonKrest.Checked;
            switch (worker.FormWorker.Size)
            {
                case 3:
                    worker.FormWorker.BuildPlayingFuild(this, 330, 150, 120, 120, "gameButton", Color.White, buttonGame_Click, FlatStyle.Popup, ImageLayout.Zoom);
                    break;
                case 4:
                    worker.FormWorker.BuildPlayingFuild(this, 270, 90, 120, 120, "gameButton", Color.White, buttonGame_Click, FlatStyle.Popup, ImageLayout.Zoom);
                    break;
                case 5:
                    worker.FormWorker.BuildPlayingFuild(this, 210, 30, 120, 120, "gameButton", Color.White, buttonGame_Click, FlatStyle.Popup, ImageLayout.Zoom);
                    break;
                default:
                    break;
            }
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch((sender as ComboBox).SelectedItem.ToString())
            {
                case "3x3":
                    worker.FormWorker.Size = 3;
                    worker.FormWorker.BuildPlayingFuild(this, 330, 150, 120, 120, "gameButton", Color.White, buttonGame_Click, FlatStyle.Popup, ImageLayout.Zoom);
                    break;
                case "4x4":
                    worker.FormWorker.Size = 4;
                    worker.FormWorker.BuildPlayingFuild(this, 270, 90, 120, 120, "gameButton", Color.White, buttonGame_Click, FlatStyle.Popup, ImageLayout.Zoom);
                    break;
                case "5x5":
                    worker.FormWorker.Size = 5;
                    worker.FormWorker.BuildPlayingFuild(this, 210, 30, 120, 120, "gameButton", Color.White, buttonGame_Click, FlatStyle.Popup, ImageLayout.Zoom);
                    break;
                default:
                    break;
            }
        }
    }
}
