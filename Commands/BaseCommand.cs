/*  MyNote(by Stephan Kammel, Dresden, Germany, 2024)
 *  
 *  BaseCommand 
 * 
 *  implementing ICommand interface
 */
using System.Windows.Input;

namespace MyNote_MVVM.Commands
{
    public abstract class BaseCommand : ICommand
    {

        public event EventHandler? CanExecuteChanged;


        public virtual bool CanExecute(object? parameter)
        {
            return true;
        }


        public abstract void Execute(object? parameter);


        protected void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }


    }
}
// EOF