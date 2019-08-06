using System.IO;
using Microsoft.AspNetCore.Mvc;
using Markdig;
using marknote.Services;
using marknote.Models;

namespace marknote.Controllers
{
    public class NoteController : Controller
    {
        private readonly INotebook notebook;

        public NoteController(INotebook notebook)
        {
            this.notebook = notebook;
        }

        public IActionResult Render(string path)
        {
            string fullPath = Path.Combine(notebook.NoteDir, path);

            NoteModel model = new NoteModel
            {
                Title = path,
                FullPath = fullPath,
                Recent = notebook.GetRecent(8)
            };

            // TODO - do a better job hunting for files
            if (!System.IO.File.Exists(fullPath))
            {
                return NotFound();
            }

            string fileContent = System.IO.File.ReadAllText(fullPath);

            if (path.EndsWith(".md"))
            {
                model.NoteHtml = Markdown.ToHtml(fileContent);
            }
            else
            {
                model.NoteText = fileContent;
            }

            return View(model);
        }
    }
}
