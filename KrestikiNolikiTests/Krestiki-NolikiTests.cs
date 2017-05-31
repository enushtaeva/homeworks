using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Krestiki_Noliki;
using Krestiki_Noliki.Classes;
using KrestikiNolikiTests.Classes;
using System.Windows.Forms;
using Ploeh.AutoFixture;
using ClassLibrary1;
using System.Collections.Generic;
using KrestikiNolikiTests.Properties;
using System.Drawing;
using Krestiki_Noliki.Interfaces;
using Moq;
using ServerForGame.Classes;
using ServerForGame.Controllers;
using ServerForGame.Interfaces;
using Krestiki_Noliki.Classes.Statistics;

namespace KrestikiNolikiTests
{
    [TestClass]
    public class Krestiki_NolikiTests
    {

        [TestMethod]
        public void StepOfComputerTest()
        {
            FormKrestikiNoliki form = new FormKrestikiNoliki();
            FormWorkerTest formworker = new FormWorkerTest();
            formworker.BuildPlayingFuildTest(form, 10, 10, 200, 200, "gameButton", Color.Red, null, FlatStyle.Popup,ImageLayout.Zoom);
            GameWorker gameworker = new GameWorker();
            int[][] position = new int[][] { new int[] { 0, 5, 7 }, new int[] { 0, 5, 7 }, new int[] { 5, 0, 0 } };
            TabControl TabCont = new TabControl();
            int x, y = 0;
            foreach (Control c in form.Controls)
            {
                if (c is TabControl)
                    TabCont = c as TabControl;

            }
            foreach (Control c in TabCont.TabPages[0].Controls)
            {
                if (!(c is Button)) continue;
                if (c.Tag != null)
                {
                    x = Int32.Parse(c.Tag.ToString()[0].ToString());
                    y = Int32.Parse(c.Tag.ToString()[1].ToString());
                    if (position[x - 1][y - 1] != 0) (c as Button).BackgroundImage = (Image)Resources.krestik;
                }
            }
            string step = gameworker.StepOfComputer(form, "31", position, true, 3, 5, 7);
            Assert.IsFalse(step == string.Empty || step == null || step!="33");
        }


        [TestMethod]
        public void ChangeDataSource2Test()
        {
            FormKrestikiNoliki form = new FormKrestikiNoliki();
            var stat = new Fixture().Create<List<StatisticOnTask>>();
            DataGridView temp = new DataGridView();
            TabControl TabCont = new TabControl();
            FormWorker worker = new FormWorker(null, null, null, null, null);
            worker.ChangeDataSource2(form, stat);
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
            Assert.AreNotEqual(0, temp.Rows.Count);
        }


        [TestMethod]
        public void StepOfComputerFormTest()
        {
            FormKrestikiNoliki form = new FormKrestikiNoliki();
            FormWorkerTest formworker = new FormWorkerTest();
            int[][] position = new int[][] { new int[] { 0, 5, 0 }, new int[] { 0, 0, 0}, new int[] { 0, 0, 0 } };
            IGameWorker loggerDependency =
                Mock.Of<IGameWorker>(d => d.StepOfComputer(It.IsAny<Form>(), It.IsAny<string>(), It.IsAny<int[][]>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()) == "11");

            formworker.BuildPlayingFuildTest(form, 10, 10, 200, 200, "gameButton", Color.Red, null, FlatStyle.Popup,ImageLayout.Zoom);
            TabControl TabCont = new TabControl();
            foreach (Control c in form.Controls)
            {
                if (c is TabControl)
                    TabCont = c as TabControl;

            }
            foreach (Control c in TabCont.TabPages[0].Controls)
            {
                if (!(c is Button)) continue;
                if (c.Tag != null && c.Tag.ToString()=="12")
                {
                    (c as Button).BackgroundImage = (Image)Resources.krestik;
                    break;
                }
            }
            FormWorker worker = new FormWorker(loggerDependency, null, null, null, null);
            worker.StepOfComputer(form, "12", true);
            Button temp = new Button();
            foreach (Control c in TabCont.TabPages[0].Controls)
            {
                if (!(c is Button)) continue;
                if (c.Tag != null && c.Tag.ToString() == "11")
                {
                    temp = c as Button;
                    break;
                }
            }
            Assert.IsNotNull(temp.BackgroundImage);
        }

