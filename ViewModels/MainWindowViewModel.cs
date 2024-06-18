using MyNote_MVVM.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MyNote_MVVM.ViewModels
{
    class MainWindowViewModel : BaseViewModel
    {
        public BaseViewModel MainViewModel { get; set; } = new MainViewModel();

        public ICommand CloseCommand { get; }
        public ICommand LeftPressCommand { get; }

        public MainWindowViewModel()
        {
            CloseCommand = new RelayCommand((s) => Close(), (s) => true);
            LeftPressCommand = new RelayCommand((s) => Drag(s), (s) => true);
        }

        private void Close()
        {
            MessageBoxResult result = MessageBox.Show(
                $"Do you wan't to close MyNote?\n\n",
                "Close MyNote", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }


        private void Drag(object s)
        {
            MainWindow mainWindow = (MainWindow)s;

            mainWindow.DragMove();
        }
    }
}
