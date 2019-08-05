
using System;

namespace marknote.Models
{
    public class NoteEntry
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Url { get; set; }
        public DateTime LastModified { get; set; }
    }
}
