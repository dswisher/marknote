
using System.Collections.Generic;

namespace marknote.Services
{
    public interface INotebook
    {
        string NoteDir { get; }

        IEnumerable<string> GetRecent();
    }
}
