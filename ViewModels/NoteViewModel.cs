﻿/*  MyNote (by Stephan Kammel, Dresden, Germany, 2024)
 *  
 *  NoteViewModel  : BaseViewModel
 * 
 *  viewmodel for Note model
 */
using MyNote_MVVM.Commands;
using MyNote_MVVM.Models;
using System.Text;
using System.Windows.Input;

namespace MyNote_MVVM.ViewModels
{
    class NoteViewModel : BaseViewModel
    {
        private Note _Note;
        public Note GetNote => _Note;

		public ICommand NewEntryCommand { get; }


		public int ID
		{
			get { return _Note.ID; }
			set
			{
                _Note.ID = value;
				OnPropertyChanged(nameof(ID));
			}
		}

		public string Title
		{
			get { return _Note.Title; }
			set
			{
                _Note.Title = value;

                DateTime_Edited = CurrentDateTime;

                OnPropertyChanged(nameof(Title));
			}
		}

		public DateTime CurrentDateTime => DateTime.Now;


        public DateTime DateTime_Created
        {
            get { return _Note.DateTime_Created; }
            set
            {
                _Note.DateTime_Created = value;
                OnPropertyChanged(nameof(DateTime_Created));
            }
        }


        public DateTime DateTime_Edited
		{
			get { return _Note.DateTime_Edited; }
			set
			{
                _Note.DateTime_Edited = value;
				OnPropertyChanged(nameof(DateTime_Edited));
			}
		}


		private string _Content;
		public string Content
		{
			get { return _Note.Content; }
			set
			{
                _Note.Content = value;

                DateTime_Edited = CurrentDateTime;

                OnPropertyChanged(nameof(Content));
			}
		}


        public NoteViewModel(Note note)
        {
			_Note = note;

			NewEntryCommand = new RelayCommand((s) => NewEntry(), (s) => true );

            System.Windows.Threading.DispatcherTimer Timer = new System.Windows.Threading.DispatcherTimer();

            Timer.Tick += Timer_Tick;

            Timer.Interval = new TimeSpan(0, 0, 0, 0, 125);

            Timer.Start();
        }

		private void NewEntry()
        {
            DateTime_Edited = CurrentDateTime;
            Content = Content.Insert(0, $"{DateTime_Edited}\n\n\n");
        }

		private void Timer_Tick(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(CurrentDateTime));
        } 


	}
}
// EOF