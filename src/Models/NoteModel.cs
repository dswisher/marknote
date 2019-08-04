
using System.Collections.Generic;

namespace marknote.Models
{
    public class NoteModel
    {
        public string Title { get; set; }
        public string FullPath { get; set;}

        public string NoteHtml { get; set; }
        public string NoteText { get; set; }

        // TODO - replace string with a RecentItem class
        public IEnumerable<string> Recent { get; set; }
    }
}
