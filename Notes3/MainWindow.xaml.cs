using Notes3.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace Notes3
{
    public partial class MainWindow : Window 
    {

        private ObservableCollection<Note> _notes = new ObservableCollection<Note>();

        public event PropertyChangedEventHandler PropertyChanged;

        Point _offsetPoint = new(0, 0);
        public ObservableCollection<Note> Notes
        {
            get { return _notes; }
            set
            {
                _notes = value;
                OnPropertyChanged();
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            _notes = DatabaseManagement.GetAllNotes();
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void CreateNewNote(object sender, EventArgs e)
        {
            var note = new Note("", false, 0, 0);
            DatabaseManagement.AddNotes(note);
            Notes.Add(note);
        }

        public void ChangeNoteText(object sender, EventArgs e)
        {
            if (sender is TextBox textbox && textbox.DataContext is Note note)
            {
                Note newNote = note;
                newNote.text = textbox.Text;
                DatabaseManagement.EditNotes(note.id, newNote);
            }
        }
        public void ChangePinMode(object sender, EventArgs e)
        {
            if (sender is Button button &&  button.DataContext is Note note)
            {
                Note newNote = note;
                newNote.pinMode = !newNote.pinMode;
                DatabaseManagement.EditNotes(note.id, newNote);
            }
        }

        public void DeleteNote(object sender, EventArgs e)
        {
            if (sender is Button button && button.DataContext is Note note)
            {
                Notes.Remove(note);
                // Можно добавить функцию для сохранения заметки в буфер перед окончательным удалением
                DatabaseManagement.DeleteNotes(note);
            }
        }

        private void MoveMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Grid NoteObject)
            {
                var _note = NoteObject.DataContext as Note;
                if (_note != null)
                {
                    if (!_note.pinMode)
                    {
                        NoteObject.CaptureMouse();

                        Point posCursor = e.MouseDevice.GetPosition(this);

                        if (_note != null)
                        {
                            _offsetPoint = new Point(
                                posCursor.X - _note.xPos,
                                posCursor.Y - _note.yPos);
                        }
                    }
                }
            }
        }

        private void MoveMouseMove(object sender, MouseEventArgs e)
        {
            if (sender is Grid NoteObject && NoteObject.IsMouseCaptured)
            {
                Point currentPosition = e.GetPosition(this);

                var _note = NoteObject.DataContext as Note;
                if (_note != null)
                {
                    _note.xPos = currentPosition.X - _offsetPoint.X;
                    _note.yPos = currentPosition.Y - _offsetPoint.Y;
                }
            }
        }

        private void MoveMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is Grid NoteObject)
            {
                NoteObject.ReleaseMouseCapture();
                var _note = NoteObject.DataContext as Note;
                if (_note != null && !_note.pinMode) 
                {
                    Note newNote = _note as Note;

                    Point currentPosition = e.GetPosition(this);
                    _note.xPos = currentPosition.X - _offsetPoint.X;
                    _note.yPos = currentPosition.Y - _offsetPoint.Y;
                    DatabaseManagement.EditNotes(_note.id, newNote);
                }
            }
        }
    }
}