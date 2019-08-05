
using System.Collections.Generic;
using System.IO;
using System.Linq;
using marknote.Models;

namespace marknote.Services
{
    public class Notebook : INotebook
    {
        // TODO - pull this from config!
        public string NoteDir { get { return "/Users/swisherd/git/notes"; } }

        public IEnumerable<NoteEntry> GetRecent(int max = 5)
        {
            return GetAllNotes()
                .OrderByDescending(f => f.LastModified)
                .Take(max);
        }

        public IEnumerable<NoteEntry> GetAllNotes()
        {
            var directory = new DirectoryInfo(NoteDir);

            var files = directory.GetFiles("*.*", SearchOption.AllDirectories)
                .Where(x => x.Name.EndsWith(".md") || x.Name.EndsWith(".txt"))
                .Select(x => MakeEntry(x));

            return files;
        }

        private NoteEntry MakeEntry(FileInfo info)
        {
            string path = Path.Combine(info.Directory.FullName, info.Name);
            string frag = info.Directory.FullName.Substring(NoteDir.Length);
            if (frag.StartsWith("/"))
            {
                frag = frag.Substring(1);
            }
            string url = Path.Combine("/note", frag, info.Name);

            return new NoteEntry
            {
                Name = info.Name,
                Url = url,
                Path = path,
                LastModified = info.LastWriteTime
            };
        }
    }
}
