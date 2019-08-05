
using System.Collections.Generic;
using marknote.Models;

namespace marknote.Services
{
    public interface INotebook
    {
        string NoteDir { get; }

        IEnumerable<NoteEntry> GetRecent(int max = 5);
        IEnumerable<NoteEntry> GetAllNotes();
    }
}
