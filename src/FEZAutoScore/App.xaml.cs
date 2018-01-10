using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace FEZAutoScore
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
#if DEBUG
            AppDomain.CurrentDomain.FirstChanceException += CurrentDomain_FirstChanceException;
#endif
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            DispatcherUnhandledException += Application_DispatcherUnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
        }

#if DEBUG
        private void CurrentDomain_FirstChanceException(object sender, FirstChanceExceptionEventArgs e)
        {
            if (e.Exception is TaskCanceledException) return;

            //Debugger.Break();
        }
#endif

        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            if (e.Exception is TaskCanceledException) return;

            ApplicationError.HandleUnexpectedError(e.Exception);
        }

        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            ApplicationError.HandleUnexpectedError(e.Exception);
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var showMessageBox = !e.IsTerminating;

            ApplicationError.HandleUnexpectedError(e.ExceptionObject as Exception, showMessageBox);
        }
    }

    public class ApplicationError
    {
        private const string ErrorLogFileName = "error_{0}.log";

        private static readonly DirectoryInfo _directory = new DirectoryInfo(
                Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "errorlog"));

        private static bool _isFirstError = true;

        public static void HandleUnexpectedError(Exception ex, bool showMessageBox = true)
        {
            if (!_isFirstError)
            {
                return;
            }

            _isFirstError = true;

            try
            {
                OutputAsLogFile(ex);
            }
            catch { }

            if (showMessageBox)
            {
                MessageBox.Show(Properties.Resources.UnexpectedErrorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            Application.Current.Shutdown(-1);
        }

        private static void OutputAsLogFile(Exception ex)
        {
            if (ex == null)
            {
                return;
            }

            if (!_directory.Exists)
            {
                _directory.Create();
            }

            var logfileName = string.Format(ErrorLogFileName, DateTime.Now.ToString("yyyyMMddHHmmss"));
            var fullPath = Path.Combine(_directory.FullName, logfileName);

            using (var sw = new StreamWriter(fullPath, false, Encoding.UTF8))
            {
                sw.WriteLine("---Exception------------------------");
                sw.WriteLine("[Message]");
                sw.WriteLine(ex.Message);
                sw.WriteLine("[Source]");
                sw.WriteLine(ex.Source);
                sw.WriteLine("[StackTrace]");
                sw.WriteLine(ex.StackTrace);
                sw.WriteLine(string.Empty);

                OutputInnnerException(ex, sw);
            }
        }

        private static void OutputInnnerException(Exception ex, StreamWriter sw)
        {
            if (ex.InnerException == null)
            {
                return;
            }

            sw.WriteLine("---InnerException-------------------");
            sw.WriteLine("[Message]");
            sw.WriteLine(ex.InnerException.Message);
            sw.WriteLine("[Source]");
            sw.WriteLine(ex.InnerException.Source);
            sw.WriteLine("[StackTrace]");
            sw.WriteLine(ex.InnerException.StackTrace);
            sw.WriteLine(string.Empty);

            OutputInnnerException(ex.InnerException, sw);
        }
    }
}
