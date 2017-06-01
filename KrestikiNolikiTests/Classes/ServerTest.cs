using ClassLibrary1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;
using ServerForGame.Classes;
using ServerForGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrestikiNolikiTests.Classes
{
    [TestClass]
   public  class ServerTest
    {
        //Просто метод ValideData загружает статистику из файла, и если список статистики окажется пустым, то он должен добавить туда два элемента
        //(два логина - для пользователя и компьютера - с нулевыми количествами побед, поражений и ничьих)
        [TestMethod]
        public void ValidateDataTest_StatisticCount_NotNull()
        {
            //Arrange
            var mock = new Mock<IJSONWorker<Statistic>>();//Создаем заглушку для интефейса, работающего с файлом, из которого считывается статистика
            mock.Setup(a => a.GetData(It.IsAny<string>())).Returns(new List<Statistic>());//в методе GetData заглушки возвращаем пустой список со статистикой
            mock.Setup(a => a.WriteData(It.IsAny<List<Statistic>>(), It.IsAny<string>()));//пустой метод WriteData, но он должен вызваться хотя-бы один раз, так как список пустой, его надо заполнить и записать в файл
            StatisticsWorker worker = new StatisticsWorker(mock.Object, null, null);//этот класс работает с файлом статистики и с базой данных, пока нам нужен только интерфейс для работы с файлом

            //Act
            var c = worker.ValidateData("hdfdfdfdf");//указываем рандомный путь к файлу

            //Assert
            Assert.IsFalse(c.Count == 0);//список не пуст
            mock.Verify(m => m.WriteData(It.IsAny<List<Statistic>>(), It.IsAny<string>()), Times.Exactly(1));//произошла запись данных в файл
        }

        //Проверка метода, считывающего лист со статистикой из файла и преобразующего его в строку
        //возвращаемая строка не должна быть пустой или null
        [TestMethod]
        public void PostDataTest_StatisticString_NotNull_NotEmpty()
        {
            //Arrange
            var mock = new Mock<IJSONWorker<Statistic>>();//Заглушка для интерфейса работы с файлом
            mock.Setup(a => a.GetData(It.IsAny<string>())).Returns(new Fixture().Create<List<Statistic>>());//GetData пусть возвращает рандомный список со статистикой
            StatisticsWorker worker = new StatisticsWorker(mock.Object, null, null);

            //Act
            var c = worker.PostData("hdfdfdfdf");

            //Assert
            Assert.IsFalse(c == null || c == string.Empty);
        }


        //Проверка метода, изменяющего список со статистикой (увеличивающего колчество побед, поражений или ничьих)
        //После изменения списка, этот список должен разослаться клиентам и обновиться на экране, чтобы не перегружать страницу
        [TestMethod]
        public void SetWinOrWonTest_BroadcastList_IsDone()
        {
            //Arrange
            var mock = new Mock<IJSONWorker<Statistic>>();
            mock.Setup(a => a.GetData(It.IsAny<string>())).Returns(new Fixture().Create<List<Statistic>>());
            var mockhub = new Mock<IHubWorker>();
            mockhub.Setup(a => a.BroadcastObject(It.IsAny<object>()));//метод пока ничего не делает, но обязательно должен вызваться  один раз
            StatisticsWorker worker = new StatisticsWorker(mock.Object, mockhub.Object, null);

            //Act
            worker.SetWinOrWon(new Fixture().Create<ServerObject>(), new Fixture().Create<string>());

            //Assert
            mockhub.Verify(m => m.BroadcastObject(It.IsAny<object>()), Times.Exactly(1));
        }

    }
}
