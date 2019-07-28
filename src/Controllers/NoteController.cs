using System.IO;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Markdig;

namespace marknote.Controllers
{
    public class NoteController : Controller
    {
        public IActionResult Render(string path)
        {
            string content = Markdown.ToHtml("This is a text with some *emphasis*");

            // string content = String.Format("Hello world!  Path='{0}'.", path);
            byte[] bytes = Encoding.UTF8.GetBytes(content);

            var stream = new MemoryStream(bytes);
            var fileStreamResult = new FileStreamResult(stream, "text/plain");

            // fileStreamResult.FileDownloadName = "FileStreamExample.csv";

            return fileStreamResult;
        }
    }
}
