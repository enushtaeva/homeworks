using Krestiki_Noliki;
using Krestiki_Noliki.Classes;
using KrestikiNolikiTests.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KrestikiNolikiTests.Classes
{
    [TestClass]
    public class GameWorkerTest
    {
        //Проверка, что ход компьютера рассчитывается корректно
        [TestMethod]
        public void StepOfComputerTest_StepNotNull_NotEmpty_33()
        {
            //Arrange
            FormKrestikiNoliki form = new FormKrestikiNoliki();//форма, на которой находится игровое поле, и для которой будет рассчитываться ход компьютера
            FormWorkerTest formworker = new FormWorkerTest();//необходим, чтобы создать игровое поле
            formworker.BuildPlayingFuildTest(form, 10, 10, 200, 200, "gameButton", Color.Red, null, FlatStyle.Popup, ImageLayout.Zoom);//строим игровое поле на форме
            GameWorker gameworker = new GameWorker();//класс, реализующий интерфейс с логикой, его метод будем тестировать
            int[][] position = new int[][] { new int[] { 0, 5, 7 }, new int[] { 0, 5, 7 }, new int[] { 5, 0, 0 } };//массив, описывающий положение фигур на поле 5-крестик 7 - нолик 0-пустая клетка
            TabControl TabCont = new TabControl();//все кнопки лежат в TabControl
            int x, y = 0;//необходимы, чтобы поменять BackGround у требуемых кнопок (расставить фигуры на поле)
            foreach (Control c in form.Controls)//Находим TabControl
            {
                if (c is TabControl)
                    TabCont = c as TabControl;

            }
            foreach (Control c in TabCont.TabPages[0].Controls)//Расставляем фигуры
            {
                if (!(c is Button)) continue;
                if (c.Tag != null)
                {
                    x = Int32.Parse(c.Tag.ToString()[0].ToString());
                    y = Int32.Parse(c.Tag.ToString()[1].ToString());
                    if (position[x - 1][y - 1] != 0) (c as Button).BackgroundImage = (Image)Resources.krestik;
                }
            }

            //Act
            string step = gameworker.StepOfComputer(form, "31", position, true, 3, 5, 7);//рассчитывает и возвращает ход компьютера, в данном случае наиболее выгодный ход -
            //третья клетка третьей строки, krestik=true - пользователь играет за крестики, он оказался невнимателен и упустил победный ход, зато у компьютера есть шанс победить
            //поставив нолик в указанную клетку, что он и делает, вернув 33 - это тег кнопки, background которой надо поменять

            //Assert
            Assert.IsFalse(step == string.Empty || step == null || step != "33");
        }

        //Проверка, что компьютер "видит", что образовалась победа, проигрыш или ничья на поле
        [TestMethod]
        public void ValideWinOrWonTest_1()
        {
            //Arrange
            FormKrestikiNoliki form = new FormKrestikiNoliki();
            FormWorkerTest formworker = new FormWorkerTest();
            formworker.BuildPlayingFuildTest(form, 10, 10, 200, 200, "gameButton", Color.Red, null, FlatStyle.Popup, ImageLayout.Zoom);
            GameWorker gameworker = new GameWorker();
            int[][] position = new int[][] { new int[] { 0, 5, 7 }, new int[] { 0, 5, 7 }, new int[] { 0, 5, 0 } };//по массиву фигур видно, что у нас образовался столбец из крестиков
            TabControl TabCont = new TabControl();
            int x, y = 0;
            foreach (Control c in form.Controls)
            {
                if (c is TabControl)
                    TabCont = c as TabControl;

            }
            foreach (Control c in TabCont.TabPages[0].Controls)//расставляем фигуры на игровом поле
            {
                if (!(c is Button)) continue;
                if (c.Tag != null)
                {
                    x = Int32.Parse(c.Tag.ToString()[0].ToString());
                    y = Int32.Parse(c.Tag.ToString()[1].ToString());
                    if (position[x - 1][y - 1] != 0) (c as Button).BackgroundImage = (Image)Resources.krestik;
                }
            }

            //Act
            int winwon = gameworker.ValideWinOrWon(form, position, true, 3, 5, 7);//так как krestik=true(3 параметр), то пользователь играет за крестики, у нас столбец из крестиков, следовательно 
            //результат - победа пользователя, он обозначается цифрой 1 (2 - проигрыш пользователя, победа компьютера, 3 - ничья), можно было сделать bool, но тогда непонятно, как обозначать ничью

            //Assert
            Assert.IsTrue(winwon == 1);
        }

    }
}

