using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Krestiki_Noliki.Interfaces
{
    public interface IGameWorker
    {
        int ValideWinOrWon(Form form, int[][] arrfigures, bool krestik, int size,int krestikvalue, int nolikvalue);
        string StepOfComputer(Form form, string buttontag, int[][] arrfigures, bool krestik, int size, int krestikvalue, int nolikvalue);
    }
}
