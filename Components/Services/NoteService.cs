using VSHCTwebApp.Components.Models;
using System.Collections.Generic;

namespace VSHCTwebApp.Components.Services
{
    public class NoteService
    {
        private static readonly List<Note> notes = new List<Note>();
        private readonly List<Note> _notes = notes;

        public void AddNote(Note note)
        {
            if (note == null)
                throw new ArgumentNullException(nameof(note));

            note.CreatedAt = DateTime.Now;
            notes.Add(note);
        }

        public List<Note> GetNotes() => _notes;

        public Note GetNoteByTitle(string title)
        {
            return notes.FirstOrDefault(n => n.Title == title);
        }
    }
}
