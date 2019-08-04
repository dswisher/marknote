
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace marknote.Services
{
    public class Notebook : INotebook
    {
        // TODO - pull this from config!
        public string NoteDir { get { return "/Users/swisherd/git/notes"; } }

        public IEnumerable<string> GetRecent()
        {
            var directory = new DirectoryInfo(NoteDir);

            IEnumerable<string> files = directory.GetFiles()
                .OrderByDescending(f => f.LastWriteTime)
                .Select(x => x.Name)
                .Where(x => x.EndsWith(".md") || x.EndsWith(".txt"))
                .Take(5);

            return files;
        }
    }
}
