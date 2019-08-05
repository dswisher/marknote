
using System.Collections.Generic;

namespace marknote.Models
{
    public class NoteModel
    {
        public string Title { get; set; }
        public string FullPath { get; set;}

        public string NoteHtml { get; set; }
        public string NoteText { get; set; }

        public IEnumerable<NoteEntry> Recent { get; set; }
    }
}
