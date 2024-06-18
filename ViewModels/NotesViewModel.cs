using MyNote_MVVM.Commands;
using MyNote_MVVM.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace MyNote_MVVM.ViewModels
{
    class NotesViewModel : BaseViewModel
    {
        private ObservableCollection<NoteViewModel> _notes;


        public ICommand NewNoteCommand { get; }
        public ICommand ShowHideCommand { get; }


        public ICommand DeleteNoteCommand { get; }


        private NoteViewModel _Note;
        public NoteViewModel Note
        {
            get { return _Note; }
            set
            {
                _Note = value;
                OnPropertyChanged(nameof(Note));
            }
        }


        private bool _ShowHideClicked;
        public bool ShowHideClicked
        {
            get { return _ShowHideClicked; }
            set
            {
                _ShowHideClicked = value;
                OnPropertyChanged(nameof(ShowHideClicked));
            }
        }

        public ICollectionView Notes { get; set; }


        public NotesViewModel(ObservableCollection<NoteViewModel> notes)
        {
            _notes = notes;

            NewNoteCommand = new RelayCommand((s) => NewNote(), (s) => true);
            DeleteNoteCommand = new RelayCommand((s) => DeleteNote(s), (s) => true);
            ShowHideCommand = new RelayCommand((s) => ShowHide(), (s) => true);

            Notes = CollectionViewSource.GetDefaultView(_notes);
            Notes.SortDescriptions.Add(new SortDescription("DateTime_Created", ListSortDirection.Descending)); 
                        
            if (_notes.Count > 0)
            {
                Note = _notes.Last();
            }
        }


        private void DeleteNote(object s)
        {
            IList selection = (IList)s;

            if (selection != null)
            {
                MessageBoxResult result = MessageBox.Show(
                    $"Do you wan't to delete selected note(s)?",
                    "Remove Note(s)", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    var selected = selection.Cast<NoteViewModel>().ToArray();

                    foreach (var item in selected)
                    {
                        _notes.Remove(item);
                    }
                }
            }

            if (_notes.Count > 0)
            {
                Note = _notes[0];
            }
        }


        private void NewNote()
        {
            _notes.Insert(0, new NoteViewModel(
                new Note(
                    _notes.Count,
                    "new",
                    DateTime.Now,
                    ""
                    )
                ));

            Note = _notes[0];
        }


        private void ShowHide()
        {
            if (ShowHideClicked) { ShowHideClicked = false; }
            else
            {
                ShowHideClicked = true;
            }
        }
    }
}
