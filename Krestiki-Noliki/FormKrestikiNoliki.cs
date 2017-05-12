using Krestiki_Noliki.Classes;
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
        FormWorker worker;
        GameWorker game;
        public FormKrestikiNoliki()
        {
            InitializeComponent();
        }

        private void FormKrestikiNoliki_Load(object sender, EventArgs e)
        {
            game = new GameWorker();
            worker = new FormWorker(game);
            worker.BuildPlayingFuild(this, 210, 150, 120, 120, "gameButton", Color.White, buttonGame_Click, FlatStyle.Popup,ImageLayout.Zoom);
        }

        private void buttonGame_Click(object sender, EventArgs e)
        {
            worker.Click(this, (sender as Button).Name, worker.Krestik);
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            worker.Start = true;
            worker.BuildPlayingFuild(this, 210, 150, 120, 120, "gameButton", Color.White, buttonGame_Click, FlatStyle.Popup, ImageLayout.Zoom);
        }
    }
}
