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
    class GameWorker :  IGameWorker
    {
        public TabControl TabCont { get; set; }
        public int ValideWinOrWon(Form form,int[][] arrfigures,bool krestik, int size,int krestikvalue,int nolikvalue)
        {
            int valide = ValideStrings(arrfigures, krestik, size, krestikvalue, nolikvalue);
            if (valide != 0) return valide;
            valide = ValideColumns(arrfigures, krestik, size, krestikvalue, nolikvalue);
            if (valide != 0) return valide;
            valide = ValideDiagonal(arrfigures, krestik, size, krestikvalue, nolikvalue);
            if (valide != 0) return valide;
            if(ValideStandOf(form, arrfigures, krestik, size, krestikvalue, nolikvalue)) return 3;
            return 0;
        }
        public string StepOfComputer(Form form, string buttontag, int[][] arrfigures, bool krestik, int size, int krestikvalue, int nolikvalue)
        {
            string s = "";
            s = WinStep(arrfigures, krestik, size, krestikvalue, nolikvalue);
            if (s != "") return s;
            s = StepString(arrfigures, krestik, size, krestikvalue, nolikvalue);
            if (s != "") return s;
            s = StepColumn(arrfigures, krestik, size, krestikvalue, nolikvalue);
            if (s != "") return s;
            s = StepStraightDiagonal(arrfigures, krestik, size, krestikvalue, nolikvalue);
            if (s != "") return s;
            s = StepInverseDiagonal(arrfigures, krestik, size, krestikvalue, nolikvalue);
            if (s != "") return s;
            s = TryToWin(arrfigures, krestik, size, krestikvalue, nolikvalue);
            if (s != "") return s;
            s = StepOverClickedButton(form, buttontag, arrfigures, krestik, size, krestikvalue, nolikvalue);
            if (s != "") return s;
            s = RandomHod(form, arrfigures, krestik, size, krestikvalue, nolikvalue);
            if (s != "") return s;
            return s;
        }

        #region PrivateSection
        //Делает ход в одном столбце или строке с поставленной пользователем фигурой
        private string StepOverClickedButton(Form form, string buttontag, int[][] position, bool krestik, int size, int krestikvalue, int nolikvalue)
        {

    
            foreach (Control c in form.Controls)
            {
                if (!(c is TabControl)) continue;
                TabCont = c as TabControl;
            }
            foreach (Control c in TabCont.TabPages[0].Controls)
            {
                if (!(c is Button) || c.Tag==null) continue;
                Button button = (c as Button);
                if (((button.Tag?.ToString()[0] == buttontag[0] && (button.Tag?.ToString()[1] == buttontag[1] + 1 || button.Tag?.ToString()[1] == buttontag[1] - 1))
                    || (button.Tag?.ToString()[1] == buttontag[1] && (button.Tag?.ToString()[0] == buttontag[0] + 1 || button.Tag?.ToString()[0] == buttontag[0] - 1)))
                    && button.BackgroundImage == null)
                {

                    string tag = button.Tag?.ToString();
                    if (krestik)
                    { 
                        position[Convert.ToInt32(tag[0].ToString()) - 1][Convert.ToInt32(tag[1].ToString()) - 1] = nolikvalue;
                        return tag;
                    }
                    else
                    {
                        position[Convert.ToInt32(tag[0].ToString()) - 1][Convert.ToInt32(tag[1].ToString()) - 1] = krestikvalue;
                        return tag;
                    }

                }
            }
            return "";
        }

        private int ValideStrings(int[][] arrfigures, bool krestik,int size, int krestikvalue, int nolikvalue)
        {
            for (int i = 0; i < arrfigures.Length; i++)
            {
                if (Sum(arrfigures[i].ToList()) == krestikvalue * size)
                {
                    if (krestik)
                    {
                        return 1;
                    }
                    else
                    { 
                        return 2;
                    }
                }
                if (Sum(arrfigures[i].ToList()) == nolikvalue * size)
                {
                    if (krestik)
                    {
                        return 2;
                    }
                    else
                    { 
                        return 1;
                    }
                }
            }
            return 0;
        }

        private int ValideColumns(int[][] arrfigures, bool krestik, int size, int krestikvalue, int nolikvalue)
        {
            List<int> temp = new List<int>();
            for (int i = 0; i < arrfigures.Length; i++)
            {
                temp.Clear();
                for (int j = 0; j < arrfigures.Length; j++)
                {
                    temp.Add(arrfigures[j][i]);

                }
                if (Sum(temp) == krestikvalue * size)
                {
                    if (krestik)
                    {
                        return 1;
                    }
                    else
                    {
                        
                        return 2;
                    }
                }
                if (Sum(temp) == nolikvalue * size)
                {
                    if (krestik)
                    {
                        return 2;
                    }
                    else
                    {
                        return 1;
                    }
                }

            }
            return 0;
        }

        private int ValideDiagonal(int[][] arrfigures, bool krestik, int size, int krestikvalue, int nolikvalue)
        {
            List<int> temp = new List<int>();
            for (int i = 0; i < arrfigures.Length; i++)
            {
                for (int j = 0; j < arrfigures.Length; j++)
                {
                    if (i == j)
                    {
                        temp.Add(arrfigures[i][j]);
                    }

                }
            }
            if (Sum(temp) == krestikvalue * size)
            {
                if (krestik)
                {
                   
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
            if (Sum(temp) == nolikvalue * size)
            {
                if (krestik)
                {
                    return 2;
                }
                else
                {
                    return 1;
                }
            }
            temp.Clear();
            for (int i = 0; i < arrfigures.Length; i++)
            {
                for (int j = 0; j < arrfigures.Length; j++)
                {
                    if ((i + j) == (arrfigures.Length - 1))
                    {
                        temp.Add(arrfigures[i][j]);
                    }

                }
            }
            if (Sum(temp) == krestikvalue * size)
            {
                if (krestik)
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
            if (Sum(temp) == nolikvalue * size)
            {
                if (krestik)
                {
                    return 2;
                }
                else
                {
                    return 1;
                }
            }
            return 0;

        }

        private string WinStep(int[][] arrfigures,bool krestik, int size, int krestikvalue, int nolikvalue)
        {
            List<int> temp = new List<int>();
            string step = WinStepString(arrfigures,krestik,size,krestikvalue,nolikvalue);
            if (step != "") return step;
            step = WinStepColumn(arrfigures, krestik, size, krestikvalue, nolikvalue);
            if (step != "") return step;
            step = WinStepStraightDiagonal(arrfigures, krestik, size, krestikvalue, nolikvalue);
            if (step != "") return step;
            step = WinStepInverseDiagonal(arrfigures, krestik, size, krestikvalue, nolikvalue);
            if (step != "") return step;
            return "";
        }
        //Проверяет, возможен ли выигрышный ход компьютера в какой-либо строке
        private string WinStepString(int[][] position, bool krestik, int size, int krestikvalue, int nolikvalue)
        {
            int numberofcolumn = 0;

            if (krestik)
            {
                for (int str = 0; str < position.Length; str++)
                {
                    if (Sum(position[str].ToList()) == nolikvalue * (size - 1))
                    {
                        numberofcolumn = NumberOfNullElement(position[str].ToList()) + 1;
                        position[str][numberofcolumn - 1] = nolikvalue;
                        return (str + 1).ToString() + numberofcolumn.ToString();
                    }
                }
            }
            else
            {
                for (int str = 0; str < position.Length; str++)
                {
                    if (Sum(position[str].ToList()) == krestikvalue * (size - 1))
                    {
                        numberofcolumn = NumberOfNullElement(position[str].ToList()) + 1;
                        position[str][numberofcolumn - 1] = krestikvalue;
                        return (str + 1).ToString() + (numberofcolumn).ToString();
                    }
                }
            }

            return "";
        }

        //Проверяет, возможен ли выигрышный ход компьютера в каком-либо столбце
        private string WinStepColumn(int[][] position, bool krestik, int size, int krestikvalue, int nolikvalue)
        {
            List<int> temp = new List<int>();

            int sum = 0;

            for (int col = 0; col < position.Length; col++)
            {
                temp.Clear();
                for (int str = 0; str < position.Length; str++)
                {
                    temp.Add(position[str][col]);

                }
                sum = Sum(temp);
                if (krestik)
                {
                    if (sum == nolikvalue*(size-1))
                    {
                        int numberofstring = NumberOfNullElement(temp) + 1;
                        position[numberofstring - 1][col] = nolikvalue;
                        return numberofstring.ToString() + (col + 1).ToString();
                    }
                }
                else
                {
                    if (sum == krestikvalue*(size-1))
                    {
                        int numberofstring = NumberOfNullElement(temp) + 1;
                        position[numberofstring - 1][col] = krestikvalue;
                        return numberofstring.ToString() + (col + 1).ToString();
                    }
                }
            }

            return "";
        }

        //Проверяет, возможен ли выигрышный ход компьютера на главной диагонали
        private string WinStepStraightDiagonal(int[][] position, bool krestik, int size, int krestikvalue, int nolikvalue)
        {
            List<int> temp = new List<int>();
            int sum = 0;
            int numberofstringandcolumn = 0;
            string pos = "";

            for (int str = 0; str < position.Length; str++)
            {

                for (int col = 0; col < position.Length; col++)
                {
                    if (str == col)
                        temp.Add(position[str][col]);

                }
            }
            sum = Sum(temp);
            if (krestik)
            {
                if (sum == nolikvalue * (size - 1))
                {
                    numberofstringandcolumn = NumberOfNullElement(temp) + 1;
                    pos = numberofstringandcolumn.ToString();
                    position[numberofstringandcolumn - 1][numberofstringandcolumn - 1] = nolikvalue;
                    return pos + pos;
                }
            }
            else
            {
                if (sum == krestikvalue * (size - 1))
                {
                    numberofstringandcolumn = NumberOfNullElement(temp) + 1;
                    pos = numberofstringandcolumn.ToString();
                    position[numberofstringandcolumn - 1][numberofstringandcolumn - 1] = krestikvalue;
                    return pos + pos;
                }
            }
            return "";
        }

        //Проверяет, возможен ли выигрышный ход компьютера на побочной диагонали
        private string WinStepInverseDiagonal(int[][] position, bool krestik, int size, int krestikvalue, int nolikvalue)
        {
            List<int> temp = new List<int>();
            int numberofstring = 0;
            int sum = 0;

            for (int str = 0; str < position.Length; str++)
            {

                for (int col = 0; col < position.Length; col++)
                {
                    if ((str + col) == (position.Length - 1))
                        temp.Add(position[str][col]);

                }
            }
            sum = Sum(temp);
            if (krestik)
            {
                if (sum == nolikvalue * (size - 1))
                {
                    numberofstring = NumberOfNullElement(temp) + 1;
                    position[numberofstring - 1][position.Length - numberofstring] = nolikvalue;
                    return numberofstring.ToString() + (position.Length - numberofstring + 1).ToString();
                }
            }
            else
            {
                if (sum == krestikvalue * (size - 1))
                {
                    numberofstring = NumberOfNullElement(temp) + 1;
                    position[numberofstring - 1][position.Length - numberofstring] = krestikvalue;
                    return numberofstring.ToString() + (position.Length - numberofstring + 1).ToString();
                }
            }
            return "";
        }
        //Проверяет, есть ли столбец с двумя фигурами пользователя, и если есть, то возвращает номер клетки, куда нужно поставить фигуру компьютера, чтобы пользователь
        //не мог заполнить столбец
        private string StepColumn(int[][] position, bool krestik, int size, int krestikvalue, int nolikvalue)
        {
            List<int> temp = new List<int>();
            int sum = 0;
            int numberofstring = 0;

            for (int i = 0; i < position.Length; i++)
            {
                temp.Clear();
                for (int j = 0; j < position.Length; j++)
                {
                    temp.Add(position[j][i]);

                }
                sum = Sum(temp);
                if (krestik)
                {
                    if (sum == krestikvalue*(size-1))
                    {
                        numberofstring = NumberOfNullElement(temp) + 1;
                        position[numberofstring - 1][i] = nolikvalue;
                        return numberofstring.ToString() + (i + 1).ToString();
                    }
                }
                else
                {
                    if (sum == nolikvalue*(size-1))
                    {
                        numberofstring = NumberOfNullElement(temp) + 1;
                        position[numberofstring - 1][i] = krestikvalue;
                        return (numberofstring).ToString() + (i + 1).ToString();
                    }
                }

            }

            return "";
        }

        //Проверяет, стоят ли на главной диагонали две фигуры пользователя, и если да, то возвращает номер клетки, куда нужно поставить фигуру компьютера, чтобы пользователь
        //не мог заполнить диагональ
        private string StepStraightDiagonal(int[][] position, bool krestik, int size, int krestikvalue, int nolikvalue)
        {
            List<int> temp = new List<int>();
            int sum = 0;
            int numberofpos = 0;
            string pos = "";

            for (int i = 0; i < position.Length; i++)
            {

                for (int j = 0; j < position.Length; j++)
                {
                    if (i == j)
                        temp.Add(position[i][j]);

                }
            }
            sum = Sum(temp);
            if (krestik)
            {

                if (sum == krestikvalue * (size - 1))
                {
                    numberofpos = NumberOfNullElement(temp) + 1;
                    pos = numberofpos.ToString();
                    position[numberofpos - 1][numberofpos - 1] = nolikvalue;
                    return pos + pos;
                }
            }
            else
            {

                if (sum == nolikvalue * (size - 1))
                {
                    numberofpos = NumberOfNullElement(temp) + 1;
                    pos = numberofpos.ToString();
                    position[numberofpos - 1][numberofpos - 1] = krestikvalue;
                    return pos + pos;
                }
            }
            return "";
        }

        //Проверяет, стоят ли на побочной диагонали две фигуры пользователя, и если да, то возвращает номер клетки, куда нужно поставить фигуру компьютера, чтобы пользователь
        //не мог заполнить диагональ
        private string StepInverseDiagonal(int[][] position, bool krestik, int size, int krestikvalue, int nolikvalue)
        {

            List<int> temp = new List<int>();
            int sum = 0;

            for (int i = 0; i < position.Length; i++)
            {

                for (int j = 0; j < position.Length; j++)
                {
                    if ((i + j) == (position.Length - 1))
                        temp.Add(position[i][j]);

                }
            }
            sum = Sum(temp);
            return StringStep(sum, temp,position,krestik,size,krestikvalue,nolikvalue);

        }

        //Возвращает ход в виде строки
        private string StringStep(int sum, List<int> temp, int[][] position, bool krestik, int size, int krestikvalue, int nolikvalue)
        {
            int numberofpos = 0;

            if (krestik)
            {
                if (sum == krestikvalue*(size-1))
                {
                    numberofpos = NumberOfNullElement(temp) + 1;
                    position[numberofpos - 1][position.Length - numberofpos] = nolikvalue;
                    return numberofpos.ToString() + (position.Length - numberofpos + 1).ToString();
                }
            }
            else
            {
                if (sum == nolikvalue*(size-1))
                {
                    numberofpos = NumberOfNullElement(temp) + 1;
                    position[numberofpos - 1][position.Length - numberofpos] = krestikvalue;
                    return numberofpos.ToString() + (position.Length - numberofpos + 1).ToString();
                }
            }
            return "";
        }

        private string StepString(int[][] position, bool krestik, int size, int krestikvalue, int nolikvalue)
        {
            int numb = 0;
            if (krestik)
            {
                for (int i = 0; i < position.Length; i++)
                {

                    if (Sum(position[i].ToList()) == krestikvalue*(size-1))
                    {
                        numb = NumberOfNullElement(position[i].ToList()) + 1;
                        position[i][numb - 1] = nolikvalue;
                        return (i + 1).ToString() + numb.ToString();
                    }
                }
            }
            else
            {
                for (int i = 0; i < position.Length; i++)
                {
                    if (Sum(position[i].ToList()) == nolikvalue*(size-1))
                    {
                        numb = NumberOfNullElement(position[i].ToList()) + 1;
                        position[i][numb - 1] = krestikvalue;
                        return (i + 1).ToString() + numb.ToString();
                    }
                }
            }

            return "";
        }

        //Возвращает номер нулевого элемента в массиве, необходима для поиска пустых клеток на поле
        private int NumberOfNullElement(IEnumerable<int> mas)
        {
            int k = 0;

            foreach (int i in mas)
            {
                if (i == 0)
                {
                    return k;
                }
                k++;
            }
            return k;
        }

        private int Sum(IEnumerable<int> mas)
        {
            int sum = 0;
            foreach (int i in mas)
            {
                sum += i;
            }
            return sum;
        }

        private string RandomHod(Form form,  int[][] position, bool krestik, int size, int krestikvalue, int nolikvalue)
        {
            foreach (Control c in form.Controls)
            {
                if (!(c is TabControl)) continue;
                TabCont = c as TabControl;
            }
            foreach (Control c in TabCont.TabPages[0].Controls)
            {
                if (!(c is Button) || c.Tag == null) continue;
                Button button = c as Button;
                if (button.BackgroundImage == null)
                {
                    string tag = button.Tag?.ToString();
                    if (krestik)
                    {
                        position[Convert.ToInt32(tag[0].ToString()) - 1][Convert.ToInt32(tag[1].ToString()) - 1] = nolikvalue;
                        return button.Tag.ToString();
                    }
                    else
                    {
                        position[Convert.ToInt32(tag[0].ToString()) - 1][Convert.ToInt32(tag[1].ToString()) - 1] = krestikvalue;
                        return button.Tag.ToString();
                    }
                }
            }
            return "";
        }

        private bool ValideStandOf(Form form, int[][] position, bool krestik, int size, int krestikvalue, int nolikvalue)
        {
            int count = 0;
            int b1 = 0, b2 = 0;
            foreach (Control c in form.Controls)
            {
                if (!(c is TabControl)) continue;
                TabCont = c as TabControl;
            }
            foreach (Control c in TabCont.TabPages[0].Controls)
            {
                if (!(c is Button) || c.Tag == null) continue;
                if ((c as Button).BackgroundImage == null) count++;
            }
            if (count == 0)
            {
                return true;
            }
            count = 0;
            for (int i = 0; i < position.Length; i++)
            {
                b1 = 0; b2 = 0;
                for (int j = 0; j < position.Length; j++)
                {
                    if (position[i][j] == krestikvalue) b1++;
                    if (position[i][j] == nolikvalue) b2++;
                }
                if (b1 != 0 && b2 != 0) count++;
            }
            if (count != size) return false;
            count = 0;
            for (int i = 0; i < position.Length; i++)
            {
                b1 = 0; b2 = 0;
                for (int j = 0; j < position.Length; j++)
                {
                    if (position[j][i] == krestikvalue) b1++;
                    if (position[j][i] == nolikvalue) b2++;
                }
                if (b1 != 0 && b2 != 0) count++;
            }
            if (count != size) return false;
            count = 0;
            b1 = 0; b2 = 0;
            for (int i = 0; i < position.Length; i++)
            {

                for (int j = 0; j < position.Length; j++)
                {
                    if (i == j)
                    {
                        if (position[i][j] == krestikvalue) b1++;
                        if (position[i][j] == nolikvalue) b2++;
                    }
                }
            }
            if (b1 == 0 || b2 == 0) return false;
            b1 = 0; b2 = 0;

            for (int i = 0; i < position.Length; i++)
            {

                for (int j = 0; j < position.Length; j++)
                {
                    if ((i + j) == (position.Length - 1))
                    {
                        if (position[i][j] == krestikvalue) b1++;
                        if (position[i][j] == nolikvalue) b2++;
                    }
                }

            }
            if (b1 == 0 || b2 == 0) return false;

            return true;
        }

        
        private string TryToWin(int[][] position, bool krestik, int size, int krestikvalue, int nolikvalue)
        {
            if (krestik)
            {
                for (int i = 0; i < position.Length; i++)
                {
                    if (WinStr(position[i].ToList(),krestik,krestikvalue,nolikvalue))
                    {
                        string temp1 = (i + 1).ToString() + (NumberOfNullElement(position[i].ToList()) + 1).ToString();
                        position[i][NumberOfNullElement(position[i].ToList())] = nolikvalue;
                        return temp1;
                    }

                }
            }
            else
            {
                for (int i = 0; i < position.Length; i++)
                {
                    if (WinStr(position[i].ToList(), krestik, krestikvalue, nolikvalue))
                    {
                        string temp1 = (i + 1).ToString() + (NumberOfNullElement(position[i].ToList()) + 1).ToString();
                        position[i][NumberOfNullElement(position[i].ToList())] = krestikvalue;
                        return temp1;
                    }
                }
            }
            List<int> temp = new List<int>();
            for (int i = 0; i < position.Length; i++)
            {
                temp = new List<int>();
                for (int j = 0; j < position.Length; j++)
                {
                    temp.Add(position[j][i]);

                }
                if (krestik)
                {
                    if (WinStr(temp, krestik, krestikvalue, nolikvalue))
                    {
                        string temp1 = (NumberOfNullElement(temp) + 1).ToString() + (i + 1).ToString();
                        position[NumberOfNullElement(temp)][i] = nolikvalue;
                        return temp1;
                    }
                }
                else
                {
                    if (WinStr(temp, krestik, krestikvalue, nolikvalue))
                    {
                        string temp1 = (NumberOfNullElement(temp) + 1).ToString() + (i + 1).ToString();
                        position[NumberOfNullElement(temp)][i] = krestikvalue;
                        return temp1;
                    }
                }
            }
            temp = new List<int>();
            for (int i = 0; i < position.Length; i++)
            {

                for (int j = 0; j < position.Length; j++)
                {
                    if (i == j)
                        temp.Add(position[i][j]);

                }
            }
            if (krestik)
            {
                if (WinStr(temp, krestik, krestikvalue, nolikvalue))
                {
                    string temp1 = (NumberOfNullElement(temp) + 1).ToString() + (NumberOfNullElement(temp) + 1).ToString();
                    position[NumberOfNullElement(temp)][NumberOfNullElement(temp)] = nolikvalue;
                    return temp1;
                }
            }
            else
            {
                if (WinStr(temp, krestik, krestikvalue, nolikvalue))
                {
                    string temp1 = (NumberOfNullElement(temp) + 1).ToString() + (NumberOfNullElement(temp) + 1).ToString();
                    position[NumberOfNullElement(temp)][NumberOfNullElement(temp)] = krestikvalue;
                    return temp1;
                }
            }
            temp = new List<int>();
            for (int i = 0; i < position.Length; i++)
            {

                for (int j = 0; j < position.Length; j++)
                {
                    if ((i + j) == (position.Length - 1))
                        temp.Add(position[i][j]);

                }
            }
            if (krestik)
            {
                if (WinStr(temp, krestik, krestikvalue, nolikvalue))
                {
                    string temp1 = (NumberOfNullElement(temp) + 1).ToString() + (position.Length - NumberOfNullElement(temp)).ToString();
                    position[NumberOfNullElement(temp)][position.Length - NumberOfNullElement(temp) - 1] = nolikvalue;
                    return temp1;
                }
            }
            else
            {
                if (WinStr(temp, krestik, krestikvalue, nolikvalue))
                {
                    string temp1 = (NumberOfNullElement(temp) + 1).ToString() + (position.Length - NumberOfNullElement(temp)).ToString();
                    position[NumberOfNullElement(temp)][position.Length - NumberOfNullElement(temp) - 1] = krestikvalue;
                    return temp1;
                }
            }
            return "";
        }

        private bool WinStr(IEnumerable<int> arr,bool krestik,int krestikvalue, int nolikvalue)
        {
            int count1 = 0, count2 = 0;
            foreach (int i in arr)
            {
                if (krestik)
                {
                    if (i == nolikvalue) count1++; if (i == krestikvalue) count2++;
                }
                else
                {
                    if (i == krestikvalue) count1++; if (i == nolikvalue) count2++;
                }
            }
            if (count1 != 0 && count2 == 0) return true;
            return false;
        }

        #endregion

    }
}
