using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes3.Models
{
    class DatabaseManagement
    {
        public static ObservableCollection<Note> GetAllNotes()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = new ObservableCollection<Note>(db.notes.ToList());
                return result;
            }
        }

        public static void AddNotes(Note newNote)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.notes.Add(newNote);
                db.SaveChanges();
            }
        }

        public static void DeleteNotes(Note notes)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.notes.Remove(notes);
                db.SaveChanges();
            }
        }
        public static void EditNotes(int ID, Note newNotes)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Note notes = db.notes.FirstOrDefault(n => n.id == ID);
                if (notes != null)
                {
                    notes.text = newNotes.text;
                    notes.xPos = newNotes.xPos;
                    notes.yPos = newNotes.yPos;
                    notes.width = newNotes.width;
                    notes.height = newNotes.height;
                    notes.pinMode = newNotes.pinMode;
                    notes.creationDate = newNotes.creationDate;
                    db.SaveChanges();
                }
            }
        }
    }
}
