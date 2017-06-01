using Krestiki_Noliki.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KrestikiNolikiTests.Classes
{
    public class GameWorkerStub : IGameWorker
    {
        public string StepOfComputer(Form form, string buttontag, int[][] arrfigures, bool krestik, int size, int krestikvalue, int nolikvalue)
        {
            return "11";
        }

        public int ValideWinOrWon(Form form, int[][] arrfigures, bool krestik, int size, int krestikvalue, int nolikvalue)
        {
            throw new NotImplementedException();
        }
    }
}