        [TestMethod]
        public void ValidateDataTest()
        {
            var mock = new Mock<IJSONWorker<Statistic>>();
            mock.Setup(a => a.GetData(It.IsAny<string>())).Returns(new List<Statistic>());
            mock.Setup(a => a.WriteData(It.IsAny<List<Statistic>>(), It.IsAny<string>()));
            StatisticsWorker worker = new StatisticsWorker(mock.Object, null, null);
            var c = worker.ValidateData("hdfdfdfdf");
            Assert.IsFalse(c.Count == 0);
            mock.Verify(m => m.WriteData(It.IsAny<List<Statistic>>(), It.IsAny<string>()), Times.Exactly(1));
        }

        [TestMethod]
        public void PostDataTest()
        {
            var mock = new Mock<IJSONWorker<Statistic>>();
            mock.Setup(a => a.GetData(It.IsAny<string>())).Returns(new Fixture().Create<List<Statistic>>());
            StatisticsWorker worker = new StatisticsWorker(mock.Object, null, null);
            var c = worker.PostData("hdfdfdfdf");
            Assert.IsFalse(c==null || c==string.Empty);
        }

        [TestMethod]
        public void SetWinOrWonTest()
        {
            var mock = new Mock<IJSONWorker<Statistic>>();
            mock.Setup(a => a.GetData(It.IsAny<string>())).Returns(new Fixture().Create<List<Statistic>>());
            var mockhub= new Mock<IHubWorker>();
            mockhub.Setup(a => a.BroadcastObject(It.IsAny<object>()));
            StatisticsWorker worker = new StatisticsWorker(mock.Object, mockhub.Object, null);
            worker.SetWinOrWon(new Fixture().Create<ServerObject>(), new Fixture().Create<string>());
            mockhub.Verify(m => m.BroadcastObject(It.IsAny<object>()), Times.Exactly(1));
        }

        [TestMethod]
        public void BuildPlayingFuildTest()
        {
            var mock = new Mock<IXmlWorker<Statistic>>();
            int count = 0;
            var mocktask = new Mock<IXmlWorker<StatisticOnTask>>();
            FormKrestikiNoliki form = new FormKrestikiNoliki();
            mock.Setup(a => a.GetData(It.IsAny<string>())).Returns(new Fixture().Create<List<Statistic>>());
            mock.Setup(a => a.WriteData(It.IsAny<string>(), It.IsAny<List<Statistic>>()));
            mocktask.Setup(a => a.GetData(It.IsAny<string>())).Returns(new Fixture().Create<List<StatisticOnTask>>());
            FormWorker formworker = new FormWorker(null, mock.Object, null, mocktask.Object, null);
            TabControl TabCont = new TabControl();
            formworker.Size = 3;
            formworker.BuildPlayingFuild(form, 10, 10, 200, 200, "gameButton", Color.Red, null, FlatStyle.Popup, ImageLayout.Zoom);
            foreach (Control c in form.Controls)
            {
                if (c is TabControl)
                    TabCont = c as TabControl;

            }
            foreach (Control c in TabCont.TabPages[0].Controls)
            {
               if (!(c is Button)) continue;
               if(c.Tag!=null && c.Name.Contains("gameButton"))
                {
                    count++;
                }
            }
            Assert.IsTrue(count==9);

        }

        [TestMethod]
        public void ValideWinOrWonTest()
        {
            FormKrestikiNoliki form = new FormKrestikiNoliki();
            FormWorkerTest formworker = new FormWorkerTest();
            formworker.BuildPlayingFuildTest(form, 10, 10, 200, 200, "gameButton", Color.Red, null, FlatStyle.Popup, ImageLayout.Zoom);
            GameWorker gameworker = new GameWorker();
            int[][] position = new int[][] { new int[] { 0, 5, 7 }, new int[] { 0, 5, 7 }, new int[] { 0, 5, 0 } };
            TabControl TabCont = new TabControl();
            int x, y = 0;
            foreach (Control c in form.Controls)
            {
                if (c is TabControl)
                    TabCont = c as TabControl;

            }
            foreach (Control c in TabCont.TabPages[0].Controls)
            {
                if (!(c is Button)) continue;
                if (c.Tag != null)
                {
                    x = Int32.Parse(c.Tag.ToString()[0].ToString());
                    y = Int32.Parse(c.Tag.ToString()[1].ToString());
                    if (position[x - 1][y - 1] != 0) (c as Button).BackgroundImage = (Image)Resources.krestik;
                }
            }
            int winwon=gameworker.ValideWinOrWon(form, position, true, 3, 5, 7);
            Assert.IsTrue(winwon == 1);
        }

    }
}
