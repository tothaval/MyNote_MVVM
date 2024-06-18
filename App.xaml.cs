/*  MyNote (by Stephan Kammel, Dresden, Germany, 2024)
 *  
 *  App 
 * 
 *  manages application startup and exit behaviour
 *  
 */
using MyNote_MVVM.ViewModels;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace MyNote_MVVM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            string folder = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + "\\notes\\";

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            MainWindow mainWindow = new MainWindow()
            {
                DataContext = new MainWindowViewModel()
            };

            mainWindow.Show();

            base.OnStartup(e);
        }


    }
}
// EOF