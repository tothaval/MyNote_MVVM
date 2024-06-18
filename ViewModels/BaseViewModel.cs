/*  MyNote (by Stephan Kammel, Dresden, Germany, 2024)
 *  
 *  BaseViewModel
 * 
 *  helper class that implements INotifyPropertyChanged
 */
using System.ComponentModel;

namespace MyNote_MVVM.ViewModels
{
    class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;


        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        protected virtual void Dispose() { }


    }
}
// EOF