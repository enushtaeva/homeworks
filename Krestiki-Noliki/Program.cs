using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Krestiki_Noliki
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(MyExceptionHandler);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException, true);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormKrestikiNoliki());
        }
        static void MyExceptionHandler(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show("А вот и я, глобальный обработчик исключений, в visual studio я не работаю, но из debug я нормально запускаюсь","Глобальное исключение");
        }
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            using (ThreadExceptionDialog exceptionDlg = new ThreadExceptionDialog((Exception)args.ExceptionObject))
            {
                exceptionDlg.ShowDialog();
            }
        }
    }
}
