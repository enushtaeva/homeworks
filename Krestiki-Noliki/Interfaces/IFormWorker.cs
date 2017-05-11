using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Krestiki_Noliki.Interfaces
{
    interface IFormWorker
    {
        void BuildPlayingFuild(Form form, int leftfirstbutton, int topfirstbutton, int widthbutton, int heightbutton);
        void ClickButton(Button button);
    }
}
