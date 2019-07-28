using System.IO;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Markdig;
using System;

namespace marknote.Controllers
{
    public class NoteController : Controller
    {
        // TODO - pull this from config!
        private const string NoteDir = "/Users/swisherd/git/notes";

        public IActionResult Render(string path)
        {
            string fullPath = Path.Combine(NoteDir, path);

            // TODO - do a better job hunting for files
            if (!System.IO.File.Exists(fullPath))
            {
                return NotFound();
            }

            string fileContent = System.IO.File.ReadAllText(fullPath);

            if (path.EndsWith(".md"))
            {
                ViewData["NoteHtml"] = Markdown.ToHtml(fileContent);
            }
            else
            {
                ViewData["NoteText"] = fileContent;
            }

            ViewData["Title"] = path;   // TODO - pull this from file metadata?

            return View();
        }
    }
}
