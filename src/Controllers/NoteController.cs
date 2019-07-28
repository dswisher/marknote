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

            if (!System.IO.File.Exists(fullPath))
            {
                return NotFound();
            }

            string content = Markdown.ToHtml(System.IO.File.ReadAllText(fullPath));

            ViewData["Title"] = path;   // TODO - pull this from file metadata?
            ViewData["NoteContent"] = content;

            return View();
        }
    }
}
