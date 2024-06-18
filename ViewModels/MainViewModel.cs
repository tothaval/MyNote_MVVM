/*  MyNote (by Stephan Kammel, Dresden, Germany, 2024)
 *  
 *  MainViewModel  : BaseViewModel
 * 
 *  viewmodel for MainView
 */

using MyNote_MVVM.Commands;
using MyNote_MVVM.Utility;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace MyNote_MVVM.ViewModels
{
    class MainViewModel : BaseViewModel
    {
		private ObservableCollection<NoteViewModel>? _NoteViewModelCollection;


        private double _Rounding;
        public double Rounding
        {
            get { return _Rounding; }
            set
            {
                _Rounding = value;
                Application.Current.Resources["Radius"] = new CornerRadius(_Rounding);
                OnPropertyChanged(nameof(Rounding));
            }
        }


        private double _InnerRounding;
        public double InnerRounding
        {
            get { return _InnerRounding; }
            set
            {
                _InnerRounding = value;
                Application.Current.Resources["NoteRadius"] = new CornerRadius(_InnerRounding);
                OnPropertyChanged(nameof(InnerRounding));
            }
        }


        private double _ContentFontSizeSliderValue;
        public double ContentFontSizeSliderValue
        {
            get { return _ContentFontSizeSliderValue; }
            set
            {
                _ContentFontSizeSliderValue = value;

                Application.Current.Resources["ContentFontSize"] = _ContentFontSizeSliderValue;
                OnPropertyChanged(nameof(ContentFontSizeSliderValue));
            }
        }
        

        private double _DateTimeFontSizeSliderValue;
        public double DateTimeFontSizeSliderValue
        {
            get { return _DateTimeFontSizeSliderValue; }
            set
            {
                _DateTimeFontSizeSliderValue = value;

                Application.Current.Resources["DateTimeFontSize"] = _DateTimeFontSizeSliderValue;
                OnPropertyChanged(nameof(DateTimeFontSizeSliderValue));
            }
        }


        private double _HeaderFontSizeSliderValue;
        public double HeaderFontSizeSliderValue
        {
            get { return _HeaderFontSizeSliderValue; }
            set
            {
                _HeaderFontSizeSliderValue = value;

                Application.Current.Resources["HeaderFontSize"] = _HeaderFontSizeSliderValue;
                OnPropertyChanged(nameof(HeaderFontSizeSliderValue));
            }
        }
        


        private NoteViewModel? _Protocol;
		public NoteViewModel? Protocol
		{
			get { return _Protocol; }
			set
			{
				_Protocol = value;
				OnPropertyChanged(nameof(Protocol));
			}
		}


        private NotesViewModel _Notes;

        public NotesViewModel Notes
        {
            get { return _Notes; }
            set
            {
                _Notes = value;
                OnPropertyChanged(nameof(Notes));
            }
        }



        private bool _OptionsClicked;
        public bool OptionsClicked
        {
            get { return _OptionsClicked; }
            set
            {
                _OptionsClicked = value;
                OnPropertyChanged(nameof(OptionsClicked));
            }
        }

        public ICommand OptionsClickedCommand { get; }


        public MainViewModel()
        {
            Load();

            Application.Current.MainWindow.Closing += MainWindow_Closing;

            OptionsClickedCommand = new RelayCommand((s) => ShowOptions(), (s) => true);
        }


        private void ShowOptions()
        {
            if (OptionsClicked) { OptionsClicked = false; }
            else
            {
                OptionsClicked = true;
            }
        }


        private async void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            await SaveNotes();
        }

        private async void Load()
        {
            await LoadNotes();
        }

        private async Task LoadNotes()
        {
            PersistanceHandler persistanceHandler = new PersistanceHandler();

            _NoteViewModelCollection = persistanceHandler.DeSerializeNotes();

            if (_NoteViewModelCollection ==  null)
            {
                _NoteViewModelCollection = new ObservableCollection<NoteViewModel>();
            }

            _Protocol = persistanceHandler.DeSerializeProtocol();

            if (Protocol == null)
            {
                Protocol = new NoteViewModel(new Models.Note(
                    _NoteViewModelCollection.Count,
                    "title",
                    DateTime.Now,
                    "note"
                    ));
            }

            Notes = new NotesViewModel(_NoteViewModelCollection);
        }


        private async Task SaveNotes()
        {
            PersistanceHandler persistanceHandler = new PersistanceHandler();

            persistanceHandler.SerializeNotes(_NoteViewModelCollection);

            persistanceHandler.SerializeNote(Protocol);
        }
    }
}
// EOF