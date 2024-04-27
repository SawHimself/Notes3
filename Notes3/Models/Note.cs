using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Notes3.Models
{
    public class Note : INotifyPropertyChanged
    {
        private int ID;
        private DateTime CreationDate;
        private double XPos;
        private double YPos;
        private double Width;
        private double Height;
        private string Text = "";
        private bool PinMode = false;

        public bool pinMode
        {
            get { return PinMode; }
            set
            {
                PinMode = value;
                OnPropertyChanged();
            }
        }

        public int id
        {
            get { return ID; }
            set
            {
                ID = value;
                OnPropertyChanged();
            }
        }

        public DateTime creationDate
        {
            get { return CreationDate; }
            set
            {
                CreationDate = value;
                OnPropertyChanged();
            }
        }

        public double xPos
        {
            get { return XPos; }
            set
            {
                XPos = value;
                OnPropertyChanged();
            }
        }

        public double yPos
        {
            get { return YPos; }
            set
            {
                YPos = value;
                OnPropertyChanged();
            }
        }

        public double width
        {
            get { return Width; }
            set
            {
                Width = value;
                OnPropertyChanged();
            }
        }

        public double height
        {
            get { return Height; }
            set
            {
                Height = value;
                OnPropertyChanged();
            }
        }

        public string text
        {
            get { return Text; }
            set 
            { 
                Text = value;
                OnPropertyChanged();
            }
        }

        public Note(string text, bool pinMode, double xPos, double yPos, double width, double height )
        {
            Text = text;
            PinMode = pinMode;
            XPos = xPos;
            YPos = yPos;
            Width = width;
            Height = height;
            CreationDate = DateTime.Now;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
