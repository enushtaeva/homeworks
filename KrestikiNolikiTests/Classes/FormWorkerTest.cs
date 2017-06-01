using ClassLibrary1;
using Krestiki_Noliki;
using Krestiki_Noliki.Classes;
using Krestiki_Noliki.Classes.Statistics;
using Krestiki_Noliki.Interfaces;
using KrestikiNolikiTests.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;
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
    public class FormWorkerTest
    {

        //Этот метод необходим, чтобы отдельно строить игровое поле на тестовой форме, просто в  FormWorker этот же метод еще и загружает статистику из файла по относительному пути, 
        //а так как для тестов ее загружать не нужно, да и зачем добавлять лишний файл в проект, то пришлось создать отдельный метод
        public void BuildPlayingFuildTest(Form form, int leftfirstbutton, int topfirstbutton, int widthbutton, int heightbutton, string buttonname, Color color, EventHandler functionToExec, FlatStyle flatstyle, ImageLayout layout)
        {
            TabControl TabCont = new TabControl();
            int[][] ArrayFigures;
            Button tempbutton;
            int number = 0;
            ArrayFigures = new int[3][];
            foreach (Control c in form.Controls)
            {
                if (c is TabControl)
                    TabCont = c as TabControl;

            }
            for (int i = 0; i < 3; i++)
            {
                ArrayFigures[i] = new int[3];
                for (int j = 0; j < 3; j++)
                {
                    ArrayFigures[i][j] = 0;
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
                    number++;
                }
            }

        }

        //Проверка, что в DataGridView со статистикой по заданию на форме добавляются строки со значениями (пользователь увидит статистику)
        [TestMethod]
        public void ChangeDataSource2Test_DataGridViewRowsCount_notnull()
        {
            //Arrange
            FormKrestikiNoliki form = new FormKrestikiNoliki();//форма, содержащая в себе DataGridView
            var stat = new Fixture().Create<List<StatisticOnTask>>();//Рандомный набор со статистикой
            DataGridView temp = new DataGridView();//будет указывать на DataGridView на форме, чтобы можно было получить количество строк
            TabControl TabCont = new TabControl();//так как DataGridView находится в TabControl, сначала придется извлечь TabControl 
            FormWorker worker = new FormWorker(null, null, null, null, null);//собственно класс, который содержит метод, добавляющий статистику

            //Act
            worker.ChangeDataSource2(form, stat);//Добавляем в DataGridView строки

            //Assert
            foreach (Control c in form.Controls)
            {
                if (!(c is TabControl)) continue;//находим на форме TabControl
                TabCont = c as TabControl;
                break;
            }
            foreach (Control c in TabCont.TabPages[2].Controls)//находим в TabControl DataGridView
            {
                if (c is DataGridView)
                {
                    temp = c as DataGridView;
                    break;
                }
            }
            Assert.AreNotEqual(0, temp.Rows.Count);//Проверяем, что у найденного DataGridView количество строк не равно нулю (статистика добавилась)
        }

        //Проверка, что ход компьютера отображается на игровом поле (у кнопки появляется фоновая картинка)
        [TestMethod]
        public void StepOfComputerFormTest_ButtonBackground_NotNull()
        {
            //Arrange
            FormKrestikiNoliki form = new FormKrestikiNoliki();//форма, на которой будет строиться игровое поле
            int[][] position = new int[][] { new int[] { 0, 0, 0 }, new int[] { 0, 0, 0 }, new int[] { 0, 0, 0 } };//массив, описывающий положение фигур на поле 5-крестик 7 - нолик 0-пустая клетка
            IGameWorker gameworker =
                new GameWorkerStub();//создаем заглушку, которая
            //будет играть роль интерфейса с логикой игры, и в функции StepOfComputer пусть будет возвращаться 11 - это тег кнопки, у которой надо сменить background
            BuildPlayingFuildTest(form, 10, 10, 200, 200, "gameButton", Color.Red, null, FlatStyle.Popup, ImageLayout.Zoom);//Строим игровое поле на тестовой форме
            TabControl TabCont = new TabControl();//TabControl на форме
            FormWorker worker = new FormWorker(gameworker, null, null, null, null);//создаем класс для работы с формой, и в качестве интерфейса с логикой подаем на вход заглушку
            Button temp = new Button();//кнопка, у которой должен будет измениться Background

            //Act
            worker.StepOfComputer(form, "", true);//вызывается метод StepOfComputer из интерфейса логики, роль которого играет заглушка и его результат отображается на форме

            //Assert
            foreach (Control c in form.Controls)
            {
                if (!(c is TabControl)) continue;//находим на форме TabControl
                TabCont = c as TabControl;
                break;
            }
            foreach (Control c in TabCont.TabPages[0].Controls)//Находим кнопку в TabControl
            {
                if (!(c is Button)) continue;
                if (c.Tag != null && c.Tag.ToString() == "11")
                {
                    temp = c as Button;
                    break;
                }
            }
            Assert.IsNotNull(temp.BackgroundImage);//у кнопки есть фоновая картинка
        }

        //Проверка, что на форме корректно простраивается игровое поле размером 3х3
        //По крайней мере оно содержит 9 кнопок
        [TestMethod]
        public void BuildPlayingFuildTest_CountOfButtons_9()
        {
            //Arrange
            var mock = new Mock<IXmlWorker<Statistic>>();//Заглушка для работы со статистикой, просто в данном случае нам не нужно ничего и никуда записывать, или получать откуда-то данные
            int count = 0;//количество кнопок игровогог поля
            var mocktask = new Mock<IXmlWorker<StatisticOnTask>>();//Заглушка для работы со статистикой, там два файла статистики в разных форматах
            FormKrestikiNoliki form = new FormKrestikiNoliki();//форма, на которой будет строиться игровое поле
            mock.Setup(a => a.GetData(It.IsAny<string>())).Returns(new Fixture().Create<List<Statistic>>());//создаем заглушки для методов GetData (пусть вернет рандомный набор статистики) и WriteData (пусть ничего не делает)
            mock.Setup(a => a.WriteData(It.IsAny<string>(), It.IsAny<List<Statistic>>()));
            mocktask.Setup(a => a.GetData(It.IsAny<string>())).Returns(new Fixture().Create<List<StatisticOnTask>>());
            FormWorker formworker = new FormWorker(null, mock.Object, null, mocktask.Object, null);//класс, метод которого будем провекрять
            TabControl TabCont = new TabControl();//кнопки добавляются в первую вкладку TabControl
            formworker.Size = 3;//Задаем размер игрового поля, так-то он по умолчанию 3х3, но чтобы было понятно, где он меняется и как его задать

            //Act
            formworker.BuildPlayingFuild(form, 10, 10, 200, 200, "gameButton", Color.Red, null, FlatStyle.Popup, ImageLayout.Zoom);

            //Assert
            //Находим TabControl на форме, и считаем в нем кнопки с игрового поля (мы им задали имя gameButton, к нему потом еще добавляется номер кнопки)
            foreach (Control c in form.Controls)
            {
                if (c is TabControl)
                    TabCont = c as TabControl;

            }
            foreach (Control c in TabCont.TabPages[0].Controls)
            {
                if (!(c is Button)) continue;
                if (c.Tag != null && c.Name.Contains("gameButton"))
                {
                    count++;
                }
            }
            Assert.IsTrue(count == 9);//проверка количества игровых кнопок

        }


    }
}
